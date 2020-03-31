using Funcionarios.Domain.Entities.Requests;
using Funcionarios.Domain.Entities.Views;
using Funcionarios.Domain.Interfaces.Services;
using Funcionarios.Domain.Services;
using Funcionarios.Infra.Data.Repositories;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funcionarios.Apresentation.Wpf.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IDialogCoordinator dialog;
        private readonly IFuncionarioService funcionarioService;
        public DelegateCommand IncluirCommand { get; set; }
        public DelegateCommand AlterarCommand { get; set; }
        public DelegateCommand InativarCommand { get; set; }
        public DelegateCommand LimparTelaCommand { get; set; }
        public ProgressDialogController Progresso { get; set; }

        private bool _modoEdicao = false;
        public bool ModoEdicao
        {
            get { return _modoEdicao; }
            set
            {
                SetProperty(ref _modoEdicao, value);
            }
        }

        private bool _editarFuncionario = true;
        public bool EditarFuncionario
        {
            get { return _editarFuncionario; }
            set
            {
                SetProperty(ref _editarFuncionario, value);
            }
        }

        private FuncionarioRequest _request = new FuncionarioRequest();
        public FuncionarioRequest Request
        {
            get { return _request; }
            set
            {
                SetProperty(ref _request, value);
            }
        }

        private List<FuncionarioView> _listaFuncionario;
        public List<FuncionarioView> ListaFuncionario
        {
            get { return _listaFuncionario; }
            set { SetProperty(ref _listaFuncionario, value); }
        }

        private FuncionarioView _view = new FuncionarioView();
        public FuncionarioView View
        {
            get { return _view; }
            set
            {
                SetProperty(ref _view, value);
                ModoEdicao = _view?.Id > 0;
                EditarFuncionario = !_modoEdicao;
            }
        }

        public MainWindowViewModel(IDialogCoordinator dialog)
        {
            this.dialog = dialog;
            funcionarioService = new FuncionarioService(new FuncionarioRepository());
            IncluirCommand = new DelegateCommand(Incluir, () => !ModoEdicao).ObservesProperty(() => ModoEdicao);
            AlterarCommand = new DelegateCommand(Alterar, () => ModoEdicao).ObservesProperty(() => ModoEdicao);
            InativarCommand = new DelegateCommand(Inativar, () => ModoEdicao).ObservesProperty(() => ModoEdicao);
            LimparTelaCommand = new DelegateCommand(Limpar);
            BuscarFuncionarios();
        }

        private async void Incluir()
        {
            Request.Nome = View.Nome;
            Request.Idade = View.Idade;
            Request.Cargo = View.Cargo;
            Request.Endereco = View.Endereco;
            Request.Bairro = View.Bairro;
            Request.Cidade = View.Cidade;
            var funcionarioCriado = funcionarioService.Incluir(Request, "Carlosg");
            if (!funcionarioService.Validar)
            {
                var linq = funcionarioService.Notificacoes.Select(msg => msg.Mensagem);
                await this.dialog.ShowMessageAsync(this, "Atenção", string.Join("\r\n", linq));
                funcionarioService.LimparNotificacoes();
            }
            if (funcionarioCriado != null)
            {
                Progresso = await dialog.ShowProgressAsync(this, "Progresso", "Incluindo dados do funcionário. Aguarde...");
                Progresso.SetIndeterminate();
                var t = Task.Factory.StartNew(() => { BuscarFuncionarios(); });
                await t;
                await Progresso?.CloseAsync();
                await this.dialog.ShowMessageAsync(this, "Atenção", "Funcionário cadastrado com sucesso !!!");
                Limpar();
            }
        }

        private async void Alterar()
        {
            if (View != null && View.Id > 0)
            {
                var funcionarioExistente = funcionarioService.ObterPorId(View.Id);
                if (funcionarioExistente != null)
                {
                    Request.Id = View.Id;
                    Request.Idade = View.Idade;
                    Request.Cargo = View.Cargo;
                    Request.Endereco = View.Endereco;
                    Request.Bairro = View.Bairro;
                    Request.Cidade = View.Cidade;
                    funcionarioExistente = funcionarioService.Alterar(Request, "Carlosg");
                    if (funcionarioService.Validar)
                    {
                        Progresso = await dialog.ShowProgressAsync(this, "Progresso", "Alterando dados do funcionário. Aguarde...");
                        Progresso.SetIndeterminate();
                        await Progresso?.CloseAsync();
                        await this.dialog.ShowMessageAsync(this, "Atenção", "Funcionário alterado com sucesso !!!");
                        Limpar();
                    }
                    else
                    {
                        await this.dialog.ShowMessageAsync(this, "Atenção", string.Join("\r\n", funcionarioService.Notificacoes.Select(s => s.Mensagem)));
                        funcionarioService.LimparNotificacoes();
                    }
                    BuscarFuncionarios();
                }
            }
        }

        private async void Inativar()
        {
            if (View != null && View.Id > 0)
            {
                var funcionarioExistente = funcionarioService.ObterPorId(View.Id);
                if (funcionarioExistente != null)
                {
                    Progresso = await dialog.ShowProgressAsync(this, "Progresso", "Inativando funcionário. Aguarde...");
                    Progresso.SetIndeterminate();
                    var t = Task.Factory.StartNew(() => { funcionarioService.Inativar(View.Id, "Carlosg"); });
                    await t;
                    await Progresso?.CloseAsync();
                    await this.dialog.ShowMessageAsync(this, "Atenção", "Funcionário inativado com sucesso !!!");
                    Limpar();
                    BuscarFuncionarios();
                }
                else
                {
                    await this.dialog.ShowMessageAsync(this, "Atenção", string.Join("\r\n", funcionarioExistente.Notificacoes.Select(s => s.Mensagem)));
                    funcionarioService.LimparNotificacoes();
                }
            }
        }

        private void BuscarFuncionarios()
        {
            ListaFuncionario = funcionarioService.ListarTodos().ToList();
        }

        private void Limpar()
        {
            View = new FuncionarioView();
        }
    }
}
