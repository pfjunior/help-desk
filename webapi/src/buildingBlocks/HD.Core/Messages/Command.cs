using FluentValidation.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace HD.Core.Messages;

public abstract class Command : Message, IRequest<ValidationResult>
{
    protected Command()
    {
        Timestamp = DateTime.Now;
        ValidationResult = new ValidationResult();
    }

    public DateTime Timestamp { get; private set; }

    [JsonIgnore]
    public ValidationResult ValidationResult { get; set; }

    public virtual bool IsValid() => throw new NotImplementedException();
}
