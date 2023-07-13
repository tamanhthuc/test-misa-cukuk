using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface;
using MISA.WebFresher042023.Demo.Core.Respond;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public class DepartmentService : BaseService<Department,Department, Department, Department>, IDepartmentService
    {

        private readonly IDepartmentRespository _deparmentRepository;

        public DepartmentService(IDepartmentRespository departmentRepository, IMapper mapper) : base(departmentRepository, mapper)
        {
            _deparmentRepository = departmentRepository;
        }
       
    }
}
