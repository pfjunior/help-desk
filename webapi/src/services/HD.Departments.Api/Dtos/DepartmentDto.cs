using HD.Departments.Api.Domain.Entities;

namespace HD.Departments.Api.Dtos;

public class DepartmentDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }


    public static DepartmentDto ToDto(Department department) => new DepartmentDto { Id = department.Id, Code = department.Code, Name = department.Name };
}
