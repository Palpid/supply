namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bom_item_supplier_20170525_01 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ITEMS_BOM", "IX_VER_ITEM");
            AddColumn("dbo.ITEMS_BOM", "ID_SUPPLIER", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.ITEMS_BOM", new[] { "ID_VER", "ID_SUBVER", "ID_ITEM_BCN", "ID_SUPPLIER" }, unique: true, name: "IX_VER_ITEM");
            AddForeignKey("dbo.ITEMS_BOM", "ID_SUPPLIER", "dbo.SUPPLIERS", "ID_SUPPLIER", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ITEMS_BOM", "ID_SUPPLIER", "dbo.SUPPLIERS");
            DropIndex("dbo.ITEMS_BOM", "IX_VER_ITEM");
            DropColumn("dbo.ITEMS_BOM", "ID_SUPPLIER");
            CreateIndex("dbo.ITEMS_BOM", new[] { "ID_VER", "ID_SUBVER", "ID_ITEM_BCN" }, unique: true, name: "IX_VER_ITEM");
        }
    }
}
