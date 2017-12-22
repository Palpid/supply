namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class factorySupplierCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CUSTOMERS", "FACTORY", c => c.Boolean(nullable: false));
            AddColumn("dbo.CUSTOMERS_HISTORY", "FACTORY", c => c.Boolean(nullable: false));
            AddColumn("dbo.SUPPLIERS", "FACTORY", c => c.Boolean(nullable: false));
            AddColumn("dbo.SUPPLIERS_HISTORY", "FACTORY", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SUPPLIERS_HISTORY", "FACTORY");
            DropColumn("dbo.SUPPLIERS", "FACTORY");
            DropColumn("dbo.CUSTOMERS_HISTORY", "FACTORY");
            DropColumn("dbo.CUSTOMERS", "FACTORY");
        }
    }
}
