namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class detailBOM_User_20170606_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DETAIL_BOM_HF_HISTORY", "USER", c => c.String(maxLength: 20));
            AddColumn("dbo.DETAIL_BOM_HW_HISTORY", "USER", c => c.String(maxLength: 20));
            AddColumn("dbo.DETAIL_BOM_MT_HISTORY", "USER", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DETAIL_BOM_MT_HISTORY", "USER");
            DropColumn("dbo.DETAIL_BOM_HW_HISTORY", "USER");
            DropColumn("dbo.DETAIL_BOM_HF_HISTORY", "USER");
        }
    }
}
