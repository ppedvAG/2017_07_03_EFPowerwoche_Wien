namespace HalloCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConfigurationToBlutprobenTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Blutprobes", newName: "BlutProben");
            RenameColumn(table: "dbo.BlutProben", name: "Id", newName: "ProbenId");
            AlterColumn("dbo.BlutProben", "LFBIS", c => c.String(nullable: false, maxLength: 7));
            AlterColumn("dbo.BlutProben", "Datum", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BlutProben", "Datum", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BlutProben", "LFBIS", c => c.String());
            RenameColumn(table: "dbo.BlutProben", name: "ProbenId", newName: "Id");
            RenameTable(name: "dbo.BlutProben", newName: "Blutprobes");
        }
    }
}
