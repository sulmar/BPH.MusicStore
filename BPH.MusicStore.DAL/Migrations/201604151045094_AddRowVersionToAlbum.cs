namespace BPH.MusicStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRowVersionToAlbum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "RowVersion", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "RowVersion");
        }
    }
}
