using AutoMapper;
using HD.Api.ViewModels.Departments;
using HD.Domain.Core.Interfaces;
using HD.Domain.Departments.Entities;
using HD.Domain.Departments.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HD.Api.Controllers;

[Route("api/departments")]
public class DepartmentsController : MainController
{
    private readonly IDepartmentService _service;
    private readonly IDepartmentRepository _repository;
    private readonly IMapper _mapper;

    public DepartmentsController(IDepartmentService service, IDepartmentRepository repository, IMapper mapper, INotifier notifier) : base(notifier)
    {
        _service = service;
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<DepartmentViewModel>> GetAll()
    {
        return _mapper.Map<IEnumerable<DepartmentViewModel>>(await _repository.GetAll());
    }

    [HttpGet("users")]
    public async Task<IEnumerable<DepartmentViewModel>> GetDepartmentsUsers()
    {
        return _mapper.Map<IEnumerable<DepartmentViewModel>>(await _repository.GetDepartmentsUsers());
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<DepartmentViewModel>> GetById(Guid id)
    {
        var department = await GetDepartmentById(id);

        if (department == null) return NotFound();

        return department;
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<DepartmentViewModel>> GetByCode(string code)
    {
        var department = _mapper.Map<DepartmentViewModel>(await _repository.GetByCode(code));

        if (department == null) return NotFound();

        return department;
    }

    [HttpPost]
    public async Task<ActionResult<CreateDepartmentViewModel>> Create(CreateDepartmentViewModel viewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _service.Add(new Department(viewModel.Code, viewModel.Name));

        return CustomResponse(HttpStatusCode.Created, viewModel);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<UpdateDepartmentViewModel>> Update(Guid id, [FromBody] UpdateDepartmentViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            NotifyError("O id informado não é o mesmo que foi passado na query");
            CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var department = new Department();
        department.SetId(viewModel.Id);
        department.SetCode(viewModel.Code);
        department.SetName(viewModel.Name);

        await _service.Update(department);

        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult<DepartmentViewModel>> Delete(Guid id)
    {
        await _service.Delete(id);

        return CustomResponse(HttpStatusCode.NoContent);
    }

    #region Private Methods
    private async Task<DepartmentViewModel> GetDepartmentById(Guid id) => _mapper.Map<DepartmentViewModel>(await _repository.GetById(id));
    #endregion
}
