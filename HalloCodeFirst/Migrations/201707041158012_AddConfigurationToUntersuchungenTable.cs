namespace HalloCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConfigurationToUntersuchungenTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Untersuchungs", newName: "Untersuchungen");
            RenameColumn(table: "dbo.Untersuchungen", name: "Id", newName: "UntersuchungsId");
            AlterColumn("dbo.Untersuchungen", "Keim", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Untersuchungen", "Datum", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Untersuchungen", "Datum", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Untersuchungen", "Keim", c => c.String());
            RenameColumn(table: "dbo.Untersuchungen", name: "UntersuchungsId", newName: "Id");
            RenameTable(name: "dbo.Untersuchungen", newName: "Untersuchungs");
        }
    }
}
