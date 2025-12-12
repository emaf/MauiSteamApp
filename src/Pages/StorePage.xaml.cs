using MauiStream.ViewModels;
using MauiStream.Models;
#if DEBUG
using MauiStream.Helpers;
#endif

namespace MauiStream.Pages;

public partial class StorePage : ContentPage
{
    private readonly StoreViewModel _viewModel;
    private IDispatcherTimer? _autoScrollTimer;
    private bool _isSearchExpanded;
    private bool _isDetailPanelCurrentlyVisible = true;
    private const double DetailPanelWidthThreshold = 600;

#if DEBUG
    private IDispatcherTimer? _visualTreeDebounceTimer;
#endif

    public StorePage()
    {
        InitializeComponent();
        _viewModel = new StoreViewModel();
        BindingContext = _viewModel;

#if DEBUG
        Loaded += OnPageLoaded;
#endif
    }

#if DEBUG
    private void OnPageLoaded(object? sender, EventArgs e)
    {
        this.PrintVisualTree("StorePage.Loaded");

        // Subscribe to visual tree changes on Content
        if (Content is Element content)
        {
            content.DescendantAdded += OnVisualTreeChanged;
            content.DescendantRemoved += OnVisualTreeChanged;
        }
    }

    private void OnVisualTreeChanged(object? sender, ElementEventArgs e)
    {
        // Debounce rapid changes (e.g., CollectionView loading items)
        _visualTreeDebounceTimer?.Stop();
        _visualTreeDebounceTimer = Dispatcher.CreateTimer();
        _visualTreeDebounceTimer.Interval = TimeSpan.FromMilliseconds(500);
        _visualTreeDebounceTimer.Tick += (s, args) =>
        {
            _visualTreeDebounceTimer?.Stop();
            this.PrintVisualTree("StorePage.Changed");
        };
        _visualTreeDebounceTimer.Start();
    }

    private void CleanupVisualTreeDebug()
    {
        _visualTreeDebounceTimer?.Stop();
        _visualTreeDebounceTimer = null;

        Loaded -= OnPageLoaded;

        if (Content is Element content)
        {
            content.DescendantAdded -= OnVisualTreeChanged;
            content.DescendantRemoved -= OnVisualTreeChanged;
        }
    }
#endif

    private async void OnSearchTapped(object? sender, TappedEventArgs e)
    {
        if (_isSearchExpanded) return;
        _isSearchExpanded = true;

        ExpandedSearch.IsVisible = true;
        await Task.WhenAll(
            HeaderContent.FadeToAsync(0, 500, Easing.CubicOut),
            ExpandedSearch.FadeToAsync(1, 500, Easing.CubicOut)
        );
        HeaderContent.IsVisible = false;
        
        // Use MainThread with delay to ensure keyboard shows after animation
        await Task.Delay(100);
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Task.Delay(50);
            SearchEntry.Focus();
        });
    }

    private async void OnSearchCancelTapped(object? sender, TappedEventArgs e)
    {
        await CollapseSearch();
    }

    private async void OnOutsideTapped(object? sender, TappedEventArgs e)
    {
        if (_isSearchExpanded)
        {
            await CollapseSearch();
        }
    }

    private async void OnSearchCompleted(object? sender, EventArgs e)
    {
        // Handle search submission here if needed
        await CollapseSearch();
    }

    private async Task CollapseSearch()
    {
        if (!_isSearchExpanded) return;

        // Hide keyboard first
#if ANDROID
        if (Platform.CurrentActivity?.CurrentFocus != null)
        {
            var inputMethodManager = (Android.Views.InputMethods.InputMethodManager?)Platform.CurrentActivity.GetSystemService(Android.Content.Context.InputMethodService);
            inputMethodManager?.HideSoftInputFromWindow(Platform.CurrentActivity.CurrentFocus.WindowToken, Android.Views.InputMethods.HideSoftInputFlags.None);
        }
#endif
        SearchEntry.Unfocus();
        HeaderContent.IsVisible = true;
        await Task.WhenAll(
            ExpandedSearch.FadeToAsync(0, 500, Easing.CubicIn),
            HeaderContent.FadeToAsync(1, 500, Easing.CubicIn)
        );
        ExpandedSearch.IsVisible = false;
        SearchEntry.Text = string.Empty;
        _isSearchExpanded = false;
    }

    protected override async void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (width <= 0) return;

        var shouldShowPanel = width >= DetailPanelWidthThreshold;

        if (shouldShowPanel != _isDetailPanelCurrentlyVisible)
        {
            _isDetailPanelCurrentlyVisible = shouldShowPanel;

            if (shouldShowPanel)
            {
                // Show panel with fade in - restore width first so layout recalculates
                DetailPanel.WidthRequest = 160;
                DetailPanel.IsVisible = true;
                CarouselBorder.Margin = new Thickness(0, 0, 10, 0);
                await DetailPanel.FadeToAsync(1, 400, Easing.CubicOut);
                _viewModel.IsDetailPanelVisible = true;
            }
            else
            {
                // Hide panel with fade out
                await DetailPanel.FadeToAsync(0, 400, Easing.CubicIn);
                DetailPanel.IsVisible = false;
                // Set width to 0 so the Auto column collapses completely
                DetailPanel.WidthRequest = 0;
                CarouselBorder.Margin = new Thickness(0);
                _viewModel.IsDetailPanelVisible = false;
            }
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        StartAutoScroll();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        StopAutoScroll();
#if DEBUG
        CleanupVisualTreeDebug();
#endif
    }

    private void StartAutoScroll()
    {
        _autoScrollTimer = Dispatcher.CreateTimer();
        _autoScrollTimer.Interval = TimeSpan.FromSeconds(5);
        _autoScrollTimer.Tick += OnAutoScrollTick;
        _autoScrollTimer.Start();
    }

    private void StopAutoScroll()
    {
        if (_autoScrollTimer != null)
        {
            _autoScrollTimer.Stop();
            _autoScrollTimer.Tick -= OnAutoScrollTick;
            _autoScrollTimer = null;
        }
    }

    private void OnAutoScrollTick(object? sender, EventArgs e)
    {
        if (_viewModel.FeaturedGames.Count == 0) return;

        var nextIndex = (_viewModel.CurrentFeaturedIndex + 1) % _viewModel.FeaturedGames.Count;
        _viewModel.CurrentFeaturedIndex = nextIndex;
        FeaturedCarousel.ScrollTo(nextIndex, animate: true);
    }

    private void OnCarouselCurrentItemChanged(object? sender, CurrentItemChangedEventArgs e)
    {
        if (e.CurrentItem is Game game)
        {
            var index = _viewModel.FeaturedGames.IndexOf(game);
            if (index >= 0)
            {
                _viewModel.CurrentFeaturedIndex = index;
            }
        }
    }

    private void OnFeaturedGameTapped(object? sender, TappedEventArgs e)
    {
        if (e.Parameter is Game game)
        {
            // Update carousel index to keep in sync
            var index = _viewModel.FeaturedGames.IndexOf(game);
            if (index >= 0)
            {
                _viewModel.CurrentFeaturedIndex = index;
            }

            // Show popup only when detail panel is hidden
            if (!_isDetailPanelCurrentlyVisible)
            {
                ShowGamePopup(game);
            }
        }
    }

    private void OnSpecialOfferTapped(object? sender, TappedEventArgs e)
    {
        if (e.Parameter is Game game && !_isDetailPanelCurrentlyVisible)
        {
            ShowGamePopup(game);
        }
    }

    private async void ShowGamePopup(Game game)
    {
        _viewModel.PopupGame = game;
        
        // Directly set the Game property on PopupContent
        if (this.FindByName("PopupContent") is Controls.GameDetailContent popupContent)
        {
            popupContent.Game = game;
        }
        
        PopupOverlay.IsVisible = true;
        PopupOverlay.Opacity = 0;
        PopupPanel.TranslationY = 800;

        await Task.WhenAll(
            PopupOverlay.FadeToAsync(1, 300, Easing.CubicOut),
            PopupPanel.TranslateToAsync(0, 0, 350, Easing.CubicOut)
        );
    }

    private async void OnPopupOverlayTapped(object? sender, TappedEventArgs e)
    {
        await HideGamePopup();
    }

    private async void OnPopupCloseTapped(object? sender, TappedEventArgs e)
    {
        await HideGamePopup();
    }

    private async Task HideGamePopup()
    {
        await Task.WhenAll(
            PopupOverlay.FadeToAsync(0, 250, Easing.CubicIn),
            PopupPanel.TranslateToAsync(0, 800, 300, Easing.CubicIn)
        );

        PopupOverlay.IsVisible = false;
        _viewModel.PopupGame = null;
    }
}
