namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FK_detailItemHf_20170607_01 : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.DETAIL_BOM_HF", "ID_BOM", "dbo.ITEMS_BOM");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DETAIL_BOM_HF", "ID_BOM", "dbo.ITEMS_BOM");
        }
    }
}
