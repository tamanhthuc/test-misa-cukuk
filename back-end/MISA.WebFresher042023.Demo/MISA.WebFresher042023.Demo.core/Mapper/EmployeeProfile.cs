using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MISA.WebFresher042023.Demo.Core.DTo;
using MISA.WebFresher042023.Demo.Core.Entity;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();

        }
    }
}
