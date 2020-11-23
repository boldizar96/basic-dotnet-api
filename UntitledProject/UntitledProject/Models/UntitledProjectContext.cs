using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UntitledProject.Models;

namespace UntitledProject.Models
{
    public partial class UntitledProjectContext : IdentityDbContext<AppUser>
    {

        public UntitledProjectContext(DbContextOptions<UntitledProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                //entity.HasKey(e => e.UserId)
                //    .HasName("PK__AppUser__1788CC4CA13EBEE7");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                ////entity.Property(e => e.Password)
                //    .IsRequired()
                //    .HasMaxLength(20)
                //    .IsUnicode(false);


                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasMany<Product>();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                // entity.Property(e => e.Category)
                //   .IsRequired()
                //  .HasMaxLength(100)
                //.IsUnicode(false);
                entity.HasMany<Category>(e => e.Categories);
                

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Offerer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasMany<Product>(c => c.Products);
            });

            base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<UntitledProject.Models.Category> Category { get; set; }
    }
}
