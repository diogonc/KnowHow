namespace KnowHow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoParticipante : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cursoes", "QuantidadeDeParticipantes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cursoes", "QuantidadeDeParticipantes");
        }
    }
}
