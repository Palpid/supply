namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class functionalityReport_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FUNCTIONALITY_REPORTS",
                c => new
                    {
                        FUNCTIONALITY_REPORT_ID = c.Int(nullable: false, identity: true),
                        FUNCTIONALITY_ID = c.Int(nullable: false),
                        REPORT_NAME_EN = c.String(nullable: false, maxLength: 250),
                        REPORT_FILE = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.FUNCTIONALITY_REPORT_ID)
                .ForeignKey("dbo.FUNCTIONALITIES", t => t.FUNCTIONALITY_ID, cascadeDelete: false)
                .Index(t => t.FUNCTIONALITY_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FUNCTIONALITY_REPORTS", "FUNCTIONALITY_ID", "dbo.FUNCTIONALITIES");
            DropIndex("dbo.FUNCTIONALITY_REPORTS", new[] { "FUNCTIONALITY_ID" });
            DropTable("dbo.FUNCTIONALITY_REPORTS");
        }
    }
}
