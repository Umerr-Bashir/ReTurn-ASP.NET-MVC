namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stuclasid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ClassId", c => c.Int());
            CreateIndex("dbo.Users", "ClassId");
            AddForeignKey("dbo.Users", "ClassId", "dbo.Classes", "Id");
            DropColumn("dbo.Users", "Class");
            DropColumn("dbo.Users", "Section");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Section", c => c.String());
            AddColumn("dbo.Users", "Class", c => c.String());
            DropForeignKey("dbo.Users", "ClassId", "dbo.Classes");
            DropIndex("dbo.Users", new[] { "ClassId" });
            DropColumn("dbo.Users", "ClassId");
        }
    }
}
