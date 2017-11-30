namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boxes_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BOXES",
                c => new
                    {
                        ID_BOX = c.String(nullable: false, maxLength: 50),
                        DESCRIPTION = c.String(nullable: false, maxLength: 250),
                        LENGTH = c.Int(nullable: false),
                        WIDTH = c.Int(nullable: false),
                        HEIGHT = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_BOX);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BOXES");
        }
    }
}
