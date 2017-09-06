namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class key_Material_levels_01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ITEMS_EY", "ID_MATERIAL_L2", "dbo.MATERIALS_L2");
            DropForeignKey("dbo.ITEMS_EY", "ID_MATERIAL_L3", "dbo.MATERIALS_L3");
            DropForeignKey("dbo.MATERIALS_L3", "ID_MATERIAL_L2", "dbo.MATERIALS_L2");
            DropForeignKey("dbo.ITEMS_HF", "ID_MATERIAL_L2", "dbo.MATERIALS_L2");
            DropForeignKey("dbo.ITEMS_HF", "ID_MATERIAL_L3", "dbo.MATERIALS_L3");
            DropIndex("dbo.ITEMS_EY", new[] { "ID_MATERIAL_L2" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_MATERIAL_L3" });
            DropIndex("dbo.MATERIALS_L3", new[] { "ID_MATERIAL_L2" });
            DropIndex("dbo.ITEMS_HF", new[] { "ID_MATERIAL_L2" });
            DropIndex("dbo.ITEMS_HF", new[] { "ID_MATERIAL_L3" });
            DropPrimaryKey("dbo.MATERIALS_L2");
            DropPrimaryKey("dbo.MATERIALS_L3");
            AddPrimaryKey("dbo.MATERIALS_L2", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            AddPrimaryKey("dbo.MATERIALS_L3", new[] { "ID_MATERIAL_L3", "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            CreateIndex("dbo.ITEMS_EY", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            CreateIndex("dbo.ITEMS_EY", new[] { "ID_MATERIAL_L3", "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            CreateIndex("dbo.MATERIALS_L3", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            CreateIndex("dbo.ITEMS_HF", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            CreateIndex("dbo.ITEMS_HF", new[] { "ID_MATERIAL_L3", "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            AddForeignKey("dbo.ITEMS_EY", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" }, "dbo.MATERIALS_L2", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            AddForeignKey("dbo.ITEMS_EY", new[] { "ID_MATERIAL_L3", "ID_MATERIAL_L2", "ID_MATERIAL_L1" }, "dbo.MATERIALS_L3", new[] { "ID_MATERIAL_L3", "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            AddForeignKey("dbo.MATERIALS_L3", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" }, "dbo.MATERIALS_L2", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" }, cascadeDelete: true);
            AddForeignKey("dbo.ITEMS_HF", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" }, "dbo.MATERIALS_L2", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            AddForeignKey("dbo.ITEMS_HF", new[] { "ID_MATERIAL_L3", "ID_MATERIAL_L2", "ID_MATERIAL_L1" }, "dbo.MATERIALS_L3", new[] { "ID_MATERIAL_L3", "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ITEMS_HF", new[] { "ID_MATERIAL_L3", "ID_MATERIAL_L2", "ID_MATERIAL_L1" }, "dbo.MATERIALS_L3");
            DropForeignKey("dbo.ITEMS_HF", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" }, "dbo.MATERIALS_L2");
            DropForeignKey("dbo.MATERIALS_L3", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" }, "dbo.MATERIALS_L2");
            DropForeignKey("dbo.ITEMS_EY", new[] { "ID_MATERIAL_L3", "ID_MATERIAL_L2", "ID_MATERIAL_L1" }, "dbo.MATERIALS_L3");
            DropForeignKey("dbo.ITEMS_EY", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" }, "dbo.MATERIALS_L2");
            DropIndex("dbo.ITEMS_HF", new[] { "ID_MATERIAL_L3", "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            DropIndex("dbo.ITEMS_HF", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            DropIndex("dbo.MATERIALS_L3", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_MATERIAL_L3", "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_MATERIAL_L2", "ID_MATERIAL_L1" });
            DropPrimaryKey("dbo.MATERIALS_L3");
            DropPrimaryKey("dbo.MATERIALS_L2");
            AddPrimaryKey("dbo.MATERIALS_L3", "ID_MATERIAL_L3");
            AddPrimaryKey("dbo.MATERIALS_L2", "ID_MATERIAL_L2");
            CreateIndex("dbo.ITEMS_HF", "ID_MATERIAL_L3");
            CreateIndex("dbo.ITEMS_HF", "ID_MATERIAL_L2");
            CreateIndex("dbo.MATERIALS_L3", "ID_MATERIAL_L2");
            CreateIndex("dbo.ITEMS_EY", "ID_MATERIAL_L3");
            CreateIndex("dbo.ITEMS_EY", "ID_MATERIAL_L2");
            AddForeignKey("dbo.ITEMS_HF", "ID_MATERIAL_L3", "dbo.MATERIALS_L3", "ID_MATERIAL_L3");
            AddForeignKey("dbo.ITEMS_HF", "ID_MATERIAL_L2", "dbo.MATERIALS_L2", "ID_MATERIAL_L2");
            AddForeignKey("dbo.MATERIALS_L3", "ID_MATERIAL_L2", "dbo.MATERIALS_L2", "ID_MATERIAL_L2", cascadeDelete: true);
            AddForeignKey("dbo.ITEMS_EY", "ID_MATERIAL_L3", "dbo.MATERIALS_L3", "ID_MATERIAL_L3");
            AddForeignKey("dbo.ITEMS_EY", "ID_MATERIAL_L2", "dbo.MATERIALS_L2", "ID_MATERIAL_L2");
        }
    }
}
