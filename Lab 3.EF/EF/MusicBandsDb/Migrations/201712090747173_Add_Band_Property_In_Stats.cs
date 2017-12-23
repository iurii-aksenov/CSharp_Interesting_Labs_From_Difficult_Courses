namespace MusicBandsDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Band_Property_In_Stats : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Stats", "BandId");
            AddForeignKey("dbo.Stats", "BandId", "dbo.Bands", "BandId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stats", "BandId", "dbo.Bands");
            DropIndex("dbo.Stats", new[] { "BandId" });
        }
    }
}
