namespace HalloCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWurscht : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.BlutProben", "Wurscht", c => c.String(nullable: false));
            Sql("ALTER TABLE BlutProben ADD Wurscht AS([LFBIS] + ' - ' + CONVERT(nvarchar(4), Year(Datum)) + CONVERT(nvarchar(2), Month(Datum)) + CONVERT(nvarchar(2), Day(Datum)));");
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlutProben", "Wurscht");
        }
    }
}
