namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmcid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentClassEnrollments", "ClassId", "dbo.Classes");
            DropIndex("dbo.StudentClassEnrollments", new[] { "ClassId" });
            DropColumn("dbo.StudentClassEnrollments", "ClassId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentClassEnrollments", "ClassId", c => c.Int(nullable: false));
            CreateIndex("dbo.StudentClassEnrollments", "ClassId");
            AddForeignKey("dbo.StudentClassEnrollments", "ClassId", "dbo.Classes", "Id");
        }
    }
}
