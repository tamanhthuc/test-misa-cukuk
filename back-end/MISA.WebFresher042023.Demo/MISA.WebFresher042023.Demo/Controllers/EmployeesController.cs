using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Headers;
using Dapper;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.DTo;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface;

using MISA.WebFresher042023.Demo.MISAException;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>

    {
        #region Fields
      
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }

    #endregion

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilter(int pageSize = 10, int pageNumber = 1, string? textSearch = "")
        {
            var result = await _employeeService.GetListAsync(pageSize, pageNumber, textSearch);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportDataExcell()
        {
            var excelData = await _employeeService.ExportDataToExcel();



            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");
        }

        [HttpGet("code-max")]
        public async Task<IActionResult> GetMaxEmployeeCode()
        {
            var result = await _employeeService.GetMaxEmployeeCode();

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportFileData(IFormFile file)
        {
            var result = await _employeeService.ImportDataExcel(file.OpenReadStream());

            return StatusCode(StatusCodes.Status201Created, result);
        }

    }
}
