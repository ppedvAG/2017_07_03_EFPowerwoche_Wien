namespace HalloCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUntersuchungenTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Untersuchungs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Keim = c.String(),
                        Datum = c.DateTime(nullable: false),
                        Beschreibung = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Untersuchungs");
        }
    }
}
