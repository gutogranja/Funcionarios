using System;
using System.Collections.Generic;
using System.Text;

namespace Funcionarios.Domain.Entities.Requests
{
    public class FuncionarioRequest
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
