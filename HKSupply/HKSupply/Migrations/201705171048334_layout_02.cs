namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class layout_02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LAYOUTS", "LAYOUT_NAME", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LAYOUTS", "LAYOUT_NAME");
        }
    }
}
