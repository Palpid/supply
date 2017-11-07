namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SuppluQttyDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DOC_LINES", "QUANTITY", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.DOC_LINES", "QUANTITY_ORIGINAL", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.DOC_LINES", "DELIVERED_QUANTITY", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DOC_LINES", "DELIVERED_QUANTITY", c => c.Int(nullable: false));
            AlterColumn("dbo.DOC_LINES", "QUANTITY_ORIGINAL", c => c.Int(nullable: false));
            AlterColumn("dbo.DOC_LINES", "QUANTITY", c => c.Int(nullable: false));
        }
    }
}
