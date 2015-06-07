namespace Ads.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class elmer : DbMigration
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
                        category_Id = c.Int(),
                        precio = c.Decimal(precision: 18, scale: 2),
                        amueblado = c.Int(),
                        cuartos = c.Int(),
                        cuartos_banio = c.Int(),
                        comision = c.Int(),
                        metros = c.String(),
                        precio1 = c.Decimal(precision: 18, scale: 2),
                        marca = c.Int(),
                        modelo = c.Int(),
                        tipo = c.Int(),
                        anio = c.Int(),
                        kilometraje = c.String(),
                        vin = c.String(),
                        condicion = c.Int(),
                        Discriminator = c.String(maxLength: 128),
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
            DropForeignKey("dbo.articles", "category_Id", "dbo.categories");
            DropIndex("dbo.resources", new[] { "article_id" });
            DropIndex("dbo.articleTypes", new[] { "category_id" });
            DropIndex("dbo.articles", new[] { "category_Id" });
            DropIndex("dbo.articles", new[] { "customer_id" });
            DropTable("dbo.resources");
            DropTable("dbo.customers");
            DropTable("dbo.articleTypes");
            DropTable("dbo.categories");
            DropTable("dbo.articles");
        }
    }
}
