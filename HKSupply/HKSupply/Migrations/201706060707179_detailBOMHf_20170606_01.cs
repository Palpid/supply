namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class detailBOMHf_20170606_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DETAIL_BOM_HF",
                c => new
                    {
                        ID_BOM = c.Int(nullable: false),
                        ID_BOM_DETAIL = c.Int(nullable: false),
                        QUANTITY = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.ID_BOM, t.ID_BOM_DETAIL })
                .ForeignKey("dbo.ITEMS_BOM", t => t.ID_BOM_DETAIL, cascadeDelete: false)
                .Index(t => t.ID_BOM_DETAIL);
            
            CreateTable(
                "dbo.DETAIL_BOM_HF_HISTORY",
                c => new
                    {
                        ID_BOM = c.Int(nullable: false),
                        ID_BOM_DETAIL = c.Int(nullable: false),
                        ID_VER_BOM = c.Int(nullable: false),
                        ID_SUBVER_BOM = c.Int(nullable: false),
                        TIMESTAMP_BOM = c.DateTime(nullable: false),
                        ID_VER_BOM_DETAIL = c.Int(nullable: false),
                        ID_SUBVER_BOM_DETAIL = c.Int(nullable: false),
                        TIMESTAMP_BOM_DETAIL = c.DateTime(nullable: false),
                        QUANTITY = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.ID_BOM, t.ID_BOM_DETAIL, t.ID_VER_BOM, t.ID_SUBVER_BOM, t.TIMESTAMP_BOM, t.ID_VER_BOM_DETAIL, t.ID_SUBVER_BOM_DETAIL, t.TIMESTAMP_BOM_DETAIL })
                .ForeignKey("dbo.ITEMS_BOM", t => t.ID_BOM_DETAIL, cascadeDelete: false)
                .ForeignKey("dbo.ITEMS_BOM", t => t.ID_BOM, cascadeDelete: false)
                .Index(t => t.ID_BOM)
                .Index(t => t.ID_BOM_DETAIL);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DETAIL_BOM_HF_HISTORY", "ID_BOM", "dbo.ITEMS_BOM");
            DropForeignKey("dbo.DETAIL_BOM_HF_HISTORY", "ID_BOM_DETAIL", "dbo.ITEMS_BOM");
            DropForeignKey("dbo.DETAIL_BOM_HF", "ID_BOM_DETAIL", "dbo.ITEMS_BOM");
            DropIndex("dbo.DETAIL_BOM_HF_HISTORY", new[] { "ID_BOM_DETAIL" });
            DropIndex("dbo.DETAIL_BOM_HF_HISTORY", new[] { "ID_BOM" });
            DropIndex("dbo.DETAIL_BOM_HF", new[] { "ID_BOM_DETAIL" });
            DropTable("dbo.DETAIL_BOM_HF_HISTORY");
            DropTable("dbo.DETAIL_BOM_HF");
        }
    }
}
