using HumanResourcesManagmentCapstone.Models;
using HumanResourcesManagmentCapstone.ViewModel;

namespace HumanResourcesManagmentCapstone.App_Start
{
    /// <summary>
    /// Configuration of AutoMapper class
    /// Add all the required mappings between model and view models here
    /// </summary>
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeViewModel>().ReverseMap();
                cfg.CreateMap<Attendance, AttendanceViewModel>().ReverseMap();

            });
        }
    }
}