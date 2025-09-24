using Domain.Interfaces.InterfaceServicos;
using Domain.Interfaces.ISistemaFinanceiro;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SistemaFinanceiroController : ControllerBase
    {
        private readonly ISistemaFinanceiro _ISistemaFinanceiro;
        private readonly ISistemaFinanceiroServico _ISistemaFinanceiroServico;
        public SistemaFinanceiroController(ISistemaFinanceiro ISistemaFinanceiro, ISistemaFinanceiroServico ISistemaFinanceiroServico)
        {
            _ISistemaFinanceiro = ISistemaFinanceiro;
            _ISistemaFinanceiroServico = ISistemaFinanceiroServico;
        }

        [HttpGet("/api/ListarSistemaUsuario")]
        [Produces("application/json")]
        public async Task<object> ListaSistemaUsuario(string emailUsuario)
        {
            return await _ISistemaFinanceiro.ListarSistemasUsuario(emailUsuario);
        }

        [HttpPost("/api/AdicionarSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            await _ISistemaFinanceiroServico.AdicionarSistemaFinanceiro(sistemaFinanceiro);

            return Task.FromResult(sistemaFinanceiro);
        }

        [HttpPut("/api/AtualizarSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            await _ISistemaFinanceiroServico.AtualizarSistemaFinanceiro(sistemaFinanceiro);

            return Task.FromResult(sistemaFinanceiro);
        }

        [HttpGet("/api/ObterSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> ObterSistemaFinanceiro(int id)
        {
            return await _ISistemaFinanceiro.GetEntityById(id);
        }

        [HttpDelete("/api/DeletarSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> DeletarSistemaFinanceiro(int id)
        {
            try
            {
                var sistemaFinanceiro = await _ISistemaFinanceiro.GetEntityById(id);
                await _ISistemaFinanceiro.Delete(sistemaFinanceiro);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
