namespace ScannerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstaffdevicesn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "emerPer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staffs", "emerPer");
        }
    }
}
