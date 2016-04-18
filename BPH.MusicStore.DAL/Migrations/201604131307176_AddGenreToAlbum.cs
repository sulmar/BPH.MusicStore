namespace BPH.MusicStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenreToAlbum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Genre", c => c.String());

        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Genre");
        }
    }
}
