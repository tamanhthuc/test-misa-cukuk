using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualBasic;
using MISA.WebFresher042023.Demo.Core.DTo;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.Interface;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.MISAException;
using MISA.WebFresher042023.Demo.Core.Resources;
using MISA.WebFresher042023.Demo.Core.Respond;
using MISA.WebFresher042023.Demo.MISAException;
using OfficeOpenXml;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public class EmployeeService
        : BaseService<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IDepartmentRespository _departmentRespository;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRespository departmentRespository, IMapper mapper) : base(employeeRepository, mapper)
        {
            _employeeRepository = employeeRepository;

            _departmentRespository = departmentRespository;
        }




        public override async Task<int?> InsertAsync(EmployeeCreateDto employeeCreateDto)
        {

            // kiểm tra trùng mã nhân viên

            var employeeCode = employeeCreateDto.EmployeeCode;

            var isDuplicated = await IsDuplicatedAsync(employeeCode, null);

            if (isDuplicated == true)
            {

                var errMsg = String.Format(ResourceVN.UserMsg_DupEmployeeCode, employeeCode);


                throw new DuplicatedCodeException(new Dictionary<string, string>
                {
                    {"EmployeeCode", errMsg }
                });
            }

            // kiểm tra department có tồn tại trong database không

            var department = await _departmentRespository.GetAsync(employeeCreateDto.DepartmentId);

            if (department == null)
            {
                throw new NotFoundException();
            }

            var result = await base.InsertAsync(employeeCreateDto);

            return result;
        }

        public override async Task<int> UpdateAsync(Guid id, EmployeeUpdateDto entityUpdateDto)
        {


            // kiểm tra trùng mã nhân viên

            var isDuplicated = await IsDuplicatedAsync(entityUpdateDto.EmployeeCode, id);

            if (isDuplicated == true)
            {
                throw new DuplicatedCodeException(new Dictionary<string, string>
                {
                    {"EmployeeCode", ResourceVN.UserMsg_DuplicateEmployeeCode }
                });
            }

            // kiểm tra department có tồn tại trong database không

            var department = await _departmentRespository.GetAsync(entityUpdateDto.DepartmentId);

            if (department == null)
            {
                throw new NotFoundException();
            }

            var result = await base.UpdateAsync(id, entityUpdateDto);

            return result;
        }

        public async Task<bool> IsDuplicatedAsync(string code, Guid? id)
        {
            return await _employeeRepository.IsDuplicatedAsync(code, id);
        }

        public async Task<PageResult<EmployeeDto>> GetListAsync(int pageSize, int pageNumer, string? textSearch)
        {
   
            var filterEmployee = await _employeeRepository.GetListAsync(pageSize, pageNumer, textSearch);

            var entityDtos = _mapper.Map<List<EmployeeDto>>(filterEmployee.Data);

            var pageResultDto = new PageResult<EmployeeDto>
            {
                TotalPage = filterEmployee.TotalPage,
                TotalRecord = filterEmployee.TotalRecord,
                CurrentPage = filterEmployee.CurrentPage,
                CurrentPageRecords = filterEmployee.CurrentPageRecords,
                Data = entityDtos
            };

            return pageResultDto;
        }

        public async Task<byte[]> ExportDataToExcel()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var package = new ExcelPackage();

            var workSheet = package.Workbook.Worksheets.Add("Employees");

            string[] columnHeaders = { "MÃ NHÂN VIÊN", "TÊN NHÂN VIÊN", "GIỚI TÍNH", "NGÀY SINH", "SỐ CMND", "CHỨC DANH", "TÊN ĐƠN VỊ", "SỐ TÀI KHOẢN", "TÊN NGÂN HÀNG", "CHI NHÁNH TK NGÂN HÀNG" };

            for (int i = 0; i< columnHeaders.Length; i++)
            {
                workSheet.Cells[1, i+1].Value = columnHeaders[i];
            }

            string[] fieldsToExport = { "EmployeeCode", "FullName", "Gender", "DateOfBirth", "IdentityNumber", "PositionName", "DepartmentName", "BankAccount", "BankName", "BankBranch" };

            for (int i = 0; i < employees.Count; i++)
            {
                var employee = employees[i];

               
                for (int j = 0; j < fieldsToExport.Length; j++)
                {
                    var fieldValue = employee.GetType().GetProperty(fieldsToExport[j])?.GetValue(employee);

                    if (fieldsToExport[j] == "Gender" && fieldValue is Gender genderValue)
                    {
                        string genderText = genderValue == Gender.Male ? "Nam" : (genderValue == Gender.FeMale ? "Nữ" : "Khác");
                        workSheet.Cells[i + 2, j + 1].Value = genderText;
                    }

                    else if (fieldsToExport[j] == "DateOfBirth" && fieldValue is DateTime DateOfBirth)
                    {
                        workSheet.Cells[i + 2, j + 1].Value = DateOfBirth;
                        workSheet.Cells[i + 2, j + 1].Style.Numberformat.Format = "dd/mm/yyyy";
                    }
                    else
                    {
                        workSheet.Cells[i + 2, j + 1].Value = fieldValue;
                    }
                }


            }

            workSheet.Cells.AutoFitColumns();

            package.Save();

            var stream = new MemoryStream();

            package.SaveAs(stream);

            return stream.ToArray();
        }

        public async Task<string> GetMaxEmployeeCode()
        {
            var maxEmployeeCode = await _employeeRepository.GetMaxCodeEmployee();

            return maxEmployeeCode;
        }

        public Task<int> ImportDataExcel(Stream fileStream)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertEmployees(List<Employee> employees)
        {
            throw new NotImplementedException();
        }
    }
}
