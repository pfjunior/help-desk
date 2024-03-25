using AutoMapper;
using HD.Api.ViewModels.Employees;
using HD.Domain.Core.Interfaces;
using HD.Domain.Employees.Entities;
using HD.Domain.Employees.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HD.Api.Controllers;

[Route("api/employees")]
public class EmployeesController : MainController
{
    private readonly IEmployeeService _service;
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeService service, IEmployeeRepository repository, IMapper mapper, INotifier notifier) : base(notifier)
    {
        _service = service;
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<EmployeeViewModel>> GetAll()
    {
        return _mapper.Map<IEnumerable<EmployeeViewModel>>(await _repository.GetEmployeesDepartments());
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<EmployeeViewModel>> GetById(Guid id)
    {
        var result = await GetEmployeeById(id);

        if (result == null) return NotFound();

        return result;
    }

    [HttpGet("{resgistration}")]
    public async Task<ActionResult<EmployeeViewModel>> GetByRegistration(string registration)
    {
        var result = _mapper.Map<EmployeeViewModel>(await _repository.GetByRegistration(registration));

        if (result == null) return NotFound();

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<CreateEmployeeViewModel>> Create([FromBody] CreateEmployeeViewModel viewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _service.Add(_mapper.Map<Employee>(viewModel));

        return CustomResponse(HttpStatusCode.Created, viewModel);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<UpdateEmployeeViewModel>> Update(Guid id, [FromBody] UpdateEmployeeViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            NotifyError("O id informado não é o mesmo que foi passado na query");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _service.Update(_mapper.Map<Employee>(viewModel));

        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult<EmployeeViewModel>> Delete(Guid id)
    {
        await _service.Delete(id);

        return CustomResponse(HttpStatusCode.NoContent);
    }

    #region Private Methods
    private async Task<EmployeeViewModel> GetEmployeeById(Guid id) => _mapper.Map<EmployeeViewModel>(await _repository.GetById(id));
    #endregion
}
