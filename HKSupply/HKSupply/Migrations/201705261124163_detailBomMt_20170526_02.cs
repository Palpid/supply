namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class detailBomMt_20170526_02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DETAIL_BOM_MT", "DENSITY", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.DETAIL_BOM_MT_HISTORY", "DENSITY", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DETAIL_BOM_MT_HISTORY", "DENSITY");
            DropColumn("dbo.DETAIL_BOM_MT", "DENSITY");
        }
    }
}
