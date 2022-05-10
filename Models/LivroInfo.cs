

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DESAFIO.Models {

    [Table("LivrosInfo")]
    public class LivroInfo {

        [Key]
        public int LivroInfoId { get; set; }

        public string Descricao { get; set; }

        public int Paginas { get; set; }

        public string Editora { get; set; }

        public int LivroId { get; set; }
        public Livro Livro { get; set; }

        public List<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
    }
}