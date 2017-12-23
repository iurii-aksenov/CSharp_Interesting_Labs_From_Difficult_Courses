namespace MusicBandsDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bands", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Musicians", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.MusicianProfiles", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Performances", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Songs", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stats", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stats", "IsDeleted");
            DropColumn("dbo.Songs", "IsDeleted");
            DropColumn("dbo.Performances", "IsDeleted");
            DropColumn("dbo.MusicianProfiles", "IsDeleted");
            DropColumn("dbo.Musicians", "IsDeleted");
            DropColumn("dbo.Bands", "IsDeleted");
        }
    }
}
