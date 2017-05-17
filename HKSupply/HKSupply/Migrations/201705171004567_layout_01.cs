namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class layout_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LAYOUTS",
                c => new
                    {
                        ID_LAYOUT = c.Int(nullable: false, identity: true),
                        FUNCTIONALITY_ID = c.Int(nullable: false),
                        USER = c.String(maxLength: 20, unicode: false),
                        OBJECT_NAME = c.String(maxLength: 100, unicode: false),
                        LAYOUT_STRING = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.ID_LAYOUT)
                .ForeignKey("dbo.FUNCTIONALITIES", t => t.FUNCTIONALITY_ID, cascadeDelete: true)
                .Index(t => t.FUNCTIONALITY_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LAYOUTS", "FUNCTIONALITY_ID", "dbo.FUNCTIONALITIES");
            DropIndex("dbo.LAYOUTS", new[] { "FUNCTIONALITY_ID" });
            DropTable("dbo.LAYOUTS");
        }
    }
}
