using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher042023.Demo.Core.DTo;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Respond;

namespace MISA.WebFresher042023.Demo.Core.Interface
{
    /// <summary>
    /// Interface Employee Respositorys
    /// </summary>
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Kiểm tra mã bản ghi có trùng trong database không
        /// </summary>
        /// <param name="code">mã bản ghi</param>
        /// <returns>false || true</returns>
        Task<bool> IsDuplicatedAsync(string code, Guid? id);

        /// <summary>
        /// Lấy danh sách tất cả các bản ghi theo pageSize, pageNumber, textSearch
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <param name="pageNumber">Trang page hiện tại</param>
        /// <param name="textSearch">nội dụng muốn tìm kiếm</param>
        /// <returns>Danh sách các bản ghi</returns>
        /// Created by: tmthuc (30/06/2023)
        Task<PageResult<Employee>> GetListAsync(int pageSize, int pageNumber, string?  textSearch);

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="ids">Mảng id của các bản ghi cần xoá</param>
        /// <returns>1</returns>
        /// Created by: tmthuc (24/06/2023)

        Task<int> DeleteMultipleAsync(List<Guid> ids);

        /// <summary>
        /// Lấy mã nhân viên lơn nhất có trong database
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        Task<string> GetMaxCodeEmployee();
    }
}
