using Microsoft.EntityFrameworkCore;
using  ConstructionWebAPI.Entities;
using ConstructionWebAPI.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ConstructionWebAPI.Controllers;

namespace ConstructionWebAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Building> Buildings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(500);

                entity.Property(e => e.RefreshToken)
                      .HasMaxLength(500)
                      .IsRequired(false);  

                entity.Property(e => e.RefreshTokenExpiryTime)
                      .IsRequired(false);  

                //Enum Conversion :
                entity.Property(e => e.Gender)
                .HasConversion(new EnumToStringConverter<GenderEnum>())
                .HasColumnType("varchar(50)");
                entity.Property(e => e.UserRole)
                .HasConversion(new EnumToStringConverter<UserRoleEnum>())
                .HasColumnType("varchar(50)");
            
                

                //Index : 

                entity.HasIndex(e => e.Email)
                .IsUnique();

                //Relations : 

                entity.HasMany(e => e.Assignments)
                .WithOne(e => e.User).HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e=>e.Salary).IsRequired().HasColumnType("decimal(10,2)");
            

                //Enum Conversion : 
                entity.Property(e => e.Gender)
                .HasConversion(new EnumToStringConverter<GenderEnum>())
                .HasColumnType("varchar(50)");

                //Relations : 
                entity.HasMany(e => e.Assignments)
                .WithOne(e => e.Worker).HasForeignKey(e => e.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Building>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(10,2)");
                entity.Property(e => e.Address).IsRequired().HasMaxLength(50);
                //Enum Conversion :

                entity.Property(e => e.BuildingType)
                .HasConversion(new EnumToStringConverter<BuildingTypeEnum>())
                .HasColumnType("varchar(50)");

                //Relations : 
                entity.HasOne(e => e.Owner)
                .WithMany(e => e.Buildings).HasForeignKey(e => e.OwnerId).IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(e => e.Assignments).WithOne(e => e.Building)
                .HasForeignKey(e => e.BuildingId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.StartTime).IsRequired(false);
                entity.Property(e => e.EndTime).IsRequired(false);
                entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Description).IsRequired().HasMaxLength(300);

                //Enum Conversion : 
                entity.Property(e => e.Status).
                HasConversion(new EnumToStringConverter<AssignmentStatus>())
                .HasColumnType("varchar(50)");
                entity.Property(e => e.Priority).
             HasConversion(new EnumToStringConverter<PriorityEnum>())
             .HasColumnType("varchar(50)");

                entity.Property(e => e.UserId).IsRequired(false);
                entity.Property(e => e.WorkerId).IsRequired(false);
                entity.Property(e => e.BuildingId).IsRequired(false);


            });

        }

    }
}
