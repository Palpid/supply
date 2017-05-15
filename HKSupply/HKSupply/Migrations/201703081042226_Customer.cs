namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customer : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.CUSTOMERS",
                c => new
                    {
                        ID_CUSTOMER = c.String(nullable: false, maxLength: 128),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        CUST_NAME = c.String(),
                        ACTIVE = c.Boolean(nullable: false),
                        VAT_NUM = c.String(),
                        SHIPING_ADDRESS = c.String(),
                        BILLING_ADDRESS = c.String(),
                        CONTACT_NAME = c.String(),
                        CONTACT_PHONE = c.String(),
                        ID_INCOTERM = c.Int(nullable: false),
                        IDPAYMENTTERMS = c.Int(nullable: false),
                        CURRENCY = c.String(),
                    })
                .PrimaryKey(t => new { t.ID_CUSTOMER, t.ID_VER, t.ID_SUBVER });
            
            CreateTable(
                "dbo.CUSTOMERS_HISTORY",
                c => new
                    {
                        ID_CUSTOMER = c.String(nullable: false, maxLength: 128),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        CUST_NAME = c.String(),
                        ACTIVE = c.Boolean(nullable: false),
                        VAT_NUM = c.String(),
                        SHIPING_ADDRESS = c.String(),
                        BILLING_ADDRESS = c.String(),
                        CONTACT_NAME = c.String(),
                        CONTACT_PHONE = c.String(),
                        ID_INCOTERM = c.Int(nullable: false),
                        IDPAYMENTTERMS = c.Int(nullable: false),
                        CURRENCY = c.String(),
                    })
                .PrimaryKey(t => new { t.ID_CUSTOMER, t.ID_VER, t.ID_SUBVER });
        }
        
        public override void Down()
        {

        }
    }
}
