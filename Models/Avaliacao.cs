

using System.ComponentModel.DataAnnotations.Schema;

namespace DESAFIO.Models {

    [Table("Avaliacoes")]
    public class Avaliacao {

        public int AvaliacaoId { get; set; }

        public string Nome { get; set; }

        public int Nota { get; set; }

        public string DescricaoDaAvaliacao { get; set; }

        public int LivroInfoId { get; set; }
        
    }
}