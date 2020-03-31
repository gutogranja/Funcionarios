using Dapper.Contrib.Extensions;
using Funcionarios.Domain.Helpers;
using System.Collections.Generic;

namespace Funcionarios.Domain.Interfaces.Services
{
    public interface INotificationService
    {
        [Write(false)]
        IReadOnlyCollection<Notification> Notificacoes { get; }

        [Write(false)]
        bool Validar { get; }
        void AdicionarNotificacao(IReadOnlyCollection<Notification> notificacoes);
        void LimparNotificacoes();
    }
}
