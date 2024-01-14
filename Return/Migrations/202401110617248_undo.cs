namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class undo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sections", "ClassId", "dbo.Classes");
            DropIndex("dbo.Sections", new[] { "ClassId" });
            AddColumn("dbo.Classes", "SectionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Classes", "SectionId");
            AddForeignKey("dbo.Classes", "SectionId", "dbo.Sections", "Id", cascadeDelete: true);
            DropColumn("dbo.Sections", "ClassId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sections", "ClassId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Classes", "SectionId", "dbo.Sections");
            DropIndex("dbo.Classes", new[] { "SectionId" });
            DropColumn("dbo.Classes", "SectionId");
            CreateIndex("dbo.Sections", "ClassId");
            AddForeignKey("dbo.Sections", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
        }
    }
}
