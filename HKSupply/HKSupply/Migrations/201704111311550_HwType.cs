namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HwType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HWS_TYPE_L1",
                c => new
                    {
                        ID_HW_TYPE_L1 = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_HW_TYPE_L1);
            
            CreateTable(
                "dbo.HWS_TYPE_L2",
                c => new
                    {
                        ID_HW_TYPE_L2 = c.String(nullable: false, maxLength: 100),
                        ID_HW_TYPE_L1 = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_HW_TYPE_L2)
                .ForeignKey("dbo.HWS_TYPE_L1", t => t.ID_HW_TYPE_L1, cascadeDelete: false)
                .Index(t => t.ID_HW_TYPE_L1);
            
            CreateTable(
                "dbo.HWS_TYPE_L3",
                c => new
                    {
                        ID_HW_TYPE_L3 = c.String(nullable: false, maxLength: 100),
                        ID_HW_TYPE_L2 = c.String(nullable: false, maxLength: 100),
                        ID_HW_TYPE_L1 = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_HW_TYPE_L3)
                .ForeignKey("dbo.HWS_TYPE_L1", t => t.ID_HW_TYPE_L1, cascadeDelete: false)
                .ForeignKey("dbo.HWS_TYPE_L2", t => t.ID_HW_TYPE_L2, cascadeDelete: false)
                .Index(t => t.ID_HW_TYPE_L2)
                .Index(t => t.ID_HW_TYPE_L1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HWS_TYPE_L3", "ID_HW_TYPE_L2", "dbo.HWS_TYPE_L2");
            DropForeignKey("dbo.HWS_TYPE_L3", "ID_HW_TYPE_L1", "dbo.HWS_TYPE_L1");
            DropForeignKey("dbo.HWS_TYPE_L2", "ID_HW_TYPE_L1", "dbo.HWS_TYPE_L1");
            DropIndex("dbo.HWS_TYPE_L3", new[] { "ID_HW_TYPE_L1" });
            DropIndex("dbo.HWS_TYPE_L3", new[] { "ID_HW_TYPE_L2" });
            DropIndex("dbo.HWS_TYPE_L2", new[] { "ID_HW_TYPE_L1" });
            DropTable("dbo.HWS_TYPE_L3");
            DropTable("dbo.HWS_TYPE_L2");
            DropTable("dbo.HWS_TYPE_L1");
        }
    }
}
