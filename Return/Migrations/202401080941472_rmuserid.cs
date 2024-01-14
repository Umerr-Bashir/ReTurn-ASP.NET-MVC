namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmuserid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "UserId1", "dbo.Users");
            DropIndex("dbo.Users", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "UserId1" });
            DropColumn("dbo.Users", "UserId");
            DropColumn("dbo.Users", "UserId1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserId1", c => c.Int());
            AddColumn("dbo.Users", "UserId", c => c.Int());
            CreateIndex("dbo.Users", "UserId1");
            CreateIndex("dbo.Users", "UserId");
            AddForeignKey("dbo.Users", "UserId1", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "UserId", "dbo.Users", "Id");
        }
    }
}
