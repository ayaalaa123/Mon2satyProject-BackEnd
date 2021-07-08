namespace Mon2satyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMon2satyDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MessageFromSupplier = c.String(),
                        TimeFromSupplier = c.DateTime(nullable: false),
                        MessageFromAdmin = c.String(),
                        TimeFromAdmin = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        CompanyCode = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                        Susspended = c.String(),
                    })
                .PrimaryKey(t => t.CompanyCode);
            
            CreateTable(
                "dbo.NewSuppliers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        LegalPaperwork = c.String(nullable: false),
                        InfoPaperwork = c.String(nullable: false),
                        AddedDate = c.String(),
                        Password = c.String(nullable: false, maxLength: 20),
                        CompanyCode = c.String(),
                        ManagerName = c.String(nullable: false),
                        ManagerPhone = c.String(nullable: false),
                        Phone = c.String(),
                        Fax = c.String(),
                        SubCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PrivateTenders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Brochure = c.String(),
                        Date = c.DateTime(nullable: false),
                        SupplierID = c.Int(),
                        SubCategoryID = c.Int(nullable: false),
                        expireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierID)
                .Index(t => t.SupplierID)
                .Index(t => t.SubCategoryID);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        LegalPaperwork = c.String(nullable: false),
                        InfoPaperwork = c.String(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        Password = c.String(nullable: false, maxLength: 20),
                        CompanyCode = c.String(nullable: false),
                        ManagerName = c.String(nullable: false),
                        ManagerPhone = c.String(nullable: false),
                        Susspended = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PublicTenders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Brochure = c.String(),
                        Date = c.DateTime(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                        SupplierID = c.Int(),
                        SubCategoryID = c.Int(nullable: false),
                        TenderDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierID)
                .Index(t => t.SupplierID)
                .Index(t => t.SubCategoryID);
            
            CreateTable(
                "dbo.SupplierFaxes",
                c => new
                    {
                        SupplierID = c.Int(nullable: false),
                        Fax = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.SupplierID, t.Fax })
                .ForeignKey("dbo.Suppliers", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID);
            
            CreateTable(
                "dbo.SupplierMessages",
                c => new
                    {
                        SupplierID = c.Int(nullable: false),
                        MessageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SupplierID, t.MessageID })
                .ForeignKey("dbo.Chats", t => t.MessageID, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID)
                .Index(t => t.MessageID);
            
            CreateTable(
                "dbo.SupplierPhones",
                c => new
                    {
                        SupplierID = c.Int(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.SupplierID, t.Phone })
                .ForeignKey("dbo.Suppliers", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID);
            
            CreateTable(
                "dbo.SupplierPrivateTenders",
                c => new
                    {
                        SupplierID = c.Int(nullable: false),
                        PrivateTenderID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.SupplierID, t.PrivateTenderID })
                .ForeignKey("dbo.PrivateTenders", t => t.PrivateTenderID, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID)
                .Index(t => t.PrivateTenderID);
            
            CreateTable(
                "dbo.SupplierPublicTenders",
                c => new
                    {
                        SupplierID = c.Int(nullable: false),
                        PublicTenderID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Applied = c.String(),
                        Paid = c.String(),
                    })
                .PrimaryKey(t => new { t.SupplierID, t.PublicTenderID })
                .ForeignKey("dbo.PublicTenders", t => t.PublicTenderID, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID)
                .Index(t => t.PublicTenderID);
            
            CreateTable(
                "dbo.SupplierSubCategories",
                c => new
                    {
                        SupplierID = c.Int(nullable: false),
                        SubCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SupplierID, t.SubCategoryID })
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID)
                .Index(t => t.SubCategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupplierSubCategories", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.SupplierSubCategories", "SubCategoryID", "dbo.SubCategories");
            DropForeignKey("dbo.SupplierPublicTenders", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.SupplierPublicTenders", "PublicTenderID", "dbo.PublicTenders");
            DropForeignKey("dbo.SupplierPrivateTenders", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.SupplierPrivateTenders", "PrivateTenderID", "dbo.PrivateTenders");
            DropForeignKey("dbo.SupplierPhones", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.SupplierMessages", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.SupplierMessages", "MessageID", "dbo.Chats");
            DropForeignKey("dbo.SupplierFaxes", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.PublicTenders", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.PublicTenders", "SubCategoryID", "dbo.SubCategories");
            DropForeignKey("dbo.PrivateTenders", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.PrivateTenders", "SubCategoryID", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "CategoryID", "dbo.Categories");
            DropIndex("dbo.SupplierSubCategories", new[] { "SubCategoryID" });
            DropIndex("dbo.SupplierSubCategories", new[] { "SupplierID" });
            DropIndex("dbo.SupplierPublicTenders", new[] { "PublicTenderID" });
            DropIndex("dbo.SupplierPublicTenders", new[] { "SupplierID" });
            DropIndex("dbo.SupplierPrivateTenders", new[] { "PrivateTenderID" });
            DropIndex("dbo.SupplierPrivateTenders", new[] { "SupplierID" });
            DropIndex("dbo.SupplierPhones", new[] { "SupplierID" });
            DropIndex("dbo.SupplierMessages", new[] { "MessageID" });
            DropIndex("dbo.SupplierMessages", new[] { "SupplierID" });
            DropIndex("dbo.SupplierFaxes", new[] { "SupplierID" });
            DropIndex("dbo.PublicTenders", new[] { "SubCategoryID" });
            DropIndex("dbo.PublicTenders", new[] { "SupplierID" });
            DropIndex("dbo.SubCategories", new[] { "CategoryID" });
            DropIndex("dbo.PrivateTenders", new[] { "SubCategoryID" });
            DropIndex("dbo.PrivateTenders", new[] { "SupplierID" });
            DropTable("dbo.SupplierSubCategories");
            DropTable("dbo.SupplierPublicTenders");
            DropTable("dbo.SupplierPrivateTenders");
            DropTable("dbo.SupplierPhones");
            DropTable("dbo.SupplierMessages");
            DropTable("dbo.SupplierFaxes");
            DropTable("dbo.PublicTenders");
            DropTable("dbo.Suppliers");
            DropTable("dbo.SubCategories");
            DropTable("dbo.PrivateTenders");
            DropTable("dbo.NewSuppliers");
            DropTable("dbo.Logins");
            DropTable("dbo.Chats");
            DropTable("dbo.Categories");
        }
    }
}
