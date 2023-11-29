using Lab3Library;
namespace Lab3Libraty.Tests
{
    public class ArticleLibraryTests
    {
        [Fact]
        public void AddTopicCategory_CorrectTopicCategory_TopicCategoryAdded()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string expected = "drama";

            //Act
            sut.AddTopicCategory(expected);

            //Assert
            Assert.Equal(expected, sut.topicCategories[0]);
        }

        [Fact]
        public void AddTopicCategory_IncorectValue_ArgumentOutOfRangeException()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string? expected = null;

            //Act
            sut.AddTopicCategory(expected);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.topicCategories[0].Equals(expected));

        }

        [Fact]
        public void RemoveTopicCategory_CorrectTopicCategory_TopicCategoryRemoved()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string expected = "drama";
            string n1 = "scary";
            sut.AddTopicCategory(expected);
            sut.AddTopicCategory(n1);

            //Act
            sut.RemoveTopicCategory(expected);

            //Assert
            Assert.Equal(n1, sut.topicCategories[0]);

        }

        [Fact]
        public void AddArticle_CorrectArticle_ArticleAdded()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string t = "tittle";
            string a = "author";
            string tc = "drama";

            //Act
            sut.AddArticle(t, a, tc);

            //Assert
            Assert.Equal(t, sut.articles[0].Title);
            Assert.Equal(a, sut.articles[0].Author);
            Assert.Equal(tc, sut.articles[0].TopicCategory);

        }

        [Fact]
        public void AddArticle_IncorectValue_NullReferenceException()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string? n = null;

            //Act
            sut.AddArticle(n, n, n);

            //Assert
            Assert.Throws<NullReferenceException>(() => sut.articles[0].Title.Equals(n) && sut.articles[0].Author.Equals(n) && sut.articles[0].TopicCategory.Equals(n));

        }

        [Fact]
        public void RemoveArticle_CorrectArticle_ArticleRemoved()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string a1 = "article 1";
            string a2 = "article 2";

            sut.AddArticle(a1, a1, a1);
            sut.AddArticle(a2, a2, a2);

            //Act
            sut.RemoveArticle(a1, a1);

            //Assert
            Assert.Equal(a2, sut.articles[0].Title);

        }

        [Fact]
        public void AddToFavorites_CorrectFavorites_FavoritesAdded()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string t = "tittle";
            string a = "author";
            bool expected = true;
            sut.AddArticle(t, a, a);

            //Act
            sut.AddToFavorites(t, a);

            //Assert
            Assert.Equal(expected, sut.articles[0].IsFavorite);

        }

        [Fact]
        public void RemoveFromFavorites_CorrectFavorites_FavoritesRemoved()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string t = "tittle";
            string a = "author";
            bool expected = false;
            sut.AddArticle(t, a, a);

            //Act
            sut.RemoveFromFavorites(t, a);

            //Assert
            Assert.Equal(expected, sut.articles[0].IsFavorite);

        }

        [Fact]
        public void GetFavorites_FavoritesExists_ReturnList()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string expected = "Author1";

            //Assert + Act
            Assert.Contains(sut.GetFavorites(), s => s.Author == expected);
        }

        [Fact]
        public void GetArticlesByCategory_ArticlesByCategoryExists_ReturnList()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string expected = "Author1";

            //Assert + Act
            Assert.Contains(sut.GetFavorites(), s => s.Author == expected);
        }
    }
}