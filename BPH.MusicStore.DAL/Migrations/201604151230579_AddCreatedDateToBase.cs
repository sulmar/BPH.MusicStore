namespace BPH.MusicStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedDateToBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "CreatedDate", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AddColumn("dbo.Artists", "CreatedDate", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "CreatedDate");
            DropColumn("dbo.Albums", "CreatedDate");
        }
    }
}
