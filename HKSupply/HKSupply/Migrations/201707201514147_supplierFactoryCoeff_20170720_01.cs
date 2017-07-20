namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class supplierFactoryCoeff_20170720_01 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SUPPLIERS_FACTORIES_COEFF");
            AddColumn("dbo.SUPPLIERS_FACTORIES_COEFF", "ID_ITEM_GROUP", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.SUPPLIERS_FACTORIES_COEFF", "DENSITY", c => c.Decimal(precision: 19, scale: 6, storeType: "numeric"));
            AddPrimaryKey("dbo.SUPPLIERS_FACTORIES_COEFF", new[] { "ID_SUPPLIER", "ID_FACTORY", "ID_ITEM_GROUP" });
            CreateIndex("dbo.SUPPLIERS_FACTORIES_COEFF", "ID_ITEM_GROUP");
            AddForeignKey("dbo.SUPPLIERS_FACTORIES_COEFF", "ID_ITEM_GROUP", "dbo.ITEM_GROUP", "ID_ITEM_GROUP", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SUPPLIERS_FACTORIES_COEFF", "ID_ITEM_GROUP", "dbo.ITEM_GROUP");
            DropIndex("dbo.SUPPLIERS_FACTORIES_COEFF", new[] { "ID_ITEM_GROUP" });
            DropPrimaryKey("dbo.SUPPLIERS_FACTORIES_COEFF");
            DropColumn("dbo.SUPPLIERS_FACTORIES_COEFF", "DENSITY");
            DropColumn("dbo.SUPPLIERS_FACTORIES_COEFF", "ID_ITEM_GROUP");
            AddPrimaryKey("dbo.SUPPLIERS_FACTORIES_COEFF", new[] { "ID_SUPPLIER", "ID_FACTORY" });
        }
    }
}
