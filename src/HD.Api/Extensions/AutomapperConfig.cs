using AutoMapper;
using HD.Api.ViewModels.Departments;
using HD.Api.ViewModels.Users;
using HD.Domain.Departments.Entities;
using HD.Domain.Users.Entities;

namespace HD.Api.Extensions;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Department, DepartmentViewModel>();

        CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
    }
}
