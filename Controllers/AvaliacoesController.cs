using DESAFIO.Db;
using DESAFIO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DESAFIO.Controllers {


    [ApiController]
    [Route("[controller]")]
    public class AvaliacoesController : Controller {


        private readonly AppDbContext _context;

        public AvaliacoesController(AppDbContext context) {
            _context = context;
        }

        //avaliacaos
        [HttpGet]
        // Poderia usar List em ves de IEnumerable
        public async Task<ActionResult<IEnumerable<Avaliacao>>> Get() {


            var avaliacao = await _context.Avaliacoes.AsNoTracking().ToListAsync();

            if (avaliacao == null) {
                return NotFound("Avaliacaos não encontrados.");
            }

            return avaliacao;
        }

        //avaliacoes/1
        [HttpGet("{id}", Name = "ObterAvaliacao")]
        public ActionResult<Avaliacao> Get(int id) {

            var avaliacao = _context.Avaliacoes.AsNoTracking().FirstOrDefault(p => p.AvaliacaoId == id);

            if (avaliacao == null) {
                return NotFound($"Avaliacao de id={id} não encontrado");
            }
            return avaliacao;
        }


        //avaliacoes
        [HttpPost]
        public ActionResult Post(Avaliacao avaliacao) {

            if (avaliacao is null) {
                return BadRequest();
            }

            _context.Avaliacoes.Add(avaliacao);
            _context.SaveChanges();

            // Resposta padrão
            // Aciona a rota "ObterAvaliacao"
            return new CreatedAtRouteResult("ObterAvaliacao",
                new { id = avaliacao.AvaliacaoId }, avaliacao);
        }


        //avaliacoes/id
        [HttpPut("{id}")]
        public ActionResult Put(int id, Avaliacao avaliacao) {


            if (id != avaliacao.AvaliacaoId) {
                return BadRequest();
            }

            // Precisa informar a _context que o avaliacao esta em um estado modificado
            _context.Entry(avaliacao).State = EntityState.Modified; // Alterar o estado da entidade pa modified
            _context.SaveChanges();
            return Ok(avaliacao);

        }


        // avaliacoes/1
        [HttpDelete("{id}")]
        public ActionResult<Avaliacao> Delete(int id) {

            // Usar o método Find é uma opção para localizar 
            // o avaliacao pelo id (não suporta AsNoTracking)
            //var avaliacao = _context.Avaliacaos.Find(id);

            var avaliacao = _context.Avaliacoes.FirstOrDefault(p => p.AvaliacaoId == id);


            if (avaliacao == null) {
                return NotFound("Avaliacao não existe");
            }



            _context.Avaliacoes.Remove(avaliacao);
            _context.SaveChanges();
            return Ok($"Avaliacao de nome {avaliacao.Nome} excluído");

        }

    }
}