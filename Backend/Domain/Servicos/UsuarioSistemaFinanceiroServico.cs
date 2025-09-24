using Domain.Interfaces.InterfaceServicos;
using Domain.Interfaces.IUsuarioSistemaFinanceiro;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos
{
    public class UsuarioSistemaFinanceiroServico : IUsuarioSistemaFinanceiroServico
    {
        private readonly IUsuarioSistemaFinanceiro _usuarioSistemaFinanceiro;
        public UsuarioSistemaFinanceiroServico(IUsuarioSistemaFinanceiro usuarioSistemaFinanceiro) 
        {
            _usuarioSistemaFinanceiro = usuarioSistemaFinanceiro;
        }

        public async Task CadastroUsuarioSistema(UsuarioSistemaFinanceiro usuarioSistemaFinanceiro)
        {
            await _usuarioSistemaFinanceiro.Add(usuarioSistemaFinanceiro);
        }
    }
}
