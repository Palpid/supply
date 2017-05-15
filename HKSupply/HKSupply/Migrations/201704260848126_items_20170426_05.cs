namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class items_20170426_05 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ITEMS_DOCS",
                c => new
                    {
                        ID_DOC = c.Int(nullable: false, identity: true),
                        ID_ITEM_BCN = c.String(maxLength: 20),
                        ID_VER_ITEM = c.Int(nullable: false),
                        ID_SUBVER_ITEM = c.Int(nullable: false),
                        ID_ITEM_GROUP = c.String(maxLength: 100),
                        ID_DOC_TYPE = c.String(maxLength: 100),
                        FILE_NAME = c.String(maxLength: 100),
                        FILE_PATH = c.String(maxLength: 500),
                        CREATE_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID_DOC)
                .ForeignKey("dbo.DOCS_TYPES", t => t.ID_DOC_TYPE)
                .ForeignKey("dbo.ITEM_GROUP", t => t.ID_ITEM_GROUP)
                .Index(t => t.ID_ITEM_GROUP)
                .Index(t => t.ID_DOC_TYPE);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ITEMS_DOCS", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropForeignKey("dbo.ITEMS_DOCS", "ID_DOC_TYPE", "dbo.DOCS_TYPES");
            DropIndex("dbo.ITEMS_DOCS", new[] { "ID_DOC_TYPE" });
            DropIndex("dbo.ITEMS_DOCS", new[] { "ID_ITEM_GROUP" });
            DropTable("dbo.ITEMS_DOCS");
        }
    }
}
