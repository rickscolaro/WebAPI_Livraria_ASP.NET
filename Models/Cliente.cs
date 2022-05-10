

using System.ComponentModel.DataAnnotations;

namespace DESAFIO.Models {

    public class Cliente {

        [Key]
        public int ClienteId { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        //public List<Venda> Vendas { get; set; } = new List<Venda>();
    }
}