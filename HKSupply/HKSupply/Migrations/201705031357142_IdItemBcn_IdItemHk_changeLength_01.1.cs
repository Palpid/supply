namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdItemBcn_IdItemHk_changeLength_011 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CUSTOMERS_PRICE_LIST", "ID_ITEM_BCN", "dbo.ITEMS_BCN");
            DropForeignKey("dbo.SUPPLIERS_PRICE_LIST", "ID_ITEM_BCN", "dbo.ITEMS_BCN");
            DropIndex("dbo.CUSTOMERS_PRICE_LIST", new[] { "ID_ITEM_BCN" });
            DropIndex("dbo.SUPPLIERS_PRICE_LIST", new[] { "ID_ITEM_BCN" });
            DropPrimaryKey("dbo.CUSTOMERS_PRICE_LIST");
            DropPrimaryKey("dbo.ITEMS_BCN");
            DropPrimaryKey("dbo.CUSTOMER_PRICE_LIST_HISTORY");
            DropPrimaryKey("dbo.ITEMS_EY");
            DropPrimaryKey("dbo.ITEMS_EY_HISTORY");
            DropPrimaryKey("dbo.ITEMS_HW");
            DropPrimaryKey("dbo.ITEMS_HW_HISTORY");
            DropPrimaryKey("dbo.ITEMS_MT");
            DropPrimaryKey("dbo.ITEMS_MT_HISTORY");
            DropPrimaryKey("dbo.SUPPLIERS_PRICE_LIST");
            DropPrimaryKey("dbo.SUPPLIERS_PRICE_LIST_HISTORY");
            AlterColumn("dbo.CUSTOMERS_PRICE_LIST", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ITEMS_BCN", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CUSTOMER_PRICE_LIST_HISTORY", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ITEMS_EY", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ITEMS_EY", "ID_ITEM_HK", c => c.String(maxLength: 50));
            AlterColumn("dbo.ITEMS_EY_HISTORY", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ITEMS_EY_HISTORY", "ID_ITEM_HK", c => c.String(maxLength: 50));
            AlterColumn("dbo.ITEMS_HW", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ITEMS_HW", "ID_ITEM_HK", c => c.String(maxLength: 50));
            AlterColumn("dbo.ITEMS_HW_HISTORY", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ITEMS_HW_HISTORY", "ID_ITEM_HK", c => c.String(maxLength: 50));
            AlterColumn("dbo.ITEMS_MT", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ITEMS_MT", "ID_ITEM_HK", c => c.String(maxLength: 50));
            AlterColumn("dbo.ITEMS_MT_HISTORY", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ITEMS_MT_HISTORY", "ID_ITEM_HK", c => c.String(maxLength: 50));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST_HISTORY", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.CUSTOMERS_PRICE_LIST", new[] { "ID_ITEM_BCN", "ID_CUSTOMER" });
            AddPrimaryKey("dbo.ITEMS_BCN", "ID_ITEM_BCN");
            AddPrimaryKey("dbo.CUSTOMER_PRICE_LIST_HISTORY", new[] { "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_ITEM_BCN", "ID_CUSTOMER" });
            AddPrimaryKey("dbo.ITEMS_EY", "ID_ITEM_BCN");
            AddPrimaryKey("dbo.ITEMS_EY_HISTORY", new[] { "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_ITEM_BCN" });
            AddPrimaryKey("dbo.ITEMS_HW", "ID_ITEM_BCN");
            AddPrimaryKey("dbo.ITEMS_HW_HISTORY", new[] { "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_ITEM_BCN" });
            AddPrimaryKey("dbo.ITEMS_MT", "ID_ITEM_BCN");
            AddPrimaryKey("dbo.ITEMS_MT_HISTORY", new[] { "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_ITEM_BCN" });
            AddPrimaryKey("dbo.SUPPLIERS_PRICE_LIST", new[] { "ID_ITEM_BCN", "ID_SUPPLIER" });
            AddPrimaryKey("dbo.SUPPLIERS_PRICE_LIST_HISTORY", new[] { "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_ITEM_BCN", "ID_SUPPLIER" });
            CreateIndex("dbo.CUSTOMERS_PRICE_LIST", "ID_ITEM_BCN");
            CreateIndex("dbo.SUPPLIERS_PRICE_LIST", "ID_ITEM_BCN");
            AddForeignKey("dbo.CUSTOMERS_PRICE_LIST", "ID_ITEM_BCN", "dbo.ITEMS_BCN", "ID_ITEM_BCN", cascadeDelete: false);
            AddForeignKey("dbo.SUPPLIERS_PRICE_LIST", "ID_ITEM_BCN", "dbo.ITEMS_BCN", "ID_ITEM_BCN", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SUPPLIERS_PRICE_LIST", "ID_ITEM_BCN", "dbo.ITEMS_BCN");
            DropForeignKey("dbo.CUSTOMERS_PRICE_LIST", "ID_ITEM_BCN", "dbo.ITEMS_BCN");
            DropIndex("dbo.SUPPLIERS_PRICE_LIST", new[] { "ID_ITEM_BCN" });
            DropIndex("dbo.CUSTOMERS_PRICE_LIST", new[] { "ID_ITEM_BCN" });
            DropPrimaryKey("dbo.SUPPLIERS_PRICE_LIST_HISTORY");
            DropPrimaryKey("dbo.SUPPLIERS_PRICE_LIST");
            DropPrimaryKey("dbo.ITEMS_MT_HISTORY");
            DropPrimaryKey("dbo.ITEMS_MT");
            DropPrimaryKey("dbo.ITEMS_HW_HISTORY");
            DropPrimaryKey("dbo.ITEMS_HW");
            DropPrimaryKey("dbo.ITEMS_EY_HISTORY");
            DropPrimaryKey("dbo.ITEMS_EY");
            DropPrimaryKey("dbo.CUSTOMER_PRICE_LIST_HISTORY");
            DropPrimaryKey("dbo.ITEMS_BCN");
            DropPrimaryKey("dbo.CUSTOMERS_PRICE_LIST");
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST_HISTORY", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ITEMS_MT_HISTORY", "ID_ITEM_HK", c => c.String(maxLength: 20));
            AlterColumn("dbo.ITEMS_MT_HISTORY", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ITEMS_MT", "ID_ITEM_HK", c => c.String(maxLength: 20));
            AlterColumn("dbo.ITEMS_MT", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ITEMS_HW_HISTORY", "ID_ITEM_HK", c => c.String(maxLength: 20));
            AlterColumn("dbo.ITEMS_HW_HISTORY", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ITEMS_HW", "ID_ITEM_HK", c => c.String(maxLength: 20));
            AlterColumn("dbo.ITEMS_HW", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ITEMS_EY_HISTORY", "ID_ITEM_HK", c => c.String(maxLength: 20));
            AlterColumn("dbo.ITEMS_EY_HISTORY", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ITEMS_EY", "ID_ITEM_HK", c => c.String(maxLength: 20));
            AlterColumn("dbo.ITEMS_EY", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CUSTOMER_PRICE_LIST_HISTORY", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ITEMS_BCN", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CUSTOMERS_PRICE_LIST", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 20));
            AddPrimaryKey("dbo.SUPPLIERS_PRICE_LIST_HISTORY", new[] { "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_ITEM_BCN", "ID_SUPPLIER" });
            AddPrimaryKey("dbo.SUPPLIERS_PRICE_LIST", new[] { "ID_ITEM_BCN", "ID_SUPPLIER" });
            AddPrimaryKey("dbo.ITEMS_MT_HISTORY", new[] { "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_ITEM_BCN" });
            AddPrimaryKey("dbo.ITEMS_MT", "ID_ITEM_BCN");
            AddPrimaryKey("dbo.ITEMS_HW_HISTORY", new[] { "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_ITEM_BCN" });
            AddPrimaryKey("dbo.ITEMS_HW", "ID_ITEM_BCN");
            AddPrimaryKey("dbo.ITEMS_EY_HISTORY", new[] { "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_ITEM_BCN" });
            AddPrimaryKey("dbo.ITEMS_EY", "ID_ITEM_BCN");
            AddPrimaryKey("dbo.CUSTOMER_PRICE_LIST_HISTORY", new[] { "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_ITEM_BCN", "ID_CUSTOMER" });
            AddPrimaryKey("dbo.ITEMS_BCN", "ID_ITEM_BCN");
            AddPrimaryKey("dbo.CUSTOMERS_PRICE_LIST", new[] { "ID_ITEM_BCN", "ID_CUSTOMER" });
            CreateIndex("dbo.SUPPLIERS_PRICE_LIST", "ID_ITEM_BCN");
            CreateIndex("dbo.CUSTOMERS_PRICE_LIST", "ID_ITEM_BCN");
            AddForeignKey("dbo.SUPPLIERS_PRICE_LIST", "ID_ITEM_BCN", "dbo.ITEMS_BCN", "ID_ITEM_BCN", cascadeDelete: false);
            AddForeignKey("dbo.CUSTOMERS_PRICE_LIST", "ID_ITEM_BCN", "dbo.ITEMS_BCN", "ID_ITEM_BCN", cascadeDelete: false);
        }
    }
}
