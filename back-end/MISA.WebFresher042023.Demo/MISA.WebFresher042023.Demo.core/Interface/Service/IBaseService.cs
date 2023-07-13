using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher042023.Demo.Core.DTo;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Respond;

namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{
    /// <summary>
    /// interface base serivce
    /// </summary>
    /// <typeparam name="TEntityDto">entityDto</typeparam>
    /// <typeparam name="TEntityCreateDto">entityCreatedDto</typeparam>
    /// <typeparam name="TEntityUpdateDto">entityUpdateDto</typeparam>
    public interface IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        /// <summary>
        /// Hàm lấy một nhân viên
        /// </summary>
        /// <param name="id">id của đối tượng</param>
        /// <returns>Đối tượng</returns>
        /// Created by: tmthuc (17/06/2023)
        public Task<TEntityDto> GetAsync(Guid id);

        /// <summary>
        /// Lấy tất cả các danh sách bản ghi có trong hệ thống
        /// </summary>
        /// <returns>Danh sách tất cả các bản ghi</returns>
        /// Created by: tmthuc (27/06/2023)
        public Task<List<TEntityDto>> GetAllAsync();

        

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Thông bản ghi Dto</param>
        /// <returns>1</returns>
        /// Created By: tmthuc (24/06/2023)


        public Task<int?> InsertAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Cập nhật 1 bản hi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <param name="entityUpdateDto">thông tin bản ghi cần cập nhật</param>
        /// <returns>1</returns>
        /// Created by: tmthuc (24/06/2023)

        public Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Xoá 1 bản ghi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns>1</returns>
        /// Created by: tmthuc (24/06/2023)
        public Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// xoá nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách các id của bản ghi cần xoá</param>
        /// <returns>1</returns>
        /// Created by: tmthuc (24/06/2023)
        public Task<int> DeleteMultipelAsync(List<Guid> ids);

    }
}
