namespace HalloCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlutprobenUnteruchungenRelationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Untersuchungen", "ProbeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Untersuchungen", "ProbeId");
            AddForeignKey("dbo.Untersuchungen", "ProbeId", "dbo.BlutProben", "ProbenId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Untersuchungen", "ProbeId", "dbo.BlutProben");
            DropIndex("dbo.Untersuchungen", new[] { "ProbeId" });
            DropColumn("dbo.Untersuchungen", "ProbeId");
        }
    }
}
