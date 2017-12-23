namespace MusicBandsDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddchangedintToDoubleField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stats", "RoyaltiesForAllSongs", c => c.Double(nullable: false));
            AlterColumn("dbo.Stats", "AverageRoyalties", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stats", "AverageRoyalties", c => c.Int(nullable: false));
            AlterColumn("dbo.Stats", "RoyaltiesForAllSongs", c => c.Int(nullable: false));
        }
    }
}
