namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bom_20170522_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DETAIL_BOM_HW",
                c => new
                    {
                        ID_BOM = c.Int(nullable: false),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 50),
                        QUANTITY = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        WASTE = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.ID_BOM, t.ID_ITEM_BCN })
                .ForeignKey("dbo.ITEMS_HW", t => t.ID_ITEM_BCN, cascadeDelete: false)
                .ForeignKey("dbo.ITEMS_BOM", t => t.ID_BOM, cascadeDelete: false)
                .Index(t => t.ID_BOM)
                .Index(t => t.ID_ITEM_BCN);
            
            CreateTable(
                "dbo.DETAIL_BOM_HW_HISTORY",
                c => new
                    {
                        ID_BOM = c.Int(nullable: false),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 50),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        QUANTITY = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        WASTE = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.ID_BOM, t.ID_ITEM_BCN, t.ID_VER, t.ID_SUBVER, t.TIMESTAMP })
                .ForeignKey("dbo.ITEMS_HW", t => t.ID_ITEM_BCN, cascadeDelete: false)
                .ForeignKey("dbo.ITEMS_BOM", t => t.ID_BOM, cascadeDelete: false)
                .Index(t => t.ID_BOM)
                .Index(t => t.ID_ITEM_BCN);
            
            CreateTable(
                "dbo.ITEMS_BOM",
                c => new
                    {
                        ID_BOM = c.Int(nullable: false, identity: true),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 50),
                        ID_ITEM_GROUP = c.String(nullable: false, maxLength: 100),
                        CREATE_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID_BOM)
                .ForeignKey("dbo.ITEM_GROUP", t => t.ID_ITEM_GROUP, cascadeDelete: false)
                .Index(t => new { t.ID_VER, t.ID_SUBVER, t.ID_ITEM_BCN }, unique: true, name: "IX_VER_ITEM")
                .Index(t => t.ID_ITEM_GROUP);
            
            CreateTable(
                "dbo.DETAIL_BOM_MT",
                c => new
                    {
                        ID_BOM = c.Int(nullable: false),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 50),
                        QUANTITY = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        WASTE = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.ID_BOM, t.ID_ITEM_BCN })
                .ForeignKey("dbo.ITEMS_MT", t => t.ID_ITEM_BCN, cascadeDelete: false)
                .ForeignKey("dbo.ITEMS_BOM", t => t.ID_BOM, cascadeDelete: false)
                .Index(t => t.ID_BOM)
                .Index(t => t.ID_ITEM_BCN);
            
            CreateTable(
                "dbo.DETAIL_BOM_MT_HISTORY",
                c => new
                    {
                        ID_BOM = c.Int(nullable: false),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 50),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        QUANTITY = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        WASTE = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.ID_BOM, t.ID_ITEM_BCN, t.ID_VER, t.ID_SUBVER, t.TIMESTAMP })
                .ForeignKey("dbo.ITEMS_MT", t => t.ID_ITEM_BCN, cascadeDelete: false)
                .ForeignKey("dbo.ITEMS_BOM", t => t.ID_BOM, cascadeDelete: false)
                .Index(t => t.ID_BOM)
                .Index(t => t.ID_ITEM_BCN);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DETAIL_BOM_MT_HISTORY", "ID_BOM", "dbo.ITEMS_BOM");
            DropForeignKey("dbo.DETAIL_BOM_MT_HISTORY", "ID_ITEM_BCN", "dbo.ITEMS_MT");
            DropForeignKey("dbo.DETAIL_BOM_HW_HISTORY", "ID_BOM", "dbo.ITEMS_BOM");
            DropForeignKey("dbo.DETAIL_BOM_MT", "ID_BOM", "dbo.ITEMS_BOM");
            DropForeignKey("dbo.DETAIL_BOM_MT", "ID_ITEM_BCN", "dbo.ITEMS_MT");
            DropForeignKey("dbo.ITEMS_BOM", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropForeignKey("dbo.DETAIL_BOM_HW", "ID_BOM", "dbo.ITEMS_BOM");
            DropForeignKey("dbo.DETAIL_BOM_HW_HISTORY", "ID_ITEM_BCN", "dbo.ITEMS_HW");
            DropForeignKey("dbo.DETAIL_BOM_HW", "ID_ITEM_BCN", "dbo.ITEMS_HW");
            DropIndex("dbo.DETAIL_BOM_MT_HISTORY", new[] { "ID_ITEM_BCN" });
            DropIndex("dbo.DETAIL_BOM_MT_HISTORY", new[] { "ID_BOM" });
            DropIndex("dbo.DETAIL_BOM_MT", new[] { "ID_ITEM_BCN" });
            DropIndex("dbo.DETAIL_BOM_MT", new[] { "ID_BOM" });
            DropIndex("dbo.ITEMS_BOM", new[] { "ID_ITEM_GROUP" });
            DropIndex("dbo.ITEMS_BOM", "IX_VER_ITEM");
            DropIndex("dbo.DETAIL_BOM_HW_HISTORY", new[] { "ID_ITEM_BCN" });
            DropIndex("dbo.DETAIL_BOM_HW_HISTORY", new[] { "ID_BOM" });
            DropIndex("dbo.DETAIL_BOM_HW", new[] { "ID_ITEM_BCN" });
            DropIndex("dbo.DETAIL_BOM_HW", new[] { "ID_BOM" });
            DropTable("dbo.DETAIL_BOM_MT_HISTORY");
            DropTable("dbo.DETAIL_BOM_MT");
            DropTable("dbo.ITEMS_BOM");
            DropTable("dbo.DETAIL_BOM_HW_HISTORY");
            DropTable("dbo.DETAIL_BOM_HW");
        }
    }
}
