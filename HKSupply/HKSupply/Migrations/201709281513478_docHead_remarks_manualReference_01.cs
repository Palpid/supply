namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class docHead_remarks_manualReference_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DOC_HEAD", "REMARKS", c => c.String(maxLength: 4000));
            AddColumn("dbo.DOC_HEAD", "MANUAL_REFERENCE", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DOC_HEAD", "MANUAL_REFERENCE");
            DropColumn("dbo.DOC_HEAD", "REMARKS");
        }
    }
}
