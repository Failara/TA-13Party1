using Lab3Library.Core;
namespace Lab3LibraryTests
{
    public class ArticleLibraryTests
    {
        [Fact]
        public void AddTopicCategory_CorrectTopicCategory_TopicCategoryAdded()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string expected = "Abc";

            //Act
            sut.AddTopicCategory(expected);

            //Assert
            Assert.Contains(expected, sut.topicCategories);
        }

        [Fact]
        public void AddTopicCategory_NullValue_TopicCategoryNotAdded()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string? expected = null;

            //Act
            sut.AddTopicCategory(expected);

            //Assert
            Assert.DoesNotContain(expected, sut.topicCategories);

        }

        [Fact]
        public void RemoveTopicCategory_CorrectTopicCategory_TopicCategoryRemoved()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string expected = "scary";
            sut.AddTopicCategory(expected);

            //Act
            sut.RemoveTopicCategory(expected);

            //Assert
            Assert.DoesNotContain(expected, sut.topicCategories);

        }

        [Fact]
        public void AddArticle_CorrectArticle_ArticleAdded()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string t = "tittle";
            string a = "author";
            string tc = "scary";

            //Act
            sut.AddArticle(t, a, tc);

            //Assert
            Assert.Contains(sut.articles, s => s.Author == a);

        }

        [Fact]
        public void AddArticle_NullValue_ArticleNotAdded()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string? n = null;
            var expected = new Article();
            expected = null;

            //Act
            sut.AddArticle(n, n, n);

            //Assert
            Assert.DoesNotContain(expected, sut.articles);

        }

        [Fact]
        public void RemoveArticle_CorrectArticle_ArticleRemoved()
        {
            //Arrange
            var sut = new ArticleLibrary();
            string a1 = "article 1";
            var expected = new Article { Author = a1, Title = a1 };

            sut.articles.Add(expected);
            sut.AddArticle(a1, a1, a1);

            //Act
            sut.RemoveArticle(a1, a1);

            //Assert
            Assert.DoesNotContain(expected, sut.articles);

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
            bool expected = true;

            var test = new Article();
            test.Title = t;
            test.Author = a;
            test.TopicCategory = a;
            test.IsFavorite = expected;

            sut.AddArticle(t, a, a);
            sut.AddToFavorites(t, a);

            //Act
            sut.RemoveFromFavorites(t, a);

            //Assert
            Assert.DoesNotContain(test, sut.GetFavorites());

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
            var tc = "Drama";


            //Assert + Act
            Assert.Contains(sut.GetArticlesByCategory(tc), s => s.Author == expected);
        }
    }
}