namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmtehstu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ClassId", c => c.Int());
            AddColumn("dbo.Users", "Qualification", c => c.String());
            AddColumn("dbo.Users", "Salary", c => c.Int());
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Users", "ClassId");
            AddForeignKey("dbo.Users", "ClassId", "dbo.Classes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ClassId", "dbo.Classes");
            DropIndex("dbo.Users", new[] { "ClassId" });
            DropColumn("dbo.Users", "Discriminator");
            DropColumn("dbo.Users", "Salary");
            DropColumn("dbo.Users", "Qualification");
            DropColumn("dbo.Users", "ClassId");
        }
    }
}
