namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class functionalityReport_02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FUNCTIONALITY_REPORTS", "CODE", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FUNCTIONALITY_REPORTS", "CODE");
        }
    }
}
