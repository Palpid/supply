namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class items_20170413_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ITEMS", "ID_USER_ATTRI_1", "dbo.USER_ATTR_DESCRIPTION");
            DropForeignKey("dbo.ITEMS", "ID_USER_ATTRI_2", "dbo.USER_ATTR_DESCRIPTION");
            DropForeignKey("dbo.ITEMS", "ID_USER_ATTRI_3", "dbo.USER_ATTR_DESCRIPTION");
            DropIndex("dbo.ITEMS", new[] { "ID_USER_ATTRI_1" });
            DropIndex("dbo.ITEMS", new[] { "ID_USER_ATTRI_2" });
            DropIndex("dbo.ITEMS", new[] { "ID_USER_ATTRI_3" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.ITEMS", "ID_USER_ATTRI_3");
            CreateIndex("dbo.ITEMS", "ID_USER_ATTRI_2");
            CreateIndex("dbo.ITEMS", "ID_USER_ATTRI_1");
            AddForeignKey("dbo.ITEMS", "ID_USER_ATTRI_3", "dbo.USER_ATTR_DESCRIPTION", "ID_USER_ATTR");
            AddForeignKey("dbo.ITEMS", "ID_USER_ATTRI_2", "dbo.USER_ATTR_DESCRIPTION", "ID_USER_ATTR");
            AddForeignKey("dbo.ITEMS", "ID_USER_ATTRI_1", "dbo.USER_ATTR_DESCRIPTION", "ID_USER_ATTR");
        }
    }
}
