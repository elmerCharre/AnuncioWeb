namespace Ads.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class App : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 100),
                        detail = c.String(nullable: false, unicode: false, storeType: "text"),
                        customer_id = c.Int(nullable: false),
                        category_Id = c.Int(nullable: false),
                        price = c.Decimal(nullable: true, precision: 18, scale: 2),
                        date = c.DateTime(nullable: false),
                        amueblado = c.Int(),
                        cuartos = c.Int(),
                        cuartos_banio = c.Int(),
                        comision = c.Int(),
                        metros = c.String(),
                        tipo = c.Int(),
                        opcion_empleo = c.Int(),
                        compania = c.String(),
                        tiempo = c.Int(),
                        experiencia = c.Int(),
                        pago = c.Int(),
                        salario = c.Decimal(precision: 18, scale: 2),
                        marca = c.Int(),
                        modelo = c.Int(),
                        anio = c.Int(),
                        kilometraje = c.String(),
                        vin = c.String(),
                        condicion = c.Int(),
                        Discriminator = c.String(maxLength: 20),
                        Type = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.categories", t => t.category_Id)
                .ForeignKey("dbo.customers", t => t.customer_id)
                .Index(t => t.customer_id)
                .Index(t => t.category_Id);
            
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.articleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        category_id = c.Int(nullable: false),
                        type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.categories", t => t.category_id)
                .Index(t => t.category_id);
            
            CreateTable(
                "dbo.relationship_condition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        condition_id = c.Int(nullable: false),
                        articleType_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.conditions", t => t.condition_id)
                .ForeignKey("dbo.articleTypes", t => t.articleType_id)
                .Index(t => t.condition_id)
                .Index(t => t.articleType_id);
            
            CreateTable(
                "dbo.conditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.relationship_marca",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        marca_id = c.Int(nullable: false),
                        articleType_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.marcas", t => t.marca_id)
                .ForeignKey("dbo.articleTypes", t => t.articleType_id)
                .Index(t => t.marca_id)
                .Index(t => t.articleType_id);
            
            CreateTable(
                "dbo.marcas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.modelos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        marca_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.marcas", t => t.marca_id)
                .Index(t => t.marca_id);
            
            CreateTable(
                "dbo.tipos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        articleType_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.articleTypes", t => t.articleType_id)
                .Index(t => t.articleType_id);
            
            CreateTable(
                "dbo.customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        fullname = c.String(nullable: false, maxLength: 100),
                        email = c.String(nullable: false, maxLength: 100),
                        phone = c.String(nullable: false, maxLength: 40),
                        occupation = c.String(maxLength: 80),
                        address = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.resources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        path = c.String(nullable: false, maxLength: 100),
                        type = c.String(nullable: false, maxLength: 20),
                        article_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.articles", t => t.article_id)
                .Index(t => t.article_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.resources", "article_id", "dbo.articles");
            DropForeignKey("dbo.articles", "customer_id", "dbo.customers");
            DropForeignKey("dbo.articleTypes", "category_id", "dbo.categories");
            DropForeignKey("dbo.tipos", "articleType_id", "dbo.articleTypes");
            DropForeignKey("dbo.relationship_marca", "articleType_id", "dbo.articleTypes");
            DropForeignKey("dbo.relationship_marca", "marca_id", "dbo.marcas");
            DropForeignKey("dbo.modelos", "marca_id", "dbo.marcas");
            DropForeignKey("dbo.relationship_condition", "articleType_id", "dbo.articleTypes");
            DropForeignKey("dbo.relationship_condition", "condition_id", "dbo.conditions");
            DropForeignKey("dbo.articles", "category_Id", "dbo.categories");
            DropIndex("dbo.resources", new[] { "article_id" });
            DropIndex("dbo.tipos", new[] { "articleType_id" });
            DropIndex("dbo.modelos", new[] { "marca_id" });
            DropIndex("dbo.relationship_marca", new[] { "articleType_id" });
            DropIndex("dbo.relationship_marca", new[] { "marca_id" });
            DropIndex("dbo.relationship_condition", new[] { "articleType_id" });
            DropIndex("dbo.relationship_condition", new[] { "condition_id" });
            DropIndex("dbo.articleTypes", new[] { "category_id" });
            DropIndex("dbo.articles", new[] { "category_Id" });
            DropIndex("dbo.articles", new[] { "customer_id" });
            DropTable("dbo.resources");
            DropTable("dbo.customers");
            DropTable("dbo.tipos");
            DropTable("dbo.modelos");
            DropTable("dbo.marcas");
            DropTable("dbo.relationship_marca");
            DropTable("dbo.conditions");
            DropTable("dbo.relationship_condition");
            DropTable("dbo.articleTypes");
            DropTable("dbo.categories");
            DropTable("dbo.articles");
        }
    }
}
