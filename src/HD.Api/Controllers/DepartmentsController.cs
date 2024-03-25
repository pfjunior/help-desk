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

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<DepartmentViewModel>> GetById(Guid id)
    {
        var result = await GetDepartmentById(id);

        if (result == null) return NotFound();

        return result;
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<DepartmentViewModel>> GetByCode(string code)
    {
        var result = _mapper.Map<DepartmentViewModel>(await _repository.GetByCode(code));

        if (result == null) return NotFound();

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<CreateDepartmentViewModel>> Create([FromBody] CreateDepartmentViewModel viewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _service.Add(_mapper.Map<Department>(viewModel));

        return CustomResponse(HttpStatusCode.Created, viewModel);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<UpdateDepartmentViewModel>> Update(Guid id, [FromBody] UpdateDepartmentViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            NotifyError("O id informado não é o mesmo que foi passado na query");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _service.Update(_mapper.Map<Department>(viewModel));

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
