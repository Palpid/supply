namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemType_02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ITEMS_HF_HISTORY", "ID_ITEM_TYPE", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ITEMS_HF_HISTORY", "ID_ITEM_TYPE");
        }
    }
}
