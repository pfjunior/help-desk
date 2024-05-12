using FluentValidation.Results;
using HD.Core.Mediator;
using HD.Core.Messages.Integration;
using HD.MessageBus;
using HD.Users.Api.Application.Commands;

namespace HD.Users.Api.Services;

public class UserRegisterIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public UserRegisterIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
    {
        _bus = bus;
        _serviceProvider = serviceProvider;
    }


    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        return Task.CompletedTask;
    }

    #region Private Methods
    private async Task<ResponseMessage> RegisterUser(UserRegistredIntergrationEvent message)
    {
        var userCommand = new UserRegisterCommand(message.Id, message.FirstName, message.LastName, message.Email, message.DepartmentCode, message.DepartmentName);
        ValidationResult sucesso;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            sucesso = await mediator.SendCommand(userCommand);
        }

        return new ResponseMessage(sucesso);
    }

    private void SetResponder()
    {
        _bus.RespondAsync<UserRegistredIntergrationEvent, ResponseMessage>(async request => await RegisterUser(request));
        _bus.AdvancedBus.Connected += OnConnect;
    }

    private void OnConnect(object sender, EventArgs e) => _bus.RespondAsync<UserRegistredIntergrationEvent, ResponseMessage>(async request => await RegisterUser(request));
    #endregion
}
