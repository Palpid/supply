namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatType_20170524_01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ITEMS_MT", "ID_MAT_TYPE_L2", "dbo.MAT_TYPE_L2");
            DropForeignKey("dbo.ITEMS_MT", "ID_MAT_TYPE_L3", "dbo.MAT_TYPE_L3");
            DropForeignKey("dbo.MAT_TYPE_L3", "ID_MAT_TYPE_L2", "dbo.MAT_TYPE_L2");
            DropForeignKey("dbo.ITEMS_MT_HISTORY", "ID_MAT_TYPE_L2", "dbo.MAT_TYPE_L2");
            DropForeignKey("dbo.ITEMS_MT_HISTORY", "ID_MAT_TYPE_L3", "dbo.MAT_TYPE_L3");
            DropIndex("dbo.ITEMS_MT", new[] { "ID_MAT_TYPE_L2" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_MAT_TYPE_L3" });
            DropIndex("dbo.MAT_TYPE_L3", new[] { "ID_MAT_TYPE_L2" });
            DropIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_MAT_TYPE_L2" });
            DropIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_MAT_TYPE_L3" });
            DropPrimaryKey("dbo.MAT_TYPE_L2");
            DropPrimaryKey("dbo.MAT_TYPE_L3");
            AddPrimaryKey("dbo.MAT_TYPE_L2", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            AddPrimaryKey("dbo.MAT_TYPE_L3", new[] { "ID_MAT_TYPE_L3", "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            CreateIndex("dbo.ITEMS_MT", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            CreateIndex("dbo.ITEMS_MT", new[] { "ID_MAT_TYPE_L3", "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            CreateIndex("dbo.MAT_TYPE_L3", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            CreateIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            CreateIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_MAT_TYPE_L3", "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            AddForeignKey("dbo.ITEMS_MT", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" }, "dbo.MAT_TYPE_L2", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            AddForeignKey("dbo.ITEMS_MT", new[] { "ID_MAT_TYPE_L3", "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" }, "dbo.MAT_TYPE_L3", new[] { "ID_MAT_TYPE_L3", "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            AddForeignKey("dbo.MAT_TYPE_L3", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" }, "dbo.MAT_TYPE_L2", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" }, cascadeDelete: true);
            AddForeignKey("dbo.ITEMS_MT_HISTORY", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" }, "dbo.MAT_TYPE_L2", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            AddForeignKey("dbo.ITEMS_MT_HISTORY", new[] { "ID_MAT_TYPE_L3", "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" }, "dbo.MAT_TYPE_L3", new[] { "ID_MAT_TYPE_L3", "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ITEMS_MT_HISTORY", new[] { "ID_MAT_TYPE_L3", "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" }, "dbo.MAT_TYPE_L3");
            DropForeignKey("dbo.ITEMS_MT_HISTORY", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" }, "dbo.MAT_TYPE_L2");
            DropForeignKey("dbo.MAT_TYPE_L3", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" }, "dbo.MAT_TYPE_L2");
            DropForeignKey("dbo.ITEMS_MT", new[] { "ID_MAT_TYPE_L3", "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" }, "dbo.MAT_TYPE_L3");
            DropForeignKey("dbo.ITEMS_MT", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" }, "dbo.MAT_TYPE_L2");
            DropIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_MAT_TYPE_L3", "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            DropIndex("dbo.ITEMS_MT_HISTORY", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            DropIndex("dbo.MAT_TYPE_L3", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_MAT_TYPE_L3", "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_MAT_TYPE_L2", "ID_MAT_TYPE_L1" });
            DropPrimaryKey("dbo.MAT_TYPE_L3");
            DropPrimaryKey("dbo.MAT_TYPE_L2");
            AddPrimaryKey("dbo.MAT_TYPE_L3", "ID_MAT_TYPE_L3");
            AddPrimaryKey("dbo.MAT_TYPE_L2", "ID_MAT_TYPE_L2");
            CreateIndex("dbo.ITEMS_MT_HISTORY", "ID_MAT_TYPE_L3");
            CreateIndex("dbo.ITEMS_MT_HISTORY", "ID_MAT_TYPE_L2");
            CreateIndex("dbo.MAT_TYPE_L3", "ID_MAT_TYPE_L2");
            CreateIndex("dbo.ITEMS_MT", "ID_MAT_TYPE_L3");
            CreateIndex("dbo.ITEMS_MT", "ID_MAT_TYPE_L2");
            AddForeignKey("dbo.ITEMS_MT_HISTORY", "ID_MAT_TYPE_L3", "dbo.MAT_TYPE_L3", "ID_MAT_TYPE_L3");
            AddForeignKey("dbo.ITEMS_MT_HISTORY", "ID_MAT_TYPE_L2", "dbo.MAT_TYPE_L2", "ID_MAT_TYPE_L2");
            AddForeignKey("dbo.MAT_TYPE_L3", "ID_MAT_TYPE_L2", "dbo.MAT_TYPE_L2", "ID_MAT_TYPE_L2", cascadeDelete: true);
            AddForeignKey("dbo.ITEMS_MT", "ID_MAT_TYPE_L3", "dbo.MAT_TYPE_L3", "ID_MAT_TYPE_L3");
            AddForeignKey("dbo.ITEMS_MT", "ID_MAT_TYPE_L2", "dbo.MAT_TYPE_L2", "ID_MAT_TYPE_L2");
        }
    }
}
