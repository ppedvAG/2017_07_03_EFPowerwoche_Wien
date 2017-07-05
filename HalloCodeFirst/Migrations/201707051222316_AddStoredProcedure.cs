namespace HalloCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStoredProcedure : DbMigration
    {
        public override void Up()
        {
            Sql(@"
CREATE PROCEDURE GetUntersuchungenAfter
	@year int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Datum, Keim, Beschreibung
	FROM Untersuchungen
	WHERE YEAR(Datum) > @year
END
GO");
        }
        
        public override void Down()
        {
            Sql("DROP PROCEDURE GetUntersuchungenAfter");
        }
    }
}
