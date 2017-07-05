namespace HalloCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMaterialienTabelle : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Materialien (Name, Beschreibung) VALUES ('Milch', NULL)");
            Sql("INSERT INTO Materialien (Name, Beschreibung) VALUES ('Blut', NULL)");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Materialien WHERE Name = 'Milch'");
            Sql("DELETE FROM Materialien WHERE Name = 'Blut'");
        }
    }
}
