using FeatureLogArquivos.Domain;
using DocumentFormat.OpenXml.Vml;
using DominandoEFCore;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using FeatureLogArquivos.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FeatureLogArquivos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private  ILogService _logService;
        private IFuncaoRepository  _funcaoRepository;

        public LogController(ILogService logService, IFuncaoRepository funcaoRepository)
        {
            _logService = logService;
            _funcaoRepository = funcaoRepository;
        }

        // GET: api/<LogController>
        [HttpGet("teste")]
        public IActionResult TesteLogBanco()
        {
            TesteInterceptacaoSaveChanges2();
            return Ok();
        }

        private void CriarBancoEDadosTeste()
        {
            using (var db = new FeatureLogArquivos.Data.ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var funcao = new Funcao
                {
                    Nome = "Função teste ",

                };
                for (int i = 0; i < 10; i++)
                {
                    funcao.Teste.Add(new Teste { CampoTeste = "Campo Teste " + i });
                }
                db.Funcoes.Add(funcao);

                db.SaveChanges();
            }
        }
        private void TesteInterceptacaoSaveChanges2()
        {
            CriarBancoEDadosTeste();

            var entidadeBanco = _funcaoRepository.ObterAsNoTracking().Result;


            //Conectado
            //var entidadeBanco = _funcaoRepository.ObterSEMAsNoTracking().Result;

            entidadeBanco.Nome = "NOME DA FUNÇÂO EDITADO PARA TESTE";
            int contador = 10;
            foreach (var item in entidadeBanco.Teste)
            {
                item.CampoTeste = "CAMPO TESTE EDITADO " + contador;
                contador += 10;
            }

            //Desconectado
            _logService.LogChanges<Funcao>("JOSE TESTE", entidadeBanco.Id, entidadeBanco);

            //Aqui não deve estar salvo no banco com as modificações somente o historico(testar)
            _funcaoRepository.Atualizar(entidadeBanco);
        }


        //private void TesteInterceptacaoSaveChanges()
        //{
        //    var entidadeBanco = new Funcao();
        //    using (var db = new FeatureLogArquivos.Data.ApplicationContext())
        //    {
        //        db.Database.EnsureDeleted();
        //        db.Database.EnsureCreated();

        //        var funcao = new Funcao
        //        {
        //            Nome = "Função teste ",

        //        };
        //        for (int i = 0; i < 10; i++)
        //        {
        //            funcao.Teste.Add(new Teste { CampoTeste = "Campos teste " + i });

        //        }
        //        db.Funcoes.Add(funcao);

        //        db.SaveChanges();

        //        //Desconectado
        //        entidadeBanco = db.Funcoes.Include(x => x.Teste).AsNoTracking().FirstOrDefault();

        //        //Conectado
        //        //entidadeEnditada =  db.Funcoes.Include(x => x.Teste).FirstOrDefault();

        //        entidadeBanco.Nome = "NOME DA FUNÇÂO EDITADO";
        //        int contador = 10;
        //        foreach (var item in entidadeBanco.Teste)
        //        {
        //            item.CampoTeste = "CAMPO EDITADO " + contador;
        //            contador += 10;
        //        }

        //        //Desconectado
        //        _logService.LogChanges<Funcao>("Nome Teste", entidadeBanco.Id, entidadeBanco);

        //        //Conectado             
        //        //LogUpdateAoContext<Funcao>("Nome Teste", entidadeEditada.Id);


        //        db.Update(entidadeBanco);//Talvez tenha q comentar isso no cenraio conectado
        //        db.SaveChanges();
        //    }
        //}
    }
}
