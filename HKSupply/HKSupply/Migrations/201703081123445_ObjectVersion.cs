namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ObjectVersion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OBJECTS_VERSIONS",
                c => new
                    {
                        OBJECT = c.String(nullable: false, maxLength: 128),
                        VERSION = c.Int(nullable: false),
                        SUBVERSION = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OBJECT);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OBJECTS_VERSIONS");
        }
    }
}
