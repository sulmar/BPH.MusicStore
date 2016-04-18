namespace BPH.MusicStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropRowVersion : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Albums", "RowVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "RowVersion", c => c.Binary());
        }
    }
}
