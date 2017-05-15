namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class familyHK_2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FAMILY_HK", newName: "FAMILIES_HK");
            DropForeignKey("dbo.ITEMS", "ID_FAMILY_HK", "dbo.FAMILY_HK");
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_FAMILY_HK", "dbo.FAMILY_HK");
            DropPrimaryKey("dbo.FAMILIES_HK");
            //AddColumn("dbo.FAMILIES_HK", "ID_FAMILY_HK", c => c.String(nullable: false, maxLength: 100));
            RenameColumn("dbo.FAMILIES_HK", "ID", "ID_FAMILY_HK");
            AlterColumn("dbo.FAMILIES_HK", "ID_FAMILY_HK", c => c.String(nullable: false, maxLength: 100));
            
            AlterColumn("dbo.ITEMS", "ID_FAMILY_HK", c => c.String(maxLength: 100));
            AlterColumn("dbo.ITEMS_HISTORY", "ID_FAMILY_HK", c => c.String(maxLength: 100));

            AlterColumn("dbo.FAMILIES_HK", "DESCRIPTION", c => c.String(nullable: false, maxLength: 500));
            AddPrimaryKey("dbo.FAMILIES_HK", "ID_FAMILY_HK");
            AddForeignKey("dbo.ITEMS", "ID_FAMILY_HK", "dbo.FAMILIES_HK", "ID_FAMILY_HK");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_FAMILY_HK", "dbo.FAMILIES_HK", "ID_FAMILY_HK");
            //DropColumn("dbo.FAMILIES_HK", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FAMILIES_HK", "ID", c => c.String(nullable: false, maxLength: 30));
            DropForeignKey("dbo.ITEMS_HISTORY", "ID_FAMILY_HK", "dbo.FAMILIES_HK");
            DropForeignKey("dbo.ITEMS", "ID_FAMILY_HK", "dbo.FAMILIES_HK");
            DropPrimaryKey("dbo.FAMILIES_HK");
            AlterColumn("dbo.FAMILIES_HK", "DESCRIPTION", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.FAMILIES_HK", "ID_FAMILY_HK");
            AddPrimaryKey("dbo.FAMILIES_HK", "ID");
            AddForeignKey("dbo.ITEMS_HISTORY", "ID_FAMILY_HK", "dbo.FAMILY_HK", "ID");
            AddForeignKey("dbo.ITEMS", "ID_FAMILY_HK", "dbo.FAMILY_HK", "ID");
            RenameTable(name: "dbo.FAMILIES_HK", newName: "FAMILY_HK");
        }
    }
}
