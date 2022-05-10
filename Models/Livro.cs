

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DESAFIO.Models {

    
    public class Livro {

        [Key]
        public int LivroId { get; set; }

        public string Nome { get; set; }

        public double Valor { get; set; }

        public int Estoque { get; set; }

        public int AutorId { get; set; }
        public Autor Autor { get; set; }

        public LivroInfo LivroInfo { get; set; }

        public List<Venda> Vendas { get; set; } = new List<Venda>();

        


    }
}