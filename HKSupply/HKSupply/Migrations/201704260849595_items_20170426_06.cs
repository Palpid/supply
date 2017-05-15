namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class items_20170426_06 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DOCS_TYPES", name: "Description", newName: "DESCRIPTION");
        }
        
        public override void Down()
        {
        }
    }
}
