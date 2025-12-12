using MauiStream.Models;

namespace MauiStream.Controls;

public partial class GameDetailContent : ContentView
{
    public static readonly BindableProperty GameProperty = BindableProperty.Create(
        nameof(Game),
        typeof(Game),
        typeof(GameDetailContent),
        null);

    public static readonly BindableProperty ShowImageProperty = BindableProperty.Create(
        nameof(ShowImage),
        typeof(bool),
        typeof(GameDetailContent),
        true);

    public static readonly BindableProperty ShowMoreLikeThisProperty = BindableProperty.Create(
        nameof(ShowMoreLikeThis),
        typeof(bool),
        typeof(GameDetailContent),
        true);

    public static readonly BindableProperty ShowTitleProperty = BindableProperty.Create(
        nameof(ShowTitle),
        typeof(bool),
        typeof(GameDetailContent),
        true);

    public static readonly BindableProperty ShowTagsProperty = BindableProperty.Create(
        nameof(ShowTags),
        typeof(bool),
        typeof(GameDetailContent),
        true);

    public Game? Game
    {
        get => (Game?)GetValue(GameProperty);
        set => SetValue(GameProperty, value);
    }

    public bool ShowImage
    {
        get => (bool)GetValue(ShowImageProperty);
        set => SetValue(ShowImageProperty, value);
    }

    public bool ShowMoreLikeThis
    {
        get => (bool)GetValue(ShowMoreLikeThisProperty);
        set => SetValue(ShowMoreLikeThisProperty, value);
    }

    public bool ShowTitle
    {
        get => (bool)GetValue(ShowTitleProperty);
        set => SetValue(ShowTitleProperty, value);
    }

    public bool ShowTags
    {
        get => (bool)GetValue(ShowTagsProperty);
        set => SetValue(ShowTagsProperty, value);
    }

    public GameDetailContent()
    {
        InitializeComponent();
        BindingContext = this;
    }
}
