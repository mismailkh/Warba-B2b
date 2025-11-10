using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using WB.Domain.Entities.AuditLogs;
using WB.Domain.Entities.LOB;
using WB.Domain.Entities.Lookups;
using WB.Domain.Entities.Notification;
using WB.Domain.Entities.Organization;
using WB.Domain.Entities.Ums;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Infrastructure.DbContext
{
    public class DatabaseContext : IdentityDbContext<User, Role, string, UserClaims, UserRoles, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        #region Models
        public virtual DbSet<Claim> Claim { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRoles> UserRoles { get; set; } = null!;
        public virtual DbSet<UserActivity> UserActivity { get; set; } = null!;
        public virtual DbSet<UserPersonalInformation> UserPersonalInformation { get; set; } = null!;
        public virtual DbSet<NotificationEvent> NotificationEvents { get; set; }
        public virtual DbSet<NotificationTemplate> NotificationTemplates { get; set; } = null!;
        public virtual DbSet<NotificationEventPlaceholders> NotificationEventPlaceholders { get; set; } = null!;  
        public virtual DbSet<Notification> Notifications { get; set; } = null!; 
        public virtual DbSet<Translation> Translation { get; set; } = null!; 
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; } = null!; 
        public virtual DbSet<ProcessLog> ProcessLogs { get; set; } = null!;
        public virtual DbSet<Product> Product { get; set; } = null!;
        public virtual DbSet<Process> Process { get; set; } = null!;
        public virtual DbSet<Subprocess> Subprocess { get; set; } = null!;
        public virtual DbSet<ProductProcess> ProductProcess { get; set; } = null!;
        public virtual DbSet<ProcessSubprocess> ProcessSubprocess { get; set; } = null!;
        public virtual DbSet<ProductProcessSubprocess> ProductProcessSubprocess { get; set; } = null!;
        public virtual DbSet<ListProcessLogResponseDto> ProcessLogResponeDto { get; set; } = null!;

        #region Organization
        public virtual DbSet<Organization> Organizations { get; set; } = null!;
        public virtual DbSet<OrganizationPaymentMethod> OrganizationPaymentMethods { get; set; } = null!;
        public virtual DbSet<OrganizationType> OrganizationTypes { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Designation> Designations { get; set; } = null!;
        #endregion

        #endregion

        #region VMs
        [NotMapped]
        public virtual DbSet<LookupListResponseDto> LookupListResponseDto { get; set; }
        [NotMapped]
        public virtual DbSet<UserListResponseDto> UserListResponseDto { get; set; } = null!;
        [NotMapped]
        public virtual DbSet<GroupListResponseDto> GroupListResponseDto { get; set; } = null!;
        public virtual DbSet<RolesListResponseDto> RolesListResponseDto { get; set; } = null!;
        [NotMapped]
        public virtual DbSet<TodoItemListResponseDto> TodoItemListResponseDto { get; set; } = null!;
        [NotMapped]
        public virtual DbSet<NotificationResponseDto> NotificationListResponseDto { get; set; } = null!;
        public virtual DbSet<NotificationEventResponseDto> EventListResponseDto { get; set; } = null!;
        public virtual DbSet<NotificationTemplateResponseDto> TemplateListResponseDto { get; set; } = null!;
        [NotMapped]
        public virtual DbSet<UsersDetailResponseDto> UsersDetailResponseDto { get; set; } = null!;
        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().Ignore(u => u.PhoneNumber);
            builder.Entity<User>().Ignore(u => u.PhoneNumberConfirmed);
            builder.Entity<Role>().Ignore(u => u.Name);
            builder.Entity<User>().ToTable("USER", "ums");
            builder.Entity<Role>().ToTable("ROLE_PR", "ums");
            builder.Entity<UserRoles>().ToTable("USER_ROLE", "ums");

            builder.Entity<User>()
                .HasMany(t => t.UserRoles)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .HasPrincipalKey(t => t.Id);

            builder.Entity<User>()
                .HasOne(t => t.PersonalInformation)
                .WithOne(t => t.User);


            builder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
                entity.HasOne(ur => ur.User)
              .WithMany(u => u.UserRoles)
              .HasForeignKey(ur => ur.UserId);

                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRole)
                      .HasForeignKey(ur => ur.RoleId);
            });

            builder.Entity<OrganizationType>(entity =>
            {
                entity.ToTable("ORGANIZATION_TYPE", "org");
                entity.HasKey(o => o.Id);
            });

            builder.Entity<Organization>(entity =>
            {
                entity.ToTable("ORGANIZATION", "org");
                entity.HasKey(o => o.Id);
                entity.HasOne(o => o.OrganizationType)
                      .WithMany(t => t.Organizations)
                      .HasForeignKey(o => o.TypeId);
                entity.HasMany(o => o.OrganizationPaymentMethods)
                       .WithOne(opm => opm.Organization)
                       .HasForeignKey(opm => opm.OrganizationId);
            });
            builder.Entity<OrganizationPaymentMethod>(entity =>
            {
                entity.ToTable("ORGANIZATION_PAYMENT_METHOD", "org");
                entity.HasKey(o => o.Id);
                entity.HasOne(opm => opm.Organization)
                       .WithMany(o => o.OrganizationPaymentMethods)
                       .HasForeignKey(opm => opm.OrganizationId);
            });
            builder.Entity<Department>(entity =>
            {
                entity.ToTable("DEPARTMENT", "org");
                entity.HasKey(o => o.Id);
                entity.HasOne(d => d.Organization)
                      .WithMany(o => o.Departments)
                      .HasForeignKey(d => d.OrganizationId);
            });
            builder.Entity<Designation>(entity =>
            {
                entity.ToTable("DESIGNATION", "ums");
                entity.HasKey(o => o.Id);
            });
            builder.Entity<Product>()
                .HasMany(t => t.ProductProcesses)
                .WithOne(t => t.Product)
                .HasForeignKey(t => t.ProductId);

            builder.Entity<ProductProcess>(entity =>
            {
                entity.HasOne(ur => ur.Product)
              .WithMany(u => u.ProductProcesses)
              .HasForeignKey(ur => ur.ProductId);

                entity.HasOne(ur => ur.Process);

                entity.HasMany(t => t.ProductProcessSubprocesses)
                    .WithOne(t => t.ProductProcess)
                    .HasForeignKey(t => t.ProductProcessId);
            });

            builder.Entity<Process>()
                .HasMany(t => t.ProcessSubprocesses)
                .WithOne(t => t.Process)
                .HasForeignKey(t => t.ProcessId);

            builder.Entity<ProcessSubprocess>(entity =>
            {
                entity.HasOne(ur => ur.Process)
              .WithMany(u => u.ProcessSubprocesses)
              .HasForeignKey(ur => ur.ProcessId);

                entity.HasOne(ur => ur.Subprocess);
            });

        }
    }
}
