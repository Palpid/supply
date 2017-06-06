namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class detailBomHw_scrap_20170530_01 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.DETAIL_BOM_HW", "SCRAP", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            //AddColumn("dbo.DETAIL_BOM_HW_HISTORY", "SCRAP", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            //DropColumn("dbo.DETAIL_BOM_HW", "WASTE");
            //DropColumn("dbo.DETAIL_BOM_HW_HISTORY", "WASTE");

            RenameColumn("dbo.DETAIL_BOM_HW", "WASTE", "SCRAP");
            RenameColumn("dbo.DETAIL_BOM_HW_HISTORY", "WASTE", "SCRAP");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.DETAIL_BOM_HW_HISTORY", "WASTE", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            //AddColumn("dbo.DETAIL_BOM_HW", "WASTE", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            //DropColumn("dbo.DETAIL_BOM_HW_HISTORY", "SCRAP");
            //DropColumn("dbo.DETAIL_BOM_HW", "SCRAP");

            RenameColumn("dbo.DETAIL_BOM_HW", "SCRAP", "WASTE");
            RenameColumn("dbo.DETAIL_BOM_HW_HISTORY", "SCRAP", "WASTE");
        }
    }
}
