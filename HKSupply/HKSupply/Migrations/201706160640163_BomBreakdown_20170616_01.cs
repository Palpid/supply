namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BomBreakdown_20170616_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BOM_BREAKDOWN",
                c => new
                    {
                        ID_BOM_BREAKDOWN = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_BOM_BREAKDOWN);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BOM_BREAKDOWN");
        }
    }
}
