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
        public DbSet<FlowGroupWf> FlowGroupWfs { get; set; }
        public DbSet<GroupUserWf> GroupUserWfs { get; set; }
        public DbSet<FlowStateWf> FlowStateWfs { get; set; }
        public DbSet<ActionGroupWf> ActionGroupWfs { get; set; }
        public DbSet<StateActionWf> StateActionWfs { get; set; }
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

            // FlowWf -> FlowGroupWf (one-to-many)
            modelBuilder.Entity<FlowWf>()
                .HasMany(e => e.FlowGroups)
                .WithOne(fg => fg.Flow)
                .HasForeignKey(fg => fg.FlowId)
                .OnDelete(DeleteBehavior.Restrict);

            // FlowWf -> FlowStateWf (one-to-many)
            modelBuilder.Entity<FlowWf>()
                .HasMany(e => e.FlowStates)
                .WithOne(fs => fs.Flow)
                .HasForeignKey(fs => fs.FlowId)
                .OnDelete(DeleteBehavior.Restrict);

            // GroupWf -> FlowGroupWf (one-to-many)
            modelBuilder.Entity<GroupWf>()
                .HasMany(e => e.FlowGroups)
                .WithOne(fg => fg.Group)
                .HasForeignKey(fg => fg.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // GroupWf -> GroupUserWf (one-to-many)
            modelBuilder.Entity<GroupWf>()
                .HasMany(e => e.GroupUsers)
                .WithOne(gu => gu.Group)
                .HasForeignKey(gu => gu.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // GroupWf -> ActionGroupWf (one-to-many)
            modelBuilder.Entity<GroupWf>()
                .HasMany(e => e.ActionGroups)
                .WithOne(ag => ag.Group)
                .HasForeignKey(ag => ag.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // StateWf -> FlowStateWf (one-to-many)
            modelBuilder.Entity<StateWf>()
                .HasMany(e => e.FlowStates)
                .WithOne(fs => fs.State)
                .HasForeignKey(fs => fs.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // StateWf -> StateActionWf (one-to-many)
            modelBuilder.Entity<StateWf>()
                .HasMany(e => e.StateActions)
                .WithOne(sa => sa.State)
                .HasForeignKey(sa => sa.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // ActionWf -> ActionRuleWf (one-to-many)
            modelBuilder.Entity<ActionWf>()
                .HasMany(e => e.ActionRules)
                .WithOne(ar => ar.Action)
                .HasForeignKey(ar => ar.ActionId)
                .OnDelete(DeleteBehavior.Restrict);

            // ActionWf -> ActionGroupWf (one-to-many)
            modelBuilder.Entity<ActionWf>()
                .HasMany(e => e.ActionGroups)
                .WithOne(ag => ag.Action)
                .HasForeignKey(ag => ag.ActionId)
                .OnDelete(DeleteBehavior.Restrict);

            // FlowHistoryWf -> FlowWf (One-to-One or One-to-Many)
            modelBuilder.Entity<FlowHistoryWf>()
                .HasOne(e => e.Flow)
                .WithMany()
                .HasForeignKey(e => e.FlowId)
                .OnDelete(DeleteBehavior.Restrict);

            // FlowHistoryWf -> PreviousState (One-to-One or One-to-Many)
            modelBuilder.Entity<FlowHistoryWf>()
                .HasOne(e => e.PreviousState)
                .WithMany()
                .HasForeignKey(e => e.PreviousStateId)
                .OnDelete(DeleteBehavior.Restrict);

            // FlowHistoryWf -> NewState (One-to-One or One-to-Many)
            modelBuilder.Entity<FlowHistoryWf>()
                .HasOne(e => e.NewState)
                .WithMany()
                .HasForeignKey(e => e.NewStateId)
                .OnDelete(DeleteBehavior.Restrict);

            // FlowHistoryWf -> ActionPerformed (One-to-One or One-to-Many)
            modelBuilder.Entity<FlowHistoryWf>()
                .HasOne(e => e.ActionPerformed)
                .WithMany()
                .HasForeignKey(e => e.ActionPerformedId)
                .OnDelete(DeleteBehavior.Restrict);

            // FlowHistoryWf -> PerformedByUser (One-to-One or One-to-Many)
            modelBuilder.Entity<FlowHistoryWf>()
                .HasOne(e => e.PerformedByUser)
                .WithMany()
                .HasForeignKey(e => e.PerformedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // FlowHistoryWf -> RequestFlowHistoryWf (one-to-many)
            modelBuilder.Entity<FlowHistoryWf>()
                .HasMany(e => e.RequestFlowHistories)
                .WithOne(rfh => rfh.FlowHistory)
                .HasForeignKey(rfh => rfh.FlowHistoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // RequestFlowHistoryWf -> FlowHistoryWf (Many-to-One)
            modelBuilder.Entity<RequestFlowHistoryWf>()
                .HasOne(e => e.FlowHistory)
                .WithMany(fh => fh.RequestFlowHistories)
                .HasForeignKey(e => e.FlowHistoryId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
