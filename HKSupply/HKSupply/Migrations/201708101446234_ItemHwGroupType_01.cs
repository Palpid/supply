namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemHwGroupType_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ITEMS_HW", "ID_GROUP_TYPE", c => c.String(maxLength: 50));
            AddColumn("dbo.ITEMS_HW_HISTORY", "ID_GROUP_TYPE", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ITEMS_HW_HISTORY", "ID_GROUP_TYPE");
            DropColumn("dbo.ITEMS_HW", "ID_GROUP_TYPE");
        }
    }
}
