namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnitsOnPriceListTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CUSTOMERS_PRICE_LIST", "UNIT_CODE", c => c.String(maxLength: 2));
            AddColumn("dbo.CUSTOMER_PRICE_LIST_HISTORY", "UNIT_CODE", c => c.String(maxLength: 2));
            AddColumn("dbo.SUPPLIERS_PRICE_LIST", "UNIT_CODE", c => c.String(maxLength: 2));
            AddColumn("dbo.SUPPLIERS_PRICE_LIST_HISTORY", "UNIT_CODE", c => c.String(maxLength: 2));
            CreateIndex("dbo.CUSTOMERS_PRICE_LIST", "UNIT_CODE");
            CreateIndex("dbo.SUPPLIERS_PRICE_LIST", "UNIT_CODE");
            AddForeignKey("dbo.CUSTOMERS_PRICE_LIST", "UNIT_CODE", "dbo.UNITS", "UNIT_CODE");
            AddForeignKey("dbo.SUPPLIERS_PRICE_LIST", "UNIT_CODE", "dbo.UNITS", "UNIT_CODE");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SUPPLIERS_PRICE_LIST", "UNIT_CODE", "dbo.UNITS");
            DropForeignKey("dbo.CUSTOMERS_PRICE_LIST", "UNIT_CODE", "dbo.UNITS");
            DropIndex("dbo.SUPPLIERS_PRICE_LIST", new[] { "UNIT_CODE" });
            DropIndex("dbo.CUSTOMERS_PRICE_LIST", new[] { "UNIT_CODE" });
            DropColumn("dbo.SUPPLIERS_PRICE_LIST_HISTORY", "UNIT_CODE");
            DropColumn("dbo.SUPPLIERS_PRICE_LIST", "UNIT_CODE");
            DropColumn("dbo.CUSTOMER_PRICE_LIST_HISTORY", "UNIT_CODE");
            DropColumn("dbo.CUSTOMERS_PRICE_LIST", "UNIT_CODE");
        }
    }
}
