namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Items_33 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SUPPLIERS");
            AlterColumn("dbo.ITEMS", "ID_DEFAULT_SUPPLIER", c => c.String(maxLength: 3));
            AlterColumn("dbo.ITEMS_HISTORY", "ID_DEFAULT_SUPPLIER", c => c.String(maxLength: 3));
            AlterColumn("dbo.SUPPLIERS", "ID_SUPPLIER", c => c.String(nullable: false, maxLength: 3));
            AddPrimaryKey("dbo.SUPPLIERS", new[] { "ID_SUPPLIER", "ID_VER", "ID_SUBVER" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SUPPLIERS");
            AlterColumn("dbo.SUPPLIERS", "ID_SUPPLIER", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ITEMS_HISTORY", "ID_DEFAULT_SUPPLIER", c => c.String(maxLength: 20));
            AlterColumn("dbo.ITEMS", "ID_DEFAULT_SUPPLIER", c => c.String(maxLength: 20));
            AddPrimaryKey("dbo.SUPPLIERS", new[] { "ID_SUPPLIER", "ID_VER", "ID_SUBVER" });
        }
    }
}
