namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyCompany_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MY_COMPANY",
                c => new
                    {
                        ID_MY_COMPANY = c.String(nullable: false, maxLength: 100),
                        NAME = c.String(maxLength: 500),
                        VAT_NUM = c.String(maxLength: 100),
                        SHIPING_ADDRESS = c.String(maxLength: 2500),
                        SHIPING_ADDRESS_ZH = c.String(maxLength: 2500),
                        BILLING_ADDRESS = c.String(maxLength: 2500),
                        BILLING_ADDRESS_ZH = c.String(maxLength: 2500),
                        CONTACT_NAME = c.String(maxLength: 100),
                        CONTACT_NAME_ZH = c.String(maxLength: 100),
                        CONTACT_PHONE = c.String(maxLength: 100),
                        COMMENTS = c.String(maxLength: 2500),
                        ID_DEFAULT_CURRENCY = c.String(maxLength: 4),
                    })
                .PrimaryKey(t => t.ID_MY_COMPANY)
                .ForeignKey("dbo.CURRENCIES", t => t.ID_DEFAULT_CURRENCY)
                .Index(t => t.ID_DEFAULT_CURRENCY);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MY_COMPANY", "ID_DEFAULT_CURRENCY", "dbo.CURRENCIES");
            DropIndex("dbo.MY_COMPANY", new[] { "ID_DEFAULT_CURRENCY" });
            DropTable("dbo.MY_COMPANY");
        }
    }
}
