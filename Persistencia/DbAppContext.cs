using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia
{
    public class DbAppContext : DbContext
    {
        public DbAppContext(DbContextOptions<DbAppContext> options) : base(options)
        {
        }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioRoles> UsuariosRoles { get; set; }
        
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Hamburguesa> Hamburguesas { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Hamburguesa_ingredientes> Hamburguesa_Ingredientes { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioRoles>().HasKey(p => new { p.UsuarioId, p.RolId});

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
    }
}