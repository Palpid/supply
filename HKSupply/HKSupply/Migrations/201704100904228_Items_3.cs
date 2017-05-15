namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Items_3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ITEMS");
            DropPrimaryKey("dbo.SUPPLIERS");
            CreateTable(
                "dbo.COLORS",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 30),
                        DESCRIPTION = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FAMILY_HK",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 30),
                        DESCRIPTION = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ITEM_GROUP",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 30),
                        DESCRIPTION = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MODELS",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 30),
                        DESCRIPTION = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.STATUS_CIAL",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 30),
                        DESCRIPTION = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.STATUS_PROD",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 30),
                        DESCRIPTION = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ITEMS", "ID_PROTOTYPE", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.ITEMS", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.ITEMS", "ID_ITEM_GROUP", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.ITEMS", "PROTOTYPE_NAME", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "PROTOTYPE_DESCRIPTION", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "PROTOTYPE_STATUS", c => c.Int(nullable: false));
            AddColumn("dbo.ITEMS", "ID_EY1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_EY2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_EY3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_MAT1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_MAT2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_MAT3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_HW1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_HW2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_HW3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_DEFAULT_SUPPLIER", c => c.String(maxLength: 20));
            AddColumn("dbo.ITEMS", "ID_MODEL", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS", "ID_FAMILY_HK", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS", "ID_COLOR_1", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS", "ID_COLOR_2", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS", "ID_ITEM_HK", c => c.String(maxLength: 20));
            AddColumn("dbo.ITEMS", "ITEM_DESCRIPTION", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "COMMENTS", c => c.String(maxLength: 2500));
            AddColumn("dbo.ITEMS", "SEGMENT", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS", "CATEGORY", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "AGE", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "LAUNCHED_DATE", c => c.DateTime());
            AddColumn("dbo.ITEMS", "REMOVAL_DATE", c => c.DateTime());
            AddColumn("dbo.ITEMS", "ID_STATUS_CIAL", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS", "ID_STATUS_PROD", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS", "ID_USER_ATTRI_1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_USER_ATTRI_2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_USER_ATTRI_3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "UNIT", c => c.String(maxLength: 2));
            AddColumn("dbo.ITEMS", "CREATE_DATE", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SUPPLIERS", "ID_SUPPLIER", c => c.String(nullable: false, maxLength: 20));
            AddPrimaryKey("dbo.ITEMS", new[] { "ID_VER", "ID_SUBVER", "ID_PROTOTYPE", "ID_ITEM_BCN", "ID_ITEM_GROUP" });
            AddPrimaryKey("dbo.SUPPLIERS", new[] { "ID_SUPPLIER", "ID_VER", "ID_SUBVER" });
            CreateIndex("dbo.ITEMS", "ID_ITEM_GROUP");
            CreateIndex("dbo.ITEMS", "ID_MODEL");
            CreateIndex("dbo.ITEMS", "ID_FAMILY_HK");
            CreateIndex("dbo.ITEMS", "ID_COLOR_1");
            CreateIndex("dbo.ITEMS", "ID_COLOR_2");
            CreateIndex("dbo.ITEMS", "ID_STATUS_CIAL");
            CreateIndex("dbo.ITEMS", "ID_STATUS_PROD");
            AddForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS", "ID");
            AddForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS", "ID");
            AddForeignKey("dbo.ITEMS", "ID_FAMILY_HK", "dbo.FAMILY_HK", "ID");
            AddForeignKey("dbo.ITEMS", "ID_ITEM_GROUP", "dbo.ITEM_GROUP", "ID", cascadeDelete: false);
            AddForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS", "ID");
            AddForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL", "ID");
            AddForeignKey("dbo.ITEMS", "ID_STATUS_PROD", "dbo.STATUS_PROD", "ID");
            DropColumn("dbo.ITEMS", "ITEM_CODE");
            DropColumn("dbo.ITEMS", "ITEM_NAME");
            DropColumn("dbo.ITEMS", "MODEL");
            DropColumn("dbo.ITEMS", "ACTIVE");
            DropColumn("dbo.ITEMS", "ID_STATUS");
            DropColumn("dbo.ITEMS", "LAUNCHED");
            DropColumn("dbo.ITEMS", "RETIRED");
            DropColumn("dbo.ITEMS", "MM_FRONT");
            DropColumn("dbo.ITEMS", "SIZE");
            DropColumn("dbo.ITEMS", "CATEGORY_NAME");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ITEMS", "CATEGORY_NAME", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS", "SIZE", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS", "MM_FRONT", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.ITEMS", "RETIRED", c => c.DateTime());
            AddColumn("dbo.ITEMS", "LAUNCHED", c => c.DateTime());
            AddColumn("dbo.ITEMS", "ID_STATUS", c => c.Int(nullable: false));
            AddColumn("dbo.ITEMS", "ACTIVE", c => c.Boolean(nullable: false));
            AddColumn("dbo.ITEMS", "MODEL", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.ITEMS", "ITEM_NAME", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.ITEMS", "ITEM_CODE", c => c.String(nullable: false, maxLength: 20));
            DropForeignKey("dbo.ITEMS", "ID_STATUS_PROD", "dbo.STATUS_PROD");
            DropForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropForeignKey("dbo.ITEMS", "ID_FAMILY_HK", "dbo.FAMILY_HK");
            DropForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS");
            DropIndex("dbo.ITEMS", new[] { "ID_STATUS_PROD" });
            DropIndex("dbo.ITEMS", new[] { "ID_STATUS_CIAL" });
            DropIndex("dbo.ITEMS", new[] { "ID_COLOR_2" });
            DropIndex("dbo.ITEMS", new[] { "ID_COLOR_1" });
            DropIndex("dbo.ITEMS", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS", new[] { "ID_MODEL" });
            DropIndex("dbo.ITEMS", new[] { "ID_ITEM_GROUP" });
            DropPrimaryKey("dbo.SUPPLIERS");
            DropPrimaryKey("dbo.ITEMS");
            AlterColumn("dbo.SUPPLIERS", "ID_SUPPLIER", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.ITEMS", "CREATE_DATE");
            DropColumn("dbo.ITEMS", "UNIT");
            DropColumn("dbo.ITEMS", "ID_USER_ATTRI_3");
            DropColumn("dbo.ITEMS", "ID_USER_ATTRI_2");
            DropColumn("dbo.ITEMS", "ID_USER_ATTRI_1");
            DropColumn("dbo.ITEMS", "ID_STATUS_PROD");
            DropColumn("dbo.ITEMS", "ID_STATUS_CIAL");
            DropColumn("dbo.ITEMS", "REMOVAL_DATE");
            DropColumn("dbo.ITEMS", "LAUNCHED_DATE");
            DropColumn("dbo.ITEMS", "AGE");
            DropColumn("dbo.ITEMS", "CATEGORY");
            DropColumn("dbo.ITEMS", "SEGMENT");
            DropColumn("dbo.ITEMS", "COMMENTS");
            DropColumn("dbo.ITEMS", "ITEM_DESCRIPTION");
            DropColumn("dbo.ITEMS", "ID_ITEM_HK");
            DropColumn("dbo.ITEMS", "ID_COLOR_2");
            DropColumn("dbo.ITEMS", "ID_COLOR_1");
            DropColumn("dbo.ITEMS", "ID_FAMILY_HK");
            DropColumn("dbo.ITEMS", "ID_MODEL");
            DropColumn("dbo.ITEMS", "ID_DEFAULT_SUPPLIER");
            DropColumn("dbo.ITEMS", "ID_HW3");
            DropColumn("dbo.ITEMS", "ID_HW2");
            DropColumn("dbo.ITEMS", "ID_HW1");
            DropColumn("dbo.ITEMS", "ID_MAT3");
            DropColumn("dbo.ITEMS", "ID_MAT2");
            DropColumn("dbo.ITEMS", "ID_MAT1");
            DropColumn("dbo.ITEMS", "ID_EY3");
            DropColumn("dbo.ITEMS", "ID_EY2");
            DropColumn("dbo.ITEMS", "ID_EY1");
            DropColumn("dbo.ITEMS", "PROTOTYPE_STATUS");
            DropColumn("dbo.ITEMS", "PROTOTYPE_DESCRIPTION");
            DropColumn("dbo.ITEMS", "PROTOTYPE_NAME");
            DropColumn("dbo.ITEMS", "ID_ITEM_GROUP");
            DropColumn("dbo.ITEMS", "ID_ITEM_BCN");
            DropColumn("dbo.ITEMS", "ID_PROTOTYPE");
            DropTable("dbo.STATUS_PROD");
            DropTable("dbo.STATUS_CIAL");
            DropTable("dbo.MODELS");
            DropTable("dbo.ITEM_GROUP");
            DropTable("dbo.FAMILY_HK");
            DropTable("dbo.COLORS");
            AddPrimaryKey("dbo.SUPPLIERS", new[] { "ID_SUPPLIER", "ID_VER", "ID_SUBVER" });
            AddPrimaryKey("dbo.ITEMS", new[] { "ITEM_CODE", "ID_VER", "ID_SUBVER" });
        }
    }
}
