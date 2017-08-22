namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemType_03 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ItemTypes", newName: "ITEM_TYPE");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ITEM_TYPE", newName: "ItemTypes");
        }
    }
}
