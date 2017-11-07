namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnitsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UNITS",
                c => new
                    {
                        UNIT_CODE = c.String(nullable: false, maxLength: 2),
                        DESCRIPTION = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.UNIT_CODE);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UNITS");
        }
    }
}
