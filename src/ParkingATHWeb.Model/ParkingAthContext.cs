using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.Model
{
    public class ParkingAthContext : DbContext
    {
        private readonly bool _useInMemory;

        public ParkingAthContext(bool useInMemory = false)
        {
            _useInMemory = useInMemory;
        }


        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<GateUsage> GateUsage { get; set; }
        public virtual DbSet<PriceTreshold> PriceTreshold { get; set; }
        public virtual DbSet<Token> Token { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_useInMemory)
            {
                optionsBuilder.UseInMemoryDatabase();
            }
            else
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ParkingATHWeb.Data;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.GateUsages)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<PriceTreshold>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.PriceTreshold)
                .HasForeignKey(x => x.PriceTresholdId);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.PriceTreshold)
                .WithMany(x => x.Orders)
                .HasPrincipalKey(x => x.Id);

            modelBuilder.Entity<User>()
                .HasOne(x => x.PasswordChangeToken)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(x => x.EmailChangeToken)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
