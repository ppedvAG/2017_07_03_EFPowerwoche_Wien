namespace Inheritance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TPT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fahrzeuge",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Geschwindigkeit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LKWs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MaxLadung = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fahrzeuge", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.PKWs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Sitzplaetzte = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fahrzeuge", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PKWs", "Id", "dbo.Fahrzeuge");
            DropForeignKey("dbo.LKWs", "Id", "dbo.Fahrzeuge");
            DropIndex("dbo.PKWs", new[] { "Id" });
            DropIndex("dbo.LKWs", new[] { "Id" });
            DropTable("dbo.PKWs");
            DropTable("dbo.LKWs");
            DropTable("dbo.Fahrzeuge");
        }
    }
}
