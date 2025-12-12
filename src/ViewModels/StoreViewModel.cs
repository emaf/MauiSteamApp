using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MauiStream.Models;
using MauiStream.Services;

namespace MauiStream.ViewModels;

public class StoreViewModel : INotifyPropertyChanged
{
    private readonly GameDataService _gameDataService;
    private int _currentFeaturedIndex;
    private bool _isDetailPanelVisible = true;
    private Game? _popupGame;

    public ObservableCollection<Game> FeaturedGames { get; }
    public ObservableCollection<Game> SpecialOffers { get; }

    public Game? PopupGame
    {
        get => _popupGame;
        set
        {
            if (_popupGame != value)
            {
                _popupGame = value;
                OnPropertyChanged();
            }
        }
    }

    public int CurrentFeaturedIndex
    {
        get => _currentFeaturedIndex;
        set
        {
            if (_currentFeaturedIndex != value)
            {
                _currentFeaturedIndex = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentFeaturedGame));
            }
        }
    }

    public Game? CurrentFeaturedGame => 
        CurrentFeaturedIndex >= 0 && CurrentFeaturedIndex < FeaturedGames.Count 
            ? FeaturedGames[CurrentFeaturedIndex] 
            : null;

    public bool IsDetailPanelVisible
    {
        get => _isDetailPanelVisible;
        set
        {
            if (_isDetailPanelVisible != value)
            {
                _isDetailPanelVisible = value;
                OnPropertyChanged();
            }
        }
    }

    public StoreViewModel()
    {
        _gameDataService = new GameDataService();
        FeaturedGames = new ObservableCollection<Game>(_gameDataService.GetFeaturedGames());
        SpecialOffers = new ObservableCollection<Game>(_gameDataService.GetSpecialOffers());
        _currentFeaturedIndex = 0;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
