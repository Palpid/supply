namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class layout_03 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LAYOUTS", "LAYOUT_STRING", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LAYOUTS", "LAYOUT_STRING", c => c.String(maxLength: 8000, unicode: false));
        }
    }
}
