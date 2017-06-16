namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BomBreakdown_20170616_03 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DETAIL_BOM_HW", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN");
            DropForeignKey("dbo.DETAIL_BOM_MT", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN");
            DropForeignKey("dbo.DETAIL_BOM_HW_HISTORY", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN");
            DropForeignKey("dbo.DETAIL_BOM_MT_HISTORY", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN");
            DropIndex("dbo.DETAIL_BOM_HW", new[] { "ID_BOM_BREAKDOWN" });
            DropIndex("dbo.DETAIL_BOM_MT", new[] { "ID_BOM_BREAKDOWN" });
            DropIndex("dbo.DETAIL_BOM_HW_HISTORY", new[] { "ID_BOM_BREAKDOWN" });
            DropIndex("dbo.DETAIL_BOM_MT_HISTORY", new[] { "ID_BOM_BREAKDOWN" });
            DropPrimaryKey("dbo.DETAIL_BOM_HW");
            DropPrimaryKey("dbo.DETAIL_BOM_MT");
            DropPrimaryKey("dbo.DETAIL_BOM_HW_HISTORY");
            DropPrimaryKey("dbo.DETAIL_BOM_MT_HISTORY");
            AlterColumn("dbo.DETAIL_BOM_HW", "ID_BOM_BREAKDOWN", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.DETAIL_BOM_MT", "ID_BOM_BREAKDOWN", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.DETAIL_BOM_HW_HISTORY", "ID_BOM_BREAKDOWN", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.DETAIL_BOM_MT_HISTORY", "ID_BOM_BREAKDOWN", c => c.String(nullable: false, maxLength: 100));
            AddPrimaryKey("dbo.DETAIL_BOM_HW", new[] { "ID_BOM", "ID_ITEM_BCN", "ID_BOM_BREAKDOWN" });
            AddPrimaryKey("dbo.DETAIL_BOM_MT", new[] { "ID_BOM", "ID_ITEM_BCN", "ID_BOM_BREAKDOWN" });
            AddPrimaryKey("dbo.DETAIL_BOM_HW_HISTORY", new[] { "ID_BOM", "ID_ITEM_BCN", "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_BOM_BREAKDOWN" });
            AddPrimaryKey("dbo.DETAIL_BOM_MT_HISTORY", new[] { "ID_BOM", "ID_ITEM_BCN", "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_BOM_BREAKDOWN" });
            CreateIndex("dbo.DETAIL_BOM_HW", "ID_BOM_BREAKDOWN");
            CreateIndex("dbo.DETAIL_BOM_MT", "ID_BOM_BREAKDOWN");
            CreateIndex("dbo.DETAIL_BOM_HW_HISTORY", "ID_BOM_BREAKDOWN");
            CreateIndex("dbo.DETAIL_BOM_MT_HISTORY", "ID_BOM_BREAKDOWN");
            AddForeignKey("dbo.DETAIL_BOM_HW", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN", "ID_BOM_BREAKDOWN", cascadeDelete: false);
            AddForeignKey("dbo.DETAIL_BOM_MT", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN", "ID_BOM_BREAKDOWN", cascadeDelete: false);
            AddForeignKey("dbo.DETAIL_BOM_HW_HISTORY", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN", "ID_BOM_BREAKDOWN", cascadeDelete: false);
            AddForeignKey("dbo.DETAIL_BOM_MT_HISTORY", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN", "ID_BOM_BREAKDOWN", cascadeDelete: false);
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
            DropPrimaryKey("dbo.DETAIL_BOM_MT_HISTORY");
            DropPrimaryKey("dbo.DETAIL_BOM_HW_HISTORY");
            DropPrimaryKey("dbo.DETAIL_BOM_MT");
            DropPrimaryKey("dbo.DETAIL_BOM_HW");
            AlterColumn("dbo.DETAIL_BOM_MT_HISTORY", "ID_BOM_BREAKDOWN", c => c.String(maxLength: 100));
            AlterColumn("dbo.DETAIL_BOM_HW_HISTORY", "ID_BOM_BREAKDOWN", c => c.String(maxLength: 100));
            AlterColumn("dbo.DETAIL_BOM_MT", "ID_BOM_BREAKDOWN", c => c.String(maxLength: 100));
            AlterColumn("dbo.DETAIL_BOM_HW", "ID_BOM_BREAKDOWN", c => c.String(maxLength: 100));
            AddPrimaryKey("dbo.DETAIL_BOM_MT_HISTORY", new[] { "ID_BOM", "ID_ITEM_BCN", "ID_VER", "ID_SUBVER", "TIMESTAMP" });
            AddPrimaryKey("dbo.DETAIL_BOM_HW_HISTORY", new[] { "ID_BOM", "ID_ITEM_BCN", "ID_VER", "ID_SUBVER", "TIMESTAMP" });
            AddPrimaryKey("dbo.DETAIL_BOM_MT", new[] { "ID_BOM", "ID_ITEM_BCN" });
            AddPrimaryKey("dbo.DETAIL_BOM_HW", new[] { "ID_BOM", "ID_ITEM_BCN" });
            CreateIndex("dbo.DETAIL_BOM_MT_HISTORY", "ID_BOM_BREAKDOWN");
            CreateIndex("dbo.DETAIL_BOM_HW_HISTORY", "ID_BOM_BREAKDOWN");
            CreateIndex("dbo.DETAIL_BOM_MT", "ID_BOM_BREAKDOWN");
            CreateIndex("dbo.DETAIL_BOM_HW", "ID_BOM_BREAKDOWN");
            AddForeignKey("dbo.DETAIL_BOM_MT_HISTORY", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN", "ID_BOM_BREAKDOWN");
            AddForeignKey("dbo.DETAIL_BOM_HW_HISTORY", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN", "ID_BOM_BREAKDOWN");
            AddForeignKey("dbo.DETAIL_BOM_MT", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN", "ID_BOM_BREAKDOWN");
            AddForeignKey("dbo.DETAIL_BOM_HW", "ID_BOM_BREAKDOWN", "dbo.BOM_BREAKDOWN", "ID_BOM_BREAKDOWN");
        }
    }
}
