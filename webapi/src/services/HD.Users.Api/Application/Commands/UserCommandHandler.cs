using FluentValidation.Results;
using HD.Core.Messages;
using HD.Users.Api.Application.Events;
using HD.Users.Api.Domain.Entities;
using HD.Users.Api.Domain.Interfaces;
using MediatR;

namespace HD.Users.Api.Application.Commands;

public class UserCommandHandler : CommandHandler, IRequestHandler<UserRegisterCommand, ValidationResult>
{
    private readonly IUserRepository _repository;

    public UserCommandHandler(IUserRepository repository) => _repository = repository;


    public async Task<ValidationResult> Handle(UserRegisterCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var newUser = new User(message.Id, message.FirstName, message.LastName, message.Email, message.DepartmentCode, message.DepartmentName);
        if (message.PhoneNumber is not null && message.Extension is not null)
            newUser.SetPhoneNumber(message.PhoneNumber, message.Extension);

        var existedUser = await _repository.Search(e => e.Email.Address == message.Email);

        if (existedUser.Any())
        {
            AddError("This e-mail is already taken");
            return ValidationResult;
        }

        await _repository.AddAsync(newUser);

        newUser.AddEvent(new UserRegistredEvent(message.Id, message.FirstName, message.LastName, message.Email, message.DepartmentCode, message.DepartmentName));

        return await PersistData(_repository.UnitOfWork);
    }
}
