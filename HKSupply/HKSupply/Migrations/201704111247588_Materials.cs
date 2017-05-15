namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Materials : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MATERIALS_L1",
                c => new
                    {
                        ID_MATERIAL_L1 = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_MATERIAL_L1);
            
            CreateTable(
                "dbo.MATERIALS_L2",
                c => new
                    {
                        ID_MATERIAL_L2 = c.String(nullable: false, maxLength: 100),
                        ID_MATERIAL_L1 = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_MATERIAL_L2)
                .ForeignKey("dbo.MATERIALS_L1", t => t.ID_MATERIAL_L1, cascadeDelete: false)
                .Index(t => t.ID_MATERIAL_L1);
            
            CreateTable(
                "dbo.MATERIALS_L3",
                c => new
                    {
                        ID_MATERIAL_L3 = c.String(nullable: false, maxLength: 100),
                        ID_MATERIAL_L2 = c.String(nullable: false, maxLength: 100),
                        ID_MATERIAL_L1 = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_MATERIAL_L3)
                .ForeignKey("dbo.MATERIALS_L1", t => t.ID_MATERIAL_L1, cascadeDelete: false)
                .ForeignKey("dbo.MATERIALS_L2", t => t.ID_MATERIAL_L2, cascadeDelete: false)
                .Index(t => t.ID_MATERIAL_L2)
                .Index(t => t.ID_MATERIAL_L1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MATERIALS_L3", "ID_MATERIAL_L2", "dbo.MATERIALS_L2");
            DropForeignKey("dbo.MATERIALS_L3", "ID_MATERIAL_L1", "dbo.MATERIALS_L1");
            DropForeignKey("dbo.MATERIALS_L2", "ID_MATERIAL_L1", "dbo.MATERIALS_L1");
            DropIndex("dbo.MATERIALS_L3", new[] { "ID_MATERIAL_L1" });
            DropIndex("dbo.MATERIALS_L3", new[] { "ID_MATERIAL_L2" });
            DropIndex("dbo.MATERIALS_L2", new[] { "ID_MATERIAL_L1" });
            DropTable("dbo.MATERIALS_L3");
            DropTable("dbo.MATERIALS_L2");
            DropTable("dbo.MATERIALS_L1");
        }
    }
}
