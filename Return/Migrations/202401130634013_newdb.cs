namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Salary", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Users", "Salary", c => c.Int());
        }
    }
}
