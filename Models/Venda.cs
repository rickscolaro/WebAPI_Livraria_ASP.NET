

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DESAFIO.Models {


    public class Venda {

        [Key]
        public int VendaId { get; set; }

        public double Valor { get; set; }

        public DateTime Data { get; set; } 

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int LivroId { get; set; }

        public Livro Livro { get; set; }

        public List<Livro> Livros { get; set; } = new List<Livro>();
    }
}