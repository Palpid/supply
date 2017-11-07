namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnitsSupplyOnItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ITEMS_HW", "UNIT_SUPPLY", c => c.String(maxLength: 2));
            AddColumn("dbo.ITEMS_MT", "UNIT_SUPPLY", c => c.String(maxLength: 2));
            AddColumn("dbo.ITEMS_EY", "UNIT_SUPPLY", c => c.String(maxLength: 2));
            AddColumn("dbo.ITEMS_EY_HISTORY", "UNIT_SUPPLY", c => c.String(maxLength: 2));
            AddColumn("dbo.ITEMS_HF", "UNIT_SUPPLY", c => c.String(maxLength: 2));
            AddColumn("dbo.ITEMS_HF_HISTORY", "UNIT_SUPPLY", c => c.String(maxLength: 2));
            AddColumn("dbo.ITEMS_HW_HISTORY", "UNIT_SUPPLY", c => c.String(maxLength: 2));
            AddColumn("dbo.ITEMS_MT_HISTORY", "UNIT_SUPPLY", c => c.String(maxLength: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ITEMS_MT_HISTORY", "UNIT_SUPPLY");
            DropColumn("dbo.ITEMS_HW_HISTORY", "UNIT_SUPPLY");
            DropColumn("dbo.ITEMS_HF_HISTORY", "UNIT_SUPPLY");
            DropColumn("dbo.ITEMS_HF", "UNIT_SUPPLY");
            DropColumn("dbo.ITEMS_EY_HISTORY", "UNIT_SUPPLY");
            DropColumn("dbo.ITEMS_EY", "UNIT_SUPPLY");
            DropColumn("dbo.ITEMS_MT", "UNIT_SUPPLY");
            DropColumn("dbo.ITEMS_HW", "UNIT_SUPPLY");
        }
    }
}
