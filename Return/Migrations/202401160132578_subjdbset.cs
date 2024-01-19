namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subjdbset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .ForeignKey("dbo.Users", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.SectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "TeacherId", "dbo.Users");
            DropForeignKey("dbo.Subjects", "SectionId", "dbo.Sections");
            DropIndex("dbo.Subjects", new[] { "SectionId" });
            DropIndex("dbo.Subjects", new[] { "TeacherId" });
            DropTable("dbo.Subjects");
        }
    }
}
