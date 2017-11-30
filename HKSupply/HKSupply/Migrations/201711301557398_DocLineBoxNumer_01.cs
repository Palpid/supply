namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocLineBoxNumer_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DOC_LINES", "BOX_NUMBER", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DOC_LINES", "BOX_NUMBER");
        }
    }
}
