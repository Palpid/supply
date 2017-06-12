namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scrapNUll_20170612_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DETAIL_BOM_HW", "SCRAP", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.DETAIL_BOM_HW_HISTORY", "SCRAP", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DETAIL_BOM_HW_HISTORY", "SCRAP", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.DETAIL_BOM_HW", "SCRAP", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
        }
    }
}
