namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chngs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.ClassIncharges", "ClassId", "dbo.Classes");
            DropIndex("dbo.Classes", new[] { "SectionId" });
            DropIndex("dbo.ClassIncharges", new[] { "ClassId" });
            AddColumn("dbo.Sections", "ClassId", c => c.Int(nullable: false));
            AddColumn("dbo.ClassIncharges", "SectionId", c => c.Int(nullable: false));
            CreateIndex("dbo.ClassIncharges", "SectionId");
            CreateIndex("dbo.Sections", "ClassId");
            AddForeignKey("dbo.Sections", "ClassId", "dbo.Classes", "Id");
            AddForeignKey("dbo.ClassIncharges", "SectionId", "dbo.Sections", "Id");
            DropColumn("dbo.Classes", "SectionId");
            DropColumn("dbo.ClassIncharges", "ClassId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClassIncharges", "ClassId", c => c.Int(nullable: false));
            AddColumn("dbo.Classes", "SectionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ClassIncharges", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Sections", "ClassId", "dbo.Classes");
            DropIndex("dbo.Sections", new[] { "ClassId" });
            DropIndex("dbo.ClassIncharges", new[] { "SectionId" });
            DropColumn("dbo.ClassIncharges", "SectionId");
            DropColumn("dbo.Sections", "ClassId");
            CreateIndex("dbo.ClassIncharges", "ClassId");
            CreateIndex("dbo.Classes", "SectionId");
            AddForeignKey("dbo.ClassIncharges", "ClassId", "dbo.Classes", "Id");
            AddForeignKey("dbo.Classes", "SectionId", "dbo.Sections", "Id");
        }
    }
}
