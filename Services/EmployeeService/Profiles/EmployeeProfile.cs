using AutoMapper;
using EmployeeService.Models;
using EmployeeService.DTOs;

namespace EmployeeService.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
