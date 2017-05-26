namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class detailBomMt_20170526_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DETAIL_BOM_MT", "LENGTH", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.DETAIL_BOM_MT", "WIDTH", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.DETAIL_BOM_MT", "HEIGHT", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.DETAIL_BOM_MT", "NUMBER_OF_PARTS", c => c.Int());
            AddColumn("dbo.DETAIL_BOM_MT", "COEFFICIENT1", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.DETAIL_BOM_MT", "COEFFICIENT2", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            //AddColumn("dbo.DETAIL_BOM_MT", "SCRAP", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.DETAIL_BOM_MT_HISTORY", "LENGTH", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.DETAIL_BOM_MT_HISTORY", "WIDTH", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.DETAIL_BOM_MT_HISTORY", "HEIGHT", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.DETAIL_BOM_MT_HISTORY", "NUMBER_OF_PARTS", c => c.Int());
            AddColumn("dbo.DETAIL_BOM_MT_HISTORY", "COEFFICIENT1", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.DETAIL_BOM_MT_HISTORY", "COEFFICIENT2", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            //AddColumn("dbo.DETAIL_BOM_MT_HISTORY", "SCRAP", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            //DropColumn("dbo.DETAIL_BOM_MT", "WASTE");
            //DropColumn("dbo.DETAIL_BOM_MT_HISTORY", "WASTE");

            RenameColumn("dbo.DETAIL_BOM_MT", "WASTE", "SCRAP");
            RenameColumn("dbo.DETAIL_BOM_MT_HISTORY", "WASTE", "SCRAP");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.DETAIL_BOM_MT_HISTORY", "WASTE", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            //AddColumn("dbo.DETAIL_BOM_MT", "WASTE", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            //DropColumn("dbo.DETAIL_BOM_MT_HISTORY", "SCRAP");
            DropColumn("dbo.DETAIL_BOM_MT_HISTORY", "COEFFICIENT2");
            DropColumn("dbo.DETAIL_BOM_MT_HISTORY", "COEFFICIENT1");
            DropColumn("dbo.DETAIL_BOM_MT_HISTORY", "NUMBER_OF_PARTS");
            DropColumn("dbo.DETAIL_BOM_MT_HISTORY", "HEIGHT");
            DropColumn("dbo.DETAIL_BOM_MT_HISTORY", "WIDTH");
            DropColumn("dbo.DETAIL_BOM_MT_HISTORY", "LENGTH");
            //DropColumn("dbo.DETAIL_BOM_MT", "SCRAP");
            DropColumn("dbo.DETAIL_BOM_MT", "COEFFICIENT2");
            DropColumn("dbo.DETAIL_BOM_MT", "COEFFICIENT1");
            DropColumn("dbo.DETAIL_BOM_MT", "NUMBER_OF_PARTS");
            DropColumn("dbo.DETAIL_BOM_MT", "HEIGHT");
            DropColumn("dbo.DETAIL_BOM_MT", "WIDTH");
            DropColumn("dbo.DETAIL_BOM_MT", "LENGTH");

            RenameColumn("dbo.DETAIL_BOM_MT", "SCRAP", "WASTE");
            RenameColumn("dbo.DETAIL_BOM_MT_HISTORY", "SCRAP", "WASTE");
        }
    }
}
