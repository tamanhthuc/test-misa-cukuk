using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.Resources;

namespace MISA.WebFresher042023.Demo.Core.DTo
{
    /// <summary>
    /// DTo tạo nhân viên
    /// </summary>
    public class EmployeeUpdateDto
    {
        /// <summary>
        /// Id của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        /// 
        [Required]
        public Guid EmployeeId { get; set; }


        /// <summary>
        /// mã của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        /// 
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_EmployeeCodeEmpty))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_EmployeeCodeMax20))]
        [RegularExpression(@".*\d$", ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_EmployeeCodeEndNum))]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// tên của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        /// 
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_EmployeeFullNameEmpty))]
        [MaxLength(255)]
        public string FullName { get; set; }

        /// <summary>
        /// ngày sinh nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// giới tính của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [Range(0, 2)]
        public Gender? Gender { get; set; }

        /// <summary>
        /// Id của bộ phận
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        /// <summary>
        /// Id của bộ phận
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_DepartmentEmpty))]
        public Guid DepartmentId { get; set; }

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

        /// <summary>
        /// Tên giới tinh nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [MaxLength(50)]
        public string? GenderName { get; set; }

        /// <summary>
        /// số điện thoai cố định của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [MaxLength(50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_PhoneNumberMax50))]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// số điện thoại của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [MaxLength(50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_TelePhoneNumberMax50))]
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// email của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [MaxLength(100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_EmailMax100))]
        public string? Email { get; set; }

        /// <summary>
        /// số chứng minh nhân dân của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [MaxLength(25, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_IdentityNumberMax25))]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// ngày cấp chứng minh nhân dân của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        /// 
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi làm chứng minh nhân dân của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [MaxLength(50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_IdentityPlaceMax50))]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// số tài khoản của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [MaxLength(25, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_BankAccoutMax25))]
        public string? BankAccount { get; set; }

        /// <summary>
        /// tên ngân hàng của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_BankNameMax255))]
        public string? BankName { get; set; }

        /// <summary>
        /// chi nhánh ngân hàng của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_BankBranchMax255))]
        public string? BankBranch { get; set; }

        /// <summary>
        /// mã số thuê của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [MaxLength(25, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_TaxMax255))]
        public string? TaxCode { get; set; }

        /// <summary>
        /// có là nhân viên không ?
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        /// 
        public bool? IsEmployee { get; set; }

        /// <summary>
        /// có phải là khách hàng không
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        /// 
        public bool? IsCustomer { get; set; }

        /// <summary>
        /// ngày gia nhập của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        /// 
        public DateTime? JoinDate { get; set; }

        /// <summary>
        /// lương  của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        /// 
        public decimal? Salary { get; set; }

        /// <summary>
        /// Vị trí của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_PositionName255))]
        public string? PositionName { get; set; }

        /// <summary>
        /// Địa chỉ của nhân viên
        /// </summary>
        /// Created By: tmthuc (13/06/2023)
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.UserMsg_Address255))]
        public string? Address { get; set; }
    }
}
