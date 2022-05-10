

using System.Linq;
using AutoMapper;
using DESAFIO.Db;
using DESAFIO.DTOs;
using DESAFIO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DESAFIO.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper; // DTOs Alterar para não Mostrar a senha

        public ClientesController(AppDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }



        // clientes
        // GetDTO não retorna a senha 
        [HttpGet]
        // Poderia usar List em ves de IEnumerable
        public ActionResult<IEnumerable<ClienteDTO>> Get() {


            var clientes = _context.Clientes.AsNoTracking().ToList();

            if (clientes == null) {
                return NotFound("Clientes não encontrados.");
            }

            var clientesDTO = _mapper.Map<List<ClienteDTO>>(clientes);// DTOs



            return clientesDTO;
        }

        // clientes/1
        // GetIdDTO não retorna a senha 
        [HttpGet("{id}", Name = "ObterCliente")]
        public ActionResult<ClienteDTO> Get(int id) {


            var cliente = _context.Clientes.AsNoTracking().FirstOrDefault(p => p.ClienteId == id);

            if (cliente == null) {
                return NotFound($"Cliente id={id} não encontrado");
            }

            var clienteDTO = _mapper.Map<ClienteDTO>(cliente);// DTOs

            return Json(clienteDTO);
        }


        // PostDTO
        // //clientes
        // [HttpPost]
        // public ActionResult Post(ClienteDTO clienteDTO) {

        //     // Toda vez que trabalhar com alteração do banco de dados usa-se Cliente não ClienteDTO
        //     var cliente = _mapper.Map<Cliente>(clienteDTO);// Mapeando inversamente de clienteDTO que recebeu para cliente normmal

        //     if (cliente is null) {
        //         return BadRequest();
        //     }

        //     _context.Clientes.Add(cliente);
        //     _context.SaveChanges();

        //     var clienteDTOReturn = _mapper.Map<ClienteDTO>(cliente);// retorno em DTO

        //     // Resposta padrão
        //     // Aciona a rota "ObterCliente"
        //     return new CreatedAtRouteResult("ObterCliente",
        //         new { id = cliente.ClienteId }, clienteDTOReturn);
        // }


        // avaliacoes
        [HttpPost]
        public ActionResult Post(Cliente cliente) {

            if (cliente is null) {
                return BadRequest();
            }

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            // Resposta padrão
            // Aciona a rota "ObterCliente"
            return new CreatedAtRouteResult("ObterCliente",
                new { id = cliente.ClienteId }, cliente);
        }


        //PutDTO
        //clientes/id
        // [HttpPut("{id}")]
        // public ActionResult Put(int id, ClienteDTO clienteDTO) {


        //     if (id != clienteDTO.ClienteId) {
        //         return BadRequest();
        //     }

        //     var cliente = _mapper.Map<Cliente>(clienteDTO);// Mapeando inversamente de clienteDTO que recebeu para cliente normmal

        //     // Precisa informar a _context que o cliente esta em um estado modificado
        //     _context.Entry(cliente).State = EntityState.Modified; // Alterar o estado da entidade pa modified
        //     _context.SaveChanges();

        //     var clienteDTOReturn = _mapper.Map<ClienteDTO>(cliente);// retorno em DTO

        //     return Ok(clienteDTOReturn);

        // }


        // clientes/id
        [HttpPut("{id}")]
        public ActionResult Put(int id, Cliente cliente) {


            if (id != cliente.ClienteId) {
                return BadRequest();
            }

            // Precisa informar a _context que o cliente esta em um estado modificado
            _context.Entry(cliente).State = EntityState.Modified; // Alterar o estado da entidade pa modified
            _context.SaveChanges();
            return Ok(cliente);

        }


        // clientes/1
        // DeleteDTO
        [HttpDelete("{id}")]
        public ActionResult<ClienteDTO> Delete(int id) {

            // Usar o método Find é uma opção para localizar 
            // o cliente pelo id (não suporta AsNoTracking)
            // var cliente = _context.Clientes.Find(id);

            var cliente = _context.Clientes.FirstOrDefault(p => p.ClienteId == id);

            if (cliente == null) {
                return NotFound("Cliente não existe");
            }

            // O Cliente possui vendas cadastradas? Se Sim bloquear a exclusão
            var clientes = _context.Vendas.AsNoTracking().Where(v => v.ClienteId == id).ToList();
            if (clientes.Count > 0) {

                return BadRequest($"Erro! O Cliente possui {clientes.Count} vendas cadastradas");
            }

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            var clienteDTOReturn = _mapper.Map<ClienteDTO>(cliente);// retorno em DTO

            return Ok(clienteDTOReturn);
        }


        // [HttpGet("clienteVendas")]
        // public ActionResult<ClienteDTO> GetClienteVendas(int id) {


        //     var cliente = _context.Clientes
        //         .AsNoTracking()
        //         .Include(I=>I.Vendas)
        //         .FirstOrDefault(p => p.ClienteId == id);

        //     if (cliente == null) {
        //         return NotFound($"Cliente id={id} não encontrado");
        //     }

        //     var clienteDTO = _mapper.Map<ClienteDTO>(cliente);// DTOs

        //     return Json(clienteDTO);
        // }


    }
}