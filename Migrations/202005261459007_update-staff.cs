namespace ScannerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatestaff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "job", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staffs", "job");
        }
    }
}
