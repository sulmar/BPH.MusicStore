namespace BPH.MusicStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeArtist : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Artists", "LastName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Artists", "LastName", c => c.String());
        }
    }
}
