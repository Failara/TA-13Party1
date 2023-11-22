namespace Lab3Library
{
    public class ArticleLibrary
    {
        private readonly List<string> topicCategories;
        private readonly List<Article> articles;
        public ArticleLibrary()
        {
            topicCategories = [];
            articles = [];
        }
        public void AddTopicCategory(string category)
        {
            if (!topicCategories.Contains(category))
            {
                topicCategories.Add(category);
            }
        }
        public void RemoveTopicCategory(string category)
        {
            topicCategories.Remove(category);
        }
        public void AddArticle(string title, string author, string topicCategory)
        {
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
