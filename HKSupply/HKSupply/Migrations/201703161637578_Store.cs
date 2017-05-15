namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Store : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.STORES",
                c => new
                    {
                        ID_STORE = c.String(nullable: false, maxLength: 128),
                        NAME = c.String(),
                        ACTIVE = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID_STORE);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.STORES");
        }
    }
}
