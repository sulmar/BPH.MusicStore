namespace BPH.MusicStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModifiedDateToBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "ModifiedDate", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AddColumn("dbo.Artists", "ModifiedDate", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "ModifiedDate");
            DropColumn("dbo.Albums", "ModifiedDate");
        }
    }
}
