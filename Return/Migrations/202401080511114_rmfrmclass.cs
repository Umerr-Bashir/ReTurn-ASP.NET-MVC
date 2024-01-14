namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmfrmclass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "UserId", "dbo.Users");
            DropIndex("dbo.Classes", new[] { "UserId" });
            DropColumn("dbo.Classes", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Classes", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Classes", "UserId");
            AddForeignKey("dbo.Classes", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
