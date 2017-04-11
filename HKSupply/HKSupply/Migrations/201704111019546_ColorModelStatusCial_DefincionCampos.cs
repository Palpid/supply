namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColorModelStatusCial_DefincionCampos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_MODEL", "dbo.MODELS");
            DropIndex("dbo.ITEMS", new[] { "ID_STATUS_CIAL" });
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_STATUS_CIAL" });
            DropPrimaryKey("dbo.COLORS");
            DropPrimaryKey("dbo.MODELS");
            DropPrimaryKey("dbo.STATUS_CIAL");
            AddColumn("dbo.COLORS", "ID_COLOR", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.MODELS", "ID_MODEL", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.STATUS_CIAL", "ID_STATUS_CIAL", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.COLORS", "DESCRIPTION", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.ITEMS", "ID_STATUS_CIAL", c => c.Int(nullable: false));
            AlterColumn("dbo.MODELS", "DESCRIPTION", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.STATUS_CIAL", "DESCRIPTION", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.COLORS", "ID_COLOR");
            AddPrimaryKey("dbo.MODELS", "ID_MODEL");
            AddPrimaryKey("dbo.STATUS_CIAL", "ID_STATUS_CIAL");
            CreateIndex("dbo.ITEMS", "ID_STATUS_CIAL");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL");
            AddForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL", "ID_STATUS_CIAL", cascadeDelete: true);
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL", "ID_STATUS_CIAL", cascadeDelete: true);
            AddForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS", "ID_MODEL");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_MODEL", "dbo.MODELS", "ID_MODEL");
            DropColumn("dbo.COLORS", "ID");
            DropColumn("dbo.MODELS", "ID");
            DropColumn("dbo.STATUS_CIAL", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.STATUS_CIAL", "ID", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.MODELS", "ID", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.COLORS", "ID", c => c.String(nullable: false, maxLength: 30));
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropIndex("dbo.ITEMS_HISTORY", new[] { "ID_STATUS_CIAL" });
            DropIndex("dbo.ITEMS", new[] { "ID_STATUS_CIAL" });
            DropPrimaryKey("dbo.STATUS_CIAL");
            DropPrimaryKey("dbo.MODELS");
            DropPrimaryKey("dbo.COLORS");
            AlterColumn("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", c => c.String(maxLength: 30));
            AlterColumn("dbo.STATUS_CIAL", "DESCRIPTION", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.MODELS", "DESCRIPTION", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ITEMS", "ID_STATUS_CIAL", c => c.String(maxLength: 30));
            AlterColumn("dbo.COLORS", "DESCRIPTION", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.STATUS_CIAL", "ID_STATUS_CIAL");
            DropColumn("dbo.MODELS", "ID_MODEL");
            DropColumn("dbo.COLORS", "ID_COLOR");
            AddPrimaryKey("dbo.STATUS_CIAL", "ID");
            AddPrimaryKey("dbo.MODELS", "ID");
            AddPrimaryKey("dbo.COLORS", "ID");
            CreateIndex("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL");
            CreateIndex("dbo.ITEMS", "ID_STATUS_CIAL");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_MODEL", "dbo.MODELS", "ID");
            AddForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS", "ID");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_2", "dbo.COLORS", "ID");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_1", "dbo.COLORS", "ID");
            AddForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS", "ID");
            AddForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS", "ID");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL", "ID");
            AddForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL", "ID");
        }
    }
}
