namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupplyDocs_20170725_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DOC_HEAD",
                c => new
                    {
                        ID_DOC = c.String(nullable: false, maxLength: 50),
                        ID_SUPPLY_DOC_TYPE = c.String(nullable: false, maxLength: 100),
                        CREATION_DATE = c.DateTime(nullable: false),
                        DELIVERY_DATE = c.DateTime(nullable: false),
                        DOC_DATE = c.DateTime(nullable: false),
                        ID_SUPPLY_STATUS = c.String(maxLength: 100),
                        ID_SUPPLIER = c.String(maxLength: 100),
                        ID_CUSTOMER = c.String(maxLength: 100),
                        ID_DELIVERY_TERM = c.String(maxLength: 5),
                        ID_PAYMENT_TERMS = c.String(maxLength: 4),
                        ID_CURRENCY = c.String(maxLength: 4),
                    })
                .PrimaryKey(t => t.ID_DOC)
                .ForeignKey("dbo.CURRENCIES", t => t.ID_CURRENCY)
                .ForeignKey("dbo.CUSTOMERS", t => t.ID_CUSTOMER)
                .ForeignKey("dbo.DELIVERY_TERMS", t => t.ID_DELIVERY_TERM)
                .ForeignKey("dbo.PAYMENT_TERMS", t => t.ID_PAYMENT_TERMS)
                .ForeignKey("dbo.SUPPLIERS", t => t.ID_SUPPLIER)
                .ForeignKey("dbo.SUPPLY_DOC_TYPE", t => t.ID_SUPPLY_DOC_TYPE, cascadeDelete: false)
                .ForeignKey("dbo.SUPPLY_STATUS", t => t.ID_SUPPLY_STATUS)
                .Index(t => t.ID_SUPPLY_DOC_TYPE)
                .Index(t => t.ID_SUPPLY_STATUS)
                .Index(t => t.ID_SUPPLIER)
                .Index(t => t.ID_CUSTOMER)
                .Index(t => t.ID_DELIVERY_TERM)
                .Index(t => t.ID_PAYMENT_TERMS)
                .Index(t => t.ID_CURRENCY);
            
            CreateTable(
                "dbo.DOC_LINES",
                c => new
                    {
                        ID_DOC = c.String(nullable: false, maxLength: 50),
                        NUM_LIN = c.Int(nullable: false),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 50),
                        ID_ITEM_GROUP = c.String(nullable: false, maxLength: 100),
                        ID_SUPPLY_STATUS = c.String(maxLength: 100),
                        BATCH = c.String(maxLength: 50),
                        QUANTITY = c.Int(nullable: false),
                        QUANTITY_ORIGINAL = c.Int(nullable: false),
                        DELIVERED_QUANTITY = c.Int(nullable: false),
                        REMARKS = c.String(maxLength: 4000),
                        UNIT_PRICE = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        UNIT_PRICE_BASE_CURRENCY = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.ID_DOC, t.NUM_LIN })
                .ForeignKey("dbo.ITEM_GROUP", t => t.ID_ITEM_GROUP, cascadeDelete: false)
                .ForeignKey("dbo.SUPPLY_STATUS", t => t.ID_SUPPLY_STATUS)
                .ForeignKey("dbo.DOC_HEAD", t => t.ID_DOC, cascadeDelete: false)
                .Index(t => t.ID_DOC)
                .Index(t => t.ID_ITEM_GROUP)
                .Index(t => t.ID_SUPPLY_STATUS);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DOC_HEAD", "ID_SUPPLY_STATUS", "dbo.SUPPLY_STATUS");
            DropForeignKey("dbo.DOC_HEAD", "ID_SUPPLY_DOC_TYPE", "dbo.SUPPLY_DOC_TYPE");
            DropForeignKey("dbo.DOC_HEAD", "ID_SUPPLIER", "dbo.SUPPLIERS");
            DropForeignKey("dbo.DOC_HEAD", "ID_PAYMENT_TERMS", "dbo.PAYMENT_TERMS");
            DropForeignKey("dbo.DOC_LINES", "ID_DOC", "dbo.DOC_HEAD");
            DropForeignKey("dbo.DOC_LINES", "ID_SUPPLY_STATUS", "dbo.SUPPLY_STATUS");
            DropForeignKey("dbo.DOC_LINES", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropForeignKey("dbo.DOC_HEAD", "ID_DELIVERY_TERM", "dbo.DELIVERY_TERMS");
            DropForeignKey("dbo.DOC_HEAD", "ID_CUSTOMER", "dbo.CUSTOMERS");
            DropForeignKey("dbo.DOC_HEAD", "ID_CURRENCY", "dbo.CURRENCIES");
            DropIndex("dbo.DOC_LINES", new[] { "ID_SUPPLY_STATUS" });
            DropIndex("dbo.DOC_LINES", new[] { "ID_ITEM_GROUP" });
            DropIndex("dbo.DOC_LINES", new[] { "ID_DOC" });
            DropIndex("dbo.DOC_HEAD", new[] { "ID_CURRENCY" });
            DropIndex("dbo.DOC_HEAD", new[] { "ID_PAYMENT_TERMS" });
            DropIndex("dbo.DOC_HEAD", new[] { "ID_DELIVERY_TERM" });
            DropIndex("dbo.DOC_HEAD", new[] { "ID_CUSTOMER" });
            DropIndex("dbo.DOC_HEAD", new[] { "ID_SUPPLIER" });
            DropIndex("dbo.DOC_HEAD", new[] { "ID_SUPPLY_STATUS" });
            DropIndex("dbo.DOC_HEAD", new[] { "ID_SUPPLY_DOC_TYPE" });
            DropTable("dbo.DOC_LINES");
            DropTable("dbo.DOC_HEAD");
        }
    }
}
