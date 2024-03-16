using HD.Domain.Core.Interfaces;
using HD.Domain.Core.Services;
using HD.Domain.Departments.Entities;
using HD.Domain.Departments.Interfaces;
using HD.Domain.Departments.Validations;

namespace HD.Domain.Departments.Services;

public class DepartmentService : BaseService, IDepartmentService
{
    private readonly IDepartmentRepository _repository;

    public DepartmentService(IDepartmentRepository repository, INotifier notifier) : base(notifier) => _repository = repository;

    public async Task Add(Department entity)
    {
        if (!ExecuteValidation(new DepartmentValidation(), entity)) return;

        await _repository.Add(entity);
    }

    public async Task Update(Department entity)
    {
        if (!ExecuteValidation(new DepartmentValidation(), entity)) return;

        await _repository.Update(entity);
    }

    public async Task Delete(Guid id)
    {
        var department = await _repository.GetById(id);

        if (department == null)
        {
            Notify("Esse departamento não existe");
            return;
        }

        var client = await _repository.GetClientByDepartmentCode(department.Code);
        if (client != null)
        {
            Notify("Esse departamento não pode ser excluído");
            return;
        }

        await _repository.Delete(id);
    }

    public void Dispose() => _repository?.Dispose();
}

