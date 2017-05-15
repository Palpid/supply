namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ObjectVersion_2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.OBJECTS_VERSIONS");
        }
        
        public override void Down()
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
    }
}
