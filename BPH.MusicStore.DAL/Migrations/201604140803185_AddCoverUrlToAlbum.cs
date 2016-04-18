namespace BPH.MusicStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCoverUrlToAlbum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Albums", "CoverUrl", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Albums", "CoverUrl", c => c.String());
        }
    }
}
