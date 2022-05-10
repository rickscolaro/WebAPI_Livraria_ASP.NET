
using DESAFIO.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DESAFIO.Db {

    public class AppDbContext : IdentityDbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        public DbSet<LivroInfo> LivrosInfo { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        
    }
}