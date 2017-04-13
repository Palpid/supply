namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userAtt_20170413_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.USER_ATTR_DESCRIPTION", "ID_ITEM_GROUP", c => c.String(maxLength: 100));
            CreateIndex("dbo.USER_ATTR_DESCRIPTION", "ID_ITEM_GROUP");
            AddForeignKey("dbo.USER_ATTR_DESCRIPTION", "ID_ITEM_GROUP", "dbo.ITEM_GROUP", "ID_ITEM_GROUP");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.USER_ATTR_DESCRIPTION", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropIndex("dbo.USER_ATTR_DESCRIPTION", new[] { "ID_ITEM_GROUP" });
            DropColumn("dbo.USER_ATTR_DESCRIPTION", "ID_ITEM_GROUP");
        }
    }
}
