namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Items_12042017_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS", "ID_FAMILY_HK", "dbo.FAMILIES_HK");
            DropForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS", "ID_STATUS_PROD", "dbo.STATUS_HK");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_FAMILY_HK", "dbo.FAMILIES_HK");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_PROD", "dbo.STATUS_HK");
            DropForeignKey("dbo.ITEMS", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropIndex("dbo.ITEMS", new[] { "ID_ITEM_GROUP" });
            DropIndex("dbo.ITEMS", new[] { "ID_MODEL" });
            DropIndex("dbo.ITEMS", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS", new[] { "ID_COLOR_1" });
            DropIndex("dbo.ITEMS", new[] { "ID_COLOR_2" });
            DropIndex("dbo.ITEMS", new[] { "ID_STATUS_CIAL" });
            DropIndex("dbo.ITEMS", new[] { "ID_STATUS_PROD" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_MODEL" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_COLOR_1" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_COLOR_2" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_STATUS_CIAL" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_STATUS_PROD" });
            RenameColumn(table: "dbo.ITEM_GROUP", name: "ID", newName: "ID_ITEM_GROUP");
            DropPrimaryKey("dbo.ITEM_GROUP");
            DropPrimaryKey("dbo.ITEMS");
            DropPrimaryKey("dbo.ITEMS_HISTORY");
            AlterColumn("dbo.ITEM_GROUP", "ID_ITEM_GROUP", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ITEM_GROUP", "DESCRIPTION", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.ITEMS", "ID_ITEM_GROUP", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ITEMS_HISTORY", "ID_ITEM_GROUP", c => c.String(nullable: false, maxLength: 100));
            AddPrimaryKey("dbo.ITEM_GROUP", "ID_ITEM_GROUP");
            AddPrimaryKey("dbo.ITEMS", new[] { "ID_ITEM_GROUP", "ID_PROTOTYPE", "ID_ITEM_BCN" });
            AddPrimaryKey("dbo.ITEMS_HISTORY", new[] { "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_ITEM_GROUP", "ID_PROTOTYPE", "ID_ITEM_BCN" });
            CreateIndex("dbo.ITEMS", "ID_ITEM_GROUP");
            AddForeignKey("dbo.ITEMS", "ID_ITEM_GROUP", "dbo.ITEM_GROUP", "ID_ITEM_GROUP", cascadeDelete: false);
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_ITEM_GROUP", "dbo.ITEM_GROUP", "ID_ITEM_GROUP", cascadeDelete: false);
            DropColumn("dbo.ITEMS", "ID_EY1");
            DropColumn("dbo.ITEMS", "ID_EY2");
            DropColumn("dbo.ITEMS", "ID_EY3");
            DropColumn("dbo.ITEMS", "ID_MAT1");
            DropColumn("dbo.ITEMS", "ID_MAT2");
            DropColumn("dbo.ITEMS", "ID_MAT3");
            DropColumn("dbo.ITEMS", "ID_HW1");
            DropColumn("dbo.ITEMS", "ID_HW2");
            DropColumn("dbo.ITEMS", "ID_HW3");
            DropColumn("dbo.ITEMS", "ID_DEFAULT_SUPPLIER");
            DropColumn("dbo.ITEMS", "ID_MODEL");
            DropColumn("dbo.ITEMS", "ID_FAMILY_HK");
            DropColumn("dbo.ITEMS", "CALIBER");
            DropColumn("dbo.ITEMS", "ID_COLOR_1");
            DropColumn("dbo.ITEMS", "ID_COLOR_2");
            DropColumn("dbo.ITEMS", "ID_ITEM_HK");
            DropColumn("dbo.ITEMS", "ITEM_DESCRIPTION");
            DropColumn("dbo.ITEMS", "COMMENTS");
            DropColumn("dbo.ITEMS", "SEGMENT");
            DropColumn("dbo.ITEMS", "CATEGORY");
            DropColumn("dbo.ITEMS", "AGE");
            DropColumn("dbo.ITEMS", "LAUNCH_DATE");
            DropColumn("dbo.ITEMS", "REMOVAL_DATE");
            DropColumn("dbo.ITEMS", "ID_STATUS_CIAL");
            DropColumn("dbo.ITEMS", "ID_STATUS_PROD");
            DropColumn("dbo.ITEMS", "ID_USER_ATTRI_1");
            DropColumn("dbo.ITEMS", "ID_USER_ATTRI_2");
            DropColumn("dbo.ITEMS", "ID_USER_ATTRI_3");
            DropColumn("dbo.ITEMS", "UNIT");
            DropColumn("dbo.ITEMS", "CREATE_DATE");
            DropColumn("dbo.ITEMS_HISTORY", "ID_EY1");
            DropColumn("dbo.ITEMS_HISTORY", "ID_EY2");
            DropColumn("dbo.ITEMS_HISTORY", "ID_EY3");
            DropColumn("dbo.ITEMS_HISTORY", "ID_MAT1");
            DropColumn("dbo.ITEMS_HISTORY", "ID_MAT2");
            DropColumn("dbo.ITEMS_HISTORY", "ID_MAT3");
            DropColumn("dbo.ITEMS_HISTORY", "ID_HW1");
            DropColumn("dbo.ITEMS_HISTORY", "ID_HW2");
            DropColumn("dbo.ITEMS_HISTORY", "ID_HW3");
            DropColumn("dbo.ITEMS_HISTORY", "ID_DEFAULT_SUPPLIER");
            DropColumn("dbo.ITEMS_HISTORY", "ID_MODEL");
            DropColumn("dbo.ITEMS_HISTORY", "ID_FAMILY_HK");
            DropColumn("dbo.ITEMS_HISTORY", "CALIBER");
            DropColumn("dbo.ITEMS_HISTORY", "ID_COLOR_1");
            DropColumn("dbo.ITEMS_HISTORY", "ID_COLOR_2");
            DropColumn("dbo.ITEMS_HISTORY", "ID_ITEM_HK");
            DropColumn("dbo.ITEMS_HISTORY", "ITEM_DESCRIPTION");
            DropColumn("dbo.ITEMS_HISTORY", "COMMENTS");
            DropColumn("dbo.ITEMS_HISTORY", "SEGMENT");
            DropColumn("dbo.ITEMS_HISTORY", "CATEGORY");
            DropColumn("dbo.ITEMS_HISTORY", "AGE");
            DropColumn("dbo.ITEMS_HISTORY", "LAUNCH_DATE");
            DropColumn("dbo.ITEMS_HISTORY", "REMOVAL_DATE");
            DropColumn("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL");
            DropColumn("dbo.ITEMS_HISTORY", "ID_STATUS_PROD");
            DropColumn("dbo.ITEMS_HISTORY", "ID_USER_ATTRI_1");
            DropColumn("dbo.ITEMS_HISTORY", "ID_USER_ATTRI_2");
            DropColumn("dbo.ITEMS_HISTORY", "ID_USER_ATTRI_3");
            DropColumn("dbo.ITEMS_HISTORY", "UNIT");
            DropColumn("dbo.ITEMS_HISTORY", "CREATE_DATE");
            DropColumn("dbo.ITEMS_HISTORY", "USER");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ITEMS_HISTORY", "USER", c => c.String(maxLength: 20));
            AddColumn("dbo.ITEMS_HISTORY", "CREATE_DATE", c => c.DateTime(nullable: false));
            AddColumn("dbo.ITEMS_HISTORY", "UNIT", c => c.String(maxLength: 2));
            AddColumn("dbo.ITEMS_HISTORY", "ID_USER_ATTRI_3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_USER_ATTRI_2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_USER_ATTRI_1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_STATUS_PROD", c => c.Int(nullable: false));
            AddColumn("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", c => c.Int(nullable: false));
            AddColumn("dbo.ITEMS_HISTORY", "REMOVAL_DATE", c => c.DateTime());
            AddColumn("dbo.ITEMS_HISTORY", "LAUNCH_DATE", c => c.DateTime());
            AddColumn("dbo.ITEMS_HISTORY", "AGE", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "CATEGORY", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "SEGMENT", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "COMMENTS", c => c.String(maxLength: 2500));
            AddColumn("dbo.ITEMS_HISTORY", "ITEM_DESCRIPTION", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_ITEM_HK", c => c.String(maxLength: 20));
            AddColumn("dbo.ITEMS_HISTORY", "ID_COLOR_2", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "ID_COLOR_1", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "CALIBER", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.ITEMS_HISTORY", "ID_FAMILY_HK", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_MODEL", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS_HISTORY", "ID_DEFAULT_SUPPLIER", c => c.String(maxLength: 3));
            AddColumn("dbo.ITEMS_HISTORY", "ID_HW3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_HW2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_HW1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_MAT3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_MAT2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_MAT1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_EY3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_EY2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HISTORY", "ID_EY1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "CREATE_DATE", c => c.DateTime(nullable: false));
            AddColumn("dbo.ITEMS", "UNIT", c => c.String(maxLength: 2));
            AddColumn("dbo.ITEMS", "ID_USER_ATTRI_3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_USER_ATTRI_2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_USER_ATTRI_1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_STATUS_PROD", c => c.Int(nullable: false));
            AddColumn("dbo.ITEMS", "ID_STATUS_CIAL", c => c.Int(nullable: false));
            AddColumn("dbo.ITEMS", "REMOVAL_DATE", c => c.DateTime());
            AddColumn("dbo.ITEMS", "LAUNCH_DATE", c => c.DateTime());
            AddColumn("dbo.ITEMS", "AGE", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "CATEGORY", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "SEGMENT", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS", "COMMENTS", c => c.String(maxLength: 2500));
            AddColumn("dbo.ITEMS", "ITEM_DESCRIPTION", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_ITEM_HK", c => c.String(maxLength: 20));
            AddColumn("dbo.ITEMS", "ID_COLOR_2", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS", "ID_COLOR_1", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS", "CALIBER", c => c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"));
            AddColumn("dbo.ITEMS", "ID_FAMILY_HK", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_MODEL", c => c.String(maxLength: 30));
            AddColumn("dbo.ITEMS", "ID_DEFAULT_SUPPLIER", c => c.String(maxLength: 3));
            AddColumn("dbo.ITEMS", "ID_HW3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_HW2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_HW1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_MAT3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_MAT2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_MAT1", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_EY3", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_EY2", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS", "ID_EY1", c => c.String(maxLength: 100));
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropForeignKey("dbo.ITEMS", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropIndex("dbo.ITEMS", new[] { "ID_ITEM_GROUP" });
            DropPrimaryKey("dbo.ITEMS_HISTORY");
            DropPrimaryKey("dbo.ITEMS");
            DropPrimaryKey("dbo.ITEM_GROUP");
            AlterColumn("dbo.ITEMS", "ID_ITEM_GROUP", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.ITEM_GROUP", "DESCRIPTION", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ITEM_GROUP", "ID_ITEM_GROUP", c => c.String(nullable: false, maxLength: 30));
            AddPrimaryKey("dbo.ITEMS_HISTORY", new[] { "ID_VER", "ID_SUBVER", "ID_PROTOTYPE", "ID_ITEM_BCN", "ID_ITEM_GROUP" });
            AddPrimaryKey("dbo.ITEMS", new[] { "ID_VER", "ID_SUBVER", "ID_PROTOTYPE", "ID_ITEM_BCN", "ID_ITEM_GROUP" });
            AddPrimaryKey("dbo.ITEM_GROUP", "ID");
            RenameColumn(table: "dbo.ITEM_GROUP", name: "ID_ITEM_GROUP", newName: "ID");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_STATUS_PROD");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_COLOR_2");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_COLOR_1");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_FAMILY_HK");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_MODEL");
            CreateIndex("dbo.ITEMS", "ID_STATUS_PROD");
            CreateIndex("dbo.ITEMS", "ID_STATUS_CIAL");
            CreateIndex("dbo.ITEMS", "ID_COLOR_2");
            CreateIndex("dbo.ITEMS", "ID_COLOR_1");
            CreateIndex("dbo.ITEMS", "ID_FAMILY_HK");
            CreateIndex("dbo.ITEMS", "ID_MODEL");
            CreateIndex("dbo.ITEMS", "ID_ITEM_GROUP");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_ITEM_GROUP", "dbo.ITEM_GROUP", "ID", cascadeDelete: false);
            AddForeignKey("dbo.ITEMS", "ID_ITEM_GROUP", "dbo.ITEM_GROUP", "ID", cascadeDelete: false);
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_PROD", "dbo.STATUS_HK", "ID_STATUS_PROD", cascadeDelete: false);
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL", "ID_STATUS_CIAL", cascadeDelete: false);
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_MODEL", "dbo.MODELS", "ID_MODEL");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_FAMILY_HK", "dbo.FAMILIES_HK", "ID_FAMILY_HK");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS", "ID_STATUS_PROD", "dbo.STATUS_HK", "ID_STATUS_PROD", cascadeDelete: false);
            AddForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL", "ID_STATUS_CIAL", cascadeDelete: false);
            AddForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS", "ID_MODEL");
            AddForeignKey("dbo.ITEMS", "ID_FAMILY_HK", "dbo.FAMILIES_HK", "ID_FAMILY_HK");
            AddForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR");
        }
    }
}