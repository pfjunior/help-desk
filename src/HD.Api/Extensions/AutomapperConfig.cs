using AutoMapper;
using HD.Api.ViewModels.Departments;
using HD.Api.ViewModels.Employees;
using HD.Api.ViewModels.Tickets;
using HD.Domain.Departments.Entities;
using HD.Domain.Employees.Entities;
using HD.Domain.Tickets.Entities;

namespace HD.Api.Extensions;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        #region Department
        CreateMap<CreateDepartmentViewModel, Department>();

        CreateMap<UpdateDepartmentViewModel, Department>();

        CreateMap<Department, DepartmentViewModel>();
        #endregion

        #region Employee
        CreateMap<CreateEmployeeViewModel, Employee>();

        CreateMap<UpdateEmployeeViewModel, Employee>();

        CreateMap<Employee, EmployeeViewModel>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
        #endregion

        #region Ticket
        CreateMap<Ticket, TicketViewModel>();
        #endregion
    }
}
