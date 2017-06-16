namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BomBreakdown_20170616_02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DETAIL_BOM_HW", "ID_BOM_BREAKDOWN", c => c.String(maxLength: 100));
            AddColumn("dbo.DETAIL_BOM_MT", "ID_BOM_BREAKDOWN", c => c.String(maxLength: 100));
            AddColumn("dbo.DETAIL_BOM_HW_HISTORY", "ID_BOM_BREAKDOWN", c => c.String(maxLength: 100));
            AddColumn("dbo.DETAIL_BOM_MT_HISTORY", "ID_BOM_BREAKDOWN", c => c.String(maxLength: 100));
            CreateIndex("dbo.DETAIL_BOM_HW", "ID_BOM_BREAKDOWN");
            CreateIndex("dbo.DETAIL_BOM_MT", "ID_BOM_BREAKDOWN");
            CreateIndex("dbo.DETAIL_BOM_HW_HISTORY", "ID_BOM_BREAKDOWN");
            CreateIndex("dbo.DETAIL_BOM_MT_HISTORY", "ID_BOM_BREAKDOWN");
            AddForeignKey("dbo.DETAIL_BOM_HW", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN", "ID_BOM_BREAKDOWN");
            AddForeignKey("dbo.DETAIL_BOM_MT", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN", "ID_BOM_BREAKDOWN");
            AddForeignKey("dbo.DETAIL_BOM_HW_HISTORY", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN", "ID_BOM_BREAKDOWN");
            AddForeignKey("dbo.DETAIL_BOM_MT_HISTORY", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN", "ID_BOM_BREAKDOWN");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DETAIL_BOM_MT_HISTORY", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN");
            DropForeignKey("dbo.DETAIL_BOM_HW_HISTORY", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN");
            DropForeignKey("dbo.DETAIL_BOM_MT", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN");
            DropForeignKey("dbo.DETAIL_BOM_HW", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN");
            DropIndex("dbo.DETAIL_BOM_MT_HISTORY", new[] { "ID_BOM_BREAKDOWN" });
            DropIndex("dbo.DETAIL_BOM_HW_HISTORY", new[] { "ID_BOM_BREAKDOWN" });
            DropIndex("dbo.DETAIL_BOM_MT", new[] { "ID_BOM_BREAKDOWN" });
            DropIndex("dbo.DETAIL_BOM_HW", new[] { "ID_BOM_BREAKDOWN" });
            DropColumn("dbo.DETAIL_BOM_MT_HISTORY", "ID_BOM_BREAKDOWN");
            DropColumn("dbo.DETAIL_BOM_HW_HISTORY", "ID_BOM_BREAKDOWN");
            DropColumn("dbo.DETAIL_BOM_MT", "ID_BOM_BREAKDOWN");
            DropColumn("dbo.DETAIL_BOM_HW", "ID_BOM_BREAKDOWN");
        }
    }
}
