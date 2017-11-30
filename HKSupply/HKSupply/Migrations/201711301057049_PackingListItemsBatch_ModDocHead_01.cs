namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PackingListItemsBatch_ModDocHead_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PACKING_LIST_ITEM_BATCH",
                c => new
                    {
                        ID_DOC = c.String(nullable: false, maxLength: 50),
                        ID_DOC_RELATED = c.String(nullable: false, maxLength: 50),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 50),
                        ID_ITEM_GROUP = c.String(nullable: false, maxLength: 100),
                        BATCH = c.String(nullable: false, maxLength: 50),
                        QUANTITY = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.ID_DOC, t.ID_DOC_RELATED, t.ID_ITEM_BCN, t.ID_ITEM_GROUP, t.BATCH })
                .ForeignKey("dbo.ITEM_GROUP", t => t.ID_ITEM_GROUP, cascadeDelete: false)
                .ForeignKey("dbo.DOC_HEAD", t => t.ID_DOC, cascadeDelete: false)
                .Index(t => t.ID_DOC)
                .Index(t => t.ID_ITEM_GROUP);
            
            CreateIndex("dbo.DOC_BOXES", "ID_DOC");
            AddForeignKey("dbo.DOC_BOXES", "ID_DOC", "dbo.DOC_HEAD", "ID_DOC", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PACKING_LIST_ITEM_BATCH", "ID_DOC", "dbo.DOC_HEAD");
            DropForeignKey("dbo.PACKING_LIST_ITEM_BATCH", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropForeignKey("dbo.DOC_BOXES", "ID_DOC", "dbo.DOC_HEAD");
            DropIndex("dbo.PACKING_LIST_ITEM_BATCH", new[] { "ID_ITEM_GROUP" });
            DropIndex("dbo.PACKING_LIST_ITEM_BATCH", new[] { "ID_DOC" });
            DropIndex("dbo.DOC_BOXES", new[] { "ID_DOC" });
            DropTable("dbo.PACKING_LIST_ITEM_BATCH");
        }
    }
}
