using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeatureLogArquivos.Models
{
    [Table("Historicos")]
    public class Historico
    {
        public int Id { get; set; }
        public string? Log { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string? Descricao { get; set; }
    }

    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public EntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
