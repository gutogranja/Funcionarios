using System;
using System.Collections.Generic;
using System.Text;

namespace Funcionarios.Domain.Entities.Views
{
    public class FuncionarioView
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Cargo { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
    }
}
