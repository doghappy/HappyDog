using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HappyDog.Infrastructure.Test.PaginationTests
{
    [TestClass]
    public class GetSimpleLinksTest
    {
        [TestMethod]
        public void Test0()
        {
            var pagination = new Pagination<int>
            {
                Page = 1,
                Size = 10,
                TotalItems = 10,
                PreviousText = "Prev",
                NextText = "Next",
                AutoHide = false
            };
            string html = pagination.GetSimpleLinks(p => "/test?p=" + p);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item disabled\"><span class=\"page-link\">Prev</span></li><li class=\"page-item disabled\"><span class=\"page-link\">Next</span></li></ul></nav>";
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
                PreviousText = "Prev",
                NextText = "Next",
                Append = "2 / 2"
            };
            string html = pagination.GetSimpleLinks(p => "/test?p=" + p);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">Prev</a></li><li class=\"page-item disabled\"><span class=\"page-link\">Next</span></li><li class=\"page-item disabled\"><span class=\"page-link\">2 / 2</span></li></ul></nav>";
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
                LastPageText = "&gt;&gt;",
                Append = "3 / 5"
            };
            string html = pagination.GetSimpleLinks(p => "/test?p=" + p);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=4\">Next</a></li><li class=\"page-item disabled\"><span class=\"page-link\">3 / 5</span></li></ul></nav>";
            Assert.AreEqual(expected, html);
        }
    }
}
