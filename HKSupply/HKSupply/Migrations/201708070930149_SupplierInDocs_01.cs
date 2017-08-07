namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupplierInDocs_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ITEMS_DOCS", "ID_SUPPLIER", c => c.String(maxLength: 100));
            CreateIndex("dbo.ITEMS_DOCS", "ID_SUPPLIER");
            AddForeignKey("dbo.ITEMS_DOCS", "ID_SUPPLIER", "dbo.SUPPLIERS", "ID_SUPPLIER");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ITEMS_DOCS", "ID_SUPPLIER", "dbo.SUPPLIERS");
            DropIndex("dbo.ITEMS_DOCS", new[] { "ID_SUPPLIER" });
            DropColumn("dbo.ITEMS_DOCS", "ID_SUPPLIER");
        }
    }
}
