namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColorModelStatusCial_DefincionCampos_23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");

            DropPrimaryKey("dbo.COLORS");
            DropPrimaryKey("dbo.MODELS");
            DropPrimaryKey("dbo.STATUS_CIAL");

            AddColumn("dbo.COLORS", "ID_COLOR_", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.COLORS", "DESCRIPTION_", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.MODELS", "ID_MODEL_", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.MODELS", "DESCRIPTION_", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.STATUS_CIAL", "ID_STATUS_CIAL_", c => c.Int(nullable: false));
            AddColumn("dbo.STATUS_CIAL", "DESCRIPTION_", c => c.String(nullable: false, maxLength: 500));

            AddPrimaryKey("dbo.COLORS", "ID_COLOR_");
            AddPrimaryKey("dbo.MODELS", "ID_MODEL_");
            AddPrimaryKey("dbo.STATUS_CIAL", "ID_STATUS_CIAL_");
            AddForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR_");
            AddForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR_");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR_");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR_");
            AddForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS", "ID_MODEL_");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_MODEL", "dbo.MODELS", "ID_MODEL_");
            AddForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL", "ID_STATUS_CIAL_", cascadeDelete: false);
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL", "ID_STATUS_CIAL_", cascadeDelete: false);

            DropColumn("dbo.COLORS", "ID_COLOR");
            DropColumn("dbo.COLORS", "DESCRIPTION");
            DropColumn("dbo.MODELS", "ID_MODEL");
            DropColumn("dbo.MODELS", "DESCRIPTION");
            DropColumn("dbo.STATUS_CIAL", "ID_STATUS_CIAL");
            DropColumn("dbo.STATUS_CIAL", "DESCRIPTION");

        }
        
        public override void Down()
        {
            AddColumn("dbo.STATUS_CIAL", "DESCRIPTION", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.STATUS_CIAL", "ID_STATUS_CIAL", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.MODELS", "DESCRIPTION", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.MODELS", "ID_MODEL", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.COLORS", "DESCRIPTION", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.COLORS", "ID_COLOR", c => c.String(nullable: false, maxLength: 30));
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS");
            DropPrimaryKey("dbo.STATUS_CIAL");
            DropPrimaryKey("dbo.MODELS");
            DropPrimaryKey("dbo.COLORS");
            DropColumn("dbo.STATUS_CIAL", "DESCRIPTION");
            DropColumn("dbo.STATUS_CIAL", "ID_STATUS_CIAL");
            DropColumn("dbo.MODELS", "DESCRIPTION_");
            DropColumn("dbo.MODELS", "ID_MODEL_");
            DropColumn("dbo.COLORS", "DESCRIPTION_");
            DropColumn("dbo.COLORS", "ID_COLOR_");
            AddPrimaryKey("dbo.STATUS_CIAL", "ID_STATUS_CIAL");
            AddPrimaryKey("dbo.MODELS", "ID_MODEL");
            AddPrimaryKey("dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_STATUS_CIAL", "dbo.STATUS_CIAL", "ID_STATUS_CIAL", cascadeDelete: false);
            AddForeignKey("dbo.ITEMS", "ID_STATUS_CIAL", "dbo.STATUS_CIAL", "ID_STATUS_CIAL", cascadeDelete: false);
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_MODEL", "dbo.MODELS", "ID_MODEL");
            AddForeignKey("dbo.ITEMS", "ID_MODEL", "dbo.MODELS", "ID_MODEL");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR");
        }
    }
}
