namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DOC_HEAD_ATTACH_FILES_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DOC_HEAD_ATTACH_FILES",
                c => new
                    {
                        ID_FILE = c.Int(nullable: false, identity: true),
                        ID_DOC_HEAD = c.String(maxLength: 50),
                        FILE_NAME = c.String(maxLength: 250),
                        FILE_EXTENSION = c.String(maxLength: 10),
                        FILE_PATH = c.String(maxLength: 500),
                        USER = c.String(maxLength: 20),
                        CREATE_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID_FILE);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DOC_HEAD_ATTACH_FILES");
        }
    }
}
