namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class text : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassAssignments", "Text", c => c.String());
            AddColumn("dbo.StudentAssignments", "Text", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentAssignments", "Text");
            DropColumn("dbo.ClassAssignments", "Text");
        }
    }
}
