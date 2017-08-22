namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemType_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemTypes",
                c => new
                    {
                        ID_ITEM_TYPE = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_ITEM_TYPE);
            
            AddColumn("dbo.ITEMS_HF", "ID_ITEM_TYPE", c => c.String(maxLength: 100));
            CreateIndex("dbo.ITEMS_HF", "ID_ITEM_TYPE");
            AddForeignKey("dbo.ITEMS_HF", "ID_ITEM_TYPE", "dbo.ItemTypes", "ID_ITEM_TYPE");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ITEMS_HF", "ID_ITEM_TYPE", "dbo.ItemTypes");
            DropIndex("dbo.ITEMS_HF", new[] { "ID_ITEM_TYPE" });
            DropColumn("dbo.ITEMS_HF", "ID_ITEM_TYPE");
            DropTable("dbo.ItemTypes");
        }
    }
}
