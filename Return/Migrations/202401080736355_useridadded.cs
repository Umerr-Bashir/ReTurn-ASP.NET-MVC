namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useridadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserId", c => c.Int());
            AddColumn("dbo.Users", "UserId1", c => c.Int());
            CreateIndex("dbo.Users", "UserId");
            CreateIndex("dbo.Users", "UserId1");
            AddForeignKey("dbo.Users", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "UserId1", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserId1", "dbo.Users");
            DropForeignKey("dbo.Users", "UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "UserId1" });
            DropIndex("dbo.Users", new[] { "UserId" });
            DropColumn("dbo.Users", "UserId1");
            DropColumn("dbo.Users", "UserId");
        }
    }
}
