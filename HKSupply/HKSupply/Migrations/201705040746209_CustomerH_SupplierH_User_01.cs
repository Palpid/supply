namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerH_SupplierH_User_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CUSTOMERS_HISTORY", "USER", c => c.String(maxLength: 20));
            AddColumn("dbo.SUPPLIERS_HISTORY", "USER", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SUPPLIERS_HISTORY", "USER");
            DropColumn("dbo.CUSTOMERS_HISTORY", "USER");
        }
    }
}
