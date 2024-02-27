using Microsoft.EntityFrameworkCore;
using ScrapingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData.Configurations
{
    public class AppDbContext : DbContext 
    {
        public DbSet<ModelName> Models { get; set; }
        public DbSet<SubModel> SubModels { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<GroupOfParts> Groups { get; set; }
        public DbSet<SubGroup> SubGroups { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<SubPart> SubParts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Toyota;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelName>()
                .HasMany(x => x.SubModels)
                .WithOne(x => x.ModelName)
                .HasForeignKey(x => x.ModelNameId)
                .HasPrincipalKey(x => x.ModelNameId);

            modelBuilder.Entity<SubModel>()
                .HasMany(x => x.Equipments)
                .WithOne(x => x.Model)
                .HasForeignKey(x => x.ModelId)
                .HasPrincipalKey(x => x.ModelId);

            modelBuilder.Entity<Equipment>()
                .HasMany(x => x.GroupOfParts)
                .WithOne(x => x.Equipment)
                .HasForeignKey(x => x.EquipmentId)
                .HasPrincipalKey(x => x.EquipmentId);

            modelBuilder.Entity<GroupOfParts>()
                .HasMany(x => x.SubGroups)
                .WithOne(x => x.GroupOfPart)
                .HasForeignKey(x => x.GroupId)
                .HasPrincipalKey (x => x.GroupId);

            modelBuilder.Entity<SubGroup>()
                .HasMany(x => x.Parts)
                .WithOne(x => x.SubGroup)
                .HasForeignKey(x => x.SubGroupId)
                .HasPrincipalKey(x => x.SubGroupId);

            modelBuilder.Entity<Part>()
               .HasMany(x => x.SubParts)
               .WithOne(x => x.Part)
               .HasForeignKey(x => x.PartId)
               .HasPrincipalKey(x => x.PartId);
        }
    }
}
