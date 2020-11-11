using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HappyDog.Infrastructure.Test.PaginationTests
{
    [TestClass]
    public class TotalPagesTest
    {
        [TestMethod]
        public void Test0()
        {
            var pagination = new Pagination<int>
            {
                Page = 1,
                Size = 10,
                TotalItems = 10
            };
            Assert.AreEqual(1, pagination.TotalPages);
        }

        [TestMethod]
        public void Test1()
        {
            var pagination = new Pagination<int>
            {
                Page = 1,
                Size = 10,
                TotalItems = 11
            };
            Assert.AreEqual(2, pagination.TotalPages);
        }
    }
}
