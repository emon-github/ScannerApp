namespace ScannerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatColumnsName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "client", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "client");
        }
    }
}
