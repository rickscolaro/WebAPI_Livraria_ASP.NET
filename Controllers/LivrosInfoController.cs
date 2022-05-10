

using DESAFIO.Db;
using DESAFIO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DESAFIO.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class LivrosInfoController : Controller {

        private readonly AppDbContext _context;

        public LivrosInfoController(AppDbContext context) {
            _context = context;
        }

        //livroInfos
        [HttpGet]
        // Poderia usar List em ves de IEnumerable
        public async Task<ActionResult<IEnumerable<LivroInfo>>> Get() {


            var livroInfo = await _context.LivrosInfo
            .AsNoTracking()
            .Include(I => I.Livro)
            .ToListAsync();

            if (livroInfo == null) {
                return NotFound("LivroInfos não encontrados.");
            }

            return livroInfo;
        }

        //livroInfos/1
        [HttpGet("{id}", Name = "ObterLivroInfo")]
        public ActionResult<LivroInfo> Get(int id) {

            var livroInfo = _context.LivrosInfo.AsNoTracking().FirstOrDefault(p => p.LivroInfoId == id);

            if (livroInfo == null) {
                return NotFound($"LivroInfo de id={id} não encontrado");
            }
            return livroInfo;
        }


        //livroInfos
        [HttpPost]
        public ActionResult Post(LivroInfo livroInfo) {

            if (livroInfo is null) {
                return BadRequest();
            }

            _context.LivrosInfo.Add(livroInfo);
            _context.SaveChanges();

            // Resposta padrão
            // Aciona a rota "ObterLivroInfo"
            return new CreatedAtRouteResult("ObterLivroInfo",
                new { id = livroInfo.LivroInfoId }, livroInfo);
        }


        //livroInfos/id
        [HttpPut("{id}")]
        public ActionResult Put(int id, LivroInfo livroInfo) {


            if (id != livroInfo.LivroInfoId) {
                return BadRequest();
            }

            // Precisa informar a _context que o livroInfo esta em um estado modificado
            _context.Entry(livroInfo).State = EntityState.Modified; // Alterar o estado da entidade pa modified
            _context.SaveChanges();
            return Ok(livroInfo);

        }


        // livroInfos/1
        [HttpDelete("{id}")]
        public ActionResult<LivroInfo> Delete(int id) {

            // Usar o método Find é uma opção para localizar 
            // o livroInfo pelo id (não suporta AsNoTracking)
            //var livroInfo = _context.LivroInfos.Find(id);

            var livroInfo = _context.LivrosInfo.FirstOrDefault(p => p.LivroInfoId == id);


            if (livroInfo == null) {
                return NotFound("LivroInfo não existe");
            }


            _context.LivrosInfo.Remove(livroInfo);
            _context.SaveChanges();
            return Ok($"LivroInfo de id{livroInfo.LivroInfoId} excluído");

        }

      
    }
}