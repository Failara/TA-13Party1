using System.Globalization;

namespace Lab3Library
{
    public class ArticleLibrary
    {
        public readonly List<string> topicCategories;
        public readonly List<Article> articles;
        public ArticleLibrary()
        {
            topicCategories = ["Drama", "Sport", "Comedy"];
            articles = [
                new() { Author = "Author1", Title = "Title1", TopicCategory = "Drama", IsFavorite = true },
                new() { Author = "Author2", Title = "Title2", TopicCategory = "Sport", IsFavorite = false },
                new() { Author = "Author3", Title = "Title3", TopicCategory = "Comedy", IsFavorite = false }];
        }
        public void AddTopicCategory(string category)
        {
            if (!topicCategories.Contains(category) && category != null)
            {
                topicCategories.Add(category);
            }
        }
        public void RemoveTopicCategory(string category)
        {
            if (category != null)
                topicCategories.Remove(category);
        }
        public void AddArticle(string title, string author, string topicCategory)
        {
            if (title != null && author != null && topicCategory != null)
                AddTopicCategory(topicCategory);
            var article = new Article { Title = title, Author = author, TopicCategory = topicCategory };
            articles.Add(article);
        }
        public void RemoveArticle(string title, string author)
        {
            var article = articles.FirstOrDefault(a => a.Title == title && a.Author == author);
            if (article != null)
            {
                articles.Remove(article);
            }
        }
        public void AddToFavorites(string title, string author)
        {
            var article = articles.FirstOrDefault(a => a.Title == title && a.Author == author);
            if (article != null)
            {
                article.IsFavorite = true;
            }
        }
        public void RemoveFromFavorites(string title, string author)
        {
            var article = articles.FirstOrDefault(a => a.Title == title && a.Author == author);
            if (article != null)
            {
                article.IsFavorite = false;
            }
        }
        public List<Article> GetFavorites()
        {
            return articles.Where(a => a.IsFavorite).OrderByDescending(a => a.Title).ToList();
        }
        public List<Article> GetArticlesByCategory(string topicCategory)
        {
            return articles.Where(a => a.TopicCategory == topicCategory).OrderByDescending(a => a.Title).ToList();
        }
    }
}
