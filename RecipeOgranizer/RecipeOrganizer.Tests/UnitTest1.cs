using RecipeOgranizer.Dal.Context;
using RecipeOgranizer.Dal.Models;


namespace RecipeOrganizer.Tests
{
    using RecipeOrganizer.Bll;
    using Moq;
    using Microsoft.EntityFrameworkCore;

    public class Tests
    {
        private Mock<RecipeOrganizerContext> _context;
        private Bll _bll;
        [SetUp]
        public void Setup()
        {
            _context = new Mock<RecipeOrganizerContext>();
            _bll = new Bll(_context.Object);
        }

        private static DbSet<T> MockDbSet<T>(params T[] elements) where T : class
        {
            var queryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return dbSetMock.Object;
        }

        [Test]
        public void GetUserByEmail_ValidEmail_ReturnsUser()
        {
            var user = new User { Email = "test@example.com" };
            _context.Setup(c => c.Users).Returns(MockDbSet(user));

            var result = _bll.GetUserByEmail("test@example.com");

            Assert.IsNotNull(result);
            Assert.AreEqual(user.Email, result.Email);
        }
        [Test]
        public void IsValidEmail_ValidEmail_ReturnsTrue()
        {
            var email = "test@example.com";
            var incorrect_email = "test-example-org";

            var result = Bll.IsValidEmail(email);
            var false_result = Bll.IsValidEmail(incorrect_email);

            Assert.IsTrue(result);
            Assert.IsFalse(false_result);
        }
        [Test]
        public void IsValidPassword_ValidPassword_ReturnsTrue()
        {
            var password = "ValidPassword123";
            var incorrect_password = "notvalid";

            var result = Bll.IsValidPassword(password);
            var false_result = Bll.IsValidPassword(incorrect_password);

            Assert.IsFalse(false_result);
            Assert.IsTrue(result);
        }
    }
}