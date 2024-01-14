namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classincharge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassIncharges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ClassId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassIncharges", "UserId", "dbo.Users");
            DropForeignKey("dbo.ClassIncharges", "ClassId", "dbo.Classes");
            DropIndex("dbo.ClassIncharges", new[] { "UserId" });
            DropIndex("dbo.ClassIncharges", new[] { "ClassId" });
            DropTable("dbo.ClassIncharges");
        }
    }
}
