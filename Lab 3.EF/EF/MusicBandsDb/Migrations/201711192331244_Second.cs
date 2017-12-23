namespace MusicBandsDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Performances",
                c => new
                    {
                        PerformanceId = c.Int(nullable: false, identity: true),
                        PerformanceName = c.String(maxLength: 50),
                        Proceeds = c.Int(nullable: false),
                        Place = c.String(maxLength: 50),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PerformanceId);
            
            CreateTable(
                "dbo.PerformanceBands",
                c => new
                    {
                        Performance_PerformanceId = c.Int(nullable: false),
                        Band_BandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Performance_PerformanceId, t.Band_BandId })
                .ForeignKey("dbo.Performances", t => t.Performance_PerformanceId, cascadeDelete: true)
                .ForeignKey("dbo.Bands", t => t.Band_BandId, cascadeDelete: true)
                .Index(t => t.Performance_PerformanceId)
                .Index(t => t.Band_BandId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PerformanceBands", "Band_BandId", "dbo.Bands");
            DropForeignKey("dbo.PerformanceBands", "Performance_PerformanceId", "dbo.Performances");
            DropIndex("dbo.PerformanceBands", new[] { "Band_BandId" });
            DropIndex("dbo.PerformanceBands", new[] { "Performance_PerformanceId" });
            DropTable("dbo.PerformanceBands");
            DropTable("dbo.Performances");
        }
    }
}
