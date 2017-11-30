namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class docBoxes_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DOC_BOXES",
                c => new
                    {
                        ID_DOC = c.String(nullable: false, maxLength: 50),
                        BOX_NUMBER = c.Int(nullable: false),
                        ID_BOX = c.String(nullable: false, maxLength: 50),
                        NET_WEIGHT = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        GROSS_WEIGHT = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.ID_DOC, t.BOX_NUMBER })
                .ForeignKey("dbo.BOXES", t => t.ID_BOX, cascadeDelete: false)
                .Index(t => t.ID_BOX);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DOC_BOXES", "ID_BOX", "dbo.BOXES");
            DropIndex("dbo.DOC_BOXES", new[] { "ID_BOX" });
            DropTable("dbo.DOC_BOXES");
        }
    }
}
