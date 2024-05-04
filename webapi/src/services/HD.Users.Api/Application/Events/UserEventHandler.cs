using MediatR;

namespace HD.Users.Api.Application.Events;

public class UserEventHandler : INotificationHandler<UserRegistredEvent>
{
    public Task Handle(UserRegistredEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send an e-mail confirmation
        return Task.CompletedTask;
    }
}
