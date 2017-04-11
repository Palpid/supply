namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statusHK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ITEMS", "ID_STATUS_PROD", "dbo.STATUS_PROD");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_PROD", "dbo.STATUS_PROD");
            DropIndex("dbo.ITEMS", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS", new[] { "ID_STATUS_PROD" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_STATUS_PROD" });
            CreateTable(
                "dbo.STATUS_HK",
                c => new
                    {
                        ID_STATUS_PROD = c.Int(nullable: false, identity: false),
                        DESCRIPTION = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_STATUS_PROD);
            
            AlterColumn("dbo.ITEMS", "ID_FAMILY_HK", c => c.String(maxLength: 100));
            AlterColumn("dbo.ITEMS", "ID_STATUS_PROD", c => c.Int(nullable: false));
            AlterColumn("dbo.ITEMS_HISTORY", "ID_FAMILY_HK", c => c.String(maxLength: 100));
            AlterColumn("dbo.ITEMS_HISTORY", "ID_STATUS_PROD", c => c.Int(nullable: false));
            CreateIndex("dbo.ITEMS", "ID_FAMILY_HK");
            CreateIndex("dbo.ITEMS", "ID_STATUS_PROD");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_FAMILY_HK");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_STATUS_PROD");
            AddForeignKey("dbo.ITEMS", "ID_STATUS_PROD", "dbo.STATUS_HK", "ID_STATUS_PROD", cascadeDelete: true);
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_PROD", "dbo.STATUS_HK", "ID_STATUS_PROD", cascadeDelete: true);
            DropTable("dbo.STATUS_PROD");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.STATUS_PROD",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 30),
                        DESCRIPTION = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_PROD", "dbo.STATUS_HK");
            DropForeignKey("dbo.ITEMS", "ID_STATUS_PROD", "dbo.STATUS_HK");
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_STATUS_PROD" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_FAMILY_HK" });
            DropIndex("dbo.ITEMS", new[] { "ID_STATUS_PROD" });
            DropIndex("dbo.ITEMS", new[] { "ID_FAMILY_HK" });
            AlterColumn("dbo.ITEMS_HISTORY", "ID_STATUS_PROD", c => c.String(maxLength: 30));
            AlterColumn("dbo.ITEMS_HISTORY", "ID_FAMILY_HK", c => c.String(maxLength: 30));
            AlterColumn("dbo.ITEMS", "ID_STATUS_PROD", c => c.String(maxLength: 30));
            AlterColumn("dbo.ITEMS", "ID_FAMILY_HK", c => c.String(maxLength: 30));
            DropTable("dbo.STATUS_HK");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_STATUS_PROD");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_FAMILY_HK");
            CreateIndex("dbo.ITEMS", "ID_STATUS_PROD");
            CreateIndex("dbo.ITEMS", "ID_FAMILY_HK");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_PROD", "dbo.STATUS_PROD", "ID");
            AddForeignKey("dbo.ITEMS", "ID_STATUS_PROD", "dbo.STATUS_PROD", "ID");
        }
    }
}
