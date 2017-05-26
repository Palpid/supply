namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hwType_25042014_01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ITEMS_HW", "ID_HW_TYPE_L2", "dbo.HWS_TYPE_L2");
            DropForeignKey("dbo.ITEMS_HW", "ID_HW_TYPE_L3", "dbo.HWS_TYPE_L3");
            DropForeignKey("dbo.HWS_TYPE_L3", "ID_HW_TYPE_L2", "dbo.HWS_TYPE_L2");
            DropForeignKey("dbo.ITEMS_HW_HISTORY", "ID_HW_TYPE_L2", "dbo.HWS_TYPE_L2");
            DropForeignKey("dbo.ITEMS_HW_HISTORY", "ID_HW_TYPE_L3", "dbo.HWS_TYPE_L3");
            DropIndex("dbo.ITEMS_HW", new[] { "ID_HW_TYPE_L2" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_HW_TYPE_L3" });
            DropIndex("dbo.HWS_TYPE_L3", new[] { "ID_HW_TYPE_L2" });
            DropIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_HW_TYPE_L2" });
            DropIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_HW_TYPE_L3" });
            DropPrimaryKey("dbo.HWS_TYPE_L2");
            DropPrimaryKey("dbo.HWS_TYPE_L3");
            AddPrimaryKey("dbo.HWS_TYPE_L2", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            AddPrimaryKey("dbo.HWS_TYPE_L3", new[] { "ID_HW_TYPE_L3", "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            CreateIndex("dbo.ITEMS_HW", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            CreateIndex("dbo.ITEMS_HW", new[] { "ID_HW_TYPE_L3", "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            CreateIndex("dbo.HWS_TYPE_L3", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            CreateIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            CreateIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_HW_TYPE_L3", "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            AddForeignKey("dbo.ITEMS_HW", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" }, "dbo.HWS_TYPE_L2", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            AddForeignKey("dbo.ITEMS_HW", new[] { "ID_HW_TYPE_L3", "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" }, "dbo.HWS_TYPE_L3", new[] { "ID_HW_TYPE_L3", "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            AddForeignKey("dbo.HWS_TYPE_L3", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" }, "dbo.HWS_TYPE_L2", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" }, cascadeDelete: true);
            AddForeignKey("dbo.ITEMS_HW_HISTORY", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" }, "dbo.HWS_TYPE_L2", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            AddForeignKey("dbo.ITEMS_HW_HISTORY", new[] { "ID_HW_TYPE_L3", "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" }, "dbo.HWS_TYPE_L3", new[] { "ID_HW_TYPE_L3", "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ITEMS_HW_HISTORY", new[] { "ID_HW_TYPE_L3", "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" }, "dbo.HWS_TYPE_L3");
            DropForeignKey("dbo.ITEMS_HW_HISTORY", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" }, "dbo.HWS_TYPE_L2");
            DropForeignKey("dbo.HWS_TYPE_L3", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" }, "dbo.HWS_TYPE_L2");
            DropForeignKey("dbo.ITEMS_HW", new[] { "ID_HW_TYPE_L3", "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" }, "dbo.HWS_TYPE_L3");
            DropForeignKey("dbo.ITEMS_HW", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" }, "dbo.HWS_TYPE_L2");
            DropIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_HW_TYPE_L3", "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            DropIndex("dbo.ITEMS_HW_HISTORY", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            DropIndex("dbo.HWS_TYPE_L3", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_HW_TYPE_L3", "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_HW_TYPE_L2", "ID_HW_TYPE_L1" });
            DropPrimaryKey("dbo.HWS_TYPE_L3");
            DropPrimaryKey("dbo.HWS_TYPE_L2");
            AddPrimaryKey("dbo.HWS_TYPE_L3", "ID_HW_TYPE_L3");
            AddPrimaryKey("dbo.HWS_TYPE_L2", "ID_HW_TYPE_L2");
            CreateIndex("dbo.ITEMS_HW_HISTORY", "ID_HW_TYPE_L3");
            CreateIndex("dbo.ITEMS_HW_HISTORY", "ID_HW_TYPE_L2");
            CreateIndex("dbo.HWS_TYPE_L3", "ID_HW_TYPE_L2");
            CreateIndex("dbo.ITEMS_HW", "ID_HW_TYPE_L3");
            CreateIndex("dbo.ITEMS_HW", "ID_HW_TYPE_L2");
            AddForeignKey("dbo.ITEMS_HW_HISTORY", "ID_HW_TYPE_L3", "dbo.HWS_TYPE_L3", "ID_HW_TYPE_L3");
            AddForeignKey("dbo.ITEMS_HW_HISTORY", "ID_HW_TYPE_L2", "dbo.HWS_TYPE_L2", "ID_HW_TYPE_L2");
            AddForeignKey("dbo.HWS_TYPE_L3", "ID_HW_TYPE_L2", "dbo.HWS_TYPE_L2", "ID_HW_TYPE_L2", cascadeDelete: true);
            AddForeignKey("dbo.ITEMS_HW", "ID_HW_TYPE_L3", "dbo.HWS_TYPE_L3", "ID_HW_TYPE_L3");
            AddForeignKey("dbo.ITEMS_HW", "ID_HW_TYPE_L2", "dbo.HWS_TYPE_L2", "ID_HW_TYPE_L2");
        }
    }
}
