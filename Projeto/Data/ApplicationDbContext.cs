using Microsoft.EntityFrameworkCore;
using Projeto.Features.Client;
using Projeto.Features.Client.Dtos;
using Projeto.Features.Log;
using Projeto.Features.Log.Dtos;
using Projeto.Features.User;
using System.Text.Json;

namespace Projeto.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ClientDashboardDTO> DashboardClients { get; set; }
        public DbSet<LogReportDTO> LogsReport { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(50);
                entity.HasIndex(u => u.Name).HasDatabaseName("IX_USER_NAME");
                entity.Property(u => u.Email).IsRequired();
                entity.HasIndex(u => u.Email).IsUnique().HasDatabaseName("IX_USER_EMAIL");
                entity.Property(u => u.Password).IsRequired().HasMaxLength(255);
                entity.Property(u => u.Role).HasConversion<string>().HasDefaultValue(UserRole.User);
                entity.Property(u => u.Removed).HasDefaultValue(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
                entity.HasIndex(c => c.Name).HasDatabaseName("IX_CLIENT_NAME");
                entity.Property(c => c.Age).IsRequired(false);
                entity.Property(c => c.Email).IsRequired();
                entity.HasIndex(c => c.Email).IsUnique().HasDatabaseName("IX_CLIENT_EMAIL");
                entity.Property(c => c.Address).IsRequired(false).HasMaxLength(100);
                entity.Property(c => c.Others).IsRequired(false).HasMaxLength(300);
                entity.Property(c => c.Interests).IsRequired().HasMaxLength(300);
                entity.Property(c => c.Feelings).IsRequired().HasMaxLength(300);
                entity.Property(c => c.Values).IsRequired().HasMaxLength(300);
                entity.Property(c => c.IsActive).HasDefaultValue(true);
                entity.HasIndex(c => c.IsActive).HasDatabaseName("IX_CLIENT_ISINACTIVE").HasFilter("[IsActive] = 0");
                entity.Property(c => c.RegisterDate).IsRequired();
                entity.HasIndex(c => c.RegisterDate).HasDatabaseName("IX_CLIENT_REGISTERDATE");
                entity.Property(c => c.UserId).IsRequired();
                entity.Property(c => c.Removed).HasDefaultValue(false);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.TimeDate).IsRequired();
                entity.Property(l => l.UserEmail).IsRequired();
                entity.Property(l => l.Action).IsRequired();

                entity.Property(l => l.OldData)
                    .HasConversion(
                        v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => v == null ? null : JsonSerializer.Deserialize<ClientAuditDTO>(v, (JsonSerializerOptions)null))
                    .IsRequired(false);

                entity.Property(l => l.NewData)
                    .HasConversion(
                        v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => v == null ? null : JsonSerializer.Deserialize<ClientAuditDTO>(v, (JsonSerializerOptions)null))
                    .IsRequired(false);

                entity.Property(l => l.Removed).HasDefaultValue(false);
            });

            modelBuilder.Entity<ClientDashboardDTO>(entity =>
            {
                entity.ToView("DashboardList");
                entity.HasNoKey();
            });

            modelBuilder.Entity<LogReportDTO>(entity =>
            {
                entity.ToTable((string)null);
                entity.HasNoKey();
            });
        }
    }
}