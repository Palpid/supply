namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FUNCTIONALITIES",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FUNCTIONALITY_NAME = c.String(nullable: false, maxLength: 50, unicode: false),
                        CATEGORY = c.String(nullable: false, maxLength: 20, unicode: false),
                        READ = c.Boolean(nullable: false),
                        NEW = c.Boolean(nullable: false),
                        MODIFY = c.Boolean(nullable: false),
                        ROLE_ID = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ROLES", t => t.ROLE_ID, cascadeDelete: true)
                .Index(t => t.ROLE_ID);
            
            CreateTable(
                "dbo.ROLES",
                c => new
                    {
                        ROLE_ID = c.String(nullable: false, maxLength: 20, unicode: false),
                        DESCRIPTION = c.String(nullable: false, maxLength: 200, unicode: false),
                        ENABLED = c.Boolean(nullable: false),
                        REMARKS = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.ROLE_ID);
            
            CreateTable(
                "dbo.USERS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        USER_LOGIN = c.String(nullable: false, maxLength: 20, unicode: false),
                        PASSWORD = c.String(nullable: false, maxLength: 100, unicode: false),
                        NAME = c.String(nullable: false, maxLength: 50, unicode: false),
                        ROLE_ID = c.String(nullable: false, maxLength: 20, unicode: false),
                        ENABLED = c.Boolean(nullable: false),
                        LAST_LOGOUT = c.DateTime(),
                        REMARKS = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ROLES", t => t.ROLE_ID, cascadeDelete: true)
                .Index(t => t.USER_LOGIN, unique: true, name: "IX_USERLOGIN_UNIQUE")
                .Index(t => t.ROLE_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.USERS", "ROLE_ID", "dbo.ROLES");
            DropForeignKey("dbo.FUNCTIONALITIES", "ROLE_ID", "dbo.ROLES");
            DropIndex("dbo.USERS", new[] { "ROLE_ID" });
            DropIndex("dbo.USERS", "IX_USERLOGIN_UNIQUE");
            DropIndex("dbo.FUNCTIONALITIES", new[] { "ROLE_ID" });
            DropTable("dbo.USERS");
            DropTable("dbo.ROLES");
            DropTable("dbo.FUNCTIONALITIES");
        }
    }
}
