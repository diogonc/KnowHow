namespace KnowHow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criadasTabelas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cursoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descricao = c.String(),
                        Data = c.DateTime(nullable: false),
                        Local = c.String(),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Organizador = c.String(),
                        QuantidadeDeInteressados = c.Int(nullable: false),
                        UrlDaImagem = c.String(),
                        HoraDeInicio = c.String(),
                        Duracao = c.String(),
                        Aprovado = c.Boolean(nullable: false),
                        Categoria_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.Categoria_Id)
                .Index(t => t.Categoria_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cursoes", "Categoria_Id", "dbo.Categorias");
            DropIndex("dbo.Cursoes", new[] { "Categoria_Id" });
            DropTable("dbo.Cursoes");
            DropTable("dbo.Categorias");
        }
    }
}
