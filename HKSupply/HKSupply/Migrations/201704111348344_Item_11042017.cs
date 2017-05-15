namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Item_11042017 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SUPPLIERS", name: "SHIPING_ADDRESS", newName: "SHIPING_ADDRESS_");
            RenameColumn(table: "dbo.SUPPLIERS", name: "BILLING_ADDRESS", newName: "BILLING_ADDRESS_");
            RenameColumn(table: "dbo.SUPPLIERS", name: "CONTACT_NAME", newName: "CONTACT_NAME_");
            RenameColumn(table: "dbo.SUPPLIERS", name: "CONTACT_PHONE", newName: "CONTACT_PHONE_");
            RenameColumn(table: "dbo.SUPPLIERS", name: "ID_INCOTERM", newName: "ID_INCOTERM_");
            RenameColumn(table: "dbo.SUPPLIERS", name: "IDPAYMENTTERMS", newName: "ID_PAYMENT_TERMS");
            RenameColumn(table: "dbo.SUPPLIERS_HISTORY", name: "SHIPING_ADDRESS", newName: "SHIPING_ADDRESS_");
            RenameColumn(table: "dbo.SUPPLIERS_HISTORY", name: "BILLING_ADDRESS", newName: "BILLING_ADDRESS_");
            RenameColumn(table: "dbo.SUPPLIERS_HISTORY", name: "CONTACT_NAME", newName: "CONTACT_NAME_");
            RenameColumn(table: "dbo.SUPPLIERS_HISTORY", name: "CONTACT_PHONE", newName: "CONTACT_PHONE_");
            RenameColumn(table: "dbo.SUPPLIERS_HISTORY", name: "ID_INCOTERM", newName: "ID_INCOTERM_");
            RenameColumn(table: "dbo.SUPPLIERS_HISTORY", name: "IDPAYMENTTERMS", newName: "ID_PAYMENT_TERMS");
            DropPrimaryKey("dbo.SUPPLIERS");
            DropPrimaryKey("dbo.SUPPLIERS_HISTORY");
            AddColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "CONTACT_NAME_ZH", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS", "COMMENTS", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY", c => c.String(maxLength: 4));
            AddColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_ZH", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_NAME_ZH", c => c.String(maxLength: 100));
            AddColumn("dbo.SUPPLIERS_HISTORY", "COMMENTS", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "ID_DEFAULT_CURRENCY", c => c.String(maxLength: 4));
            AlterColumn("dbo.SUPPLIERS", "ID_SUPPLIER", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.SUPPLIERS", "SUPPLIER_NAME", c => c.String(maxLength: 500));
            AlterColumn("dbo.SUPPLIERS", "VAT_NUM", c => c.String(maxLength: 100));
            AlterColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_", c => c.String(maxLength: 2500));
            AlterColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_", c => c.String(maxLength: 2500));
            AlterColumn("dbo.SUPPLIERS", "CONTACT_NAME_", c => c.String(maxLength: 100));
            AlterColumn("dbo.SUPPLIERS", "CONTACT_PHONE_", c => c.String(maxLength: 100));
            AlterColumn("dbo.SUPPLIERS", "ID_INCOTERM_", c => c.String(maxLength: 8));
            AlterColumn("dbo.SUPPLIERS", "ID_PAYMENT_TERMS", c => c.String(maxLength: 4));
            AlterColumn("dbo.SUPPLIERS_HISTORY", "ID_SUPPLIER", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.SUPPLIERS_HISTORY", "SUPPLIER_NAME", c => c.String(maxLength: 500));
            AlterColumn("dbo.SUPPLIERS_HISTORY", "VAT_NUM", c => c.String(maxLength: 100));
            AlterColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_", c => c.String(maxLength: 2500));
            AlterColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_", c => c.String(maxLength: 2500));
            AlterColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_NAME_", c => c.String(maxLength: 100));
            AlterColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_PHONE_", c => c.String(maxLength: 100));
            AlterColumn("dbo.SUPPLIERS_HISTORY", "ID_INCOTERM_", c => c.String(maxLength: 8));
            AlterColumn("dbo.SUPPLIERS_HISTORY", "ID_PAYMENT_TERMS", c => c.String(maxLength: 4));
            AddPrimaryKey("dbo.SUPPLIERS", "ID_SUPPLIER");
            AddPrimaryKey("dbo.SUPPLIERS_HISTORY", new[] { "ID_SUPPLIER", "ID_VER", "ID_SUBVER", "TIMESTAMP" });
            CreateIndex("dbo.SUPPLIERS", "ID_INCOTERM_");
            CreateIndex("dbo.SUPPLIERS", "ID_PAYMENT_TERMS");
            CreateIndex("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY");
            AddForeignKey("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY", "dbo.CURRENCIES", "ID_CURRENCY");
            AddForeignKey("dbo.SUPPLIERS", "ID_INCOTERM_", "dbo.INCOTERMS", "ID_INCOTERM");
            AddForeignKey("dbo.SUPPLIERS", "ID_PAYMENT_TERMS", "dbo.PAYMENT_TERMS", "ID_PAYMENT_TERMS");
            DropColumn("dbo.SUPPLIERS", "CURRENCY");
            DropColumn("dbo.SUPPLIERS_HISTORY", "CURRENCY");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SUPPLIERS_HISTORY", "CURRENCY", c => c.String());
            AddColumn("dbo.SUPPLIERS", "CURRENCY", c => c.String());
            DropForeignKey("dbo.SUPPLIERS", "ID_PAYMENT_TERMS", "dbo.PAYMENT_TERMS");
            DropForeignKey("dbo.SUPPLIERS", "ID_INCOTERM_", "dbo.INCOTERMS");
            DropForeignKey("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY", "dbo.CURRENCIES");
            DropIndex("dbo.SUPPLIERS", new[] { "ID_DEFAULT_CURRENCY" });
            DropIndex("dbo.SUPPLIERS", new[] { "ID_PAYMENT_TERMS" });
            DropIndex("dbo.SUPPLIERS", new[] { "ID_INCOTERM_" });
            DropPrimaryKey("dbo.SUPPLIERS_HISTORY");
            DropPrimaryKey("dbo.SUPPLIERS");
            AlterColumn("dbo.SUPPLIERS_HISTORY", "ID_PAYMENT_TERMS", c => c.Int(nullable: false));
            AlterColumn("dbo.SUPPLIERS_HISTORY", "ID_INCOTERM_", c => c.Int(nullable: false));
            AlterColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_PHONE_", c => c.String());
            AlterColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_NAME_", c => c.String());
            AlterColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_", c => c.String());
            AlterColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_", c => c.String());
            AlterColumn("dbo.SUPPLIERS_HISTORY", "VAT_NUM", c => c.String());
            AlterColumn("dbo.SUPPLIERS_HISTORY", "SUPPLIER_NAME", c => c.String());
            AlterColumn("dbo.SUPPLIERS_HISTORY", "ID_SUPPLIER", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.SUPPLIERS", "ID_PAYMENT_TERMS", c => c.Int(nullable: false));
            AlterColumn("dbo.SUPPLIERS", "ID_INCOTERM_", c => c.Int(nullable: false));
            AlterColumn("dbo.SUPPLIERS", "CONTACT_PHONE_", c => c.String());
            AlterColumn("dbo.SUPPLIERS", "CONTACT_NAME_", c => c.String());
            AlterColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_", c => c.String());
            AlterColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_", c => c.String());
            AlterColumn("dbo.SUPPLIERS", "VAT_NUM", c => c.String());
            AlterColumn("dbo.SUPPLIERS", "SUPPLIER_NAME", c => c.String());
            AlterColumn("dbo.SUPPLIERS", "ID_SUPPLIER", c => c.String(nullable: false, maxLength: 3));
            DropColumn("dbo.SUPPLIERS_HISTORY", "ID_DEFAULT_CURRENCY");
            DropColumn("dbo.SUPPLIERS_HISTORY", "COMMENTS");
            DropColumn("dbo.SUPPLIERS_HISTORY", "CONTACT_NAME_ZH");
            DropColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_ZH");
            DropColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_ZH");
            DropColumn("dbo.SUPPLIERS", "ID_DEFAULT_CURRENCY");
            DropColumn("dbo.SUPPLIERS", "COMMENTS");
            DropColumn("dbo.SUPPLIERS", "CONTACT_NAME_ZH");
            DropColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_ZH");
            DropColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_ZH");
            AddPrimaryKey("dbo.SUPPLIERS_HISTORY", new[] { "ID_SUPPLIER", "ID_VER", "ID_SUBVER" });
            AddPrimaryKey("dbo.SUPPLIERS", new[] { "ID_SUPPLIER", "ID_VER", "ID_SUBVER" });
            RenameColumn(table: "dbo.SUPPLIERS_HISTORY", name: "ID_PAYMENT_TERMS", newName: "IDPAYMENTTERMS");
            RenameColumn(table: "dbo.SUPPLIERS_HISTORY", name: "ID_INCOTERM_", newName: "ID_INCOTERM");
            RenameColumn(table: "dbo.SUPPLIERS_HISTORY", name: "CONTACT_PHONE_", newName: "CONTACT_PHONE");
            RenameColumn(table: "dbo.SUPPLIERS_HISTORY", name: "CONTACT_NAME_", newName: "CONTACT_NAME");
            RenameColumn(table: "dbo.SUPPLIERS_HISTORY", name: "BILLING_ADDRESS_", newName: "BILLING_ADDRESS");
            RenameColumn(table: "dbo.SUPPLIERS_HISTORY", name: "SHIPING_ADDRESS_", newName: "SHIPING_ADDRESS");
            RenameColumn(table: "dbo.SUPPLIERS", name: "ID_PAYMENT_TERMS", newName: "IDPAYMENTTERMS");
            RenameColumn(table: "dbo.SUPPLIERS", name: "ID_INCOTERM_", newName: "ID_INCOTERM");
            RenameColumn(table: "dbo.SUPPLIERS", name: "CONTACT_PHONE_", newName: "CONTACT_PHONE");
            RenameColumn(table: "dbo.SUPPLIERS", name: "CONTACT_NAME_", newName: "CONTACT_NAME");
            RenameColumn(table: "dbo.SUPPLIERS", name: "BILLING_ADDRESS_", newName: "BILLING_ADDRESS");
            RenameColumn(table: "dbo.SUPPLIERS", name: "SHIPING_ADDRESS_", newName: "SHIPING_ADDRESS");
        }
    }
}
