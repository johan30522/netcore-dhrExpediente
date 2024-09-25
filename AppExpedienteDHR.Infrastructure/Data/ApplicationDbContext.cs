using AppExpedienteDHR.Core.Domain.Entities;
using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.Entities.General;
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

        #region Tablas de Catalogos

        public DbSet<Canton> Cantones { get; set; }
        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<Escolaridad> Escolaridades { get; set; }
        public DbSet<EstadoCivil> EstadosCiviles { get; set; }
        public DbSet<TipoIdentificacion> TiposIdentificacion { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Sexo> Sexos { get; set; }

        #endregion

        #region Workflow entities
        public DbSet<FlowWf> FlowWfs { get; set; }
        public DbSet<GroupWf> GroupWfs { get; set; }
        public DbSet<StateWf> StateWfs { get; set; }
        public DbSet<ActionWf> ActionWfs { get; set; }
        public DbSet<GroupUserWf> GroupUserWfs { get; set; }
        public DbSet<ActionGroupWf> ActionGroupWfs { get; set; }
        public DbSet<ActionRuleWf> ActionRuleWfs { get; set; }
        public DbSet<FlowHistoryWf> FlowHistoryWfs { get; set; }
        public DbSet<FlowRequestHeaderWf> FlowRequestHeaderWfs { get; set; }
        #endregion

        #region Dhr Entities
        public DbSet<Denunciante> Denunciantes { get; set; }
        public DbSet<Denuncia> Denuncias { get; set; }
        public DbSet<PersonaAfectada> PersonasAfectadas { get; set; }
        public DbSet<DenunciaAdjunto> DenunciaAdjuntos { get; set; }
        public DbSet<Expediente> Expedientes { get; set; }
        public DbSet<Adjunto> Adjuntos { get; set; }

        #endregion

        public DbSet<LockRecord> LockRecords { get; set; }


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

            #region Catalog entities Fluent API configuration

            // Configuración para ignorar la recreación de tablas existentes en las migraciones
            modelBuilder.Entity<TipoIdentificacion>().ToTable("TipoIdentificacion", "gen").Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Sexo>().ToTable("Sexo", "gen").Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Provincia>().ToTable("Provincias", "gen").Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Canton>().ToTable("Cantones", "gen").Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Distrito>().ToTable("Distritos", "gen").Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Escolaridad>().ToTable("Escolaridad", "gen").Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<EstadoCivil>().ToTable("EstadoCivil", "gen").Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Pais>().ToTable("Paises", "gen").Metadata.SetIsTableExcludedFromMigrations(true);

            // Configuración de Provincia -> Canton (uno a muchos)
            modelBuilder.Entity<Provincia>()
                .HasMany(p => p.Cantones)
                .WithOne(c => c.Provincia)
                .HasForeignKey(c => c.CodigoProvincia)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Canton -> Distrito (uno a muchos)
            modelBuilder.Entity<Canton>()
                .HasMany(c => c.Distritos)
                .WithOne(d => d.Canton)
                .HasForeignKey(d => d.CodigoCanton)
                .OnDelete(DeleteBehavior.Restrict);



            #endregion

            #region Dhr entities Fluent API configuration
            // Configuración de Denunciante -> Denuncia (uno a muchos)
            modelBuilder.Entity<Denuncia>()
                .HasOne(d => d.Denunciante)
                .WithMany(den => den.Denuncias)
                .HasForeignKey(d => d.DenuncianteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Denuncia -> PersonaAfectada (Opcional, uno a muchos)
            modelBuilder.Entity<Denuncia>()
                .HasOne(d => d.PersonaAfectada)
                .WithMany(pa => pa.Denuncias)
                .HasForeignKey(d => d.PersonaAfectadaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Denuncia -> DenunciaAdjunto (uno a muchos)
            modelBuilder.Entity<DenunciaAdjunto>()
                .HasOne(da => da.Denuncia)
                .WithMany(d => d.DenunciaAdjuntos)
                .HasForeignKey(da => da.DenunciaId)
                .OnDelete(DeleteBehavior.Cascade);
            // Configuración de la relación entre DenunciaAdjunto y Anexo
            modelBuilder.Entity<DenunciaAdjunto>()
                .HasOne(da => da.Adjunto)
                .WithMany()
                .HasForeignKey(da => da.AdjuntoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de Denuncia -> Expediente (uno a uno)
            modelBuilder.Entity<Denuncia>()
                .HasOne(d => d.Expediente)
                .WithOne(e => e.Denuncia)
                .HasForeignKey<Expediente>(e => e.DenunciaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Expediente -> Denunciante (uno a muchos)
            modelBuilder.Entity<Expediente>()
                .HasOne(e => e.Denunciante)
                .WithMany(den => den.Expedientes)
                .HasForeignKey(e => e.DenuncianteId)
                .OnDelete(DeleteBehavior.Restrict);


            // Configuración de Denunciante -> TipoIdentificacion (uno a muchos)
            modelBuilder.Entity<Denunciante>()
                .HasOne(d => d.TipoIdentificacion)
                .WithMany()
                .HasForeignKey(d => d.TipoIdentificacionId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configuración de Denunciante -> Sexo (uno a muchos)
            modelBuilder.Entity<Denunciante>()
                .HasOne(d => d.Sexo)
                .WithMany()
                .HasForeignKey(d => d.SexoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Denunciante -> EstadoCivil (uno a muchos)
            modelBuilder.Entity<Denunciante>()
                .HasOne(d => d.EstadoCivil)
                .WithMany()
                .HasForeignKey(d => d.EstadoCivilId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Denunciante -> Pais (uno a muchos)
            modelBuilder.Entity<Denunciante>()
                .HasOne(d => d.PaisOrigen)
                .WithMany()
                .HasForeignKey(d => d.PaisOrigenCodigo)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Denunciante -> Escolaridad (uno a muchos)
            modelBuilder.Entity<Denunciante>()
                .HasOne(d => d.Provincia)
                .WithMany()
                .HasForeignKey(d => d.ProvinciaCodigo)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Denunciante -> Canton (uno a muchos)
            modelBuilder.Entity<Denunciante>()
                .HasOne(d => d.Canton)
                .WithMany()
                .HasForeignKey(d => d.CantonCodigo)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Denunciante -> Distrito (uno a muchos)
            modelBuilder.Entity<Denunciante>()
                .HasOne(d => d.Distrito)
                .WithMany()
                .HasForeignKey(d => d.DistritoCodigo)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Denunciante -> Escolaridad (uno a muchos)
            modelBuilder.Entity<Denunciante>()
                .HasOne(d => d.Escolaridad)
                .WithMany()
                .HasForeignKey(d => d.EscolaridadId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de PersonaAfectada -> TipoIdentificacion (uno a muchos)
            modelBuilder.Entity<PersonaAfectada>()
                .HasOne(pa => pa.TipoIdentificacion)
                .WithMany()
                .HasForeignKey(pa => pa.TipoIdentificacionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de PersonaAfectada -> Sexo (uno a muchos)
            modelBuilder.Entity<PersonaAfectada>()
                .HasOne(pa => pa.Sexo)
                .WithMany()
                .HasForeignKey(pa => pa.SexoId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Other entities Fluent API configuration
            // Configuración de LockRecord -> ApplicationUser (uno a muchos)
            modelBuilder.Entity<LockRecord>()
                .HasOne(lr => lr.LockedByUser)
                .WithMany()
                .HasForeignKey(lr => lr.LockedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
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

            // Configuración de FlowWf -> FlowRequestHeaderWf (uno a muchos)
            modelBuilder.Entity<FlowWf>()
                .HasMany(f => f.RequestFlowHeaders)
                .WithOne(frh => frh.Flow)
                .HasForeignKey(frh => frh.FlowId)
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

            // Configuración de FlowRequestHeaderWf -> FlowHistoryWf (uno a muchos)
            modelBuilder.Entity<FlowRequestHeaderWf>()
                .HasMany(frh => frh.FlowHistories)
                .WithOne(fh => fh.RequestFlowHeader)
                .HasForeignKey(fh => fh.RequestFlowHeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de FlowRequestHeaderWf -> ApplicationUser (muchos a uno)
            // Usuario que inició el flujo
            modelBuilder.Entity<FlowRequestHeaderWf>()
                .HasOne(frh => frh.CreatedByUser)
                .WithMany()
                .HasForeignKey(frh => frh.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de FlowRequestHeaderWf -> StateWf (uno a muchos)
            modelBuilder.Entity<FlowRequestHeaderWf>()
                .HasOne(frh => frh.CurrentState)
                .WithMany()
                .HasForeignKey(frh => frh.CurrentStateId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de FlowHistoryWf -> ApplicationUser (muchos a uno)
            // Usuario que realizó la acción
            modelBuilder.Entity<FlowHistoryWf>()
                .HasOne(fh => fh.PerformedByUser)
                .WithMany()
                .HasForeignKey(fh => fh.PerformedByUserId)
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

            // Configuración de FlowHistoryWf -> ActionWf (uno a muchos)
            modelBuilder.Entity<FlowHistoryWf>()
                .HasOne(fh => fh.ActionPerformed)
                .WithMany()
                .HasForeignKey(fh => fh.ActionPerformedId)
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
