namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocType_ItemMtHw_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DOCS_TYPES", "ID_ITEM_GROUP", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HW", "ID_COLOR_1", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HW", "ID_COLOR_2", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HW_HISTORY", "ID_COLOR_1", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HW_HISTORY", "ID_COLOR_2", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_MT", "ID_COLOR_1", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_MT", "ID_COLOR_2", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_MT_HISTORY", "ID_COLOR_1", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_MT_HISTORY", "ID_COLOR_2", c => c.String(maxLength: 30));
            CreateIndex("dbo.DOCS_TYPES", "ID_ITEM_GROUP");
            CreateIndex("dbo.ITEMS_HW", "ID_COLOR_1");
            CreateIndex("dbo.ITEMS_HW", "ID_COLOR_2");
            CreateIndex("dbo.ITEMS_MT", "ID_COLOR_1");
            CreateIndex("dbo.ITEMS_MT", "ID_COLOR_2");
            AddForeignKey("dbo.DOCS_TYPES", "ID_ITEM_GROUP", "dbo.ITEM_GROUP", "ID_ITEM_GROUP");
            AddForeignKey("dbo.ITEMS_HW", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS_HW", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS_MT", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS_MT", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ITEMS_MT", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_MT", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HW", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HW", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.DOCS_TYPES", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropIndex("dbo.ITEMS_MT", new[] { "ID_COLOR_2" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_COLOR_1" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_COLOR_2" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_COLOR_1" });
            DropIndex("dbo.DOCS_TYPES", new[] { "ID_ITEM_GROUP" });
            DropColumn("dbo.ITEMS_MT_HISTORY", "ID_COLOR_2");
            DropColumn("dbo.ITEMS_MT_HISTORY", "ID_COLOR_1");
            DropColumn("dbo.ITEMS_MT", "ID_COLOR_2");
            DropColumn("dbo.ITEMS_MT", "ID_COLOR_1");
            DropColumn("dbo.ITEMS_HW_HISTORY", "ID_COLOR_2");
            DropColumn("dbo.ITEMS_HW_HISTORY", "ID_COLOR_1");
            DropColumn("dbo.ITEMS_HW", "ID_COLOR_2");
            DropColumn("dbo.ITEMS_HW", "ID_COLOR_1");
            DropColumn("dbo.DOCS_TYPES", "ID_ITEM_GROUP");
        }
    }
}
