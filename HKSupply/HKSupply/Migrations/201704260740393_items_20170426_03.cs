namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class items_20170426_03 : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.ITEMS_HISTORY", newName: "ITEMS_EY_HISTORY");
        }
        
        public override void Down()
        {
            //RenameTable(name: "dbo.ITEMS_EY_HISTORY", newName: "ITEMS_HISTORY");
        }
    }
}
