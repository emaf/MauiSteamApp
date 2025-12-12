using MauiStream.Models;

namespace MauiStream.Controls;

public partial class GameDetailContent : ContentView
{
    public static readonly BindableProperty GameProperty = BindableProperty.Create(
        nameof(Game),
        typeof(Game),
        typeof(GameDetailContent),
        null);

    public Game? Game
    {
        get => (Game?)GetValue(GameProperty);
        set => SetValue(GameProperty, value);
    }

    public GameDetailContent()
    {
        InitializeComponent();
        BindingContext = this;
    }
}
