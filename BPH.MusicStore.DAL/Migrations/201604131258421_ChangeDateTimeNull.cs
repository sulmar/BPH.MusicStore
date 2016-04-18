namespace BPH.MusicStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateTimeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Albums", "PublishedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Albums", "PublishedDate", c => c.DateTime(nullable: false));
        }
    }
}
