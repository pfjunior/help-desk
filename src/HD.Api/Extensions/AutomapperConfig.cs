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
        #region Department
        CreateMap<CreateDepartmentViewModel, Department>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<UpdateDepartmentViewModel, Department>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Department, DepartmentViewModel>();
        #endregion

        CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
    }
}
