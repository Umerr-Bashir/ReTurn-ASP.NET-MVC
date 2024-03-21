namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentClassAssignment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassAssignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                        FileName = c.String(),
                        FilePath = c.String(),
                        DateTime = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .ForeignKey("dbo.Users", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.StudentAssignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        FileName = c.String(),
                        FilePath = c.String(),
                        Status = c.String(),
                        DateTime = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.StudentId)
                .ForeignKey("dbo.Users", t => t.TeacherId)
                .Index(t => t.StudentId)
                .Index(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentAssignments", "TeacherId", "dbo.Users");
            DropForeignKey("dbo.StudentAssignments", "StudentId", "dbo.Users");
            DropForeignKey("dbo.ClassAssignments", "TeacherId", "dbo.Users");
            DropForeignKey("dbo.ClassAssignments", "SectionId", "dbo.Sections");
            DropIndex("dbo.StudentAssignments", new[] { "TeacherId" });
            DropIndex("dbo.StudentAssignments", new[] { "StudentId" });
            DropIndex("dbo.ClassAssignments", new[] { "SectionId" });
            DropIndex("dbo.ClassAssignments", new[] { "TeacherId" });
            DropTable("dbo.StudentAssignments");
            DropTable("dbo.ClassAssignments");
        }
    }
}
