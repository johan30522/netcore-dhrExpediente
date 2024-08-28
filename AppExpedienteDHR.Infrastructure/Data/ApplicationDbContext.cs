using AppExpedienteDHR.Core.Domain.Entities;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string,
        IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        #region Workflow entities
        public DbSet<FlowWf> FlowWfs { get; set; }
        public DbSet<GroupWf> GroupWfs { get; set; }
        public DbSet<StateWf> StateWfs { get; set; }
        public DbSet<ActionWf> ActionWfs { get; set; }
        public DbSet<GroupUserWf> GroupUserWfs { get; set; }
        public DbSet<ActionGroupWf> ActionGroupWfs { get; set; }
        public DbSet<ActionRuleWf> ActionRuleWfs { get; set; }
        public DbSet<FlowHistoryWf> FlowHistoryWfs { get; set; }
        public DbSet<RequestFlowHistoryWf> RequestFlowHistoryWfs { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Identity entities Fluent API configuration
            // Configura la relación entre ApplicationUser y IdentityUserRole
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.UserRoles)
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            // Configurar la relación entre IdentityUserRole y IdentityRole
            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            #endregion

            #region Workflow entities Fluent API configuration

            // Configuración de FlowWf -> StateWf (uno a muchos)
            modelBuilder.Entity<FlowWf>()
                .HasMany(f => f.States)
                .WithOne(s => s.Flow)
                .HasForeignKey(s => s.FlowId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de FlowWf -> GroupWf (uno a muchos)
            modelBuilder.Entity<FlowWf>()
                .HasMany(f => f.Groups)
                .WithOne(g => g.Flow)
                .HasForeignKey(g => g.FlowId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de StateWf -> ActionWf (uno a muchos)
            modelBuilder.Entity<StateWf>()
                .HasMany(s => s.Actions)
                .WithOne(a => a.State)
                .HasForeignKey(a => a.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de ActionWf -> ActionRuleWf (uno a muchos)
            modelBuilder.Entity<ActionWf>()
                .HasMany(a => a.ActionRules)
                .WithOne(ar => ar.Action)
                .HasForeignKey(ar => ar.ActionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de ActionWf -> ActionGroupWf (uno a muchos)
            modelBuilder.Entity<ActionWf>()
                .HasMany(a => a.ActionGroups)
                .WithOne(ag => ag.Action)
                .HasForeignKey(ag => ag.ActionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de GroupWf -> ActionGroupWf (uno a muchos)
            modelBuilder.Entity<GroupWf>()
                .HasMany(g => g.ActionGroups)
                .WithOne(ag => ag.Group)
                .HasForeignKey(ag => ag.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de GroupWf -> GroupUserWf (uno a muchos)
            modelBuilder.Entity<GroupWf>()
                .HasMany(g => g.GroupUsers)
                .WithOne(gu => gu.Group)
                .HasForeignKey(gu => gu.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de FlowHistoryWf -> RequestFlowHistoryWf (uno a muchos)
            modelBuilder.Entity<FlowHistoryWf>()
                .HasMany(fh => fh.RequestFlowHistories)
                .WithOne(rfh => rfh.FlowHistory)
                .HasForeignKey(rfh => rfh.FlowHistoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de FlowHistoryWf para evitar múltiples rutas de cascada
            modelBuilder.Entity<FlowHistoryWf>()
                .HasOne(fh => fh.PreviousState)
                .WithMany()
                .HasForeignKey(fh => fh.PreviousStateId)
                .OnDelete(DeleteBehavior.NoAction);  // Evitar múltiples cascadas

            modelBuilder.Entity<FlowHistoryWf>()
                .HasOne(fh => fh.NewState)
                .WithMany()
                .HasForeignKey(fh => fh.NewStateId)
                .OnDelete(DeleteBehavior.NoAction);  // Evitar múltiples cascadas

            // Configuración adicional de FlowHistoryWf
            modelBuilder.Entity<FlowHistoryWf>()
                .HasOne(fh => fh.ActionPerformed)
                .WithMany()
                .HasForeignKey(fh => fh.ActionPerformedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FlowHistoryWf>()
                .HasOne(fh => fh.PerformedByUser)
                .WithMany()
                .HasForeignKey(fh => fh.PerformedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FlowHistoryWf>()
                .HasOne(fh => fh.Flow)
                .WithMany()
                .HasForeignKey(fh => fh.FlowId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de GroupUserWf -> ApplicationUser (muchos a uno)
            modelBuilder.Entity<GroupUserWf>()
                .HasOne(gu => gu.User)
                .WithMany()
                .HasForeignKey(gu => gu.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
