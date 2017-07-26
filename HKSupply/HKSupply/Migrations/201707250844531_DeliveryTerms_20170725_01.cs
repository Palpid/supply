namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeliveryTerms_20170725_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DELIVERY_TERMS",
                c => new
                    {
                        ID_DELIVERY_TERM = c.String(nullable: false, maxLength: 5),
                        DESCRIPTION = c.String(maxLength: 500),
                        DESCRIPTION_ZH = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_DELIVERY_TERM);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DELIVERY_TERMS");
        }
    }
}
