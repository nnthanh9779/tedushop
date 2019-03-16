using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopOnline.Data.Infrastructure;
using ShopOnline.Data.Repositories;
using ShopOnline.Model.Models;

namespace ShopOnline.UnitTest.RespositoryTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        //Khai báo đối tượng tương tác csdl
        IDbFactory dbFactory;
        IPostCategoryRepository objRepository;
        IUnitOfWork unitOfWork;

        //Khởi chạy đầu tiên để thiết lập các tham số, đối tượng
        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new PostCategoryRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void PostCategory_Repository_GetAll()
        {
            var list = objRepository.GetAll().ToList();

            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void PostCategory_Repository_Create()
        {
            PostCategory category = new PostCategory();
            category.Name = "Test Category";
            category.Alias = "Test-Category";
            category.Status=true;

            var result = objRepository.Add(category);
            unitOfWork.Commit();

            //không rỗng
            Assert.IsNotNull(result);
            //so sánh
            Assert.AreEqual(1, result.ID);
        }
    }
}
