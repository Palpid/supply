namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class items_20170426_02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS", "ID_DEFAULT_SUPPLIER", "dbo.SUPPLIERS");
            DropForeignKey("dbo.ITEMS", "ID_FAMILY_HK", "dbo.FAMILIES_HK");
            DropForeignKey("dbo.ITEMS", "ID_HW_TYPE_L1", "dbo.HWS_TYPE_L1");
            DropForeignKey("dbo.ITEMS", "ID_HW_TYPE_L2", "dbo.HWS_TYPE_L2");
            DropForeignKey("dbo.ITEMS", "ID_HW_TYPE_L3", "dbo.HWS_TYPE_L3");
            DropForeignKey("dbo.ITEMS", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropForeignKey("dbo.ITEMS", "ID_MATERIAL_L1", "dbo.MATERIALS_L1");
            DropForeignKey("dbo.ITEMS", "ID_MATERIAL_L2", "dbo.MATERIALS_L2");
            DropForeignKey("dbo.ITEMS", "ID_MATERIAL_L3", "dbo.MATERIALS_L3");
            DropForeignKey("dbo.ITEMS", "ID_MAT_TYPE_L1", "dbo.MAT_TYPE_L1");
            DropForeignKey("dbo.ITEMS", "ID_MAT_TYPE_L2", "dbo.MAT_TYPE_L2");
            DropForeignKey("dbo.ITEMS", "ID_MAT_TYPE_L3", "dbo.MAT_TYPE_L3");
            DropForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS", "ID_STATUS_PROD", "dbo.STATUS_HK");
            DropIndex("dbo.ITEMS", new[] { "ID_ITEM_GROUP" });
            DropIndex("dbo.ITEMS", new[] { "ID_MATERIAL_L1" });
            DropIndex("dbo.ITEMS", new[] { "ID_MATERIAL_L2" });
            DropIndex("dbo.ITEMS", new[] { "ID_MATERIAL_L3" });
            DropIndex("dbo.ITEMS", new[] { "ID_MAT_TYPE_L1" });
            DropIndex("dbo.ITEMS", new[] { "ID_MAT_TYPE_L2" });
            DropIndex("dbo.ITEMS", new[] { "ID_MAT_TYPE_L3" });
            DropIndex("dbo.ITEMS", new[] { "ID_HW_TYPE_L1" });
            DropIndex("dbo.ITEMS", new[] { "ID_HW_TYPE_L2" });
            DropIndex("dbo.ITEMS", new[] { "ID_HW_TYPE_L3" });
            DropIndex("dbo.ITEMS", new[] { "ID_DEFAULT_SUPPLIER" });
            DropIndex("dbo.ITEMS", new[] { "ID_MODEL" });
            DropIndex("dbo.ITEMS", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS", new[] { "ID_COLOR_1" });
            DropIndex("dbo.ITEMS", new[] { "ID_COLOR_2" });
            DropIndex("dbo.ITEMS", new[] { "ID_STATUS_CIAL" });
            DropIndex("dbo.ITEMS", new[] { "ID_STATUS_PROD" });
            CreateTable(
                "dbo.ITEMS_EY",
                c => new
                    {
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 20),
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
                        ID_ITEM_HK = c.String(maxLength: 20),
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
                        PHOTO_URL = c.String(maxLength: 2500),
                    })
                .PrimaryKey(t => t.ID_ITEM_BCN)
                .ForeignKey("dbo.COLORS", t => t.ID_COLOR_1)
                .ForeignKey("dbo.COLORS", t => t.ID_COLOR_2)
                .ForeignKey("dbo.SUPPLIERS", t => t.ID_DEFAULT_SUPPLIER)
                .ForeignKey("dbo.FAMILIES_HK", t => t.ID_FAMILY_HK)
                .ForeignKey("dbo.MATERIALS_L1", t => t.ID_MATERIAL_L1)
                .ForeignKey("dbo.MATERIALS_L2", t => t.ID_MATERIAL_L2)
                .ForeignKey("dbo.MATERIALS_L3", t => t.ID_MATERIAL_L3)
                .ForeignKey("dbo.MODELS", t => t.ID_MODEL)
                .ForeignKey("dbo.PROTOTYPES", t => t.ID_PROTOTYPE)
                .ForeignKey("dbo.STATUS_CIAL", t => t.ID_STATUS_CIAL, cascadeDelete: true)
                .ForeignKey("dbo.STATUS_HK", t => t.ID_STATUS_PROD, cascadeDelete: true)
                .Index(t => t.ID_PROTOTYPE)
                .Index(t => t.ID_MATERIAL_L1)
                .Index(t => t.ID_MATERIAL_L2)
                .Index(t => t.ID_MATERIAL_L3)
                .Index(t => t.ID_DEFAULT_SUPPLIER)
                .Index(t => t.ID_MODEL)
                .Index(t => t.ID_FAMILY_HK)
                .Index(t => t.ID_COLOR_1)
                .Index(t => t.ID_COLOR_2)
                .Index(t => t.ID_STATUS_CIAL)
                .Index(t => t.ID_STATUS_PROD);
            
            CreateTable(
                "dbo.ITEMS_EY_HISTORY",
                c => new
                    {
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 20),
                        ID_PROTOTYPE = c.String(nullable: false, maxLength: 50),
                        ID_MATERIAL_L1 = c.String(maxLength: 100),
                        ID_MATERIAL_L2 = c.String(maxLength: 100),
                        ID_MATERIAL_L3 = c.String(maxLength: 100),
                        ID_DEFAULT_SUPPLIER = c.String(maxLength: 100),
                        ID_MODEL = c.String(maxLength: 100),
                        ID_FAMILY_HK = c.String(maxLength: 100),
                        CALIBER = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        ID_COLOR_1 = c.String(maxLength: 30),
                        ID_COLOR_2 = c.String(maxLength: 30),
                        ID_ITEM_HK = c.String(maxLength: 20),
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
                        PHOTO_URL = c.String(maxLength: 2500),
                    })
                .PrimaryKey(t => new { t.ID_VER, t.ID_SUBVER, t.TIMESTAMP, t.ID_ITEM_BCN, t.ID_PROTOTYPE });
            
            DropTable("dbo.ITEMS");
            DropTable("dbo.ITEMS_HISTORY");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ITEMS_HISTORY",
                c => new
                    {
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        ID_ITEM_GROUP = c.String(nullable: false, maxLength: 100),
                        ID_PROTOTYPE = c.String(nullable: false, maxLength: 50),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 20),
                        PROTOTYPE_NAME = c.String(maxLength: 100),
                        PROTOTYPE_DESCRIPTION = c.String(maxLength: 100),
                        PROTOTYPE_STATUS = c.Int(),
                        ID_MATERIAL_L1 = c.String(maxLength: 100),
                        ID_MATERIAL_L2 = c.String(maxLength: 100),
                        ID_MATERIAL_L3 = c.String(maxLength: 100),
                        ID_MAT_TYPE_L1 = c.String(maxLength: 100),
                        ID_MAT_TYPE_L2 = c.String(maxLength: 100),
                        ID_MAT_TYPE_L3 = c.String(maxLength: 100),
                        ID_HW_TYPE_L1 = c.String(maxLength: 100),
                        ID_HW_TYPE_L2 = c.String(maxLength: 100),
                        ID_HW_TYPE_L3 = c.String(maxLength: 100),
                        ID_DEFAULT_SUPPLIER = c.String(maxLength: 100),
                        ID_MODEL = c.String(maxLength: 100),
                        ID_FAMILY_HK = c.String(maxLength: 100),
                        CALIBER = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        ID_COLOR_1 = c.String(maxLength: 30),
                        ID_COLOR_2 = c.String(maxLength: 30),
                        ID_ITEM_HK = c.String(maxLength: 20),
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
                        PHOTO_URL = c.String(maxLength: 2500),
                    })
                .PrimaryKey(t => new { t.ID_VER, t.ID_SUBVER, t.TIMESTAMP, t.ID_ITEM_GROUP, t.ID_PROTOTYPE, t.ID_ITEM_BCN });
            
            CreateTable(
                "dbo.ITEMS",
                c => new
                    {
                        ID_ITEM_GROUP = c.String(nullable: false, maxLength: 100),
                        ID_PROTOTYPE = c.String(nullable: false, maxLength: 50),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 20),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        PROTOTYPE_NAME = c.String(maxLength: 100),
                        PROTOTYPE_DESCRIPTION = c.String(maxLength: 100),
                        PROTOTYPE_STATUS = c.Int(),
                        ID_MATERIAL_L1 = c.String(maxLength: 100),
                        ID_MATERIAL_L2 = c.String(maxLength: 100),
                        ID_MATERIAL_L3 = c.String(maxLength: 100),
                        ID_MAT_TYPE_L1 = c.String(maxLength: 100),
                        ID_MAT_TYPE_L2 = c.String(maxLength: 100),
                        ID_MAT_TYPE_L3 = c.String(maxLength: 100),
                        ID_HW_TYPE_L1 = c.String(maxLength: 100),
                        ID_HW_TYPE_L2 = c.String(maxLength: 100),
                        ID_HW_TYPE_L3 = c.String(maxLength: 100),
                        ID_DEFAULT_SUPPLIER = c.String(maxLength: 100),
                        ID_MODEL = c.String(maxLength: 100),
                        ID_FAMILY_HK = c.String(maxLength: 100),
                        CALIBER = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        ID_COLOR_1 = c.String(maxLength: 30),
                        ID_COLOR_2 = c.String(maxLength: 30),
                        ID_ITEM_HK = c.String(maxLength: 20),
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
                        PHOTO_URL = c.String(maxLength: 2500),
                    })
                .PrimaryKey(t => new { t.ID_ITEM_GROUP, t.ID_PROTOTYPE, t.ID_ITEM_BCN });
            
            DropForeignKey("dbo.ITEMS_EY", "ID_STATUS_PROD", "dbo.STATUS_HK");
            DropForeignKey("dbo.ITEMS_EY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS_EY", "ID_PROTOTYPE", "dbo.PROTOTYPES");
            DropForeignKey("dbo.ITEMS_EY", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS_EY", "ID_MATERIAL_L3", "dbo.MATERIALS_L3");
            DropForeignKey("dbo.ITEMS_EY", "ID_MATERIAL_L2", "dbo.MATERIALS_L2");
            DropForeignKey("dbo.ITEMS_EY", "ID_MATERIAL_L1", "dbo.MATERIALS_L1");
            DropForeignKey("dbo.ITEMS_EY", "ID_FAMILY_HK", "dbo.FAMILIES_HK");
            DropForeignKey("dbo.ITEMS_EY", "ID_DEFAULT_SUPPLIER", "dbo.SUPPLIERS");
            DropForeignKey("dbo.ITEMS_EY", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_EY", "ID_COLOR_1", "dbo.COLORS");
            DropIndex("dbo.ITEMS_EY", new[] { "ID_STATUS_PROD" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_STATUS_CIAL" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_COLOR_2" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_COLOR_1" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_MODEL" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_DEFAULT_SUPPLIER" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_MATERIAL_L3" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_MATERIAL_L2" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_MATERIAL_L1" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_PROTOTYPE" });
            DropTable("dbo.ITEMS_HISTORY");
            DropTable("dbo.ITEMS_EY");
            CreateIndex("dbo.ITEMS", "ID_STATUS_PROD");
            CreateIndex("dbo.ITEMS", "ID_STATUS_CIAL");
            CreateIndex("dbo.ITEMS", "ID_COLOR_2");
            CreateIndex("dbo.ITEMS", "ID_COLOR_1");
            CreateIndex("dbo.ITEMS", "ID_FAMILY_HK");
            CreateIndex("dbo.ITEMS", "ID_MODEL");
            CreateIndex("dbo.ITEMS", "ID_DEFAULT_SUPPLIER");
            CreateIndex("dbo.ITEMS", "ID_HW_TYPE_L3");
            CreateIndex("dbo.ITEMS", "ID_HW_TYPE_L2");
            CreateIndex("dbo.ITEMS", "ID_HW_TYPE_L1");
            CreateIndex("dbo.ITEMS", "ID_MAT_TYPE_L3");
            CreateIndex("dbo.ITEMS", "ID_MAT_TYPE_L2");
            CreateIndex("dbo.ITEMS", "ID_MAT_TYPE_L1");
            CreateIndex("dbo.ITEMS", "ID_MATERIAL_L3");
            CreateIndex("dbo.ITEMS", "ID_MATERIAL_L2");
            CreateIndex("dbo.ITEMS", "ID_MATERIAL_L1");
            CreateIndex("dbo.ITEMS", "ID_ITEM_GROUP");
            AddForeignKey("dbo.ITEMS", "ID_STATUS_PROD", "dbo.STATUS_HK", "ID_STATUS_PROD", cascadeDelete: true);
            AddForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL", "ID_STATUS_CIAL", cascadeDelete: true);
            AddForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS", "ID_MODEL");
            AddForeignKey("dbo.ITEMS", "ID_MAT_TYPE_L3", "dbo.MAT_TYPE_L3", "ID_MAT_TYPE_L3");
            AddForeignKey("dbo.ITEMS", "ID_MAT_TYPE_L2", "dbo.MAT_TYPE_L2", "ID_MAT_TYPE_L2");
            AddForeignKey("dbo.ITEMS", "ID_MAT_TYPE_L1", "dbo.MAT_TYPE_L1", "ID_MAT_TYPE_L1");
            AddForeignKey("dbo.ITEMS", "ID_MATERIAL_L3", "dbo.MATERIALS_L3", "ID_MATERIAL_L3");
            AddForeignKey("dbo.ITEMS", "ID_MATERIAL_L2", "dbo.MATERIALS_L2", "ID_MATERIAL_L2");
            AddForeignKey("dbo.ITEMS", "ID_MATERIAL_L1", "dbo.MATERIALS_L1", "ID_MATERIAL_L1");
            AddForeignKey("dbo.ITEMS", "ID_ITEM_GROUP", "dbo.ITEM_GROUP", "ID_ITEM_GROUP", cascadeDelete: true);
            AddForeignKey("dbo.ITEMS", "ID_HW_TYPE_L3", "dbo.HWS_TYPE_L3", "ID_HW_TYPE_L3");
            AddForeignKey("dbo.ITEMS", "ID_HW_TYPE_L2", "dbo.HWS_TYPE_L2", "ID_HW_TYPE_L2");
            AddForeignKey("dbo.ITEMS", "ID_HW_TYPE_L1", "dbo.HWS_TYPE_L1", "ID_HW_TYPE_L1");
            AddForeignKey("dbo.ITEMS", "ID_FAMILY_HK", "dbo.FAMILIES_HK", "ID_FAMILY_HK");
            AddForeignKey("dbo.ITEMS", "ID_DEFAULT_SUPPLIER", "dbo.SUPPLIERS", "ID_SUPPLIER");
            AddForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR");
        }
    }
}
