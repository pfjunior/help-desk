using HD.Domain.Core.Interfaces;
using HD.Domain.Core.Services;
using HD.Domain.Users.Entities;
using HD.Domain.Users.Interfaces;
using HD.Domain.Users.Validations;

namespace HD.Domain.Users.Services;

public class UserService : BaseService, IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository, INotifier notifier) : base(notifier) => _repository = repository;

    public async Task Add(User entity)
    {
        if (!ExecuteValidation(new UserValidation(), entity)) return;

        await _repository.Add(entity);
    }

    public async Task Update(User entity)
    {
        if (!ExecuteValidation(new UserValidation(), entity)) return;

        await _repository.Update(entity);
    }

    public async Task Delete(Guid id)
    {
        var User = await _repository.GetById(id);

        if (User == null)
        {
            Notify("Esse usuário não existe");
            return;
        }

        await _repository.Delete(id);
    }

    public void Dispose() => _repository?.Dispose();
}
