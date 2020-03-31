using Funcionarios.Domain.Entities;
using Funcionarios.Domain.Entities.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funcionarios.Domain.Interfaces.Repositories
{
    public interface IFuncionarioRepository : IRepositoryBase<Funcionario,FuncionarioView>
    {
        bool VerificarFuncionarioExistente(string nome);
    }
}
