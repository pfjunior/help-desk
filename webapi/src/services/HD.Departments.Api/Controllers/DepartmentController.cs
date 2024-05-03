using HD.Departments.Api.Domain.Entities;
using HD.Departments.Api.Domain.Interfaces;
using HD.Departments.Api.Dtos;
using HD.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HD.Departments.Api.Controllers;

//[Authorize]
[Route("api/department")]
public class DepartmentController : MainController
{
    private readonly IDepartmentRepository _repository;

    public DepartmentController(IDepartmentRepository repository) => _repository = repository;


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return CustomResponse(HttpStatusCode.OK, await GetDepartmentsAsync());
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var department = DepartmentDto.ToDto(await GetDepartmentByIdAsync(id));

        if (department == null) return NotFound();

        return CustomResponse(HttpStatusCode.OK, department);
    }

    [HttpGet("{code}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCode(string code)
    {
        var department = DepartmentDto.ToDto(await _repository.GetByCodeAsync(code));

        if (department == null) return NotFound();

        return CustomResponse(HttpStatusCode.OK, department);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateDepartmentDto departmentDto)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _repository.AddAsync(new Department(departmentDto.Code, departmentDto.Name));

        return CustomResponse(HttpStatusCode.Created, departmentDto);
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, UpdateDepartmentDto departmentDto)
    {
        if (id != departmentDto.Id)
        {
            AddProcessingError("The id entered is not the same as the one passed in the query");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var department = await GetDepartmentByIdAsync(id);
        department.UpdateCode(departmentDto.Code);
        department.UpdateName(departmentDto.Name);

        _repository.Update(department);

        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var department = await GetDepartmentByIdAsync(id);

        if (department is null) return NotFound();

        _repository.Delete(department);

        return CustomResponse(HttpStatusCode.NoContent);
    }

    private async Task<Department> GetDepartmentByIdAsync(Guid id) => await _repository.GetByIdAsync(id);

    private async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
    {
        var departments = await _repository.GetAllAsync();
        var departmentsDto = new List<DepartmentDto>();

        foreach (var department in departments) departmentsDto.Add(DepartmentDto.ToDto(department));

        return departmentsDto.AsEnumerable();
    }
}
