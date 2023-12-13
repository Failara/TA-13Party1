using Lab3Library.Core;

ArticleLibrary articleLibrary = new();

articleLibrary.AddArticle("Some news", "Andrew L", "News");
articleLibrary.AddToFavorites("Some news", "Andrew L");

foreach (var article in articleLibrary.GetFavorites()) 
Console.WriteLine(article.Title);