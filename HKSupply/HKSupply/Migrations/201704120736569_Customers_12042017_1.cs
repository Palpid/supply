namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customers_12042017_1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CUSTOMERS");
            DropPrimaryKey("dbo.CUSTOMERS_HISTORY");
            AddColumn("dbo.CUSTOMERS", "CUSTOMER_NAME", c => c.String(maxLength: 500));
            AddColumn("dbo.CUSTOMERS_HISTORY", "CUSTOMER_NAME", c => c.String(maxLength: 500));
            AlterColumn("dbo.CUSTOMERS", "ID_CUSTOMER", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CUSTOMERS", "VAT_NUM", c => c.String(maxLength: 100));
            AlterColumn("dbo.CUSTOMERS_HISTORY", "ID_CUSTOMER", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CUSTOMERS_HISTORY", "VAT_NUM", c => c.String(maxLength: 100));
            AddPrimaryKey("dbo.CUSTOMERS", "ID_CUSTOMER");
            AddPrimaryKey("dbo.CUSTOMERS_HISTORY", new[] { "ID_CUSTOMER", "ID_VER", "ID_SUBVER", "TIMESTAMP" });
            DropColumn("dbo.CUSTOMERS", "CUST_NAME");
            DropColumn("dbo.CUSTOMERS", "SHIPING_ADDRESS");
            DropColumn("dbo.CUSTOMERS", "BILLING_ADDRESS");
            DropColumn("dbo.CUSTOMERS", "CONTACT_NAME");
            DropColumn("dbo.CUSTOMERS", "CONTACT_PHONE");
            DropColumn("dbo.CUSTOMERS", "ID_INCOTERM");
            DropColumn("dbo.CUSTOMERS", "IDPAYMENTTERMS");
            DropColumn("dbo.CUSTOMERS", "CURRENCY");
            DropColumn("dbo.CUSTOMERS_HISTORY", "CUST_NAME");
            DropColumn("dbo.CUSTOMERS_HISTORY", "SHIPING_ADDRESS");
            DropColumn("dbo.CUSTOMERS_HISTORY", "BILLING_ADDRESS");
            DropColumn("dbo.CUSTOMERS_HISTORY", "CONTACT_NAME");
            DropColumn("dbo.CUSTOMERS_HISTORY", "CONTACT_PHONE");
            DropColumn("dbo.CUSTOMERS_HISTORY", "ID_INCOTERM");
            DropColumn("dbo.CUSTOMERS_HISTORY", "IDPAYMENTTERMS");
            DropColumn("dbo.CUSTOMERS_HISTORY", "CURRENCY");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CUSTOMERS_HISTORY", "CURRENCY", c => c.String());
            AddColumn("dbo.CUSTOMERS_HISTORY", "IDPAYMENTTERMS", c => c.Int(nullable: false));
            AddColumn("dbo.CUSTOMERS_HISTORY", "ID_INCOTERM", c => c.Int(nullable: false));
            AddColumn("dbo.CUSTOMERS_HISTORY", "CONTACT_PHONE", c => c.String());
            AddColumn("dbo.CUSTOMERS_HISTORY", "CONTACT_NAME", c => c.String());
            AddColumn("dbo.CUSTOMERS_HISTORY", "BILLING_ADDRESS", c => c.String());
            AddColumn("dbo.CUSTOMERS_HISTORY", "SHIPING_ADDRESS", c => c.String());
            AddColumn("dbo.CUSTOMERS_HISTORY", "CUST_NAME", c => c.String());
            AddColumn("dbo.CUSTOMERS", "CURRENCY", c => c.String());
            AddColumn("dbo.CUSTOMERS", "IDPAYMENTTERMS", c => c.Int(nullable: false));
            AddColumn("dbo.CUSTOMERS", "ID_INCOTERM", c => c.Int(nullable: false));
            AddColumn("dbo.CUSTOMERS", "CONTACT_PHONE", c => c.String());
            AddColumn("dbo.CUSTOMERS", "CONTACT_NAME", c => c.String());
            AddColumn("dbo.CUSTOMERS", "BILLING_ADDRESS", c => c.String());
            AddColumn("dbo.CUSTOMERS", "SHIPING_ADDRESS", c => c.String());
            AddColumn("dbo.CUSTOMERS", "CUST_NAME", c => c.String());
            DropPrimaryKey("dbo.CUSTOMERS_HISTORY");
            DropPrimaryKey("dbo.CUSTOMERS");
            AlterColumn("dbo.CUSTOMERS_HISTORY", "VAT_NUM", c => c.String());
            AlterColumn("dbo.CUSTOMERS_HISTORY", "ID_CUSTOMER", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.CUSTOMERS", "VAT_NUM", c => c.String());
            AlterColumn("dbo.CUSTOMERS", "ID_CUSTOMER", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.CUSTOMERS_HISTORY", "CUSTOMER_NAME");
            DropColumn("dbo.CUSTOMERS", "CUSTOMER_NAME");
            AddPrimaryKey("dbo.CUSTOMERS_HISTORY", new[] { "ID_CUSTOMER", "ID_VER", "ID_SUBVER" });
            AddPrimaryKey("dbo.CUSTOMERS", new[] { "ID_CUSTOMER", "ID_VER", "ID_SUBVER" });
        }
    }
}
