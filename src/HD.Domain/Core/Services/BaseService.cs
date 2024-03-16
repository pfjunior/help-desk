using FluentValidation;
using FluentValidation.Results;
using HD.Domain.Core.Entities;
using HD.Domain.Core.Interfaces;
using HD.Domain.Core.Notifications;

namespace HD.Domain.Core.Services;

public abstract class BaseService
{
    private readonly INotifier _notifier;

    protected BaseService(INotifier notifier) => _notifier = notifier;

    protected void Notify(ValidationResult validationResult) => validationResult.Errors.ForEach(item => Notify(item.ErrorMessage));

    protected void Notify(string message) => _notifier.Handle(new Notification(message));

    protected bool ExecuteValidation<TValidation, TEntity>(TValidation validation, TEntity entity)
        where TValidation : AbstractValidator<TEntity>
        where TEntity : Entity
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid) return true;

        Notify(validator);

        return false;
    }
}
