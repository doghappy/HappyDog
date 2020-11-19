using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyDog.Infrastructure
{
    public class Pagination<T>
    {
        public List<T> Data { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / Size);
        public int Size { get; set; } = 20;
        public int Page { get; set; }

        /// <summary>
        /// Previous text, default is «(&amp;laquo;)
        /// </summary>
        [JsonIgnore]
        public string PreviousText { get; set; } = "&laquo;";

        /// <summary>
        /// Previous text, default is »(&amp;raquo;)
        /// </summary>
        [JsonIgnore]
        public string NextText { get; set; } = "&raquo;";

        /// <summary>
        /// Display the page number of the first page, if the value is set.
        /// </summary>
        [JsonIgnore]
        public string FirstPageText { get; set; }

        /// <summary>
        /// Display the page number of the last page, if the value is set.
        /// </summary>
        [JsonIgnore]
        public string LastPageText { get; set; }

        /// <summary>
        /// In the last item of the page bar, Appen is displayed in the style of the page number.
        /// </summary>
        [JsonIgnore]
        public string Append => Page + " / " + TotalPages;

        /// <summary>
        /// Auto hide when TotalPages is less than 2
        /// </summary>
        [JsonIgnore]
        public bool AutoHide { get; set; } = true;

        public string GetPageNumberLinks(Func<int, string> pageUrl)
        {
            if (AutoHide && TotalPages == 1)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<nav><ul class=\"pagination\">");
            if (Page > 1)
            {
                if (FirstPageText != null)
                {
                    builder
                        .Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                        .Append(pageUrl(1))
                        .Append("\">")
                        .Append(FirstPageText)
                        .Append("</a></li>");
                }
                builder
                   .Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                   .Append(pageUrl(Page - 1))
                   .Append("\">")
                   .Append(PreviousText)
                   .Append("</a></li>");
            }
            for (int i = 1; i <= TotalPages; i++)
            {
                if (Page == i)
                    builder
                        .Append("<li class=\"page-item active\"><span class=\"page-link\">")
                        .Append(i)
                        .Append("</span></li>");
                else
                    builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                        .Append(pageUrl(i))
                        .Append("\">")
                        .Append(i)
                        .Append("</a></li>");
            }
            if (Page < TotalPages)
            {
                builder
                    .Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                    .Append(pageUrl(Page + 1))
                    .Append("\">")
                    .Append(NextText)
                    .Append("</a></li>");
                if (LastPageText != null)
                {
                    builder
                        .Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                        .Append(pageUrl(TotalPages))
                        .Append("\">")
                        .Append(LastPageText)
                        .Append("</a></li>");
                }
            }
            if (Append != null)
            {
                builder
                    .Append("<li class=\"page-item\"><span class=\"page-link\">")
                    .Append(Append)
                    .Append("</span></li>");
            }
            builder.Append("</ul></nav>");
            return builder.ToString();
        }

        /// <summary>
        /// Get the elasticity pagination bar, the count of displayed page numbers will not exceed the specified value.
        /// </summary>
        /// <param name="pageUrl"></param>
        /// <param name="total">Total number of pages</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string GetFlexibleLinks(Func<int, string> pageUrl, int total)
        {
            if (AutoHide && TotalPages == 1)
            {
                return string.Empty;
            }
            if (Page > TotalPages || Page < 1)
            {
                return string.Empty;
            }
            if (total < 1)
            {
                throw new InvalidOperationException("Make sure that ElasticityCount is greater than 0");
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<nav><ul class=\"pagination\">");

            int left = (total - 1) / 2;
            int right = total - left - 1;
            int start, end;
            if (Page <= left)
            {
                start = 1;
                end = total > TotalPages ? TotalPages : total;
            }
            else if ((TotalPages - Page) < right)
            {
                end = TotalPages;
                start = end - total + 1;
                if (start < 1)
                    start = 1;
            }
            else
            {
                start = Page - left;
                end = Page + right;
            }

            if (Page > 1)
            {
                if (FirstPageText != null)
                {
                    builder
                        .Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                        .Append(pageUrl(1))
                        .Append("\">")
                        .Append(FirstPageText)
                        .Append("</a></li>");
                }
                builder
                   .Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                   .Append(pageUrl(Page - 1))
                   .Append("\">")
                   .Append(PreviousText)
                   .Append("</a></li>");
            }
            for (int i = start; i <= end; i++)
            {
                if (Page == i)
                    builder
                        .Append("<li class=\"page-item active\"><span class=\"page-link\">")
                        .Append(i)
                        .Append("</span></li>");
                else
                    builder
                        .Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                        .Append(pageUrl(i))
                        .Append("\">")
                        .Append(i)
                        .Append("</a></li>");
            }

            if (Page < TotalPages)
            {
                builder
                    .Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                    .Append(pageUrl(Page + 1))
                    .Append("\">")
                    .Append(NextText)
                    .Append("</a></li>");
                if (LastPageText != null)
                {
                    builder
                        .Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                        .Append(pageUrl(TotalPages))
                        .Append("\">")
                        .Append(LastPageText)
                        .Append("</a></li>");
                }
            }
            if (Append != null)
            {
                builder
                    .Append("<li class=\"page-item\"><span class=\"page-link\">")
                    .Append(Append)
                    .Append("</span></li>");
            }
            builder.Append("</ul></nav>");
            return builder.ToString();
        }

        public string GetSimpleLinks(Func<int, string> pageUrl)
        {
            if (AutoHide && TotalPages == 1)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<nav><ul class=\"pagination\">");
            if (Page <= 1)
            {
                builder
                    .Append("<li class=\"page-item disabled\"><span class=\"page-link\">")
                    .Append(PreviousText)
                    .Append("</span></li>");
            }
            else
            {
                int prev = Page - 1;
                builder
                    .Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                    .Append(pageUrl(prev))
                    .Append("\">")
                    .Append(PreviousText)
                    .Append("</a></li>");
            }
            if (Page >= TotalPages)
            {
                builder
                    .Append("<li class=\"page-item disabled\"><span class=\"page-link\">")
                    .Append(NextText)
                    .Append("</span></li>");
            }
            else
            {
                int next = Page + 1;
                builder
                    .Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                    .Append(pageUrl(next))
                    .Append("\">")
                    .Append(NextText)
                    .Append("</a></li>");
            }
            if (Append != null)
            {
                builder
                    .Append("<li class=\"page-item disabled\"><span class=\"page-link\">")
                    .Append(Append)
                    .Append("</span></li>");
            }
            builder.Append("</ul></nav>");
            return builder.ToString();
        }
    }
}
