namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemBcn_PricesList_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ITEMS_BCN",
                c => new
                {
                    ID_ITEM_BCN = c.String(nullable: false, maxLength: 20),
                    DESCRIPTION = c.String(maxLength: 500),
                })
                .PrimaryKey(t => t.ID_ITEM_BCN);

            CreateTable(
                "dbo.CUSTOMERS_PRICE_LIST",
                c => new
                    {
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 20),
                        ID_CUSTOMER = c.String(nullable: false, maxLength: 100),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        PRICE = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        COMMENTS = c.String(maxLength: 2500),
                        ID_CURRENCY = c.String(maxLength: 4),
                        PRICE_BASE_CURRENCY = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        EXCHANGE_RATE_USED = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        MIN_LOT = c.Int(nullable: false),
                        INCR_LOT = c.Int(nullable: false),
                        LEAD_TIME = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_ITEM_BCN, t.ID_CUSTOMER })
                .ForeignKey("dbo.CURRENCIES", t => t.ID_CURRENCY)
                .ForeignKey("dbo.CUSTOMERS", t => t.ID_CUSTOMER, cascadeDelete: true)
                .ForeignKey("dbo.ITEMS_BCN", t => t.ID_ITEM_BCN, cascadeDelete: true)
                .Index(t => t.ID_ITEM_BCN)
                .Index(t => t.ID_CUSTOMER)
                .Index(t => t.ID_CURRENCY);
           
            CreateTable(
                "dbo.CUSTOMER_PRICE_LIST_HISTORY",
                c => new
                    {
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 20),
                        ID_CUSTOMER = c.String(nullable: false, maxLength: 100),
                        PRICE = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        COMMENTS = c.String(maxLength: 2500),
                        ID_CURRENCY = c.String(maxLength: 4),
                        PRICE_BASE_CURRENCY = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        EXCHANGE_RATE_USED = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        MIN_LOT = c.Int(nullable: false),
                        INCR_LOT = c.Int(nullable: false),
                        LEAD_TIME = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_VER, t.ID_SUBVER, t.TIMESTAMP, t.ID_ITEM_BCN, t.ID_CUSTOMER });
            
            CreateTable(
                "dbo.SUPPLIERS_PRICE_LIST",
                c => new
                    {
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 20),
                        ID_SUPPLIER = c.String(nullable: false, maxLength: 100),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        PRICE = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        COMMENTS = c.String(maxLength: 2500),
                        ID_CURRENCY = c.String(maxLength: 4),
                        PRICE_BASE_CURRENCY = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        EXCHANGE_RATE_USED = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        MIN_LOT = c.Int(nullable: false),
                        INCR_LOT = c.Int(nullable: false),
                        LEAD_TIME = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_ITEM_BCN, t.ID_SUPPLIER })
                .ForeignKey("dbo.CURRENCIES", t => t.ID_CURRENCY)
                .ForeignKey("dbo.ITEMS_BCN", t => t.ID_ITEM_BCN, cascadeDelete: true)
                .ForeignKey("dbo.SUPPLIERS", t => t.ID_SUPPLIER, cascadeDelete: true)
                .Index(t => t.ID_ITEM_BCN)
                .Index(t => t.ID_SUPPLIER)
                .Index(t => t.ID_CURRENCY);
            
            CreateTable(
                "dbo.SUPPLIERS_PRICE_LIST_HISTORY",
                c => new
                    {
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 20),
                        ID_SUPPLIER = c.String(nullable: false, maxLength: 100),
                        PRICE = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        COMMENTS = c.String(maxLength: 2500),
                        ID_CURRENCY = c.String(maxLength: 4),
                        PRICE_BASE_CURRENCY = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        EXCHANGE_RATE_USED = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        MIN_LOT = c.Int(nullable: false),
                        INCR_LOT = c.Int(nullable: false),
                        LEAD_TIME = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_VER, t.ID_SUBVER, t.TIMESTAMP, t.ID_ITEM_BCN, t.ID_SUPPLIER });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SUPPLIERS_PRICE_LIST", "ID_SUPPLIER", "dbo.SUPPLIERS");
            DropForeignKey("dbo.SUPPLIERS_PRICE_LIST", "ID_ITEM_BCN", "dbo.ITEMS_BCN");
            DropForeignKey("dbo.SUPPLIERS_PRICE_LIST", "ID_CURRENCY", "dbo.CURRENCIES");
            DropForeignKey("dbo.CUSTOMERS_PRICE_LIST", "ID_ITEM_BCN", "dbo.ITEMS_BCN");
            DropForeignKey("dbo.CUSTOMERS_PRICE_LIST", "ID_CUSTOMER", "dbo.CUSTOMERS");
            DropForeignKey("dbo.CUSTOMERS_PRICE_LIST", "ID_CURRENCY", "dbo.CURRENCIES");
            DropTable("dbo.ITEMS_BCN");
            DropIndex("dbo.SUPPLIERS_PRICE_LIST", new[] { "ID_CURRENCY" });
            DropIndex("dbo.SUPPLIERS_PRICE_LIST", new[] { "ID_SUPPLIER" });
            DropIndex("dbo.SUPPLIERS_PRICE_LIST", new[] { "ID_ITEM_BCN" });
            DropIndex("dbo.CUSTOMERS_PRICE_LIST", new[] { "ID_CURRENCY" });
            DropIndex("dbo.CUSTOMERS_PRICE_LIST", new[] { "ID_CUSTOMER" });
            DropIndex("dbo.CUSTOMERS_PRICE_LIST", new[] { "ID_ITEM_BCN" });
            DropTable("dbo.SUPPLIERS_PRICE_LIST_HISTORY");
            DropTable("dbo.SUPPLIERS_PRICE_LIST");
            DropTable("dbo.CUSTOMER_PRICE_LIST_HISTORY");
            DropTable("dbo.CUSTOMERS_PRICE_LIST");
        }
    }
}
