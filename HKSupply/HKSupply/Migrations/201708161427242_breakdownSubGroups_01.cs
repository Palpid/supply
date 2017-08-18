namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class breakdownSubGroups_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BOM_BREAKDOWN", "SUB_GROUP", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BOM_BREAKDOWN", "SUB_GROUP");
        }
    }
}
