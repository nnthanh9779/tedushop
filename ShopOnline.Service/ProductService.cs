using ShopOnline.Common;
using ShopOnline.Data.Infrastructure;
using ShopOnline.Data.Repositories;
using ShopOnline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Common;

namespace ShopOnline.Service
{
    public interface IProductService
    {
        Product Add(Product product);
        void Update(Product product);
        Product Delete(int id);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAll(string keyword);
        IEnumerable<Product> GetAllCategory(int categoryId);
        Product GetById(int id);
        void Save();
    }

    class ProductService : IProductService
    {
        IProductRepository _productResponsitory;
        ITagRepository _tagRepository;
        IProductTagRepository _productTagRepository;
        IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, ITagRepository tagRepository, IProductTagRepository productTagRepository, IUnitOfWork unitOfWork)
        {
            this._productResponsitory = productRepository;
            this._productTagRepository = productTagRepository;
            this._tagRepository = tagRepository;
            this._unitOfWork = unitOfWork;
        }

        public Product Add(Product _product)
        {
            var product = _productResponsitory.Add(_product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');

                for (int i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);

                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i].Trim();
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag();
                    productTag.TagID = tagId;
                    productTag.ProductID = product.ID;
                    _productTagRepository.Add(productTag);
                }
            }
            return product;
        }

        public Product Delete(int id)
        {
            _productTagRepository.DeleteMulti(x => x.ProductID == id);
            return _productResponsitory.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productResponsitory.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _productResponsitory.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            else
            {
                return _productResponsitory.GetAll();
            }
        }

        public IEnumerable<Product> GetAllCategory(int categoryId)
        {
            return _productResponsitory.GetMulti(x => x.Status == true && x.CategoryID == categoryId);
        }

        public Product GetById(int id)
        {
            return _productResponsitory.GetSingleById(id);
        }

        public void Update(Product product)
        {
            _productResponsitory.Update(product);
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');

                for (int i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);

                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i].Trim();
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.TagID = tagId;
                    productTag.ProductID = product.ID;
                    _productTagRepository.Add(productTag);
                }
            }
            _unitOfWork.Commit();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
