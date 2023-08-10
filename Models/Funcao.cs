using DominandoEFCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeatureLogArquivos.Domain
{
    public class Funcao
    {
        public Funcao()
        {
            Teste = new List<Teste>();
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]

        public string? Nome { get; set; }    

      
        public TesteEnum Data1 { get; set; }

        public string? Data2 { get; set; }

        public List<Teste> Teste { get; set; }
    }
    public enum TesteEnum
    {
        [Description("Ativos")]
        Ativo = 1,
        [Description("iNATIVOS")]
        inativo = 2
    }

}