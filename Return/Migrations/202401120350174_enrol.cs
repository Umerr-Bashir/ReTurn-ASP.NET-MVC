namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enrol : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentClassEnrollments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassId)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ClassId)
                .Index(t => t.SectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentClassEnrollments", "UserId", "dbo.Users");
            DropForeignKey("dbo.StudentClassEnrollments", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.StudentClassEnrollments", "ClassId", "dbo.Classes");
            DropIndex("dbo.StudentClassEnrollments", new[] { "SectionId" });
            DropIndex("dbo.StudentClassEnrollments", new[] { "ClassId" });
            DropIndex("dbo.StudentClassEnrollments", new[] { "UserId" });
            DropTable("dbo.StudentClassEnrollments");
        }
    }
}
