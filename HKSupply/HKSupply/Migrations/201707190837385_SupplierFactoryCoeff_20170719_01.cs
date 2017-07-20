namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupplierFactoryCoeff_20170719_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SUPPLIERS_FACTORIES_COEFF",
                c => new
                    {
                        ID_SUPPLIER = c.String(nullable: false, maxLength: 100),
                        ID_FACTORY = c.String(nullable: false, maxLength: 100),
                        COEFFICIENT1 = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        COEFFICIENT2 = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                        SCRAP = c.Decimal(nullable: false, precision: 19, scale: 6, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.ID_SUPPLIER, t.ID_FACTORY })
                .ForeignKey("dbo.SUPPLIERS", t => t.ID_FACTORY, cascadeDelete: false)
                .ForeignKey("dbo.SUPPLIERS", t => t.ID_SUPPLIER, cascadeDelete: false)
                .Index(t => t.ID_SUPPLIER)
                .Index(t => t.ID_FACTORY);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SUPPLIERS_FACTORIES_COEFF", "ID_SUPPLIER", "dbo.SUPPLIERS");
            DropForeignKey("dbo.SUPPLIERS_FACTORIES_COEFF", "ID_FACTORY", "dbo.SUPPLIERS");
            DropIndex("dbo.SUPPLIERS_FACTORIES_COEFF", new[] { "ID_FACTORY" });
            DropIndex("dbo.SUPPLIERS_FACTORIES_COEFF", new[] { "ID_SUPPLIER" });
            DropTable("dbo.SUPPLIERS_FACTORIES_COEFF");
        }
    }
}
