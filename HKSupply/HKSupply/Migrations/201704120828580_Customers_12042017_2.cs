namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customers_12042017_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CUSTOMERS", "SHIPING_ADDRESS", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS", "SHIPING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS", "BILLING_ADDRESS", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS", "BILLING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS", "CONTACT_NAME", c => c.String(maxLength: 100));
            AddColumn("dbo.CUSTOMERS", "CONTACT_NAME_ZH", c => c.String(maxLength: 100));
            AddColumn("dbo.CUSTOMERS", "CONTACT_PHONE", c => c.String(maxLength: 100));
            AddColumn("dbo.CUSTOMERS", "COMMENTS", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS", "ID_INCOTERM", c => c.String(maxLength: 8));
            AddColumn("dbo.CUSTOMERS", "ID_PAYMENT_TERMS", c => c.String(maxLength: 4));
            AddColumn("dbo.CUSTOMERS", "ID_DEFAULT_CURRENCY", c => c.String(maxLength: 4));
            AddColumn("dbo.CUSTOMERS_HISTORY", "SHIPING_ADDRESS", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS_HISTORY", "SHIPING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS_HISTORY", "BILLING_ADDRESS", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS_HISTORY", "BILLING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS_HISTORY", "CONTACT_NAME", c => c.String(maxLength: 100));
            AddColumn("dbo.CUSTOMERS_HISTORY", "CONTACT_NAME_ZH", c => c.String(maxLength: 100));
            AddColumn("dbo.CUSTOMERS_HISTORY", "CONTACT_PHONE", c => c.String(maxLength: 100));
            AddColumn("dbo.CUSTOMERS_HISTORY", "COMMENTS", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS_HISTORY", "ID_INCOTERM", c => c.String(maxLength: 8));
            AddColumn("dbo.CUSTOMERS_HISTORY", "ID_PAYMENT_TERMS", c => c.String(maxLength: 4));
            AddColumn("dbo.CUSTOMERS_HISTORY", "ID_DEFAULT_CURRENCY", c => c.String(maxLength: 4));
            CreateIndex("dbo.CUSTOMERS", "ID_INCOTERM");
            CreateIndex("dbo.CUSTOMERS", "ID_PAYMENT_TERMS");
            CreateIndex("dbo.CUSTOMERS", "ID_DEFAULT_CURRENCY");
            AddForeignKey("dbo.CUSTOMERS", "ID_DEFAULT_CURRENCY", "dbo.CURRENCIES", "ID_CURRENCY");
            AddForeignKey("dbo.CUSTOMERS", "ID_INCOTERM", "dbo.INCOTERMS", "ID_INCOTERM");
            AddForeignKey("dbo.CUSTOMERS", "ID_PAYMENT_TERMS", "dbo.PAYMENT_TERMS", "ID_PAYMENT_TERMS");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CUSTOMERS", "ID_PAYMENT_TERMS", "dbo.PAYMENT_TERMS");
            DropForeignKey("dbo.CUSTOMERS", "ID_INCOTERM", "dbo.INCOTERMS");
            DropForeignKey("dbo.CUSTOMERS", "ID_DEFAULT_CURRENCY", "dbo.CURRENCIES");
            DropIndex("dbo.CUSTOMERS", new[] { "ID_DEFAULT_CURRENCY" });
            DropIndex("dbo.CUSTOMERS", new[] { "ID_PAYMENT_TERMS" });
            DropIndex("dbo.CUSTOMERS", new[] { "ID_INCOTERM" });
            DropColumn("dbo.CUSTOMERS_HISTORY", "ID_DEFAULT_CURRENCY");
            DropColumn("dbo.CUSTOMERS_HISTORY", "ID_PAYMENT_TERMS");
            DropColumn("dbo.CUSTOMERS_HISTORY", "ID_INCOTERM");
            DropColumn("dbo.CUSTOMERS_HISTORY", "COMMENTS");
            DropColumn("dbo.CUSTOMERS_HISTORY", "CONTACT_PHONE");
            DropColumn("dbo.CUSTOMERS_HISTORY", "CONTACT_NAME_ZH");
            DropColumn("dbo.CUSTOMERS_HISTORY", "CONTACT_NAME");
            DropColumn("dbo.CUSTOMERS_HISTORY", "BILLING_ADDRESS_ZH");
            DropColumn("dbo.CUSTOMERS_HISTORY", "BILLING_ADDRESS");
            DropColumn("dbo.CUSTOMERS_HISTORY", "SHIPING_ADDRESS_ZH");
            DropColumn("dbo.CUSTOMERS_HISTORY", "SHIPING_ADDRESS");
            DropColumn("dbo.CUSTOMERS", "ID_DEFAULT_CURRENCY");
            DropColumn("dbo.CUSTOMERS", "ID_PAYMENT_TERMS");
            DropColumn("dbo.CUSTOMERS", "ID_INCOTERM");
            DropColumn("dbo.CUSTOMERS", "COMMENTS");
            DropColumn("dbo.CUSTOMERS", "CONTACT_PHONE");
            DropColumn("dbo.CUSTOMERS", "CONTACT_NAME_ZH");
            DropColumn("dbo.CUSTOMERS", "CONTACT_NAME");
            DropColumn("dbo.CUSTOMERS", "BILLING_ADDRESS_ZH");
            DropColumn("dbo.CUSTOMERS", "BILLING_ADDRESS");
            DropColumn("dbo.CUSTOMERS", "SHIPING_ADDRESS_ZH");
            DropColumn("dbo.CUSTOMERS", "SHIPING_ADDRESS");
        }
    }
}
