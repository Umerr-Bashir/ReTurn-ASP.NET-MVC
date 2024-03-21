namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intTostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Days", "DayName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Days", "DayName", c => c.Int(nullable: false));
        }
    }
}
