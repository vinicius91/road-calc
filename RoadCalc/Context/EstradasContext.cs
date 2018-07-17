using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using RoadCalc.Models.Entities;
using RoadCalc.Models.Identity;
using RoadCalc.Models.ViewModels;

namespace RoadCalc.Context
{

    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class EstradasContext : IdentityDbContext<ApplicationUser, RoleIntPk, int,
        UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public EstradasContext() : base("LocalMySQL")
        {

        }

        public static EstradasContext Create()
        {
            return new EstradasContext();
        }


        

        public DbSet<Claims> Claims { get; set; }

        public DbSet<InicializadorDeCurva> InicializadorDeCurvas { get; set; }

        public DbSet<Azimute> Azimutes { get; set; }

        public DbSet<ClasseDeProjeto> ClasseDeProjetos { get; set; }

        public DbSet<PontoNotavel> PontosNotaveis { get; set; }

        public DbSet<Coordenada> Coordenadas { get; set; }

        public DbSet<Trecho> Trechos { get; set; }

        public DbSet<CurvaHorizontal> Curvas { get; set; }

        public DbSet<Projeto> Projetos { get; set; }

        public DbSet<Estaca> Estacas { get; set; }

        public DbSet<CurvaVertical> CurvasVerticais { get; set; }

        public DbSet<PontoNotavelVertical> PontosNotaveisVerticais { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();



            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where
                (
                    entry =>
                        entry.Entity.GetType().GetProperty("DateIncluded") != null &&
                        entry.Entity.GetType().GetProperty("DateAltered") != null
                )
            )
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateIncluded").CurrentValue = DateTime.Now;
                    entry.Property("DateAltered").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DateIncluded").IsModified = false;
                    entry.Property("DateAltered").CurrentValue = DateTime.Now;

                }

            }

            return base.SaveChanges();

        }

        public System.Data.Entity.DbSet<EditProjetoViewModel> EditProjetoViewModels { get; set; }
    }
}