namespace ScannerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedeviceonlinestatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "onlineStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "onlineStatus");
        }
    }
}
