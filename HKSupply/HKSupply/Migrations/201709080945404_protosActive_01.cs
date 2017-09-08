namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class protosActive_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PROTOTYPES", "ACTIVE", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PROTOTYPES", "ACTIVE");
        }
    }
}
