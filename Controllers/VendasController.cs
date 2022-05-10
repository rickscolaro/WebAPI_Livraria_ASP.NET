using DESAFIO.Db;
using DESAFIO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DESAFIO.Controllers {


    [ApiController]
    [Route("[controller]")]
    public class VendasController : Controller {

        private readonly AppDbContext _context;

        public VendasController(AppDbContext context) {
            _context = context;
        }

        //vendas
        [HttpGet]
        // Poderia usar List em ves de IEnumerable
        public async Task<ActionResult<IEnumerable<Venda>>> Get() {


            var venda = await _context.Vendas.AsNoTracking().ToListAsync();

            if (venda == null) {
                return NotFound("Vendas não encontrados.");
            }

            return venda;
        }

        //vendas/1
        [HttpGet("{id}", Name = "ObterVenda")]
        public ActionResult<Venda> Get(int id) {

            var venda = _context.Vendas.AsNoTracking().FirstOrDefault(p => p.VendaId == id);

            if (venda == null) {
                return NotFound($"Venda de id={id} não encontrado");
            }
            return venda;
        }


        //vendas
        [HttpPost]
        public ActionResult Post(Venda venda) {

            if (venda is null) {
                return BadRequest();
            }

            // Pega o id do livro e verifica se o estoque é igual que zero se não libera a venda
            // e diminui uma unidade no estoque
            var emEstoque = _context.Livros.SingleOrDefault(e => e.LivroId == venda.LivroId);
            if (emEstoque.Estoque == 0) {
                return BadRequest("O livro não possui estoque.");
            }
            emEstoque.Estoque--;

            _context.Vendas.Add(venda);
            _context.SaveChanges();

            // Resposta padrão
            // Aciona a rota "ObterVenda"
            return new CreatedAtRouteResult("ObterVenda",
                new { id = venda.VendaId }, venda);
        }


        //vendas/id
        [HttpPut("{id}")]
        public ActionResult Put(int id, Venda venda) {


            if (id != venda.VendaId) {
                return BadRequest();
            }

            // Precisa informar a _context que o venda esta em um estado modificado
            _context.Entry(venda).State = EntityState.Modified; // Alterar o estado da entidade pa modified
            _context.SaveChanges();
            return Ok(venda);

        }


        // vendas/1
        [HttpDelete("{id}")]
        public ActionResult<Venda> Delete(int id) {

            // Usar o método Find é uma opção para localizar 
            // o venda pelo id (não suporta AsNoTracking)
            //var venda = _context.Vendas.Find(id);

            var venda = _context.Vendas.FirstOrDefault(p => p.VendaId == id);


            if (venda == null) {
                return NotFound("Venda não existe");
            }


            _context.Vendas.Remove(venda);
            _context.SaveChanges();
            return venda;

        }


        [HttpGet("vendasPorCliente")]
        public ActionResult<IEnumerable<Venda>> GetVendasPorCLiente(int id) {


            var livro = _context.Vendas
                .AsNoTracking()
                //.Include(I => I.Cliente)
                .Where(a => a.ClienteId == id)
                .ToList();

            if (livro == null) {
                return NotFound($" O cliente de id={id} não foi encontrado");
            }
            return livro;
        }



        [HttpGet("vendasPorLivro")]
        public ActionResult<IEnumerable<Venda>> GetVendasPorLivro(int id) {


            var livro = _context.Vendas
                .AsNoTracking()
                .Include(I => I.Livro)
                .Where(a => a.LivroId == id)
                .ToList();

            if (livro == null) {
                return NotFound($" O livro de id={id} não foi encontrado");
            }
            return livro;
        }

        [HttpGet("vendasPorAutor")]
        public ActionResult<IEnumerable<Venda>> GetVendasPorAutor(int id) {


            var livro = _context.Vendas
                .AsNoTracking()
                .Include(I => I.Livro)
                .Where(a => a.Livro.AutorId == id)
                .ToList();


            if (livro == null) {
                return NotFound($" O Autor id={id} não foi encontrado");
            }
            return livro;
        }
    }
}