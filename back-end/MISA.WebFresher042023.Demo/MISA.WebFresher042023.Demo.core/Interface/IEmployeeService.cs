using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher042023.Demo.Core.DTo;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.Respond;

namespace MISA.WebFresher042023.Demo.Core.Interface
{
    public interface IEmployeeService : IBaseService<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        /// <summary>
        /// Kiểm mã nhân viên có trùng không
        /// </summary>
        /// <param name="code">mã nhân viên</param>
        /// <returns>true || false</returns>
        /// Created by: tmthuc(24/06/2023)
        Task<bool> IsDuplicatedAsync(string code, Guid? id);

        /// <summary>
        /// Lây danh sách các bản ghi
        /// </summary>
        /// <param name="page">page hiện tại</param>
        /// <param name="pageSize">số lượng bản ghi trên mỗi page</param>
        /// <param name="filter">lọc bản ghi theo điều kiện</param>
        /// <returns>Tổng lượng bản ghi có trong database, số page hiên tại, số lượng các bản ghi trên mỗi page, danh sách các bản ghi </returns>
        /// Created by: tmthuc (24/06/2023)
        Task<PageResult<EmployeeDto>> GetListAsync(int pageSize, int pageNumber, string? textSearch);

        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <returns></returns>
        Task<byte[]> ExportDataToExcel();

        /// <summary>
        /// Lấy mã nhân viên lớn nhất có trong database
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        Task<string> GetMaxEmployeeCode();

        Task<int> ImportDataExcel(Stream fileStream);

        Task<int> InsertEmployees(List<Employee> employees);

    }
}
