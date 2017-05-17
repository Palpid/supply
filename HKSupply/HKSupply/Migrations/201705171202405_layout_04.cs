namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class layout_04 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LAYOUTS", "USER", c => c.String(maxLength: 20));
            AlterColumn("dbo.LAYOUTS", "OBJECT_NAME", c => c.String(maxLength: 100));
            AlterColumn("dbo.LAYOUTS", "LAYOUT_STRING", c => c.String());
            AlterColumn("dbo.LAYOUTS", "LAYOUT_NAME", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LAYOUTS", "LAYOUT_NAME", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.LAYOUTS", "LAYOUT_STRING", c => c.String(unicode: false));
            AlterColumn("dbo.LAYOUTS", "OBJECT_NAME", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.LAYOUTS", "USER", c => c.String(maxLength: 20, unicode: false));
        }
    }
}
