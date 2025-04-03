using Microsoft.EntityFrameworkCore;
using MunicipalityManagementSystem.Models;
using System;

namespace MunicipalityManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define primary keys
            modelBuilder.Entity<Citizen>().HasKey(c => c.CitizenID);
            modelBuilder.Entity<ServiceRequest>().HasKey(sr => sr.RequestID);
            modelBuilder.Entity<Staff>().HasKey(s => s.StaffID);
            modelBuilder.Entity<Report>().HasKey(r => r.ReportID);

            // Define foreign key relationships
            modelBuilder.Entity<ServiceRequest>()
                .HasOne<Citizen>()
                .WithMany()
                .HasForeignKey(sr => sr.CitizenID);

            modelBuilder.Entity<Report>()
                .HasOne<Citizen>()
                .WithMany()
                .HasForeignKey(r => r.CitizenID);

            // Seed data
            modelBuilder.Entity<Citizen>().HasData(
                new Citizen { CitizenID = 1, FullName = "John Doe", Address = "123 Main St", PhoneNumber = "123-456-7890", Email = "john.doe@example.com", DateOfBirth = new DateTime(1980, 1, 1), RegistrationDate = DateTime.Now },
                new Citizen { CitizenID = 2, FullName = "Jane Smith", Address = "456 Elm St", PhoneNumber = "987-654-3210", Email = "jane.smith@example.com", DateOfBirth = new DateTime(1990, 2, 2), RegistrationDate = DateTime.Now }
            );

            modelBuilder.Entity<Staff>().HasData(
                new Staff { StaffID = 1, FullName = "Alice Johnson", Position = "Manager", Department = "HR", Email = "alice.johnson@example.com", PhoneNumber = "555-123-4567", HireDate = new DateTime(2015, 3, 3) },
                new Staff { StaffID = 2, FullName = "Bob Brown", Position = "Technician", Department = "IT", Email = "bob.brown@example.com", PhoneNumber = "555-987-6543", HireDate = new DateTime(2018, 4, 4) }
            );
        }
    }
}

