namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class protoDocs_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PROTOTYPES_DOCS",
                c => new
                    {
                        ID_DOC = c.Int(nullable: false, identity: true),
                        ID_PROTOTYPE = c.String(maxLength: 50),
                        ID_DOC_TYPE = c.String(maxLength: 100),
                        FILE_NAME = c.String(maxLength: 100),
                        FILE_PATH = c.String(maxLength: 500),
                        CREATE_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID_DOC)
                .ForeignKey("dbo.DOCS_TYPES", t => t.ID_DOC_TYPE)
                .Index(t => t.ID_DOC_TYPE);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PROTOTYPES_DOCS", "ID_DOC_TYPE", "dbo.DOCS_TYPES");
            DropIndex("dbo.PROTOTYPES_DOCS", new[] { "ID_DOC_TYPE" });
            DropTable("dbo.PROTOTYPES_DOCS");
        }
    }
}
