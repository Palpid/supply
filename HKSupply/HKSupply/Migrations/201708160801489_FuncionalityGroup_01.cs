namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FuncionalityGroup_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FUNCTIONALITIES", "GROUP", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FUNCTIONALITIES", "GROUP");
        }
    }
}
