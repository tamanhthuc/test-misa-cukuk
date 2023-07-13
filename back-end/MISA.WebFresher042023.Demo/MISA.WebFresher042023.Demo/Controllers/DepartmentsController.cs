using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface;
using MISA.WebFresher042023.Demo.Core.Interface.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController<Department, Department, Department>
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentsController(IDepartmentService departmentService)
            : base(departmentService)
        {
            _departmentService = departmentService;
        }

    }
}
