using FeatureLogArquivos.Domain;

namespace FeatureLogArquivos.Interfaces
{
    public interface IFuncaoRepository
    {
        Task Atualizar(Funcao funcao);
        Task<Funcao> ObterAsNoTracking();
        Task<Funcao> ObterSEMAsNoTracking();
        Task<List<Funcao>> ObterTodosAsNoTracking();
        Task<List<Funcao>> ObterTodosSEMAsNoTracking();
    }
}
