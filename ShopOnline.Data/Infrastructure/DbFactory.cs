using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Data.Infrastructure
{
    //Khởi tạo các đối tượng DbContext (Thay vì new trực tiếp)
    public class DbFactory : Disposable, IDbFactory
    {
        DatabaseContext dbContext;

        public DatabaseContext Init()
        {
            //Nếu null tạo mới
            return dbContext ?? (dbContext = new DatabaseContext());
        }

        //ghi đè DisposeCore() của Disposable
        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
