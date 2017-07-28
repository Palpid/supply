namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocHeadUser_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DOC_HEAD", "USER", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DOC_HEAD", "USER");
        }
    }
}
