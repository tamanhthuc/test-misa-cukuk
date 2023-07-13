const MResource = {
    BUTTON: {
        ADD_EMPLOYEE: "Thêm mới nhân viên",
        CANCEL: "Huỷ",
        STORE_ADD: "Cẩt và Thêm",
        STORE: "Cất",
        AGREE: "Đồng ý",
        YES: "Có",
        NO: "Không",
        DELETE: "Xoá"
    },
    TOAST: {
        STATUS: {
            SUCCESS: "success",
            ERROR: "error"
        },
        TITLE: {
            SUCCESS: "Thành công! ",
            ERROR: "Lỗi! "
        },
        ICON: {
            ERROR: "icon-error",
            SUCCESS:"icon-success-bg-white"
        },
        CONTENT: {
            ERROR: "Hệ thống đang lỗi vui lòng thử lại sau",
            ADD_EMPLOYEE_SUCCESS: "Thực hiện thêm nhân viên thành công",
            UPDATE_EMPLOYEE_SUCCESS: "Thực hiện sửa nhân viên thành công",
            DELETE_EMPLOYEE_SUCCESS: "Thực hiện xoá nhân viên thành công",
            DELETE_MASS_EMPLOYEE_SUCCESS: "Thực hiện xoá nhiều nhân viên thành công"
        }
    },
    GENDER: {
        FEMALE: "Nữ",
        MALE: "Nam",
        DIFFER: "Khác",
        NAME: "Giới tính"
    },
    DIALOG: {
        EMPLOYEE: {
            TITLE: {
                ADD: "Thêm thông tin nhân viên",
                EDIT: "Sửa thông tin nhân viên"
            }
        }
    },
    VALIDATE: {
        EMPLOYEE_MES: {
            FullName_Requried: "Tên nhân viên là bắt buộc",
            FullName_Max100: "Tên nhân viên không thể nhiều hơn 100 kí tự",
            EmployeeCode_Required: "Mã nhân viên là bắt buộc",
            EmployeeCode_Max20: "Mã nhân viên không thể nhiều hơn 20 kí tự",
            Email: "Vui lòng nhập đúng định dạng email",
            Unit_Required: "Đơn vị là bắt buộc",
            Address_Max255: "Địa chỉ không thể nhiều hơn 255 kí tự",
            BankBranch_Max255: "Chi nhánh ngân hàng không thể nhiều hơn 255 kí tự",
            BankAccount_Max25: "Tài khoản không thể nhiều hơn 25 kí tự",
            BankName_Max25: "Tài khoản không thể nhiều hơn 25 kí tự",
            IdentityPlace_Max100: "Nơi cấp không thể nhiều hơn 100 kí tự",
            PhoneNumber_Max50: "Số Điện thoại không thể nhiều hơn 50 kí tự",
            TelePhoneNumber_Max50: "Số Điện thoại cố định không thể nhiều hơn 50 kí tự",
            PositionName_Max255: "Chức danh không thể nhiều hơn 255 kí tự",
            IdentityNumber_Max25: "Số CMND không thể nhiều hơn 25 kí tự"
        }
    },
    PAGE_HEADER:{
        EMPLOYEE: "Nhân viên"
    },
    PAGE: {
        EMPLOYEE: {
            NAVBAR_TEXT: "CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC"
        }
    },
    LOGO: {
        TEXT: "KẾ TOÁN"
    },
    PAGINATION: {
        SUMMARY: "Tổng",
        PAGE_NUMBER_TEXT: "Số bản ghi/trang",
        PAGE_TEXT: "trang",
        RECORD: "bản ghi"
    },
    NO_DATA: "Không có dữ liệu",
    EDIT: "Sửa"
}

export default MResource;