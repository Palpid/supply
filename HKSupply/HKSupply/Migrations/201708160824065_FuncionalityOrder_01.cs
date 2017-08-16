namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FuncionalityOrder_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FUNCTIONALITIES", "ORDER", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FUNCTIONALITIES", "ORDER");
        }
    }
}
