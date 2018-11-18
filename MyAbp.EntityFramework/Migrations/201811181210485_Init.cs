namespace MyAbp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrderNumber = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Orders_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 64, storeType: "nvarchar"),
                        ProductModelsIdFK = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Products_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductModels", t => t.ProductModelsIdFK)
                .Index(t => t.ProductModelsIdFK);
            
            CreateTable(
                "dbo.ProductModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ModelNumber = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ProductModels_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrdersProducts",
                c => new
                    {
                        Orders_Id = c.Long(nullable: false),
                        Products_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Orders_Id, t.Products_Id })
                .ForeignKey("dbo.Orders", t => t.Orders_Id)
                .ForeignKey("dbo.Products", t => t.Products_Id)
                .Index(t => t.Orders_Id)
                .Index(t => t.Products_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdersProducts", "Products_Id", "dbo.Products");
            DropForeignKey("dbo.OrdersProducts", "Orders_Id", "dbo.Orders");
            DropForeignKey("dbo.Products", "ProductModelsIdFK", "dbo.ProductModels");
            DropIndex("dbo.OrdersProducts", new[] { "Products_Id" });
            DropIndex("dbo.OrdersProducts", new[] { "Orders_Id" });
            DropIndex("dbo.Products", new[] { "ProductModelsIdFK" });
            DropTable("dbo.OrdersProducts");
            DropTable("dbo.ProductModels",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ProductModels_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Products",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Products_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Orders",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Orders_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
