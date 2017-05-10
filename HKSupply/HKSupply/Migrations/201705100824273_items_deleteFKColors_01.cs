namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class items_deleteFKColors_01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ITEMS_EY", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_EY", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HW", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_HW", "ID_COLOR_2", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_MT", "ID_COLOR_1", "dbo.COLORS");
            DropForeignKey("dbo.ITEMS_MT", "ID_COLOR_2", "dbo.COLORS");
            DropIndex("dbo.ITEMS_EY", new[] { "ID_COLOR_1" });
            DropIndex("dbo.ITEMS_EY", new[] { "ID_COLOR_2" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_COLOR_1" });
            DropIndex("dbo.ITEMS_HW", new[] { "ID_COLOR_2" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_COLOR_1" });
            DropIndex("dbo.ITEMS_MT", new[] { "ID_COLOR_2" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.ITEMS_MT", "ID_COLOR_2");
            CreateIndex("dbo.ITEMS_MT", "ID_COLOR_1");
            CreateIndex("dbo.ITEMS_HW", "ID_COLOR_2");
            CreateIndex("dbo.ITEMS_HW", "ID_COLOR_1");
            CreateIndex("dbo.ITEMS_EY", "ID_COLOR_2");
            CreateIndex("dbo.ITEMS_EY", "ID_COLOR_1");
            AddForeignKey("dbo.ITEMS_MT", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS_MT", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS_HW", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS_HW", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS_EY", "ID_COLOR_2", "dbo.COLORS", "ID_COLOR");
            AddForeignKey("dbo.ITEMS_EY", "ID_COLOR_1", "dbo.COLORS", "ID_COLOR");
        }
    }
}
