using Funcionarios.Domain.Entities;
using Funcionarios.Domain.Entities.Requests;
using Funcionarios.Domain.Entities.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funcionarios.Domain.Interfaces.Services
{
    public interface IFuncionarioService : INotificationService
    {
        IEnumerable<FuncionarioView> ListarTodos();
        Funcionario ObterPorId(int id);
        Funcionario Incluir(FuncionarioRequest funcionario, string usuarioCadastro);
        Funcionario Alterar(FuncionarioRequest funcionario, string usuarioCadastro);
        void Inativar(int id, string usuarioCadastro);
    }
}
