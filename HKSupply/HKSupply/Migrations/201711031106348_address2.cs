namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class address2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CUSTOMERS", "SHIPING_ADDRESS_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS", "SHIPING_ADDRESS_ZH_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS", "BILLING_ADDRESS_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS", "BILLING_ADDRESS_ZH_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS_HISTORY", "SHIPING_ADDRESS_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS_HISTORY", "SHIPING_ADDRESS_ZH_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS_HISTORY", "BILLING_ADDRESS_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.CUSTOMERS_HISTORY", "BILLING_ADDRESS_ZH_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_ZH_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_ZH_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_ZH_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_2", c => c.String(maxLength: 2500));
            AddColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_ZH_2", c => c.String(maxLength: 2500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_ZH_2");
            DropColumn("dbo.SUPPLIERS_HISTORY", "BILLING_ADDRESS_2");
            DropColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_ZH_2");
            DropColumn("dbo.SUPPLIERS_HISTORY", "SHIPING_ADDRESS_2");
            DropColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_ZH_2");
            DropColumn("dbo.SUPPLIERS", "BILLING_ADDRESS_2");
            DropColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_ZH_2");
            DropColumn("dbo.SUPPLIERS", "SHIPING_ADDRESS_2");
            DropColumn("dbo.CUSTOMERS_HISTORY", "BILLING_ADDRESS_ZH_2");
            DropColumn("dbo.CUSTOMERS_HISTORY", "BILLING_ADDRESS_2");
            DropColumn("dbo.CUSTOMERS_HISTORY", "SHIPING_ADDRESS_ZH_2");
            DropColumn("dbo.CUSTOMERS_HISTORY", "SHIPING_ADDRESS_2");
            DropColumn("dbo.CUSTOMERS", "BILLING_ADDRESS_ZH_2");
            DropColumn("dbo.CUSTOMERS", "BILLING_ADDRESS_2");
            DropColumn("dbo.CUSTOMERS", "SHIPING_ADDRESS_ZH_2");
            DropColumn("dbo.CUSTOMERS", "SHIPING_ADDRESS_2");
        }
    }
}
