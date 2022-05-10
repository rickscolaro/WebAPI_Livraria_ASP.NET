
using DESAFIO.Db;
using DESAFIO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DESAFIO.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class AutorController : Controller {


        private readonly AppDbContext _context;

        public AutorController(AppDbContext context) {
            _context = context;
        }

        //autores
        [HttpGet]
        // Poderia usar List em ves de IEnumerable
        public async Task<ActionResult<IEnumerable<Autor>>> Get() {


            var autor = await _context.Autores.AsNoTracking().ToListAsync();

            if (autor == null) {
                return NotFound("Autors não encontrados.");
            }

            return autor;
        }

        //autores/1
        [HttpGet("{id}", Name = "ObterAutor")]
        public ActionResult<Autor> Get(int id) {

            var autor = _context.Autores.AsNoTracking().FirstOrDefault(p => p.AutorId == id);

            if (autor == null) {
                return NotFound($"Autor de id={id} não encontrado");
            }
            return autor;
        }


        //autores
        [HttpPost]
        public ActionResult Post(Autor autor) {

            if (autor is null) {
                return BadRequest();
            }

            _context.Autores.Add(autor);
            _context.SaveChanges();

            // Resposta padrão
            // Aciona a rota "ObterAutor"
            return new CreatedAtRouteResult("ObterAutor",
                new { id = autor.AutorId }, autor);
        }


        //autores/id
        [HttpPut("{id}")]
        public ActionResult Put(int id, Autor autor) {


            if (id != autor.AutorId) {
                return BadRequest();
            }

            // Precisa informar a _context que o autor esta em um estado modificado
            _context.Entry(autor).State = EntityState.Modified; // Alterar o estado da entidade pa modified
            _context.SaveChanges();
            return Ok(autor);

        }


        // autores/1
        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id) {

            // Usar o método Find é uma opção para localizar 
            // o autor pelo id (não suporta AsNoTracking)
            //var autor = _context.Autors.Find(id);

            var autor = _context.Autores.FirstOrDefault(p => p.AutorId == id);

            
            if (autor == null) {
                return NotFound("Autor não existe");
            }

            
            // O Autor possui livros cadastrados? Se Sim bloquear a exclusão
            var livros = _context.Livros.AsNoTracking().Where(v => v.AutorId == id).ToList();
            if (livros.Count > 0) {
                
                return BadRequest($"Erro! O Autor possui {livros.Count} livros cadastradas");
            }

            _context.Autores.Remove(autor);
            _context.SaveChanges();
            return Ok($"Autor de nome {autor.Nome} excluído");

        }

    }
}