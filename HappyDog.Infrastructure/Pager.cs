using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HappyDog.Infrastructure
{
    public class Pager
    {
        public Pager(int page, int size)
        {
            Page = page < 1 ? 1 : page;
            Size = size < 1 ? 1 : size;
        }

        public int Page { get; set; }
        public int Size { get; private set; }

        //[ScriptIgnore]
        public int Skip { get => (Page - 1) * Size; }

        public int TotalPages { get => (int)Math.Ceiling((decimal)TotalItems / Size); }

        //[ScriptIgnore]
        public int TotalItems { get; set; }

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

        public async Task<Pagination<T>> GetPaginationAsync<T>(IQueryable<T> query)
        {
            TotalItems = await query.CountAsync();
            return new Pagination<T>
            {
                Data = await query.Skip(Skip).Take(Size).ToListAsync(),
                TotalItems = TotalItems,
                TotalPages = TotalPages
            };
        }
    }
}
