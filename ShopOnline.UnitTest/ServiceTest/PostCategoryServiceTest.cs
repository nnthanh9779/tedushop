using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopOnline.Data.Infrastructure;
using ShopOnline.Data.Repositories;
using ShopOnline.Model.Models;
using ShopOnline.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        //Mock tạo đối tượng giả
        //Mock object experience expression thông qua interface
        private Mock<IPostCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        private IPostCategoryService _categoryService;
        //Khai báo List làm giá trị mẫu test
        private List<PostCategory> _listCategory;

        [TestInitialize]
        public void Initialize()
        {
            //mock để khởi tạo 1 đối tượng giả
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new PostCategoryService(_mockRepository.Object, _mockUnitOfWork.Object);

            //tạo ds mẫu
            _listCategory = new List<PostCategory>()
            {
                new PostCategory() {ID = 1, Name = "Danh mục 1", Status = true},
                new PostCategory() { ID = 2, Name = "Danh mục 2", Status = true },
                new PostCategory() { ID = 3, Name = "Danh mục 3", Status = true },
            };
        }


        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            //setup  method
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listCategory);

            //call action
            var result = _categoryService.GetAll() as List<PostCategory>;

            //Compere
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            PostCategory category = new PostCategory();

            category.Name = "Test1";
            category.Alias = "test-1";
            category.Status = true;

            //setup
            //cài đặt phương thức Add trả về đúng ID = 1
            _mockRepository.Setup(m => m.Add(category)).Returns((PostCategory p) =>
              {
                  p.ID = 1;
                  return p;
              });

            //call action
            var result = _categoryService.Add(category);

            //compere
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}
