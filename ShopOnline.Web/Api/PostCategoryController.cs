using AutoMapper;
using ShopOnline.Model.Models;
using ShopOnline.Service;
using ShopOnline.Web.Infrastructure.Core;
using ShopOnline.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopOnline.Web.Infrastructure.Extensions;

namespace ShopOnline.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) :
            base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }

        //Select
        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            // trả về CreateHttpResponse(kề thừa ApiControllerBase) để bên trong có thể sử dụng duoc
            return CreateHttpResponse(request, () =>
            {
                var listCategory = _postCategoryService.GetAll();

                var listPostCategoryVm = Mapper.Map<List<PostCategoryViewModel>>(listCategory);
                //sau khi lấy ra list xong trả về list
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPostCategoryVm);

                return response;
            });
        }


        //Create
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryVm)
        {
            // trả về CreateHttpResponse(kề thừa ApiControllerBase) để bên trong có thể sử dụng duoc
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //Kiểm tra from đã đúng theo cấu hình trong db chưa
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory newPostCategory = new PostCategory();
                    //Tự động giá trị postCategoryVm được coppy sang newPostCategory
                    newPostCategory.UpdatePostCategory(postCategoryVm);

                    var category = _postCategoryService.Add(newPostCategory);
                    _postCategoryService.Save();

                    //sau khi Add xong trả về  đối tượng category
                    response = request.CreateResponse(HttpStatusCode.Created, category);
                }
                return response;
            });
        }

        //Update
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVm)
        {
            // trả về CreateHttpResponse(kề thừa ApiControllerBase) để bên trong có thể sử dụng duoc
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //Kiểm tra from đã đúng theo cấu hình trong db chưa
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var postCategoryDb = _postCategoryService.GetById(postCategoryVm.ID);
                    postCategoryDb.UpdatePostCategory(postCategoryVm);

                    _postCategoryService.Update(postCategoryDb);
                    _postCategoryService.Save();

                    //Message success (200)
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            // trả về CreateHttpResponse(kề thừa ApiControllerBase) để bên trong có thể sử dụng duoc
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //Kiểm tra from đã đúng theo cấu hình trong db chưa
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _postCategoryService.Delete(id);
                    _postCategoryService.Save();

                    //Message success (200)
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }
    }
}