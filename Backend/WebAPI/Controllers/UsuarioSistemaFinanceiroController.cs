using Domain.Interfaces.InterfaceServicos;
using Domain.Interfaces.IUsuarioSistemaFinanceiro;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioSistemaFinanceiroController : ControllerBase
    {
        private readonly IUsuarioSistemaFinanceiro _IUsuarioSistemaFinanceiro;
        private readonly IUsuarioSistemaFinanceiroServico _IUsuarioSistemaFinanceiroServico;

        public UsuarioSistemaFinanceiroController(IUsuarioSistemaFinanceiro IUsuarioSistemaFinanceiro, IUsuarioSistemaFinanceiroServico IUsuarioSistemaFinanceiroServico)
        {
            _IUsuarioSistemaFinanceiro = IUsuarioSistemaFinanceiro;
            _IUsuarioSistemaFinanceiroServico = IUsuarioSistemaFinanceiroServico;
        }


        [HttpGet("/api/ListarUsuarioSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> ListaUsuarioSistemaFinanceiro(int idSistema)
        {
            return await _IUsuarioSistemaFinanceiro.ListarUsuarioSistemaFinanceiro(idSistema);
        }

        [HttpPost("/api/CadastrarUsuarioSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> CadastrarUsuarioSistemaFinanceiro(int idSistema, string emailUsuario)
        {
            try
            {
                await _IUsuarioSistemaFinanceiroServico.CadastroUsuarioSistema(
                    new UsuarioSistemaFinanceiro
                    {
                        IdSistema = idSistema,
                        EmailUsuario = emailUsuario,
                        Administrador = false,
                        SistemaAtual = true
                    });
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        [HttpDelete("/api/DeletarUsuarioSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> DeletarUsuarioSistemaFinanceiro(int id)
        {
            try
            {
                var usuarioSistemaFinanceiro = await _IUsuarioSistemaFinanceiro.GetEntityById(id);
                await _IUsuarioSistemaFinanceiro.Delete(usuarioSistemaFinanceiro);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}
