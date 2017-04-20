namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PricesListHistoty_user_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CUSTOMER_PRICE_LIST_HISTORY", "USER", c => c.String(maxLength: 20));
            AddColumn("dbo.SUPPLIERS_PRICE_LIST_HISTORY", "USER", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SUPPLIERS_PRICE_LIST_HISTORY", "USER");
            DropColumn("dbo.CUSTOMER_PRICE_LIST_HISTORY", "USER");
        }
    }
}
