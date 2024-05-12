using FluentValidation.Results;
using HD.Core.Data;

namespace HD.Core.Messages;

public abstract class CommandHandler
{
    protected ValidationResult ValidationResult;

    protected CommandHandler() => ValidationResult = new ValidationResult();

    protected void AddError(string mensagem) => ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));

    protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
    {
        if (!await uow.CommitAsync()) AddError("There was an error persisting the data");

        return ValidationResult;
    }
}
