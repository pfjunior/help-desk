using AutoMapper;
using HD.Api.ViewModels.Departments;
using HD.Api.ViewModels.Employees;
using HD.Domain.Departments.Entities;
using HD.Domain.Employees.Entities;

namespace HD.Api.Extensions;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        #region Department
        CreateMap<CreateDepartmentViewModel, Department>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<UpdateDepartmentViewModel, Department>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Department, DepartmentViewModel>();
        //.ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees));
        #endregion

        #region Employee
        CreateMap<CreateEmployeeViewModel, Employee>()
            .ForMember(dest => dest.Registration, opt => opt.MapFrom(src => src.Registration))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Extension))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));

        CreateMap<UpdateEmployeeViewModel, Employee>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Registration, opt => opt.MapFrom(src => src.Registration))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Extension))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));

        CreateMap<Employee, EmployeeViewModel>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
        #endregion
    }
}
