namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class items_20170426_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DOCS_TYPES",
                c => new
                    {
                        ID_DOC_TYPE = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_DOC_TYPE);
            
            CreateTable(
                "dbo.ITEMS_HW",
                c => new
                    {
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 20),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        ID_HW_TYPE_L1 = c.String(maxLength: 100),
                        ID_HW_TYPE_L2 = c.String(maxLength: 100),
                        ID_HW_TYPE_L3 = c.String(maxLength: 100),
                        ID_DEFAULT_SUPPLIER = c.String(maxLength: 100),
                        ID_PROTOTYPE = c.String(maxLength: 50),
                        ID_FAMILY_HK = c.String(maxLength: 100),
                        ID_ITEM_HK = c.String(maxLength: 20),
                        ITEM_DESCRIPTION = c.String(maxLength: 100),
                        COMMENTS = c.String(maxLength: 2500),
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
                .ForeignKey("dbo.SUPPLIERS", t => t.ID_DEFAULT_SUPPLIER)
                .ForeignKey("dbo.FAMILIES_HK", t => t.ID_FAMILY_HK)
                .ForeignKey("dbo.HWS_TYPE_L1", t => t.ID_HW_TYPE_L1)
                .ForeignKey("dbo.HWS_TYPE_L2", t => t.ID_HW_TYPE_L2)
                .ForeignKey("dbo.HWS_TYPE_L3", t => t.ID_HW_TYPE_L3)
                .ForeignKey("dbo.PROTOTYPES", t => t.ID_PROTOTYPE)
                .ForeignKey("dbo.STATUS_CIAL", t => t.ID_STATUS_CIAL, cascadeDelete: false)
                .ForeignKey("dbo.STATUS_HK", t => t.ID_STATUS_PROD, cascadeDelete: false)
                .Index(t => t.ID_HW_TYPE_L1)
                .Index(t => t.ID_HW_TYPE_L2)
                .Index(t => t.ID_HW_TYPE_L3)
                .Index(t => t.ID_DEFAULT_SUPPLIER)
                .Index(t => t.ID_PROTOTYPE)
                .Index(t => t.ID_FAMILY_HK)
                .Index(t => t.ID_STATUS_CIAL)
                .Index(t => t.ID_STATUS_PROD);
            
            CreateTable(
                "dbo.PROTOTYPES",
                c => new
                    {
                        ID_PROTOTYPE = c.String(nullable: false, maxLength: 50),
                        PROTOTYPE_NAME = c.String(maxLength: 100),
                        PROTOTYPE_DESCRIPTION = c.String(maxLength: 100),
                        PROTOTYPE_STATUS = c.Int(nullable: false),
                        ID_DEFAULT_SUPPLIER = c.String(maxLength: 100),
                        CALIBER = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        LAUNCH_DATE = c.DateTime(),
                        CREATE_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID_PROTOTYPE)
                .ForeignKey("dbo.SUPPLIERS", t => t.ID_DEFAULT_SUPPLIER)
                .Index(t => t.ID_DEFAULT_SUPPLIER);
            
            CreateTable(
                "dbo.ITEMS_HW_HISTORY",
                c => new
                    {
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 20),
                        ID_HW_TYPE_L1 = c.String(maxLength: 100),
                        ID_HW_TYPE_L2 = c.String(maxLength: 100),
                        ID_HW_TYPE_L3 = c.String(maxLength: 100),
                        ID_DEFAULT_SUPPLIER = c.String(maxLength: 100),
                        ID_PROTOTYPE = c.String(maxLength: 50),
                        ID_FAMILY_HK = c.String(maxLength: 100),
                        ID_ITEM_HK = c.String(maxLength: 20),
                        ITEM_DESCRIPTION = c.String(maxLength: 100),
                        COMMENTS = c.String(maxLength: 2500),
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
                        USER = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => new { t.ID_VER, t.ID_SUBVER, t.TIMESTAMP, t.ID_ITEM_BCN })
                .ForeignKey("dbo.SUPPLIERS", t => t.ID_DEFAULT_SUPPLIER)
                .ForeignKey("dbo.FAMILIES_HK", t => t.ID_FAMILY_HK)
                .ForeignKey("dbo.HWS_TYPE_L1", t => t.ID_HW_TYPE_L1)
                .ForeignKey("dbo.HWS_TYPE_L2", t => t.ID_HW_TYPE_L2)
                .ForeignKey("dbo.HWS_TYPE_L3", t => t.ID_HW_TYPE_L3)
                .ForeignKey("dbo.PROTOTYPES", t => t.ID_PROTOTYPE)
                .ForeignKey("dbo.STATUS_CIAL", t => t.ID_STATUS_CIAL, cascadeDelete: false)
                .ForeignKey("dbo.STATUS_HK", t => t.ID_STATUS_PROD, cascadeDelete: false)
                .Index(t => t.ID_HW_TYPE_L1)
                .Index(t => t.ID_HW_TYPE_L2)
                .Index(t => t.ID_HW_TYPE_L3)
                .Index(t => t.ID_DEFAULT_SUPPLIER)
                .Index(t => t.ID_PROTOTYPE)
                .Index(t => t.ID_FAMILY_HK)
                .Index(t => t.ID_STATUS_CIAL)
                .Index(t => t.ID_STATUS_PROD);
            
            CreateTable(
                "dbo.ITEMS_MT",
                c => new
                    {
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 20),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        ID_MAT_TYPE_L1 = c.String(maxLength: 100),
                        ID_MAT_TYPE_L2 = c.String(maxLength: 100),
                        ID_MAT_TYPE_L3 = c.String(maxLength: 100),
                        ID_DEFAULT_SUPPLIER = c.String(maxLength: 100),
                        ID_PROTOTYPE = c.String(maxLength: 50),
                        ID_FAMILY_HK = c.String(maxLength: 100),
                        ID_ITEM_HK = c.String(maxLength: 20),
                        ITEM_DESCRIPTION = c.String(maxLength: 100),
                        COMMENTS = c.String(maxLength: 2500),
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
                .ForeignKey("dbo.SUPPLIERS", t => t.ID_DEFAULT_SUPPLIER)
                .ForeignKey("dbo.FAMILIES_HK", t => t.ID_FAMILY_HK)
                .ForeignKey("dbo.MAT_TYPE_L1", t => t.ID_MAT_TYPE_L1)
                .ForeignKey("dbo.MAT_TYPE_L2", t => t.ID_MAT_TYPE_L2)
                .ForeignKey("dbo.MAT_TYPE_L3", t => t.ID_MAT_TYPE_L3)
                .ForeignKey("dbo.PROTOTYPES", t => t.ID_PROTOTYPE)
                .ForeignKey("dbo.STATUS_CIAL", t => t.ID_STATUS_CIAL, cascadeDelete: false)
                .ForeignKey("dbo.STATUS_HK", t => t.ID_STATUS_PROD, cascadeDelete: false)
                .Index(t => t.ID_MAT_TYPE_L1)
                .Index(t => t.ID_MAT_TYPE_L2)
                .Index(t => t.ID_MAT_TYPE_L3)
                .Index(t => t.ID_DEFAULT_SUPPLIER)
                .Index(t => t.ID_PROTOTYPE)
                .Index(t => t.ID_FAMILY_HK)
                .Index(t => t.ID_STATUS_CIAL)
                .Index(t => t.ID_STATUS_PROD);
            
            CreateTable(
                "dbo.ITEMS_MT_HISTORY",
                c => new
                    {
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 20),
                        ID_MAT_TYPE_L1 = c.String(maxLength: 100),
                        ID_MAT_TYPE_L2 = c.String(maxLength: 100),
                        ID_MAT_TYPE_L3 = c.String(maxLength: 100),
                        ID_DEFAULT_SUPPLIER = c.String(maxLength: 100),
                        ID_PROTOTYPE = c.String(maxLength: 50),
                        ID_FAMILY_HK = c.String(maxLength: 100),
                        ID_ITEM_HK = c.String(maxLength: 20),
                        ITEM_DESCRIPTION = c.String(maxLength: 100),
                        COMMENTS = c.String(maxLength: 2500),
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
                        USER = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => new { t.ID_VER, t.ID_SUBVER, t.TIMESTAMP, t.ID_ITEM_BCN })
                .ForeignKey("dbo.SUPPLIERS", t => t.ID_DEFAULT_SUPPLIER)
                .ForeignKey("dbo.FAMILIES_HK", t => t.ID_FAMILY_HK)
                .ForeignKey("dbo.MAT_TYPE_L1", t => t.ID_MAT_TYPE_L1)
                .ForeignKey("dbo.MAT_TYPE_L2", t => t.ID_MAT_TYPE_L2)
                .ForeignKey("dbo.MAT_TYPE_L3", t => t.ID_MAT_TYPE_L3)
                .ForeignKey("dbo.PROTOTYPES", t => t.ID_PROTOTYPE)
                .ForeignKey("dbo.STATUS_CIAL", t => t.ID_STATUS_CIAL, cascadeDelete: false)
                .ForeignKey("dbo.STATUS_HK", t => t.ID_STATUS_PROD, cascadeDelete: false)
                .Index(t => t.ID_MAT_TYPE_L1)
                .Index(t => t.ID_MAT_TYPE_L2)
                .Index(t => t.ID_MAT_TYPE_L3)
                .Index(t => t.ID_DEFAULT_SUPPLIER)
                .Index(t => t.ID_PROTOTYPE)
                .Index(t => t.ID_FAMILY_HK)
                .Index(t => t.ID_STATUS_CIAL)
                .Index(t => t.ID_STATUS_PROD);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ITEMS_MT_HISTORY", "ID_STATUS_PROD", "dbo.STATUS_HK");
            DropForeignKey("dbo.ITEMS_MT_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS_MT_HISTORY", "ID_PROTOTYPE", "dbo.PROTOTYPES");
            DropForeignKey("dbo.ITEMS_MT_HISTORY", "ID_MAT_TYPE_L3", "dbo.MAT_TYPE_L3");
            DropForeignKey("dbo.ITEMS_MT_HISTORY", "ID_MAT_TYPE_L2", "dbo.MAT_TYPE_L2");
            DropForeignKey("dbo.ITEMS_MT_HISTORY", "ID_MAT_TYPE_L1", "dbo.MAT_TYPE_L1");
            DropForeignKey("dbo.ITEMS_MT_HISTORY", "ID_FAMILY_HK", "dbo.FAMILIES_HK");
            DropForeignKey("dbo.ITEMS_MT_HISTORY", "ID_DEFAULT_SUPPLIER", "dbo.SUPPLIERS");
            DropForeignKey("dbo.ITEMS_MT", "ID_STATUS_PROD", "dbo.STATUS_HK");
            DropForeignKey("dbo.ITEMS_MT", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS_MT", "ID_PROTOTYPE", "dbo.PROTOTYPES");
            DropForeignKey("dbo.ITEMS_MT", "ID_MAT_TYPE_L3", "dbo.MAT_TYPE_L3");
            DropForeignKey("dbo.ITEMS_MT", "ID_MAT_TYPE_L2", "dbo.MAT_TYPE_L2");
            DropForeignKey("dbo.ITEMS_MT", "ID_MAT_TYPE_L1", "dbo.MAT_TYPE_L1");
            DropForeignKey("dbo.ITEMS_MT", "ID_FAMILY_HK", "dbo.FAMILIES_HK");
            DropForeignKey("dbo.ITEMS_MT", "ID_DEFAULT_SUPPLIER", "dbo.SUPPLIERS");
            DropForeignKey("dbo.ITEMS_HW_HISTORY", "ID_STATUS_PROD", "dbo.STATUS_HK");
            DropForeignKey("dbo.ITEMS_HW_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS_HW_HISTORY", "ID_PROTOTYPE", "dbo.PROTOTYPES");
            DropForeignKey("dbo.ITEMS_HW_HISTORY", "ID_HW_TYPE_L3", "dbo.HWS_TYPE_L3");
            DropForeignKey("dbo.ITEMS_HW_HISTORY", "ID_HW_TYPE_L2", "dbo.HWS_TYPE_L2");
            DropForeignKey("dbo.ITEMS_HW_HISTORY", "ID_HW_TYPE_L1", "dbo.HWS_TYPE_L1");
            DropForeignKey("dbo.ITEMS_HW_HISTORY", "ID_FAMILY_HK", "dbo.FAMILIES_HK");
            DropForeignKey("dbo.ITEMS_HW_HISTORY", "ID_DEFAULT_SUPPLIER", "dbo.SUPPLIERS");
            DropForeignKey("dbo.ITEMS_HW", "ID_STATUS_PROD", "dbo.STATUS_HK");
            DropForeignKey("dbo.ITEMS_HW", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS_HW", "ID_PROTOTYPE", "dbo.PROTOTYPES");
            DropForeignKey("dbo.PROTOTYPES", "ID_DEFAULT_SUPPLIER", "dbo.SUPPLIERS");
            DropForeignKey("dbo.ITEMS_HW", "ID_HW_TYPE_L3", "dbo.HWS_TYPE_L3");
            DropForeignKey("dbo.ITEMS_HW", "ID_HW_TYPE_L2", "dbo.HWS_TYPE_L2");
            DropForeignKey("dbo.ITEMS_HW", "ID_HW_TYPE_L1", "dbo.HWS_TYPE_L1");
            DropForeignKey("dbo.ITEMS_HW", "ID_FAMILY_HK", "dbo.FAMILIES_HK");
            DropForeignKey("dbo.ITEMS_HW", "ID_DEFAULT_SUPPLIER", "dbo.SUPPLIERS");
            DropIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_STATUS_PROD" });
            DropIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_STATUS_CIAL" });
            DropIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_PROTOTYPE" });
            DropIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_DEFAULT_SUPPLIER" });
            DropIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_MAT_TYPE_L3" });
            DropIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_MAT_TYPE_L2" });
            DropIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_MAT_TYPE_L1" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_STATUS_PROD" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_STATUS_CIAL" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_PROTOTYPE" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_DEFAULT_SUPPLIER" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_MAT_TYPE_L3" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_MAT_TYPE_L2" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_MAT_TYPE_L1" });
            DropIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_STATUS_PROD" });
            DropIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_STATUS_CIAL" });
            DropIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_PROTOTYPE" });
            DropIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_DEFAULT_SUPPLIER" });
            DropIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_HW_TYPE_L3" });
            DropIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_HW_TYPE_L2" });
            DropIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_HW_TYPE_L1" });
            DropIndex("dbo.PROTOTYPES", new[] { "ID_DEFAULT_SUPPLIER" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_STATUS_PROD" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_STATUS_CIAL" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_PROTOTYPE" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_DEFAULT_SUPPLIER" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_HW_TYPE_L3" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_HW_TYPE_L2" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_HW_TYPE_L1" });
            DropTable("dbo.ITEMS_MT_HISTORY");
            DropTable("dbo.ITEMS_MT");
            DropTable("dbo.ITEMS_HW_HISTORY");
            DropTable("dbo.PROTOTYPES");
            DropTable("dbo.ITEMS_HW");
            DropTable("dbo.DOCS_TYPES");
        }
    }
}
