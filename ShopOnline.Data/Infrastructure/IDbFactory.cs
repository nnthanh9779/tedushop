using System;

namespace ShopOnline.Data.Infrastructure
{
    //Giao tiếp Khởi tạo các đối tượng entity thông qua Factory (áp dụng Factory Design Pattern)
    public interface IDbFactory : IDisposable
    {
        DatabaseContext Init();
    }
}