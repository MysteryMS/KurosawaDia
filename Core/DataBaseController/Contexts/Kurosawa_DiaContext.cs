﻿using DataBaseController.Modelos;
using DataBaseController.ModelsConfiguration;
using DataBaseController.Singletons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Diagnostics;

namespace DataBaseController.Contexts
{
    public class Kurosawa_DiaContext : DbContext
    {
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Servidores> Servidores { get; set; }
        public DbSet<Servidores_Usuarios> Servidores_Usuarios { get; set; }
        public DbSet<ConfiguracoesServidores> ConfiguracoesServidores { get; set; }
        public DbSet<AdmsBot> AdmsBots { get; set; }
        public DbSet<Canais> Canais { get; set; }
        public DbSet<Cargos> Cargos { get; set; }
        public DbSet<CustomReactions> CustomReactions { get; set; }
        public DbSet<Fuck> Fuck { get; set; }
        public DbSet<Insultos> Insultos { get; set; }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(DBDataSingleton.ConnectionString);

            if (Debugger.IsAttached)
            {
                optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuariosConfig());
            modelBuilder.ApplyConfiguration(new ServidoresConfig());
            modelBuilder.ApplyConfiguration(new Servidores_UsuariosConfig());
            modelBuilder.ApplyConfiguration(new ConfiguracoesServidoresConfig());
            modelBuilder.ApplyConfiguration(new AdmsBotConfig());
            modelBuilder.ApplyConfiguration(new CanaisConfig());
            modelBuilder.ApplyConfiguration(new CargosConfig());
            modelBuilder.ApplyConfiguration(new CustomReactionConfig());
            modelBuilder.ApplyConfiguration(new FuckConfig());
            modelBuilder.ApplyConfiguration(new InsultosConfig());
        }

    }
}
