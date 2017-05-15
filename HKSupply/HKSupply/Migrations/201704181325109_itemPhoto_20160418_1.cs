namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemPhoto_20160418_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ITEMS", "PHOTO", c => c.String(maxLength: 2500));
            AddColumn("dbo.ITEMS_HISTORY", "PHOTO", c => c.String(maxLength: 2500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ITEMS_HISTORY", "PHOTO");
            DropColumn("dbo.ITEMS", "PHOTO");
        }
    }
}
