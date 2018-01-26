namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pkItemsBox_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PACKING_LIST_ITEM_BOX",
                c => new
                    {
                        ID_DOC = c.String(nullable: false, maxLength: 50),
                        ID_DOC_RELATED = c.String(nullable: false, maxLength: 50),
                        ID_ITEM_BCN = c.String(nullable: false, maxLength: 50),
                        ID_ITEM_GROUP = c.String(nullable: false, maxLength: 100),
                        BOX_NUMBER = c.Int(nullable: false),
                        PC_QUANTITY = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        NET_WEIGHT = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        GROSS_WEIGHT = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.ID_DOC, t.ID_DOC_RELATED, t.ID_ITEM_BCN, t.ID_ITEM_GROUP, t.BOX_NUMBER })
                .ForeignKey("dbo.ITEM_GROUP", t => t.ID_ITEM_GROUP, cascadeDelete: false)
                .ForeignKey("dbo.DOC_HEAD", t => t.ID_DOC, cascadeDelete: false)
                .Index(t => t.ID_DOC)
                .Index(t => t.ID_ITEM_GROUP);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PACKING_LIST_ITEM_BOX", "ID_DOC", "dbo.DOC_HEAD");
            DropForeignKey("dbo.PACKING_LIST_ITEM_BOX", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropIndex("dbo.PACKING_LIST_ITEM_BOX", new[] { "ID_ITEM_GROUP" });
            DropIndex("dbo.PACKING_LIST_ITEM_BOX", new[] { "ID_DOC" });
            DropTable("dbo.PACKING_LIST_ITEM_BOX");
        }
    }
}
