namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Items_34 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.ITEMS", "LAUNCH_DATE", c => c.DateTime());
            //AddColumn("dbo.ITEMS_HISTORY", "LAUNCH_DATE", c => c.DateTime());
            //DropColumn("dbo.ITEMS", "LAUNCHED_DATE");
            //DropColumn("dbo.ITEMS_HISTORY", "LAUNCHED_DATE");
            RenameColumn("dbo.ITEMS", "LAUNCHED_DATE","LAUNCH_DATE");
            RenameColumn("dbo.ITEMS_HISTORY", "LAUNCHED_DATE", "LAUNCH_DATE");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.ITEMS_HISTORY", "LAUNCHED_DATE", c => c.DateTime());
            //AddColumn("dbo.ITEMS", "LAUNCHED_DATE", c => c.DateTime());
            //DropColumn("dbo.ITEMS_HISTORY", "LAUNCH_DATE");
            //DropColumn("dbo.ITEMS", "LAUNCH_DATE");
            RenameColumn("dbo.ITEMS", "LAUNCHED_DATE", "LAUNCH_DATE");
            RenameColumn("dbo.ITEMS_HISTORY", "LAUNCHED_DATE", "LAUNCH_DATE");
        }
    }
}
