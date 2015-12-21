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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_useInMemory)
            {
                optionsBuilder.UseInMemoryDatabase();
            }
            else
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ParkingATHWeb.Db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserProfileId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.GateUsages)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserProfileId);

            modelBuilder.Entity<PriceTreshold>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.PriceTreshold)
                .HasForeignKey(x => x.PriceTresholdId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.PasswordChangeToken)
                .WithMany(x => x.UserPasswordChangeTokens)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(x => x.EmailChangeToken)
                .WithMany(x => x.UserEmailChangeTokens)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
