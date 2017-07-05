namespace Inheritance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TPH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Salary = c.Decimal(precision: 18, scale: 2),
                        PersonType = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.People");
        }
    }
}
