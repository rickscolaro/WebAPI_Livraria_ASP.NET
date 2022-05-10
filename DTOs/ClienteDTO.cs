

using System.ComponentModel.DataAnnotations;
using DESAFIO.Models;

namespace DESAFIO.DTOs {

    public class ClienteDTO {

        [Key]
        public int ClienteId { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        // Esconde o campo senha

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        //public List<Venda> Vendas { get; set; } = new List<Venda>();
    }
}