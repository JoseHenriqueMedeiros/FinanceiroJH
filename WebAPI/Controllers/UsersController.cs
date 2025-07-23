using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebAPI.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaUsuario")]

        public async Task<IActionResult> AdicionaUsuario([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) ||
                string.IsNullOrWhiteSpace(login.Password) ||
                string.IsNullOrWhiteSpace(login.CPF))
            {
                return Ok("Falta algum dado");
            }

            var user = new ApplicationUser
            {
                Email = login.Email,
                UserName = login.Email,
                CPF = login.CPF
            };

            var result = await _userManager.CreateAsync(user, login.Password);

            if (result.Errors.Any())
            {
                return Ok(result.Errors);
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var respose_Retorn = await _userManager.ConfirmEmailAsync(user, code);

            if (respose_Retorn.Succeeded)
            {
                return Ok("Usuário adicionado!");
            }
            else
            {
                return Ok("Erro ao confirmar cadastro de usuário!");
            }

        }

    }
}