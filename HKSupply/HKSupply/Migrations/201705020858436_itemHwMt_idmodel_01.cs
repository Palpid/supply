namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemHwMt_idmodel_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ITEMS_HW", "ID_MODEL", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_HW_HISTORY", "ID_MODEL", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_MT", "ID_MODEL", c => c.String(maxLength: 100));
            AddColumn("dbo.ITEMS_MT_HISTORY", "ID_MODEL", c => c.String(maxLength: 100));
            CreateIndex("dbo.ITEMS_HW", "ID_MODEL");
            CreateIndex("dbo.ITEMS_HW_HISTORY", "ID_MODEL");
            CreateIndex("dbo.ITEMS_MT", "ID_MODEL");
            CreateIndex("dbo.ITEMS_MT_HISTORY", "ID_MODEL");
            AddForeignKey("dbo.ITEMS_HW", "ID_MODEL", "dbo.MODELS", "ID_MODEL");
            AddForeignKey("dbo.ITEMS_HW_HISTORY", "ID_MODEL", "dbo.MODELS", "ID_MODEL");
            AddForeignKey("dbo.ITEMS_MT", "ID_MODEL", "dbo.MODELS", "ID_MODEL");
            AddForeignKey("dbo.ITEMS_MT_HISTORY", "ID_MODEL", "dbo.MODELS", "ID_MODEL");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ITEMS_MT_HISTORY", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS_MT", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS_HW_HISTORY", "ID_MODEL", "dbo.MODELS");
            DropForeignKey("dbo.ITEMS_HW", "ID_MODEL", "dbo.MODELS");
            DropIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_MODEL" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_MODEL" });
            DropIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_MODEL" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_MODEL" });
            DropColumn("dbo.ITEMS_MT_HISTORY", "ID_MODEL");
            DropColumn("dbo.ITEMS_MT", "ID_MODEL");
            DropColumn("dbo.ITEMS_HW_HISTORY", "ID_MODEL");
            DropColumn("dbo.ITEMS_HW", "ID_MODEL");
        }
    }
}
