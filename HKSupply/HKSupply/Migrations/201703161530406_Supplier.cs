namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Supplier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SUPPLIERS",
                c => new
                    {
                        ID_SUPPLIER = c.String(nullable: false, maxLength: 128),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        SUPPLIER_NAME = c.String(),
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
                .PrimaryKey(t => new { t.ID_SUPPLIER, t.ID_VER, t.ID_SUBVER });
            
            CreateTable(
                "dbo.SUPPLIERS_HISTORY",
                c => new
                    {
                        ID_SUPPLIER = c.String(nullable: false, maxLength: 128),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        SUPPLIER_NAME = c.String(),
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
                .PrimaryKey(t => new { t.ID_SUPPLIER, t.ID_VER, t.ID_SUBVER });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SUPPLIERS_HISTORY");
            DropTable("dbo.SUPPLIERS");
        }
    }
}
