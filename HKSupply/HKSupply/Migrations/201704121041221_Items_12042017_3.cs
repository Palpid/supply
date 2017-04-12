namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Items_12042017_3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ITEMS", new[] { "ID_DEFAULT_SUPPLIER" });
            AlterColumn("dbo.ITEMS", "ID_DEFAULT_SUPPLIER", c => c.String(maxLength: 100));
            AlterColumn("dbo.ITEMS_HISTORY", "ID_DEFAULT_SUPPLIER", c => c.String(maxLength: 100));
            CreateIndex("dbo.ITEMS", "ID_DEFAULT_SUPPLIER");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ITEMS", new[] { "ID_DEFAULT_SUPPLIER" });
            AlterColumn("dbo.ITEMS_HISTORY", "ID_DEFAULT_SUPPLIER", c => c.String(maxLength: 3));
            AlterColumn("dbo.ITEMS", "ID_DEFAULT_SUPPLIER", c => c.String(maxLength: 3));
            CreateIndex("dbo.ITEMS", "ID_DEFAULT_SUPPLIER");
        }
    }
}
