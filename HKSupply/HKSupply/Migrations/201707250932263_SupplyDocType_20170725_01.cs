namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupplyDocType_20170725_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SUPPLY_DOC_TYPE",
                c => new
                    {
                        ID_SUPPLY_DOC_TYPE = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_SUPPLY_DOC_TYPE);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SUPPLY_DOC_TYPE");
        }
    }
}
