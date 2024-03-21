namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedClassAssignment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassAssignments", "SectionId", "dbo.Sections");
            DropIndex("dbo.ClassAssignments", new[] { "SectionId" });
            DropColumn("dbo.ClassAssignments", "SectionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClassAssignments", "SectionId", c => c.Int(nullable: false));
            CreateIndex("dbo.ClassAssignments", "SectionId");
            AddForeignKey("dbo.ClassAssignments", "SectionId", "dbo.Sections", "Id");
        }
    }
}
