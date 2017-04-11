namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatType2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MAT_TYPE_L1",
                c => new
                    {
                        ID_MAT_TYPE_L1 = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_MAT_TYPE_L1);
            
            CreateTable(
                "dbo.MAT_TYPE_L2",
                c => new
                    {
                        ID_MAT_TYPE_L2 = c.String(nullable: false, maxLength: 100),
                        ID_MAT_TYPE_L1 = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_MAT_TYPE_L2)
                .ForeignKey("dbo.MAT_TYPE_L1", t => t.ID_MAT_TYPE_L1, cascadeDelete: false)
                .Index(t => t.ID_MAT_TYPE_L1);
            
            CreateTable(
                "dbo.MAT_TYPE_L3",
                c => new
                    {
                        ID_MAT_TYPE_L3 = c.String(nullable: false, maxLength: 100),
                        ID_MAT_TYPE_L2 = c.String(nullable: false, maxLength: 100),
                        ID_MAT_TYPE_L1 = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID_MAT_TYPE_L3)
                .ForeignKey("dbo.MAT_TYPE_L1", t => t.ID_MAT_TYPE_L1, cascadeDelete: false)
                .ForeignKey("dbo.MAT_TYPE_L2", t => t.ID_MAT_TYPE_L2, cascadeDelete: false)
                .Index(t => t.ID_MAT_TYPE_L2)
                .Index(t => t.ID_MAT_TYPE_L1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MAT_TYPE_L3", "ID_MAT_TYPE_L2", "dbo.MAT_TYPE_L2");
            DropForeignKey("dbo.MAT_TYPE_L3", "ID_MAT_TYPE_L1", "dbo.MAT_TYPE_L1");
            DropForeignKey("dbo.MAT_TYPE_L2", "ID_MAT_TYPE_L1", "dbo.MAT_TYPE_L1");
            DropIndex("dbo.MAT_TYPE_L3", new[] { "ID_MAT_TYPE_L1" });
            DropIndex("dbo.MAT_TYPE_L3", new[] { "ID_MAT_TYPE_L2" });
            DropIndex("dbo.MAT_TYPE_L2", new[] { "ID_MAT_TYPE_L1" });
            DropTable("dbo.MAT_TYPE_L3");
            DropTable("dbo.MAT_TYPE_L2");
            DropTable("dbo.MAT_TYPE_L1");
        }
    }
}
