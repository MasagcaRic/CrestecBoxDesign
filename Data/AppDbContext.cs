using System.Data.Entity;
using Final_Project.Data.Entities; // Make sure this matches your actual namespace

namespace Final_Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=MyConnectionString") { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<BoxRequest> BoxRequests { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<NatureOfProject> NatureOfProjects { get; set; }
        public DbSet<OtherTest> OtherTests { get; set; }
        public DbSet<PrintingDetails> PrintingDetails { get; set; }
        public DbSet<QualityCheck> QualityChecks { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<SalesInCharge> SalesInCharges { get; set; }
        public DbSet<SpecialInstruction> SpecialInstructions { get; set; }
        public DbSet<TaskProgress> TaskProgresses { get; set; }
        public DbSet<Testing> Testings { get; set; }
        public DbSet<TestRequirement> TestRequirements { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
