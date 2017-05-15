namespace HKSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColorModelStatusCial_DefincionCampos_24 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.COLORS", name: "ID_COLOR_", newName: "ID_COLOR");
            RenameColumn(table: "dbo.COLORS", name: "DESCRIPTION_", newName: "DESCRIPTION");
            RenameColumn(table: "dbo.MODELS", name: "ID_MODEL_", newName: "ID_MODEL");
            RenameColumn(table: "dbo.MODELS", name: "DESCRIPTION_", newName: "DESCRIPTION");
            RenameColumn(table: "dbo.STATUS_CIAL", name: "ID_STATUS_CIAL_", newName: "ID_STATUS_CIAL");
            RenameColumn(table: "dbo.STATUS_CIAL", name: "DESCRIPTION_", newName: "DESCRIPTION");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.STATUS_CIAL", name: "DESCRIPTION", newName: "DESCRIPTION_");
            RenameColumn(table: "dbo.STATUS_CIAL", name: "ID_STATUS_CIAL", newName: "ID_STATUS_CIAL_");
            RenameColumn(table: "dbo.MODELS", name: "DESCRIPTION", newName: "DESCRIPTION_");
            RenameColumn(table: "dbo.MODELS", name: "ID_MODEL", newName: "ID_MODEL_");
            RenameColumn(table: "dbo.COLORS", name: "DESCRIPTION", newName: "DESCRIPTION_");
            RenameColumn(table: "dbo.COLORS", name: "ID_COLOR", newName: "ID_COLOR_");
        }
    }
}
