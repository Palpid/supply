namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusPrototype_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.STATUS_PROTOTYPE",
                c => new
                    {
                        ID_STATUS_PROTOTYPE = c.Int(nullable: false, identity: false),
                        DESCRIPTION_EN = c.String(maxLength: 500),
                        ORDER = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_STATUS_PROTOTYPE);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.STATUS_PROTOTYPE");
        }
    }
}
