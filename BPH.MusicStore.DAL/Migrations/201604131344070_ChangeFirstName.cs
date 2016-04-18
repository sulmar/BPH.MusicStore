namespace BPH.MusicStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFirstName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Artists", "FirstName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Artists", "FirstName", c => c.String());
        }
    }
}
