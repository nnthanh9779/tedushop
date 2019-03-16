namespace ShopOnline.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Changkey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.IdentityUserRoles");
            AlterColumn("dbo.IdentityUserRoles", "RoleId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.IdentityUserRoles", new[] { "RoleId", "UserId" });
        }

        public override void Down()
        {
            DropPrimaryKey("dbo.IdentityUserRoles");
            AlterColumn("dbo.IdentityUserRoles", "RoleId", c => c.String());
            AddPrimaryKey("dbo.IdentityUserRoles", "UserId");
        }
    }
}
