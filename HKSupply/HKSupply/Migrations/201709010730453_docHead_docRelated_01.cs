namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class docHead_docRelated_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DOC_HEAD", "ID_DOC_RELATED", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DOC_HEAD", "ID_DOC_RELATED");
        }
    }
}
