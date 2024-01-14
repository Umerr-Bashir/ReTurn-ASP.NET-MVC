namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stuenrol : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "SectionId", "dbo.Sections");
            DropIndex("dbo.Classes", new[] { "SectionId" });
            AddColumn("dbo.Sections", "ClassId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sections", "ClassId");
            AddForeignKey("dbo.Sections", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
            DropColumn("dbo.Classes", "SectionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Classes", "SectionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sections", "ClassId", "dbo.Classes");
            DropIndex("dbo.Sections", new[] { "ClassId" });
            DropColumn("dbo.Sections", "ClassId");
            CreateIndex("dbo.Classes", "SectionId");
            AddForeignKey("dbo.Classes", "SectionId", "dbo.Sections", "Id", cascadeDelete: true);
        }
    }
}
