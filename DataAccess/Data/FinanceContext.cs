using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class FinanceContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            builder.Entity<UserRole>()
                .HasOne(a => a.User)
                .WithMany(a => a.Roles)
                .HasForeignKey(a => a.UserId);
            builder.Entity<UserRole>()
                .HasOne(u => u.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.RoleId);
        }
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories  { get; set; }
        public DbSet<ChurchService> ChurchServices { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<BalanceLimit> BalanceLimits { get; set; }
        public DbSet<YouthMembers> YouthMembers { get; set; }
    }
}
