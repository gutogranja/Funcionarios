using Dapper.Contrib.Extensions;

namespace Funcionarios.Domain.Entities
{
    [Table("Funcionarios")]
    public class Funcionario : Entity
    {
        private Funcionario() : base("")
        {
        }

        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Cargo { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }

        public Funcionario(string nome, int idade, string cargo, string ender, string bairro, string cidade, string usuario) : base(usuario)
        {
            Nome = nome;
            Idade = idade;
            Cargo = cargo;
            Endereco = ender;
            Bairro = bairro;
            Cidade = cidade;
            if (string.IsNullOrEmpty(nome))
                AdicionarNotificacao("Funcionario", Mensagens.Nome);
            if (idade <= 0)
                AdicionarNotificacao("Funcionario", Mensagens.Idade);
            if (string.IsNullOrEmpty(cargo))
                AdicionarNotificacao("Funcionario", Mensagens.Cargo);
        }

        public void AlterarIdade(int idade)
        {
            if (idade <= 0)
                AdicionarNotificacao("Funcionario", Mensagens.Idade);
            else
                Idade = idade;
        }

        public void AlterarCargo(string cargo)
        {
            if (string.IsNullOrEmpty(cargo))
                AdicionarNotificacao("Funcionario", Mensagens.Cargo);
            else
                Cargo = cargo;
        }

        public void AlterarEndereco(string ender)
        {
            Endereco = ender;
        }

        public void AlterarBairro(string bairro)
        {
            Bairro = bairro;
        }

        public void AlterarCidade(string cidade)
        {
            Cidade = cidade;
        }
    }
}
