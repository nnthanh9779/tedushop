using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using ShopOnline.Data;
using ShopOnline.Data.Infrastructure;
using ShopOnline.Data.Repositories;
using ShopOnline.Model.Models;
using ShopOnline.Service;
using ShopOnline.Web.Mappings;
using WebGrease.Configuration;

[assembly: OwinStartup(typeof(ShopOnline.Web.App_Start.Startup))]
//Được chạy auto startup khi ung dụng chạy
namespace ShopOnline.Web.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            ConfigAutofac(app);

            //Startup.Auth.cs
            ConfigureAuth(app);
        }

        public void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //Register your web mvc controller
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //register your web api controller
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //TODO: All start automatically when required

            //Mỗi khi có 1 class gọi đến biến này sẽ Instance tự động (không cần khởi tạo new nữa)
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<DatabaseContext>().AsSelf().InstancePerRequest();


            //Asp.net Identity
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();


            //Repository
            builder.RegisterAssemblyTypes(typeof(PostCategoryRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            //Service
            builder.RegisterAssemblyTypes(typeof(PostCategoryService).Assembly)
             .Where(t => t.Name.EndsWith("Service"))
             .AsImplementedInterfaces().InstancePerRequest();

            //gán tất cả RegisterType vào 1 container của Autofac
            //eng:set all RegisterType to one container of the Autofac
            Autofac.IContainer container = builder.Build();
            //thay cho cơ chế mặc định
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //Set the WebApi DepandencyResolver
            //api và controller có thể sử dụng chung
            //eng:api and controller can used general
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
        }
    }
}
