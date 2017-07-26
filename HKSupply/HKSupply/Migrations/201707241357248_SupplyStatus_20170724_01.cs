namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupplyStatus_20170724_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SUPPLY_STATUS",
                c => new
                    {
                        ID_SUPPLY_STATUS = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_SUPPLY_STATUS);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SUPPLY_STATUS");
        }
    }
}
