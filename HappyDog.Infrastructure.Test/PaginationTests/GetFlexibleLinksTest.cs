using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HappyDog.Infrastructure.Test.PaginationTests
{
    [TestClass]
    public class GetFlexibleLinksTest
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
            string html = pagination.GetFlexibleLinks(p => "/test?p=" + p, 5);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item active\"><span class=\"page-link\">1</span></li></ul></nav>";
            Assert.AreEqual(expected, html);
        }

        [TestMethod]
        public void Test1()
        {
            var pagination = new Pagination<int>
            {
                Page = 1,
                Size = 10,
                TotalItems = 10,
                AutoHide = true,
                PreviousText = "&laquo;",
                NextText = "&raquo;"
            };
            string html = pagination.GetFlexibleLinks(p => "/test?p=" + p, 5);
            Assert.AreEqual(string.Empty, html);
        }

        [TestMethod]
        public void Test2()
        {
            var pagination = new Pagination<int>
            {
                Page = 2,
                Size = 10,
                TotalItems = 11,
                PreviousText = "Previous",
                NextText = "&raquo;"
            };
            string html = pagination.GetFlexibleLinks(p => "/test?p=" + p, 5);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">1</a></li><li class=\"page-item active\"><span class=\"page-link\">2</span></li></ul></nav>";
            Assert.AreEqual(expected, html);
        }

        [TestMethod]
        public void Test3()
        {
            var pagination = new Pagination<int>
            {
                Page = 1,
                Size = 10,
                TotalItems = 48,
                PreviousText = "Previous",
                NextText = "Next",
                Append = "1 / 5"
            };
            string html = pagination.GetFlexibleLinks(p => "/test?p=" + p, 5);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item active\"><span class=\"page-link\">1</span></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">2</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">3</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=4\">4</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=5\">5</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">Next</a></li><li class=\"page-item\"><span class=\"page-link\">1 / 5</span></li></ul></nav>";
            Assert.AreEqual(expected, html);
        }

        [TestMethod]
        public void Test4()
        {
            var pagination = new Pagination<int>
            {
                Page = 2,
                Size = 10,
                TotalItems = 48,
                PreviousText = "Previous",
                NextText = "Next",
                Append = "2 / 5"
            };
            string html = pagination.GetFlexibleLinks(p => "/test?p=" + p, 5);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">1</a></li><li class=\"page-item active\"><span class=\"page-link\">2</span></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">3</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=4\">4</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=5\">5</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">Next</a></li><li class=\"page-item\"><span class=\"page-link\">2 / 5</span></li></ul></nav>";
            Assert.AreEqual(expected, html);
        }

        [TestMethod]
        public void Test5()
        {
            var pagination = new Pagination<int>
            {
                Page = 3,
                Size = 10,
                TotalItems = 48,
                PreviousText = "Previous",
                NextText = "Next",
                Append = "3 / 5"
            };
            string html = pagination.GetFlexibleLinks(p => "/test?p=" + p, 5);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">1</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">2</a></li><li class=\"page-item active\"><span class=\"page-link\">3</span></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=4\">4</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=5\">5</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=4\">Next</a></li><li class=\"page-item\"><span class=\"page-link\">3 / 5</span></li></ul></nav>";
            Assert.AreEqual(expected, html);
        }

        [TestMethod]
        public void Test6()
        {
            var pagination = new Pagination<int>
            {
                Page = 4,
                Size = 10,
                TotalItems = 48,
                PreviousText = "Previous",
                NextText = "Next",
                Append = "4 / 5"
            };
            string html = pagination.GetFlexibleLinks(p => "/test?p=" + p, 5);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">1</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">2</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">3</a></li><li class=\"page-item active\"><span class=\"page-link\">4</span></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=5\">5</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=5\">Next</a></li><li class=\"page-item\"><span class=\"page-link\">4 / 5</span></li></ul></nav>";
            Assert.AreEqual(expected, html);
        }

        [TestMethod]
        public void Test7()
        {
            var pagination = new Pagination<int>
            {
                Page = 9,
                Size = 10,
                TotalItems = 480,
                PreviousText = "Previous",
                NextText = "Next",
                Append = "9 / 48"
            };
            string html = pagination.GetFlexibleLinks(p => "/test?p=" + p, 5);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=8\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=7\">7</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=8\">8</a></li><li class=\"page-item active\"><span class=\"page-link\">9</span></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=10\">10</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=11\">11</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=10\">Next</a></li><li class=\"page-item\"><span class=\"page-link\">9 / 48</span></li></ul></nav>";
            Assert.AreEqual(expected, html);
        }

        [TestMethod]
        public void Test8()
        {
            var pagination = new Pagination<int>
            {
                Page = 9,
                Size = 10,
                TotalItems = 48,
                PreviousText = "Previous",
                NextText = "Next",
                Append = "9 / 5"
            };
            string html = pagination.GetFlexibleLinks(p => "/test?p=" + p, 3);
            Assert.AreEqual(string.Empty, html);
        }

        [TestMethod]
        public void Test9()
        {
            var pagination = new Pagination<int>
            {
                Page = 4,
                Size = 20,
                TotalItems = 63,
                PreviousText = "Previous",
                NextText = "Next"
            };
            string html = pagination.GetFlexibleLinks(p => "/test?p=" + p, 7);
            string expected = "<nav><ul class=\"pagination\"><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">Previous</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=1\">1</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=2\">2</a></li><li class=\"page-item\"><a class=\"page-link\" href=\"/test?p=3\">3</a></li><li class=\"page-item active\"><span class=\"page-link\">4</span></li></ul></nav>";
            Assert.AreEqual(expected, html);
        }
    }
}
