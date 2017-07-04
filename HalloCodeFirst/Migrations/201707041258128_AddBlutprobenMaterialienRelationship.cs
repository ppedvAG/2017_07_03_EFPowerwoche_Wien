namespace HalloCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlutprobenMaterialienRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlutprobenMaterialien",
                c => new
                    {
                        ProbenId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProbenId, t.MaterialId })
                .ForeignKey("dbo.BlutProben", t => t.ProbenId, cascadeDelete: true)
                .ForeignKey("dbo.Materialien", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.ProbenId)
                .Index(t => t.MaterialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlutprobenMaterialien", "MaterialId", "dbo.Materialien");
            DropForeignKey("dbo.BlutprobenMaterialien", "ProbenId", "dbo.BlutProben");
            DropIndex("dbo.BlutprobenMaterialien", new[] { "MaterialId" });
            DropIndex("dbo.BlutprobenMaterialien", new[] { "ProbenId" });
            DropTable("dbo.BlutprobenMaterialien");
        }
    }
}
