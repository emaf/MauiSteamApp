using MauiStream.Models;

namespace MauiStream.Services;

public class GameDataService
{
    public List<Game> GetFeaturedGames()
    {
        return new List<Game>
        {
            new Game
            {
                Title = "Stray",
                Description = "Lost, alone and separated from family, a stray cat must untangle an ancient mystery to escape a long-forgotten cyberpunk city and find their way home.",
                Price = 29.99m,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1332010/header.jpg",
                Tags = new List<string> { "Cats", "Adventure", "Cyberpunk", "Atmospheric" },
                FriendCount = 12,
                IsAvailableNow = true
            },
            new Game
            {
                Title = "Elden Ring",
                Description = "Rise, Tarnished, and be guided by grace to brandish the power of the Elden Ring and become an Elden Lord in the Lands Between.",
                Price = 59.99m,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1245620/header.jpg",
                Tags = new List<string> { "RPG", "Dark Fantasy", "Open World", "Souls-like" },
                FriendCount = 25,
                IsAvailableNow = true
            },
            new Game
            {
                Title = "Baldur's Gate 3",
                Description = "Gather your party and return to the Forgotten Realms in a tale of fellowship and betrayal, sacrifice and survival, and the lure of absolute power.",
                Price = 59.99m,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1086940/header.jpg",
                Tags = new List<string> { "RPG", "D&D", "Party-Based", "Story Rich" },
                FriendCount = 18,
                IsAvailableNow = true
            },
            new Game
            {
                Title = "Cyberpunk 2077",
                Description = "Cyberpunk 2077 is an open-world, action-adventure RPG set in the dark future of Night City.",
                Price = 59.99m,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1091500/header.jpg",
                Tags = new List<string> { "Cyberpunk", "RPG", "Open World", "Futuristic" },
                FriendCount = 23,
                IsAvailableNow = true
            },
            new Game
            {
                Title = "Red Dead Redemption 2",
                Description = "Winner of over 175 Game of the Year Awards, RDR2 is the epic tale of outlaw Arthur Morgan and the Van der Linde gang.",
                Price = 59.99m,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/header.jpg",
                Tags = new List<string> { "Western", "Open World", "Story Rich", "Action" },
                FriendCount = 19,
                IsAvailableNow = true
            },
            new Game
            {
                Title = "Hades",
                Description = "Defy the god of the dead as you hack and slash out of the Underworld in this rogue-like dungeon crawler.",
                Price = 24.99m,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1145360/header.jpg",
                Tags = new List<string> { "Roguelike", "Dungeon Crawler", "Indie", "Action" },
                FriendCount = 11,
                IsAvailableNow = true
            }
        };
    }

    public List<Game> GetSpecialOffers()
    {
        return new List<Game>
        {
            new Game
            {
                Title = "Hogwarts Legacy",
                Description = "Hogwarts Legacy is an immersive, open-world action RPG set in the world of Harry Potter.",
                Price = 47.99m,
                OriginalPrice = 59.99m,
                DiscountPercent = 20,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/990080/header.jpg",
                Tags = new List<string> { "Magic", "Open World", "RPG", "Adventure" }
            },
            new Game
            {
                Title = "Call of Duty: Modern Warfare III",
                Description = "In the direct sequel to Call of Duty: Modern Warfare II, experience a new campaign.",
                Price = 50.99m,
                OriginalPrice = 59.99m,
                DiscountPercent = 15,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/2519060/header.jpg",
                Tags = new List<string> { "FPS", "Multiplayer", "Action", "War" }
            },
            new Game
            {
                Title = "EA SPORTS FC 25",
                Description = "EA SPORTS FC 25 gives you more ways to win for the club.",
                Price = 53.99m,
                OriginalPrice = 59.99m,
                DiscountPercent = 10,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/2669320/header.jpg",
                Tags = new List<string> { "Sports", "Football", "Multiplayer", "Simulation" }
            },
            new Game
            {
                Title = "Mass Effect Legendary Edition",
                Description = "The Mass Effect Legendary Edition includes single-player base content and over 40 DLC from Mass Effect, Mass Effect 2, and Mass Effect 3.",
                Price = 14.99m,
                OriginalPrice = 19.99m,
                DiscountPercent = 25,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1328670/header.jpg",
                Tags = new List<string> { "Sci-Fi", "RPG", "Action", "Story Rich" }
            },
            new Game
            {
                Title = "The Witcher 3: Wild Hunt",
                Description = "You are Geralt of Rivia, mercenary monster slayer. Before you stands a war-torn, monster-infested continent.",
                Price = 7.99m,
                OriginalPrice = 39.99m,
                DiscountPercent = 80,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/292030/header.jpg",
                Tags = new List<string> { "RPG", "Open World", "Fantasy", "Story Rich" }
            },
            new Game
            {
                Title = "God of War",
                Description = "Enter the Norse realm with Kratos and Atreus in this reimagining of the God of War series.",
                Price = 29.99m,
                OriginalPrice = 49.99m,
                DiscountPercent = 40,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1593500/header.jpg",
                Tags = new List<string> { "Action", "Adventure", "Mythology", "Story Rich" }
            },
            new Game
            {
                Title = "Hollow Knight",
                Description = "Forge your own path in Hollow Knight! An epic action adventure through a vast ruined kingdom of insects and heroes.",
                Price = 7.49m,
                OriginalPrice = 14.99m,
                DiscountPercent = 50,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/367520/header.jpg",
                Tags = new List<string> { "Metroidvania", "Indie", "Platformer", "Dark" }
            },
            new Game
            {
                Title = "Sekiro: Shadows Die Twice",
                Description = "Carve your own clever path to vengeance in the award winning adventure from FromSoftware.",
                Price = 29.99m,
                OriginalPrice = 59.99m,
                DiscountPercent = 50,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/814380/header.jpg",
                Tags = new List<string> { "Souls-like", "Action", "Difficult", "Ninja" }
            }
        };
    }
}
