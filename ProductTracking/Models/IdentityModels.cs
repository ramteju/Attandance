using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProductTracking.Models.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using ProductTracking.Services.Auth;
using ProductTracking.Services.Core;

namespace ProductTracking.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public bool? IsOnline { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name, string description) : base(name)
        {
            this.Description = description;
        }
        public int PriorityOrder { get; set; }
        public string Description { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<UrlTrack> UrlTracks { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }

        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, ProductTracking.Migrations.Configuration>("DefaultConnection"));

            this.Database.CommandTimeout = 300;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            #region CustomUserRole

            // Keep this:
            modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers");

            // Change TUser to ApplicationUser everywhere else - 
            // IdentityUser and ApplicationUser essentially 'share' the AspNetUsers Table in the database:
            EntityTypeConfiguration<ApplicationUser> table =
                modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");

            table.Property((ApplicationUser u) => u.UserName).IsRequired();
            // EF won't let us swap out IdentityUserRole for ApplicationUserRole here:
            modelBuilder.Entity<ApplicationUser>().HasMany<IdentityUserRole>((ApplicationUser u) => u.Roles);
            modelBuilder.Entity<IdentityUserRole>().HasKey((IdentityUserRole r) =>
                new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("AspNetUserRoles");

            // Leave this alone:
            EntityTypeConfiguration<IdentityUserLogin> entityTypeConfiguration =
                modelBuilder.Entity<IdentityUserLogin>().HasKey((IdentityUserLogin l) =>
                    new
                    {
                        UserId = l.UserId,
                        LoginProvider = l.LoginProvider,
                        ProviderKey
                        = l.ProviderKey
                    }).ToTable("AspNetUserLogins");


            //entityTypeConfiguration.HasRequired<IdentityUser>((IdentityUserLogin u) => u.UserId);
            EntityTypeConfiguration<IdentityUserClaim> table1 =
                modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaims");

            //table1.HasRequired<IdentityUser>((IdentityUserClaim u) => u.User);

            // Add this, so that IdentityRole can share a table with ApplicationRole:
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");

            // Change these from IdentityRole to ApplicationRole:
            EntityTypeConfiguration<ApplicationRole> entityTypeConfiguration1 =
                modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles");

            entityTypeConfiguration1.Property((ApplicationRole r) => r.Name).IsRequired();
            #endregion
        }

        public override int SaveChanges()
        {
            #region auditable entities
            var addedAuditedEntities = ChangeTracker.Entries<Auditable>()
              .Where(p => p.State == EntityState.Added)
              .Select(p => p.Entity);

            var modifiedAuditedEntities = ChangeTracker.Entries<Auditable>()
              .Where(p => p.State == EntityState.Modified)
              .Select(p => p.Entity);

            var now = DateTime.UtcNow;
            string userId = null;
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User != null)
                userId = System.Web.HttpContext.Current.User?.Identity?.GetUserId();

            if (userId != null)
            {
                foreach (var added in addedAuditedEntities)
                {
                    added.DateCreated = now;
                    added.LastUpdated = now;
                    added.CreatedBy = userId;
                }

                foreach (var modified in modifiedAuditedEntities)
                {
                    modified.LastUpdated = now;
                    modified.CreatedBy = userId;
                }
            }
            #endregion
            return base.SaveChanges();
        }
    }
}