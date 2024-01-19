namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class int2strng : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subjects", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subjects", "Name", c => c.Int(nullable: false));
        }
    }
}
