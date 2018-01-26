namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelBoxNumInDocLine_01 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DOC_LINES", "BOX_NUMBER");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DOC_LINES", "BOX_NUMBER", c => c.Int());
        }
    }
}
