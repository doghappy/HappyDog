using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HappyDog.Infrastructure.Test.PaginationTests
{
    [TestClass]
    public class GetPageNumberLinksTest
    {
        [TestMethod]
        public void Test0()
        {
            var pagination = new Pagination<int>
            {
                Page = 1,
                Size = 10,
                TotalItems = 10,
                AutoHide = false,
                PreviousText = "&laquo;",
                NextText = "&raquo;"
            };
            string html = pagination.GetPageNumberLinks(p => "/test?p=" + p);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item active\"><span class=\"page-link\">1</span></li><li class=\"page-item\"><span class=\"page-link\">1 / 1</span></li></ul></nav>";
            Assert.AreEqual(expected, html);
        }

        [TestMethod]
        public void Test1()
        {
            var pagination = new Pagination<int>
            {
                Page = 2,
                Size = 10,
                TotalItems = 11,
                PreviousText = "Previous",
                NextText = "&raquo;"
            };
            string html = pagination.GetPageNumberLinks(p => "/test?p=" + p);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">1</a></li><li class=\"page-item active\"><span class=\"page-link\">2</span></li><li class=\"page-item\"><span class=\"page-link\">2 / 2</span></li></ul></nav>";
            Assert.AreEqual(expected, html);
        }

        [TestMethod]
        public void Test2()
        {
            var pagination = new Pagination<int>
            {
                Page = 3,
                Size = 10,
                TotalItems = 48,
                PreviousText = "Previous",
                NextText = "Next",
                FirstPageText = "&lt;&lt;",
                LastPageText = "&gt;&gt;"
            };
            string html = pagination.GetPageNumberLinks(p => "/test?p=" + p);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">&lt;&lt;</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">1</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">2</a></li><li class=\"page-item active\"><span class=\"page-link\">3</span></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=4\">4</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=5\">5</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=4\">Next</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=5\">&gt;&gt;</a></li><li class=\"page-item\"><span class=\"page-link\">3 / 5</span></li></ul></nav>";
            Assert.AreEqual(expected, html);
        }
    }
}
