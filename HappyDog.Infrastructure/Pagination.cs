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

        public string GetPageNumberLinks(Func<int, string> pageUrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<nav><ul class=\"pagination\">");
            if (Page == 1)
                builder.Append("<li class=\"page-item disabled\"><span class=\"page-link\">&laquo;</span></li>");
            else
                builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                    .Append(pageUrl(Page - 1))
                    .Append("\">&laquo;</a></li>");
            for (int i = 1; i <= TotalPages; i++)
            {
                if (Page == i)
                    builder.Append("<li class=\"page-item active\"><span class=\"page-link\">")
                        .Append(i)
                        .Append("</span></li>");
                else
                    builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                        .Append(pageUrl(i))
                        .Append("\">")
                        .Append(i)
                        .Append("</a></li>");
            }
            if (Page == TotalPages)
                builder.Append("<li class=\"page-item disabled\"><span class=\"page-link\">&raquo;</span></li>");
            else
                builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
                    .Append(pageUrl(Page + 1))
                    .Append("\">&raquo;</a></li>");
            builder.Append("</ul></nav>");
            return builder.ToString();
        }

        //public string GetPageNumberLinks(IUrlHelper)
        //{
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append("<nav><ul class=\"pagination\">");
        //    if (Page == 1)
        //        builder.Append("<li class=\"page-item disabled\"><span class=\"page-link\">&laquo;</span></li>");
        //    else
        //        builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
        //            .Append(pageUrl(Page - 1))
        //            .Append("\">&laquo;</a></li>");
        //    for (int i = 1; i <= TotalPages; i++)
        //    {
        //        if (Page == i)
        //            builder.Append("<li class=\"page-item active\"><span class=\"page-link\">")
        //                .Append(i)
        //                .Append("</span></li>");
        //        else
        //            builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
        //                .Append(pageUrl(i))
        //                .Append("\">")
        //                .Append(i)
        //                .Append("</a></li>");
        //    }
        //    if (Page == TotalPages)
        //        builder.Append("<li class=\"page-item disabled\"><span class=\"page-link\">&raquo;</span></li>");
        //    else
        //        builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"")
        //            .Append(pageUrl(Page + 1))
        //            .Append("\">&raquo;</a></li>");
        //    builder.Append("</ul></nav>");
        //    return builder.ToString();
        //}

        public string GetSimplePagingLinks(Func<int, string> pageUrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<nav><ul class=\"pager\">");
            if (Page != 1)
            {
                builder.Append("<li><a href=\"")
                    .Append(pageUrl(Page - 1))
                    .Append("\">上一页</a></li>");
            }
            if (Page != TotalPages)
            {
                builder.Append("<li><a href=\"")
                    .Append(pageUrl(Page + 1))
                    .Append("\">下一页</a></li>");
            }
            builder.Append("</ul></nav>");
            return builder.ToString();
        }
    }
}
