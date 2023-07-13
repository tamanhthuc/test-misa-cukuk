using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Entity
{
    /// <summary>
    /// BaseEntitys
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// ngày tạo
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        /// 
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// ngưởi tạo
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        /// 
        public string? CreatedBy { get; set; }

        /// <summary>
        /// ngày chỉnh sửa
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        /// 
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// người sửa
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        /// 
        public string? ModifiedBy { get; set; }
    }
}
