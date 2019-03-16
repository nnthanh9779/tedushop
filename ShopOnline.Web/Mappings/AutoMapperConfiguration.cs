using AutoMapper;
using ShopOnline.Model.Models;
using ShopOnline.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper.Mappers;

namespace ShopOnline.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        //chú ý khai báo trong Global
        public static void Configure()
        {
            //cfg: Configuration
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();
            });
        }
    }
}