using Domain.Interfaces.ICategoria;
using Domain.Interfaces.InterfaceServicos;
using Entities.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoria _ICategoria;
        private readonly ICategoriaServico _ICategoriaServico;

        public CategoriaController(ICategoria ICategoria, ICategoriaServico ICategoriaServico)
        {
            _ICategoria = ICategoria;
            _ICategoriaServico = ICategoriaServico;
        }


        [HttpGet("/api/ListarCategoriasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarCategoriasUsuario(string emailUsuario)
        {
            return _ICategoria.ListarCategoriaUsuario(emailUsuario);
        }

        [HttpPost("/api/AdicionarCategoria")]
        [Produces("application/json")]
        public async Task<object> AdicionarCategoria(Categoria categoria)
        {
            await _ICategoriaServico.AdicionarCategoria(categoria);

            return categoria;
        }


        [HttpPut("/api/AtualizarCategoria")]
        [Produces("application/json")]
        public async Task<object> AtualizarCategoria(Categoria categoria)
        {
            await _ICategoriaServico.AtualizarCategoria(categoria);

            return categoria;
        }

        [HttpGet("/api/ObterCategoria")]
        [Produces("application/json")]
        public async Task<object> ObterCategoria(int id)
        {
            return await _ICategoria.GetEntityById(id);
        }

        [HttpDelete("/api/DeletarCategoria")]
        [Produces("application/json")]
        public async Task<object> DeletarCategoria(int id)
        {
            try
            {
                var categoria = await _ICategoria.GetEntityById(id);
                await _ICategoria.Delete(categoria);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
