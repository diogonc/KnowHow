namespace KnowHow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadaUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cursoes", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cursoes", "Url");
        }
    }
}
