namespace Return.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class daytimeTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Times",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timing = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Subjects", "DayId", c => c.Int(nullable: false));
            AddColumn("dbo.Subjects", "TimeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Subjects", "DayId");
            CreateIndex("dbo.Subjects", "TimeId");
            AddForeignKey("dbo.Subjects", "DayId", "dbo.Days", "Id");
            AddForeignKey("dbo.Subjects", "TimeId", "dbo.Times", "Id");
            DropColumn("dbo.Subjects", "Day");
            DropColumn("dbo.Subjects", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "Time", c => c.String());
            AddColumn("dbo.Subjects", "Day", c => c.String());
            DropForeignKey("dbo.Subjects", "TimeId", "dbo.Times");
            DropForeignKey("dbo.Subjects", "DayId", "dbo.Days");
            DropIndex("dbo.Subjects", new[] { "TimeId" });
            DropIndex("dbo.Subjects", new[] { "DayId" });
            DropColumn("dbo.Subjects", "TimeId");
            DropColumn("dbo.Subjects", "DayId");
            DropTable("dbo.Times");
            DropTable("dbo.Days");
        }
    }
}
