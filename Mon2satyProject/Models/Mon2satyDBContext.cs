namespace Mon2satyProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Mon2satyDBContext : DbContext
    {
        // Your context has been configured to use a 'Mon2satyDBContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Mon2satyProject.Models.Mon2satyDBContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Mon2satyDBContext' 
        // connection string in the application configuration file.
        public Mon2satyDBContext()
            : base("name=Mon2satyDBContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<NewSupplier> NewSuppliers { get; set; }

        public DbSet<SupplierFaxes> SupplierFaxes { get; set; }

        public DbSet<SupplierPhones> SupplierPhones { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<SupplierSubCategories> SupplierSubCategories { get; set; }

        public DbSet<PublicTender> PublicTenders { get; set; }

        public DbSet<PrivateTender> PrivateTenders { get; set; }

        public DbSet<SupplierPrivateTender> SupplierPrivateTenders { get; set; }

        public DbSet<SupplierPublicTender> SupplierPublicTenders { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<SupplierMessages> SupplierMessages { get; set; }

        public DbSet<Login> Logins { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}