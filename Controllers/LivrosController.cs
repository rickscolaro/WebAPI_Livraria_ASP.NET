

using AutoMapper;
using DESAFIO.Db;
using DESAFIO.DTOs;
using DESAFIO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DESAFIO.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class LivrosController : Controller {


        private readonly AppDbContext _context;

        public LivrosController(AppDbContext context) {
            _context = context;
        }

        //livros
        [HttpGet]
        // Poderia usar List em ves de IEnumerable
        public async Task<ActionResult<IEnumerable<Livro>>> Get() {


            var livro = await _context.Livros.AsNoTracking().ToListAsync();

            if (livro == null) {
                return NotFound("Livros não encontrados.");
            }

            return livro;
        }

        //livros/1
        [HttpGet("{id}", Name = "ObterLivro")]
        public ActionResult<Livro> Get(int id) {

            var livro = _context.Livros.AsNoTracking().FirstOrDefault(p => p.LivroId == id);

            if (livro == null) {
                return NotFound($"Livro de id={id} não encontrado");
            }
            return livro;
        }


        //livros
        [HttpPost]
        public ActionResult Post(Livro livro) {

            if (livro is null) {
                return BadRequest();
            }

            _context.Livros.Add(livro);
            _context.SaveChanges();

            // Resposta padrão
            // Aciona a rota "ObterLivro"
            return new CreatedAtRouteResult("ObterLivro",
                new { id = livro.LivroId }, livro);
        }




        //livros/id
        [HttpPut("{id}")]
        public ActionResult Put(int id, Livro livro) {


            if (id != livro.LivroId) {
                return BadRequest();
            }

            // Altera somente o Valor e Estoque do Livro 
            var filtro = _context.Livros.FirstOrDefault(x => x.LivroId == id);
            if (filtro != null) {
                filtro.Estoque = livro.Estoque;
                filtro.Valor = livro.Valor;
            }

            // Precisa informar a _context que o livro esta em um estado modificado
            _context.Entry(filtro).State = EntityState.Modified; // Alterar o estado da entidade pa modified
            _context.SaveChanges();
            return Ok(filtro);

        }






        // livros/1
        [HttpDelete("{id}")]
        public ActionResult<Livro> Delete(int id) {

            // Usar o método Find é uma opção para localizar 
            // o livro pelo id (não suporta AsNoTracking)
            //var livro = _context.Livros.Find(id);

            var livro = _context.Livros.FirstOrDefault(p => p.LivroId == id);


            if (livro == null) {
                return NotFound("Livro não existe");
            }


            // O Livro possui vendas realizadas? Se Sim bloquear a exclusão
            var vendas = _context.Vendas.AsNoTracking().Where(v => v.LivroId == id).ToList();
            if (vendas.Count > 0) {

                return BadRequest($"Erro! O Livro possui {vendas.Count} vendas cadastradas");
            }

            _context.Livros.Remove(livro);
            _context.SaveChanges();
            return Ok($"Livro de nome {livro.Nome} foi excluído");

        }


        [HttpGet("livrosPorAutor")]
        public ActionResult<IEnumerable<Livro>> GetLivrosPorAutor(int id) {


            var autor = _context.Livros
                .AsNoTracking()
                .Where(a => a.AutorId == id)
                .ToList();

            if (autor == null) {
                return NotFound($" Autor de id={id} não foi encontrado");
            }
            return autor;
        }

        [HttpGet("livroInfo")]
        public ActionResult<IEnumerable<Livro>> GetLivrosInfo(int id) {

            // Incluir as informações e avaliações do livro
            var livro = _context.Livros
            .AsNoTracking()
            .Include(I => I.LivroInfo)
            .Where(a => a.LivroId == id)
            .ToList();

            if (livro == null) {
                return NotFound("Livros não encontrados.");
            }

            return livro;
        }

        [HttpGet("livroInfo/Avaliacoes")]
        public ActionResult<IEnumerable<Livro>> GetLivrosInfoAvaliacoes(int id) {

            // Incluir as informações e avaliações do livro
            var livro = _context.Livros
            .AsNoTracking()
            .Include(I => I.LivroInfo.Avaliacoes)
            .Where(a => a.LivroId == id)
            .ToList();

            if (livro == null) {
                return NotFound("Livros não encontrados.");
            }

            return livro;
        }

        // Retorna as vendas deste livro
        [HttpGet("livroVendas")]
        public ActionResult<IEnumerable<Livro>> GetLivroVendas(int id) {

            // Incluir as vendas do livro
            var livro = _context.Livros
            .AsNoTracking()
            .Include(I => I.Vendas)
            .Where(a => a.LivroId == id)
            .ToList();

            if (livro == null) {
                return NotFound("Vendas não encontradas.");
            }

            return livro;
        }

        // Retorna as vendas do autor 
        [HttpGet("livroVendas/autor")]
        public ActionResult<IEnumerable<Livro>> GetLivroVendasAutor(int id) {

            // Incluir as vendas do livro
            var livro = _context.Livros
            .AsNoTracking()
            .Include(I => I.Vendas)
            .Where(a => a.AutorId == id)
            .ToList();

            if (livro == null) {
                return NotFound("Vendas não encontradas.");
            }

            return livro;
        }
    }
}