namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Item_11042017_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY", "dbo.CURRENCIES");
            DropForeignKey("dbo.SUPPLIERS", "ID_INCOTERM_", "dbo.INCOTERMS");
            DropForeignKey("dbo.SUPPLIERS", "ID_PAYMENT_TERMS", "dbo.PAYMENT_TERMS");
            DropIndex("dbo.SUPPLIERS", new[] { "ID_INCOTERM_" });
            DropIndex("dbo.SUPPLIERS", new[] { "ID_PAYMENT_TERMS" });
            DropIndex("dbo.SUPPLIERS", new[] { "ID_DEFAULT_CURRENCY" });
            DropColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_");
            DropColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_ZH");
            DropColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_");
            DropColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_ZH");
            DropColumn("dbo.SUPPLIERS", "CONTACT_NAME_");
            DropColumn("dbo.SUPPLIERS", "CONTACT_NAME_ZH");
            DropColumn("dbo.SUPPLIERS", "CONTACT_PHONE_");
            DropColumn("dbo.SUPPLIERS", "COMMENTS");
            DropColumn("dbo.SUPPLIERS", "ID_INCOTERM_");
            DropColumn("dbo.SUPPLIERS", "ID_PAYMENT_TERMS");
            DropColumn("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY");
            DropColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_");
            DropColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_ZH");
            DropColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_");
            DropColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_ZH");
            DropColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_NAME_");
            DropColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_NAME_ZH");
            DropColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_PHONE_");
            DropColumn("dbo.SUPPLIERS_HISTORY", "COMMENTS");
            DropColumn("dbo.SUPPLIERS_HISTORY", "ID_INCOTERM_");
            DropColumn("dbo.SUPPLIERS_HISTORY", "ID_PAYMENT_TERMS");
            DropColumn("dbo.SUPPLIERS_HISTORY", "ID_DEFAULT_CURRENCY");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SUPPLIERS_HISTORY", "ID_DEFAULT_CURRENCY", c => c.String(maxLength: 4));
            AddColumn("dbo.SUPPLIERS_HISTORY", "ID_PAYMENT_TERMS", c => c.String(maxLength: 4));
            AddColumn("dbo.SUPPLIERS_HISTORY", "ID_INCOTERM_", c => c.String(maxLength: 8));
            AddColumn("dbo.SUPPLIERS_HISTORY", "COMMENTS", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_PHONE_", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_NAME_ZH", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_NAME_", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY", c => c.String(maxLength: 4));
            AddColumn("dbo.SUPPLIERS", "ID_PAYMENT_TERMS", c => c.String(maxLength: 4));
            AddColumn("dbo.SUPPLIERS", "ID_INCOTERM_", c => c.String(maxLength: 8));
            AddColumn("dbo.SUPPLIERS", "COMMENTS", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "CONTACT_PHONE_", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS", "CONTACT_NAME_ZH", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS", "CONTACT_NAME_", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_", c => c.String(maxLength: 2500));
            CreateIndex("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY");
            CreateIndex("dbo.SUPPLIERS", "ID_PAYMENT_TERMS");
            CreateIndex("dbo.SUPPLIERS", "ID_INCOTERM_");
            AddForeignKey("dbo.SUPPLIERS", "ID_PAYMENT_TERMS", "dbo.PAYMENT_TERMS", "ID_PAYMENT_TERMS");
            AddForeignKey("dbo.SUPPLIERS", "ID_INCOTERM_", "dbo.INCOTERMS", "ID_INCOTERM");
            AddForeignKey("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY", "dbo.CURRENCIES", "ID_CURRENCY");
        }
    }
}
