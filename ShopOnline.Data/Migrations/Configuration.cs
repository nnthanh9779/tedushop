namespace ShopOnline.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ShopOnline.Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ShopOnline.Data.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ShopOnline.Data.DatabaseContext context)
        {
            CreateProductCategorySample(context);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //Initialize sample data
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DatabaseContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DatabaseContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "vana",
            //    Email = "vana@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Lê Văn A"
            //};

            //manager.Create(user, "123654$");

            ////if the role does not exist , create two Admin and User roles
            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("vana@gmail.com");

            ////if successful, add user to both Roles
            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        public void CreateProductCategorySample(ShopOnline.Data.DatabaseContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategories = new List<ProductCategory>()
            {
                new ProductCategory() {Name="Điện lạnh", Alias = "dien-lanh", Status = true},
                 new ProductCategory() {Name="Viễn thông", Alias = "vien-thong", Status = true},
                  new ProductCategory() {Name="Đồ gia dụng", Alias = "do-gia-dung", Status = true}
            };

                context.ProductCategories.AddRange(listProductCategories);
                context.SaveChanges();
            }

        }
    }
}
