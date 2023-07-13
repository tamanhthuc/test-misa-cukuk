using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRespository<Department>, IDepartmentRespository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
        }

    }
}
