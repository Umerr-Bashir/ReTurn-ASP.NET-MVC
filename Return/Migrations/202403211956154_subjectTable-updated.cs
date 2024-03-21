namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subjectTableupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "Day", c => c.String());
            AddColumn("dbo.Subjects", "Time", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "Time");
            DropColumn("dbo.Subjects", "Day");
        }
    }
}
