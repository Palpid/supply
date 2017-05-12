namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Items_32_ItemHistory : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ITEMS_HISTORY");
            AddColumn("dbo.ITEMS_HISTORY", "ID_PROTOTYPE", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.ITEMS_HISTORY", "ID_ITEM_BCN", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.ITEMS_HISTORY", "ID_ITEM_GROUP", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "PROTOTYPE_NAME", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "PROTOTYPE_DESCRIPTION", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "PROTOTYPE_STATUS", c => c.Int());
            AddColumn("dbo.ITEMS_HISTORY", "ID_EY1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_EY2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_EY3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_MAT1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_MAT2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_MAT3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_HW1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_HW2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_HW3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_DEFAULT_SUPPLIER", c => c.String(maxLength: 20));
            AddColumn("dbo.ITEMS_HISTORY", "ID_MODEL", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "ID_FAMILY_HK", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "ID_COLOR_1", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "ID_COLOR_2", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "ID_ITEM_HK", c => c.String(maxLength: 20));
            AddColumn("dbo.ITEMS_HISTORY", "ITEM_DESCRIPTION", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "COMMENTS", c => c.String(maxLength: 2500));
            AddColumn("dbo.ITEMS_HISTORY", "SEGMENT", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "CATEGORY", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "AGE", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "LAUNCHED_DATE", c => c.DateTime());
            AddColumn("dbo.ITEMS_HISTORY", "REMOVAL_DATE", c => c.DateTime());
            AddColumn("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "ID_STATUS_PROD", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "ID_USER_ATTRI_1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_USER_ATTRI_2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_USER_ATTRI_3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "UNIT", c => c.String(maxLength: 2));
            AddColumn("dbo.ITEMS_HISTORY", "CREATE_DATE", c => c.DateTime(nullable: false));
            AddColumn("dbo.ITEMS_HISTORY", "USER", c => c.String(maxLength: 20));
            AlterColumn("dbo.ITEMS", "PROTOTYPE_STATUS", c => c.Int());
            AddPrimaryKey("dbo.ITEMS_HISTORY", new[] { "ID_VER", "ID_SUBVER", "ID_PROTOTYPE", "ID_ITEM_BCN", "ID_ITEM_GROUP" });
            CreateIndex("dbo.ITEMS_HISTORY", "ID_ITEM_GROUP");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_MODEL");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_FAMILY_HK");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_COLOR_1");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_COLOR_2");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_STATUS_PROD");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_1", "dbo.COLORS", "ID");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_2", "dbo.COLORS", "ID");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_FAMILY_HK", "dbo.FAMILY_HK", "ID");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_ITEM_GROUP", "dbo.ITEM_GROUP", "ID", cascadeDelete: false);
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_MODEL", "dbo.MODELS", "ID");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL", "ID");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_PROD", "dbo.STATUS_PROD", "ID");
            DropColumn("dbo.ITEMS_HISTORY", "ITEM_CODE");
            DropColumn("dbo.ITEMS_HISTORY", "ITEM_NAME");
            DropColumn("dbo.ITEMS_HISTORY", "MODEL");
            DropColumn("dbo.ITEMS_HISTORY", "ACTIVE");
            DropColumn("dbo.ITEMS_HISTORY", "ID_STATUS");
            DropColumn("dbo.ITEMS_HISTORY", "LAUNCHED");
            DropColumn("dbo.ITEMS_HISTORY", "RETIRED");
            DropColumn("dbo.ITEMS_HISTORY", "MM_FRONT");
            DropColumn("dbo.ITEMS_HISTORY", "SIZE");
            DropColumn("dbo.ITEMS_HISTORY", "CATEGORY_NAME");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ITEMS_HISTORY", "CATEGORY_NAME", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "SIZE", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "MM_FRONT", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.ITEMS_HISTORY", "RETIRED", c => c.DateTime());
            AddColumn("dbo.ITEMS_HISTORY", "LAUNCHED", c => c.DateTime());
            AddColumn("dbo.ITEMS_HISTORY", "ID_STATUS", c => c.Int(nullable: false));
            AddColumn("dbo.ITEMS_HISTORY", "ACTIVE", c => c.Boolean(nullable: false));
            AddColumn("dbo.ITEMS_HISTORY", "MODEL", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ITEM_NAME", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ITEM_CODE", c => c.String(nullable: false, maxLength: 20));
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_PROD", "dbo.STATUS_PROD");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_FAMILY_HK", "dbo.FAMILY_HK");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_1", "dbo.COLORS");
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_STATUS_PROD" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_STATUS_CIAL" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_COLOR_2" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_COLOR_1" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_MODEL" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_ITEM_GROUP" });
            DropPrimaryKey("dbo.ITEMS_HISTORY");
            AlterColumn("dbo.ITEMS", "PROTOTYPE_STATUS", c => c.Int(nullable: false));
            DropColumn("dbo.ITEMS_HISTORY", "USER");
            DropColumn("dbo.ITEMS_HISTORY", "CREATE_DATE");
            DropColumn("dbo.ITEMS_HISTORY", "UNIT");
            DropColumn("dbo.ITEMS_HISTORY", "ID_USER_ATTRI_3");
            DropColumn("dbo.ITEMS_HISTORY", "ID_USER_ATTRI_2");
            DropColumn("dbo.ITEMS_HISTORY", "ID_USER_ATTRI_1");
            DropColumn("dbo.ITEMS_HISTORY", "ID_STATUS_PROD");
            DropColumn("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL");
            DropColumn("dbo.ITEMS_HISTORY", "REMOVAL_DATE");
            DropColumn("dbo.ITEMS_HISTORY", "LAUNCHED_DATE");
            DropColumn("dbo.ITEMS_HISTORY", "AGE");
            DropColumn("dbo.ITEMS_HISTORY", "CATEGORY");
            DropColumn("dbo.ITEMS_HISTORY", "SEGMENT");
            DropColumn("dbo.ITEMS_HISTORY", "COMMENTS");
            DropColumn("dbo.ITEMS_HISTORY", "ITEM_DESCRIPTION");
            DropColumn("dbo.ITEMS_HISTORY", "ID_ITEM_HK");
            DropColumn("dbo.ITEMS_HISTORY", "ID_COLOR_2");
            DropColumn("dbo.ITEMS_HISTORY", "ID_COLOR_1");
            DropColumn("dbo.ITEMS_HISTORY", "ID_FAMILY_HK");
            DropColumn("dbo.ITEMS_HISTORY", "ID_MODEL");
            DropColumn("dbo.ITEMS_HISTORY", "ID_DEFAULT_SUPPLIER");
            DropColumn("dbo.ITEMS_HISTORY", "ID_HW3");
            DropColumn("dbo.ITEMS_HISTORY", "ID_HW2");
            DropColumn("dbo.ITEMS_HISTORY", "ID_HW1");
            DropColumn("dbo.ITEMS_HISTORY", "ID_MAT3");
            DropColumn("dbo.ITEMS_HISTORY", "ID_MAT2");
            DropColumn("dbo.ITEMS_HISTORY", "ID_MAT1");
            DropColumn("dbo.ITEMS_HISTORY", "ID_EY3");
            DropColumn("dbo.ITEMS_HISTORY", "ID_EY2");
            DropColumn("dbo.ITEMS_HISTORY", "ID_EY1");
            DropColumn("dbo.ITEMS_HISTORY", "PROTOTYPE_STATUS");
            DropColumn("dbo.ITEMS_HISTORY", "PROTOTYPE_DESCRIPTION");
            DropColumn("dbo.ITEMS_HISTORY", "PROTOTYPE_NAME");
            DropColumn("dbo.ITEMS_HISTORY", "ID_ITEM_GROUP");
            DropColumn("dbo.ITEMS_HISTORY", "ID_ITEM_BCN");
            DropColumn("dbo.ITEMS_HISTORY", "ID_PROTOTYPE");
            AddPrimaryKey("dbo.ITEMS_HISTORY", new[] { "ITEM_CODE", "ID_VER", "ID_SUBVER" });
        }
    }
}
