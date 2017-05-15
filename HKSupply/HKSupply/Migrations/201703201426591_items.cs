namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class items : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ITEMS",
                c => new
                    {
                        ITEM_CODE = c.String(nullable: false, maxLength: 20),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        ITEM_NAME = c.String(nullable: false, maxLength: 100),
                        MODEL = c.String(nullable: false, maxLength: 100),
                        ACTIVE = c.Boolean(nullable: false),
                        ID_STATUS = c.Int(nullable: false),
                        LAUNCHED = c.DateTime(nullable: false),
                        RETIRED = c.DateTime(nullable: false),
                        MM_FRONT = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        SIZE = c.String(maxLength: 30),
                        CATEGORY_NAME = c.String(maxLength: 30),
                        CALIBER = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.ITEM_CODE, t.ID_VER, t.ID_SUBVER });
            
            CreateTable(
                "dbo.ITEMS_HISTORY",
                c => new
                    {
                        ITEM_CODE = c.String(nullable: false, maxLength: 20),
                        ID_VER = c.Int(nullable: false),
                        ID_SUBVER = c.Int(nullable: false),
                        TIMESTAMP = c.DateTime(nullable: false),
                        ITEM_NAME = c.String(nullable: false, maxLength: 100),
                        MODEL = c.String(nullable: false, maxLength: 100),
                        ACTIVE = c.Boolean(nullable: false),
                        ID_STATUS = c.Int(nullable: false),
                        LAUNCHED = c.DateTime(nullable: false),
                        RETIRED = c.DateTime(nullable: false),
                        MM_FRONT = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        SIZE = c.String(maxLength: 30),
                        CATEGORY_NAME = c.String(maxLength: 30),
                        CALIBER = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.ITEM_CODE, t.ID_VER, t.ID_SUBVER });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ITEMS_HISTORY");
            DropTable("dbo.ITEMS");
        }
    }
}
