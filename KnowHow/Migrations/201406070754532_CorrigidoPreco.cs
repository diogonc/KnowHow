namespace KnowHow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrigidoPreco : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cursoes", "Preco", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cursoes", "Preco", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
