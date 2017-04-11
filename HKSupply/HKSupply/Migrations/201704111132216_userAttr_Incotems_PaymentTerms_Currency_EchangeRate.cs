namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userAttr_Incotems_PaymentTerms_Currency_EchangeRate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CURRENCIES",
                c => new
                    {
                        ID_CURRENCY = c.String(nullable: false, maxLength: 4),
                        DESCRIPTION = c.String(maxLength: 500),
                        DESCRIPTION_ZH = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_CURRENCY);
            
            CreateTable(
                "dbo.ECHANGE_RATES",
                c => new
                    {
                        DATE = c.DateTime(nullable: false),
                        ID_CURRENCY_1 = c.String(nullable: false, maxLength: 4),
                        ID_CURRENCY_2 = c.String(nullable: false, maxLength: 4),
                        RATIO = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.DATE, t.ID_CURRENCY_1, t.ID_CURRENCY_2 })
                .ForeignKey("dbo.CURRENCIES", t => t.ID_CURRENCY_1, cascadeDelete: false)
                .ForeignKey("dbo.CURRENCIES", t => t.ID_CURRENCY_2, cascadeDelete: false)
                .Index(t => t.ID_CURRENCY_1)
                .Index(t => t.ID_CURRENCY_2);
            
            CreateTable(
                "dbo.INCOTERMS",
                c => new
                    {
                        ID_INCOTERM = c.String(nullable: false, maxLength: 8),
                        DESCRIPTION = c.String(maxLength: 500),
                        DESCRIPTION_ZH = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_INCOTERM);
            
            CreateTable(
                "dbo.PAYMENT_TERMS",
                c => new
                    {
                        ID_PAYMENT_TERMS = c.String(nullable: false, maxLength: 4),
                        DESCRIPTION = c.String(maxLength: 500),
                        DESCRIPTION_ZH = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_PAYMENT_TERMS);
            
            CreateTable(
                "dbo.USER_ATTR_DESCRIPTION",
                c => new
                    {
                        ID_USER_ATTR = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_USER_ATTR);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ECHANGE_RATES", "ID_CURRENCY_2", "dbo.CURRENCIES");
            DropForeignKey("dbo.ECHANGE_RATES", "ID_CURRENCY_1", "dbo.CURRENCIES");
            DropIndex("dbo.ECHANGE_RATES", new[] { "ID_CURRENCY_2" });
            DropIndex("dbo.ECHANGE_RATES", new[] { "ID_CURRENCY_1" });
            DropTable("dbo.USER_ATTR_DESCRIPTION");
            DropTable("dbo.PAYMENT_TERMS");
            DropTable("dbo.INCOTERMS");
            DropTable("dbo.ECHANGE_RATES");
            DropTable("dbo.CURRENCIES");
        }
    }
}
