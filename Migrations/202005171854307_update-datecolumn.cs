namespace ScannerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatecolumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccessRecords", "recordTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccessRecords", "recordTime", c => c.String());
        }
    }
}
