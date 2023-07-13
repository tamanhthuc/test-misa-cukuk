using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Respond;

namespace MISA.WebFresher042023.Demo.Core.Interface.Repository
{
    /// <summary>
    /// Interface base repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created By: tmthuc (24/06/2023)
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Hàm lấy một bản ghi
        /// </summary>
        /// <param name="EmployeeId">id của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: tmthuc (17/06/2023)

        Task<TEntity?> GetAsync(Guid id);

        /// <summary>
        /// Lấy tất cả các danh sách bản ghi có trong hệ thống
        /// </summary>
        /// <returns>Danh sách tất cả các bản ghi</returns>
        /// Created by: tmthuc (27/06/2023)
        public Task<List<TEntity>> GetAllAsync();


        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity">Thôngn tin bản ghi</param>
        /// <returns>1</returns>
        /// Created by: tmthuc (24/06/2023)
        
        Task<int?> InsertAsync(TEntity entity);

        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <param name="entity">Thông tin cập nhật bản ghi</param>
        /// <returns>1</returns>
        /// Created by: tmthuc (24/06/2023)
        
        Task<int> UpdateAsync(Guid id, TEntity entity);


        /// <summary>
        /// Xoá 1 bản ghi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns>1</returns>
        /// Created by: tmthuc (24/06/2023)
        
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="ids">Mảng id của các bản ghi cần xoá</param>
        /// <returns>1</returns>
        /// Created by: tmthuc (24/06/2023)
        
        Task<int> DeleteMultipleAsync(List<Guid> ids);

    }
}
