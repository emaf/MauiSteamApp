namespace MauiStream;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}

	private async void OnStoreClicked(object? sender, EventArgs e)
	{
		await GoToAsync("//StorePage");
		FlyoutIsPresented = false;
	}

	private async void OnLibraryClicked(object? sender, EventArgs e)
	{
		await GoToAsync("//LibraryPage");
		FlyoutIsPresented = false;
	}

	private async void OnCommunityClicked(object? sender, EventArgs e)
	{
		await GoToAsync("//CommunityPage");
		FlyoutIsPresented = false;
	}

	private async void OnFriendsClicked(object? sender, EventArgs e)
	{
		await GoToAsync("//FriendsPage");
		FlyoutIsPresented = false;
	}
}
