namespace HalloCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnotationsToMaterialsTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Materials", newName: "Materialien");
            RenameColumn(table: "dbo.Materialien", name: "Id", newName: "MaterialId");
            AlterColumn("dbo.Materialien", "Name", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Materialien", "Name", c => c.String());
            RenameColumn(table: "dbo.Materialien", name: "MaterialId", newName: "Id");
            RenameTable(name: "dbo.Materialien", newName: "Materials");
        }
    }
}
