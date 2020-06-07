namespace ScannerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addordrbyinstaff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "ORDER_BY_DERIVED_0", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staffs", "ORDER_BY_DERIVED_0");
        }
    }
}
