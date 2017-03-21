namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class items_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ITEMS", "LAUNCHED", c => c.DateTime());
            AlterColumn("dbo.ITEMS", "RETIRED", c => c.DateTime());
            AlterColumn("dbo.ITEMS_HISTORY", "LAUNCHED", c => c.DateTime());
            AlterColumn("dbo.ITEMS_HISTORY", "RETIRED", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ITEMS_HISTORY", "RETIRED", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ITEMS_HISTORY", "LAUNCHED", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ITEMS", "RETIRED", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ITEMS", "LAUNCHED", c => c.DateTime(nullable: false));
        }
    }
}
