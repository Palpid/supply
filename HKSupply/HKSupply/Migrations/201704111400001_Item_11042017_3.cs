namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Item_11042017_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "BILLING_ADDRESS", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "CONTACT_NAME", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS", "CONTACT_NAME_ZH", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS", "CONTACT_PHONE", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS", "COMMENTS", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "ID_INCOTERM", c => c.String(maxLength: 8));
            AddColumn("dbo.SUPPLIERS", "ID_PAYMENT_TERMS", c => c.String(maxLength: 4));
            AddColumn("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY", c => c.String(maxLength: 4));
            AddColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_NAME", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_NAME_ZH", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_PHONE", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS_HISTORY", "COMMENTS", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "ID_INCOTERM", c => c.String(maxLength: 8));
            AddColumn("dbo.SUPPLIERS_HISTORY", "ID_PAYMENT_TERMS", c => c.String(maxLength: 4));
            AddColumn("dbo.SUPPLIERS_HISTORY", "ID_DEFAULT_CURRENCY", c => c.String(maxLength: 4));
            CreateIndex("dbo.SUPPLIERS", "ID_INCOTERM");
            CreateIndex("dbo.SUPPLIERS", "ID_PAYMENT_TERMS");
            CreateIndex("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY");
            AddForeignKey("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY", "dbo.CURRENCIES", "ID_CURRENCY");
            AddForeignKey("dbo.SUPPLIERS", "ID_INCOTERM", "dbo.INCOTERMS", "ID_INCOTERM");
            AddForeignKey("dbo.SUPPLIERS", "ID_PAYMENT_TERMS", "dbo.PAYMENT_TERMS", "ID_PAYMENT_TERMS");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SUPPLIERS", "ID_PAYMENT_TERMS", "dbo.PAYMENT_TERMS");
            DropForeignKey("dbo.SUPPLIERS", "ID_INCOTERM", "dbo.INCOTERMS");
            DropForeignKey("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY", "dbo.CURRENCIES");
            DropIndex("dbo.SUPPLIERS", new[] { "ID_DEFAULT_CURRENCY" });
            DropIndex("dbo.SUPPLIERS", new[] { "ID_PAYMENT_TERMS" });
            DropIndex("dbo.SUPPLIERS", new[] { "ID_INCOTERM" });
            DropColumn("dbo.SUPPLIERS_HISTORY", "ID_DEFAULT_CURRENCY");
            DropColumn("dbo.SUPPLIERS_HISTORY", "ID_PAYMENT_TERMS");
            DropColumn("dbo.SUPPLIERS_HISTORY", "ID_INCOTERM");
            DropColumn("dbo.SUPPLIERS_HISTORY", "COMMENTS");
            DropColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_PHONE");
            DropColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_NAME_ZH");
            DropColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_NAME");
            DropColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_ZH");
            DropColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS");
            DropColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_ZH");
            DropColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS");
            DropColumn("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY");
            DropColumn("dbo.SUPPLIERS", "ID_PAYMENT_TERMS");
            DropColumn("dbo.SUPPLIERS", "ID_INCOTERM");
            DropColumn("dbo.SUPPLIERS", "COMMENTS");
            DropColumn("dbo.SUPPLIERS", "CONTACT_PHONE");
            DropColumn("dbo.SUPPLIERS", "CONTACT_NAME_ZH");
            DropColumn("dbo.SUPPLIERS", "CONTACT_NAME");
            DropColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_ZH");
            DropColumn("dbo.SUPPLIERS", "BILLING_ADDRESS");
            DropColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_ZH");
            DropColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS");
        }
    }
}
