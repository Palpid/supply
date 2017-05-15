namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Items_31 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ITEMS", new[] { "ID_ITEM_GROUP" });
            DropPrimaryKey("dbo.ITEMS");
            AlterColumn("dbo.ITEMS", "ID_ITEM_GROUP", c => c.String(nullable: false, maxLength: 30));
            AddPrimaryKey("dbo.ITEMS", new[] { "ID_VER", "ID_SUBVER", "ID_PROTOTYPE", "ID_ITEM_BCN", "ID_ITEM_GROUP" });
            CreateIndex("dbo.ITEMS", "ID_ITEM_GROUP");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ITEMS", new[] { "ID_ITEM_GROUP" });
            DropPrimaryKey("dbo.ITEMS");
            AlterColumn("dbo.ITEMS", "ID_ITEM_GROUP", c => c.String(nullable: false, maxLength: 100));
            AddPrimaryKey("dbo.ITEMS", new[] { "ID_VER", "ID_SUBVER", "ID_PROTOTYPE", "ID_ITEM_BCN", "ID_ITEM_GROUP" });
            CreateIndex("dbo.ITEMS", "ID_ITEM_GROUP");
        }
    }
}
