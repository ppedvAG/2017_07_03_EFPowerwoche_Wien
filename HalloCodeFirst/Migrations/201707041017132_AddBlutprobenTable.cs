namespace HalloCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlutprobenTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blutprobes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LFBIS = c.String(),
                        Datum = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Blutprobes");
        }
    }
}
