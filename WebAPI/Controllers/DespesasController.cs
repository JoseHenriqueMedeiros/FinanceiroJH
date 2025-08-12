using Domain.Interfaces.ICategoria;
using Domain.Interfaces.IDespesa;
using Domain.Interfaces.InterfaceServicos;
using Domain.Servicos;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController : ControllerBase
    {
        private readonly IDespesa _IDespesa;
        private readonly IDespesaServico _IDespesaServico;
        public DespesasController(IDespesa IDespesa, IDespesaServico IDespesaServico)
        {
            _IDespesa = IDespesa;
            _IDespesaServico = IDespesaServico;
        }

        [HttpGet("/api/ListarDespesasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarDespesasUsuario(string emailUsuario)
        {
            return await _IDespesa.ListarDespesasUsuario(emailUsuario);
        }

        [HttpPost("/api/AdicionarDespesa")]
        [Produces("application/json")]
        public async Task<object> AdicionarDespesa(Despesa despesa)
        {
            await _IDespesaServico.AdicionarDespesa(despesa);

            return despesa;

        }

        [HttpPut("/api/AtualizarDespesa")]
        [Produces("application/json")]
        public async Task<object> AtualizarDespesa(Despesa despesa)
        {
            await _IDespesaServico.AtualizarDespesa(despesa);

            return despesa;

        }


        [HttpGet("/api/ObterDespesa")]
        [Produces("application/json")]
        public async Task<object> ObterDespesa(int id)
        {
            return await _IDespesa.GetEntityById(id);
        }


        [HttpDelete("/api/DeletarDespesa")]
        [Produces("application/json")]
        public async Task<object> DeletarDespesa(int id)
        {
            try
            {
                var categoria = await _IDespesa.GetEntityById(id);
                await _IDespesa.Delete(categoria);

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        [HttpGet("/api/CarregarGraficos")]
        [Produces("application/json")]
        public async Task<object> CarregarGraficos(string emailUsuario)
        {
            return await _IDespesaServico.CarregaGraficos(emailUsuario);
        }


    }
}