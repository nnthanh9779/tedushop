using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Web.Infrastructure.Core
{
    //Phân trang
    public class PaginationSet<T>
    {
        public int Page { set; get; }
        public int Count //đếm item
        {
            get
            {
                return (Items != null) ? Items.Count() : 0;
            }
        }
        public int TotalPages { set; get; }//Lưu tổng số trang
        public int TotalCount { set; get; } //Lưu tổng số bảng ghi
        public IEnumerable<T> Items { set; get; } //lưu dánh sách item
    }
}