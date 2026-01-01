
using LearningLINQ;

var games = new List<Game>
{
    new Game { Title = "Galactic Frontier",       Genre = "Sci‑Fi Strategy",     ReleaseYear = 2022, Rating = 4.8, Price = 59 },
    new Game { Title = "Mystic Quest: The Hidden Temple", Genre = "Adventure / Puzzle",  ReleaseYear = 2021, Rating = 4.5, Price = 49 },
    new Game { Title = "CyberStrike Arena",       Genre = "FPS Multiplayer",     ReleaseYear = 2023, Rating = 4.2, Price = 39 },
    new Game { Title = "Legends of the Shadow Realm", Genre = "Fantasy RPG",       ReleaseYear = 2020, Rating = 4.7, Price = 54 },
    new Game { Title = "Pixel Racer: Turbo Drift", Genre = "Arcade Racing",      ReleaseYear = 2023, Rating = 4.1, Price = 29 }
};

//// Retrieving all games

////foreach (var game in games)
////{
////    Console.WriteLine($"Title : {game.Title} Genre: {game.Genre} ReleaseYear: {game.ReleaseYear} Rating: {game.Rating}");
////}

////Filtering: Use the Where method to filter games based on a specific condition, such as matching a genre. This method returns a subset of elements that satisfy the condition.

////var AllGamesGenre = games.Where(game => game.Genre == "Sci‑Fi Strategy");

////foreach (var game in AllGamesGenre)
////{
////    Console.WriteLine($"Title : {game.Title} Genre: {game.Genre.ToLower()} ReleaseYear: {game.ReleaseYear} Rating: {game.Rating}");
////}

//// Checking Conditions: The Any method checks if any element in the collection matches the condition, returning a boolean value. This is useful for quick validations.

////var modernGamesExist = games.Any(game => game.ReleaseYear > 2022);

////Console.WriteLine($"Does there exist a mordern game?  {modernGamesExist}");

//// Sorting: Sort games by a property like release year using OrderBy .Sorting helps organize data,making it easier to analyze.

//// - Ascending order via a given key(lowest -> highest)
//var sortedGamesByYear = games.OrderBy(game => game.ReleaseYear);
//Console.WriteLine("Sorting in Ascending order \n");

//foreach (var game in sortedGamesByYear)
//{
//    Console.WriteLine($" ReleaseYear: {game.ReleaseYear} Title : {game.Title} Genre: {game.Genre.ToLower()} Rating: {game.Rating}");
//}

//Console.WriteLine("\n");

//// - descending order via a given key(highest -> lowest)
//var sortedGamesByYearDesc = games.OrderByDescending(game => game.ReleaseYear);
//Console.WriteLine("Sorting in descending order \n");

//foreach (var game in sortedGamesByYearDesc)
//{
//    Console.WriteLine($" ReleaseYear: {game.ReleaseYear} Title : {game.Title} Genre: {game.Genre.ToLower()} Rating: {game.Rating}");
//}

//// Aggregating Data: LINQ aggregation methods like Average and Max perform calculations over a collection, such as finding the average price or the highest-rated game

////- Average
//var averagePrice = games.Average(game => game.Price);

//Console.WriteLine($"\n Average Game Price: ${averagePrice}");

////- Max
//var highestRating = games.Max(game => game.Rating); 

//var bestGame = games.First(g => g.Rating == highestRating);

//Console.WriteLine($"Highest rated Game: {bestGame.Title} ({bestGame.Rating})");

