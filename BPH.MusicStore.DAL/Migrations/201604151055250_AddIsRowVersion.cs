namespace BPH.MusicStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsRowVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "RowVersion");
        }
    }
}
