using FluentValidation.Results;
using HD.Core.Messages;

namespace HD.Core.Mediator;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T eventMessage) where T : Event;
    Task<ValidationResult> SendCommand<T>(T command) where T : Command;
}
