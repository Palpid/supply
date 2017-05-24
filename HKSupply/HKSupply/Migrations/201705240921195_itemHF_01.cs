namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemHF_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ITEMS_HF",
                c => new
                    {
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 50),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        ID_PROTOTYPE = c.String(maxLength: 50),
                        ID_MATERIAL_L1 = c.String(maxLength: 100),
                        ID_MATERIAL_L2 = c.String(maxLength: 100),
                        ID_MATERIAL_L3 = c.String(maxLength: 100),
                        ID_DEFAULT_SUPPLIER = c.String(maxLength: 100),
                        ID_MODEL = c.String(maxLength: 100),
                        ID_FAMILY_HK = c.String(maxLength: 100),
                        CALIBER = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        ID_COLOR_1 = c.String(maxLength: 30),
                        ID_COLOR_2 = c.String(maxLength: 30),
                        ID_ITEM_HK = c.String(maxLength: 50),
                        ITEM_DESCRIPTION = c.String(maxLength: 100),
                        COMMENTS = c.String(maxLength: 2500),
                        SEGMENT = c.String(maxLength: 30),
                        CATEGORY = c.String(maxLength: 100),
                        AGE = c.String(maxLength: 100),
                        LAUNCH_DATE = c.DateTime(),
                        REMOVAL_DATE = c.DateTime(),
                        ID_STATUS_CIAL = c.Int(nullable: false),
                        ID_STATUS_PROD = c.Int(nullable: false),
                        ID_USER_ATTRI_1 = c.String(maxLength: 100),
                        ID_USER_ATTRI_2 = c.String(maxLength: 100),
                        ID_USER_ATTRI_3 = c.String(maxLength: 100),
                        UNIT = c.String(maxLength: 2),
                        DOCS_LINK = c.String(maxLength: 512),
                        CREATE_DATE = c.DateTime(nullable: false),
                        PHOTO_PATH = c.String(maxLength: 2500),
                    })
                .PrimaryKey(t => t.ID_ITEM_BCN)
                .ForeignKey("dbo.SUPPLIERS", t => t.ID_DEFAULT_SUPPLIER)
                .ForeignKey("dbo.FAMILIES_HK", t => t.ID_FAMILY_HK)
                .ForeignKey("dbo.MATERIALS_L1", t => t.ID_MATERIAL_L1)
                .ForeignKey("dbo.MATERIALS_L2", t => t.ID_MATERIAL_L2)
                .ForeignKey("dbo.MATERIALS_L3", t => t.ID_MATERIAL_L3)
                .ForeignKey("dbo.MODELS", t => t.ID_MODEL)
                .ForeignKey("dbo.PROTOTYPES", t => t.ID_PROTOTYPE)
                .ForeignKey("dbo.STATUS_CIAL", t => t.ID_STATUS_CIAL, cascadeDelete: false)
                .ForeignKey("dbo.STATUS_HK", t => t.ID_STATUS_PROD, cascadeDelete: false)
                .Index(t => t.ID_PROTOTYPE)
                .Index(t => t.ID_MATERIAL_L1)
                .Index(t => t.ID_MATERIAL_L2)
                .Index(t => t.ID_MATERIAL_L3)
                .Index(t => t.ID_DEFAULT_SUPPLIER)
                .Index(t => t.ID_MODEL)
                .Index(t => t.ID_FAMILY_HK)
                .Index(t => t.ID_STATUS_CIAL)
                .Index(t => t.ID_STATUS_PROD);
            
            CreateTable(
                "dbo.ITEMS_HF_HISTORY",
                c => new
                    {
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 50),
                        ID_PROTOTYPE = c.String(maxLength: 50),
                        ID_MATERIAL_L1 = c.String(maxLength: 100),
                        ID_MATERIAL_L2 = c.String(maxLength: 100),
                        ID_MATERIAL_L3 = c.String(maxLength: 100),
                        ID_DEFAULT_SUPPLIER = c.String(maxLength: 100),
                        ID_MODEL = c.String(maxLength: 100),
                        ID_FAMILY_HK = c.String(maxLength: 100),
                        CALIBER = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        ID_COLOR_1 = c.String(maxLength: 30),
                        ID_COLOR_2 = c.String(maxLength: 30),
                        ID_ITEM_HK = c.String(maxLength: 50),
                        ITEM_DESCRIPTION = c.String(maxLength: 100),
                        COMMENTS = c.String(maxLength: 2500),
                        SEGMENT = c.String(maxLength: 30),
                        CATEGORY = c.String(maxLength: 100),
                        AGE = c.String(maxLength: 100),
                        LAUNCH_DATE = c.DateTime(),
                        REMOVAL_DATE = c.DateTime(),
                        ID_STATUS_CIAL = c.Int(nullable: false),
                        ID_STATUS_PROD = c.Int(nullable: false),
                        ID_USER_ATTRI_1 = c.String(maxLength: 100),
                        ID_USER_ATTRI_2 = c.String(maxLength: 100),
                        ID_USER_ATTRI_3 = c.String(maxLength: 100),
                        UNIT = c.String(maxLength: 2),
                        DOCS_LINK = c.String(maxLength: 512),
                        CREATE_DATE = c.DateTime(nullable: false),
                        USER = c.String(maxLength: 20),
                        PHOTO_PATH = c.String(maxLength: 2500),
                    })
                .PrimaryKey(t => new { t.ID_VER, t.ID_SUBVER, t.TIMESTAMP, t.ID_ITEM_BCN });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ITEMS_HF", "ID_STATUS_PROD", "dbo.STATUS_HK");
            DropForeignKey("dbo.ITEMS_HF", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS_HF", "ID_PROTOTYPE", "dbo.PROTOTYPES");
            DropForeignKey("dbo.ITEMS_HF", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS_HF", "ID_MATERIAL_L3", "dbo.MATERIALS_L3");
            DropForeignKey("dbo.ITEMS_HF", "ID_MATERIAL_L2", "dbo.MATERIALS_L2");
            DropForeignKey("dbo.ITEMS_HF", "ID_MATERIAL_L1", "dbo.MATERIALS_L1");
            DropForeignKey("dbo.ITEMS_HF", "ID_FAMILY_HK", "dbo.FAMILIES_HK");
            DropForeignKey("dbo.ITEMS_HF", "ID_DEFAULT_SUPPLIER", "dbo.SUPPLIERS");
            DropIndex("dbo.ITEMS_HF", new[] { "ID_STATUS_PROD" });
            DropIndex("dbo.ITEMS_HF", new[] { "ID_STATUS_CIAL" });
            DropIndex("dbo.ITEMS_HF", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS_HF", new[] { "ID_MODEL" });
            DropIndex("dbo.ITEMS_HF", new[] { "ID_DEFAULT_SUPPLIER" });
            DropIndex("dbo.ITEMS_HF", new[] { "ID_MATERIAL_L3" });
            DropIndex("dbo.ITEMS_HF", new[] { "ID_MATERIAL_L2" });
            DropIndex("dbo.ITEMS_HF", new[] { "ID_MATERIAL_L1" });
            DropIndex("dbo.ITEMS_HF", new[] { "ID_PROTOTYPE" });
            DropTable("dbo.ITEMS_HF_HISTORY");
            DropTable("dbo.ITEMS_HF");
        }
    }
}
