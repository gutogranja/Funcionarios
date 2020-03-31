using Funcionarios.Domain.Entities;
using Funcionarios.Domain.Entities.Requests;
using Funcionarios.Domain.Entities.Views;
using Funcionarios.Domain.Interfaces.Repositories;
using Funcionarios.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funcionarios.Domain.Services
{
    public class FuncionarioService : BaseService , IFuncionarioService
    {
        private readonly IFuncionarioRepository repositorio;

        public FuncionarioService(IFuncionarioRepository repositorio)
        {
            this.repositorio = repositorio;
        }

        public IEnumerable<FuncionarioView> ListarTodos()
        {
            return repositorio.ListarTodos();
        }

        public Funcionario ObterPorId(int id)
        {
            return repositorio.ObterPorId(id);
        }

        public Funcionario Incluir(FuncionarioRequest request, string usuarioCadastro)
        {
            var novoFuncionario = new Funcionario(request.Nome, request.Idade, request.Cargo,request.Endereco,request.Bairro,request.Cidade, usuarioCadastro);
            ValidarFuncionario(novoFuncionario);
            if (Validar)
            {
                bool funcionarioExistente = repositorio.VerificarFuncionarioExistente(request.Nome);
                if (!funcionarioExistente)
                {
                    return repositorio.Incluir(novoFuncionario);
                }
                else
                    AdicionarNotificacao("Funcionario", "Funcionário já cadastrado.");
            }
            return null;
        }

        public Funcionario Alterar(FuncionarioRequest request, string usuarioCadastro)
        {
            var funcionarioExistente = repositorio.ObterPorId(request.Id);
            if (funcionarioExistente != null)
            {
                funcionarioExistente.AlterarIdade(request.Idade);
                funcionarioExistente.AlterarCargo(request.Cargo);
                funcionarioExistente.AlterarEndereco(request.Endereco);
                funcionarioExistente.AlterarBairro(request.Bairro);
                funcionarioExistente.AlterarCidade(request.Cidade);
                ValidarFuncionario(funcionarioExistente);
                if (Validar)
                {
                    return repositorio.Alterar(funcionarioExistente);
                }
            }
            else
                AdicionarNotificacao("Funcionario", "Funcionário não encontrado");
            return null;
        }

        public void Inativar(int id, string usuario)
        {
            if (id <= 0)
                AdicionarNotificacao("Funcionario", "Funcionário não encontrado");
            var funcionarioExistente = repositorio.ObterPorId(id);
            if (funcionarioExistente != null)
            {
                funcionarioExistente.Inativar(usuario);
                repositorio.Alterar(funcionarioExistente);
            }
            else
                AdicionarNotificacao("Funcionario", "Não é possível inativar. Pois não existe o funcionário");
        }

        private void ValidarFuncionario(Funcionario funcionario)
        {
            if (!funcionario.Validar)
                AdicionarNotificacao(funcionario.Notificacoes);
        }
    }
}
