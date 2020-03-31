using Dapper;
using Funcionarios.Domain.Entities;
using Funcionarios.Domain.Entities.Views;
using Funcionarios.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Funcionarios.Infra.Data.Repositories
{
    public class FuncionarioRepository : RepositoryBase<Funcionario, FuncionarioView>, IFuncionarioRepository
    {
        public override IEnumerable<FuncionarioView> ListarTodos()
        {
            return cn.Query<FuncionarioView>("SELECT * FROM Funcionarios WHERE Ativo = 1 ORDER BY Nome").ToList();
        }

        public bool VerificarFuncionarioExistente(string nomeFuncionario)
        {
            return cn.Query<int>($"SELECT TOP 1 1 FROM Funcionarios WHERE Nome = '{nomeFuncionario}'").Any();
        }
    }
}
