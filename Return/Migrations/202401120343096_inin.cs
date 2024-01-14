namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.ClassIncharges", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.ClassIncharges", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            AddForeignKey("dbo.Classes", "SectionId", "dbo.Sections", "Id");
            AddForeignKey("dbo.ClassIncharges", "ClassId", "dbo.Classes", "Id");
            AddForeignKey("dbo.ClassIncharges", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ClassIncharges", "UserId", "dbo.Users");
            DropForeignKey("dbo.ClassIncharges", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Classes", "SectionId", "dbo.Sections");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClassIncharges", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClassIncharges", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Classes", "SectionId", "dbo.Sections", "Id", cascadeDelete: true);
        }
    }
}
