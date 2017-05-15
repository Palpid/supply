namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemPhoto_20160418_2 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.ITEMS", "PHOTO_URL", c => c.String(maxLength: 2500));
            //AddColumn("dbo.ITEMS_HISTORY", "PHOTO_URL", c => c.String(maxLength: 2500));
            //DropColumn("dbo.ITEMS", "PHOTO");
            //DropColumn("dbo.ITEMS_HISTORY", "PHOTO");

            RenameColumn("dbo.ITEMS", "PHOTO", "PHOTO_URL");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.ITEMS_HISTORY", "PHOTO", c => c.String(maxLength: 2500));
            //AddColumn("dbo.ITEMS", "PHOTO", c => c.String(maxLength: 2500));
            //DropColumn("dbo.ITEMS_HISTORY", "PHOTO_URL");
            //DropColumn("dbo.ITEMS", "PHOTO_URL");

            RenameColumn("dbo.ITEMS", "PHOTO_URL", "PHOTO");
        }
    }
}
