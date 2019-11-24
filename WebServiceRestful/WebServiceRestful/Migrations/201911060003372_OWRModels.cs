namespace WebServiceRestful.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OWRModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NOTA",
                c => new
                    {
                        ID_NOTA = c.Int(nullable: false, identity: true),
                        DESCRICAO = c.String(maxLength: 500, unicode: false),
                        ID_USUARIO = c.Int(),
                        DATA = c.DateTime(storeType: "date"),
                        COR = c.String(maxLength: 25, unicode: false),
                        TITULO = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ID_NOTA)
                .ForeignKey("dbo.USUARIO", t => t.ID_USUARIO)
                .Index(t => t.ID_USUARIO);
            
            CreateTable(
                "dbo.USUARIO",
                c => new
                    {
                        ID_USUARIO = c.Int(nullable: false, identity: true),
                        NOME = c.String(maxLength: 500, unicode: false),
                        LOGIN = c.String(maxLength: 100, unicode: false),
                        SENHA = c.String(maxLength: 100, unicode: false),
                        MATRICULA = c.String(maxLength: 50, unicode: false),
                        ID_ROLE = c.Int(),
                    })
                .PrimaryKey(t => t.ID_USUARIO)
                .ForeignKey("dbo.ROLE", t => t.ID_ROLE)
                .Index(t => t.ID_ROLE);
            
            CreateTable(
                "dbo.PONTO",
                c => new
                    {
                        ID_PONTO = c.Int(nullable: false, identity: true),
                        ID_USUARIO = c.Int(),
                        PONTO_ENTRADA = c.DateTime(storeType: "date"),
                        PONTO_ENTRADA_INTERVALO = c.DateTime(storeType: "date"),
                        PONTO_SAIDA_INTERVALO = c.DateTime(storeType: "date"),
                        PONTO_SAIDA = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID_PONTO)
                .ForeignKey("dbo.USUARIO", t => t.ID_USUARIO)
                .Index(t => t.ID_USUARIO);
            
            CreateTable(
                "dbo.ROLE",
                c => new
                    {
                        ID_ROLE = c.Int(nullable: false, identity: true),
                        DESCRICAO = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ID_ROLE);
            
            CreateTable(
                "dbo.TAREFA",
                c => new
                    {
                        ID_TAREFA = c.Int(nullable: false, identity: true),
                        DESCRICAO = c.String(maxLength: 500, unicode: false),
                        ID_USUARIO = c.Int(),
                        DATA_INICIO = c.DateTime(storeType: "date"),
                        DATA_FIM = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID_TAREFA)
                .ForeignKey("dbo.USUARIO", t => t.ID_USUARIO)
                .Index(t => t.ID_USUARIO);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAREFA", "ID_USUARIO", "dbo.USUARIO");
            DropForeignKey("dbo.USUARIO", "ID_ROLE", "dbo.ROLE");
            DropForeignKey("dbo.PONTO", "ID_USUARIO", "dbo.USUARIO");
            DropForeignKey("dbo.NOTA", "ID_USUARIO", "dbo.USUARIO");
            DropIndex("dbo.TAREFA", new[] { "ID_USUARIO" });
            DropIndex("dbo.PONTO", new[] { "ID_USUARIO" });
            DropIndex("dbo.USUARIO", new[] { "ID_ROLE" });
            DropIndex("dbo.NOTA", new[] { "ID_USUARIO" });
            DropTable("dbo.TAREFA");
            DropTable("dbo.ROLE");
            DropTable("dbo.PONTO");
            DropTable("dbo.USUARIO");
            DropTable("dbo.NOTA");
        }
    }
}
