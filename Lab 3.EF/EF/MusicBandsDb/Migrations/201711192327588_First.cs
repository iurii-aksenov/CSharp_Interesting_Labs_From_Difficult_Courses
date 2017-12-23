namespace MusicBandsDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bands",
                c => new
                    {
                        BandId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.BandId);
            
            CreateTable(
                "dbo.Musicians",
                c => new
                    {
                        MusicianId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 30),
                        SecondName = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.MusicianId);
            
            CreateTable(
                "dbo.MusicianProfiles",
                c => new
                    {
                        MusicianId = c.Int(nullable: false),
                        NativeLanguage = c.String(),
                        Birthplace = c.String(),
                        Specialization = c.String(),
                    })
                .PrimaryKey(t => t.MusicianId)
                .ForeignKey("dbo.Musicians", t => t.MusicianId)
                .Index(t => t.MusicianId);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        SongId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Royalties = c.Int(nullable: false),
                        BandId = c.Int(),
                    })
                .PrimaryKey(t => t.SongId)
                .ForeignKey("dbo.Bands", t => t.BandId)
                .Index(t => t.BandId);
            
            CreateTable(
                "dbo.Stats",
                c => new
                    {
                        StatsId = c.Int(nullable: false, identity: true),
                        BandId = c.Int(nullable: false),
                        NumberOfMusicians = c.Int(nullable: false),
                        NumberOfSongs = c.Int(nullable: false),
                        RoyaltiesForAllSongs = c.Int(nullable: false),
                        AverageRoyalties = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatsId);
            
            CreateTable(
                "dbo.MusicianBands",
                c => new
                    {
                        Musician_MusicianId = c.Int(nullable: false),
                        Band_BandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Musician_MusicianId, t.Band_BandId })
                .ForeignKey("dbo.Musicians", t => t.Musician_MusicianId, cascadeDelete: true)
                .ForeignKey("dbo.Bands", t => t.Band_BandId, cascadeDelete: true)
                .Index(t => t.Musician_MusicianId)
                .Index(t => t.Band_BandId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "BandId", "dbo.Bands");
            DropForeignKey("dbo.MusicianProfiles", "MusicianId", "dbo.Musicians");
            DropForeignKey("dbo.MusicianBands", "Band_BandId", "dbo.Bands");
            DropForeignKey("dbo.MusicianBands", "Musician_MusicianId", "dbo.Musicians");
            DropIndex("dbo.MusicianBands", new[] { "Band_BandId" });
            DropIndex("dbo.MusicianBands", new[] { "Musician_MusicianId" });
            DropIndex("dbo.Songs", new[] { "BandId" });
            DropIndex("dbo.MusicianProfiles", new[] { "MusicianId" });
            DropTable("dbo.MusicianBands");
            DropTable("dbo.Stats");
            DropTable("dbo.Songs");
            DropTable("dbo.MusicianProfiles");
            DropTable("dbo.Musicians");
            DropTable("dbo.Bands");
        }
    }
}
