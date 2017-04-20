namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PricesList_precision_1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CUSTOMERS_PRICE_LIST", "PRICE", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.CUSTOMERS_PRICE_LIST", "PRICE_BASE_CURRENCY", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.CUSTOMERS_PRICE_LIST", "EXCHANGE_RATE_USED", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.CUSTOMER_PRICE_LIST_HISTORY", "PRICE", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.CUSTOMER_PRICE_LIST_HISTORY", "PRICE_BASE_CURRENCY", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.CUSTOMER_PRICE_LIST_HISTORY", "EXCHANGE_RATE_USED", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST", "PRICE", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST", "PRICE_BASE_CURRENCY", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST", "EXCHANGE_RATE_USED", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST_HISTORY", "PRICE", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST_HISTORY", "PRICE_BASE_CURRENCY", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST_HISTORY", "EXCHANGE_RATE_USED", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST_HISTORY", "EXCHANGE_RATE_USED", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST_HISTORY", "PRICE_BASE_CURRENCY", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST_HISTORY", "PRICE", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST", "EXCHANGE_RATE_USED", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST", "PRICE_BASE_CURRENCY", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AlterColumn("dbo.SUPPLIERS_PRICE_LIST", "PRICE", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AlterColumn("dbo.CUSTOMER_PRICE_LIST_HISTORY", "EXCHANGE_RATE_USED", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AlterColumn("dbo.CUSTOMER_PRICE_LIST_HISTORY", "PRICE_BASE_CURRENCY", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AlterColumn("dbo.CUSTOMER_PRICE_LIST_HISTORY", "PRICE", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AlterColumn("dbo.CUSTOMERS_PRICE_LIST", "EXCHANGE_RATE_USED", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AlterColumn("dbo.CUSTOMERS_PRICE_LIST", "PRICE_BASE_CURRENCY", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AlterColumn("dbo.CUSTOMERS_PRICE_LIST", "PRICE", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
        }
    }
}
