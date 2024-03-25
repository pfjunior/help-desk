using HD.Domain.Core.Interfaces;
using HD.Domain.Core.Services;
using HD.Domain.Employees.Entities;
using HD.Domain.Employees.Interfaces;
using HD.Domain.Employees.Validations;

namespace HD.Domain.Employees.Services;

public class EmployeeService : BaseService, IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository, INotifier notifier) : base(notifier) => _repository = repository;

    public async Task Add(Employee entity)
    {
        if (!ExecuteValidation(new EmployeeValidation(), entity)) return;

        await _repository.Add(entity);
    }

    public async Task Update(Employee entity)
    {
        if (!ExecuteValidation(new EmployeeValidation(), entity)) return;

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
