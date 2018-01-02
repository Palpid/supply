namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocLinesRequestedQuantity_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DOC_LINES", "REQUESTED_QUANTITY", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DOC_LINES", "REQUESTED_QUANTITY");
        }
    }
}
