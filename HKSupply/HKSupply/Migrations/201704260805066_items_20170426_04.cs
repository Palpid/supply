namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class items_20170426_04 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ITEMS_EY_HISTORY");
            AlterColumn("dbo.ITEMS_EY_HISTORY", "ID_PROTOTYPE", c => c.String(maxLength: 50));
            AddPrimaryKey("dbo.ITEMS_EY_HISTORY", new[] { "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_ITEM_BCN" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ITEMS_EY_HISTORY");
            AlterColumn("dbo.ITEMS_EY_HISTORY", "ID_PROTOTYPE", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.ITEMS_EY_HISTORY", new[] { "ID_VER", "ID_SUBVER", "TIMESTAMP", "ID_ITEM_BCN", "ID_PROTOTYPE" });
        }
    }
}
