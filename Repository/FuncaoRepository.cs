using FeatureLogArquivos.Data;
using FeatureLogArquivos.Domain;
using FeatureLogArquivos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FeatureLogArquivos.Repository
{
    public class FuncaoRepository : IFuncaoRepository
    {
        private readonly ApplicationContext _contexto;

        public FuncaoRepository(ApplicationContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Funcao>> ObterTodosAsNoTracking()
        {
            return await _contexto.Funcoes.Include(x => x.Teste).AsNoTracking().ToListAsync();
        }

        public async Task<List<Funcao>> ObterTodosSEMAsNoTracking()
        {
            return await _contexto.Funcoes.Include(x => x.Teste).ToListAsync();
        }

        public async Task<Funcao> ObterAsNoTracking()
        {
            return await _contexto.Funcoes.Include(x => x.Teste).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Funcao> ObterSEMAsNoTracking()
        {
            return await _contexto.Funcoes.Include(x => x.Teste).FirstOrDefaultAsync();
        }

        public async Task Atualizar(Funcao funcao)
        {
            _contexto.Funcoes.Update(funcao);
            await _contexto.SaveChangesAsync();
        }
    }
}
