<template>
  <div class="page-container">
    <div class="page-header">
      <div class="page-header-name">{{ $_MResource.PAGE_HEADER.EMPLOYEE }}</div>
      <m-button class="btn--primary btn-add btn" @click="ShowFormDialog">
        {{ $_MResource.BUTTON.ADD_EMPLOYEE }}
      </m-button>
    </div>

    <div class="table__toolbar">
      <menu-mass-delete :is-show-delete="isDeleteMass" @confirm-delete="handleConfirmMassDelete" />

      <div class="table__toolbar__input">
        <input-text-field-icon-search
          placeholder="Tìm theo mã, tên nhân viên"
          v-model="search"
        />
        <m-icon
          icon="icon-load-again icon"
          c-class="search__load icon ml-4"
          tooltip="Tải lại dữ liệu"
          @click="refreshData"
        />
    
          <m-icon 
            c-class="ml-4 icon" 
            icon="icon-file-excel" 
            tooltip="Tải file excel" 
            @click="handleDownFile"
          />
        
      </div>
    </div>

    <div class="page-content">
      <!-- table -->
      <m-table
        :headers="headers"
        :items="items"
        @edit-item="handleEditEmployee"
        @delete="handleDeleteEmployee"
        @clone-item="handleCloneEmployee"
        name-id="EmployeeId"
        :is-loading="isLoading"
        v-model:checkboxs="checkboxEmployees"
      >
        <template #Gender="{ item }">{{
          $filters.gender(item["Gender"])
        }}</template>


        
        <template #DateOfBirth="{ item }">{{
          $filters.date(item["DateOfBirth"])
        }}</template>
      </m-table>

      <!-- Panigation table -->
      <m-pagination
        @pagechanged="onPageChange"
        :page="currentPage"
        :total-record="totalRecord"
        :total-page="totalPage"
        @select-item-page="handleSelectItemPage"
      />

      <dialog-employee
        ref="formDialogRef"
        @submit-success="handleSubmitSuccess"
        @update-success="handleUpdateSuccess"
      />

      <popup-employee-delete
        ref="popupEmployeeRef"
        :content="contentDelete"
        @delete-item="ConfirmDeleteEmployee"
      />

      <popup-delete-mass-data 
        ref="popupDeleteMassDataRef" 
        icon="icon-warning" 
        @confirm-delete-mass="submitmDeleteMass"
      />

      <m-toast ref="toastRef" />
    </div>
  </div>
  <m-loading :is-loading="isLoadingPage" />
</template>
<script>
import MLoading from "@/components/base/MLoading/MLoading.vue";
import PopupDeleteMassData from "@/components/PopupDeleteMassData/PopupDeleteMassData.vue"
import MenuMassDelete from "@/components/MenuMassDelete/MenuMassDelete.vue";
import MIcon from "@/components/base/Icon/MIcon.vue";
import InputTextFieldIconSearch from "@/components/InputTextFieldIconSearch/InputTextFieldIconSearch.vue";
import MToast from "@/components/base/toast/MToast.vue";
import MButton from "@/components/base/button/MButton.vue";
import MPagination from "@/components/base/pagination/MPagination.vue";
import DialogEmployee from "@/components/DialogEmployee/DialogEmployee.vue";
import PopupEmployeeDelete from "@/components/PopupEmployeeDelete/PopupEmployeeDelete.vue";
import MTable from "@/components/base/table/MTable.vue";
import MResource from "@/script/resource";
import api from "@/api";
import { downloadFile } from "@/helpers/utils";
import handleApiErrorMixin from "@/mixins/handleApiErrorMixin.vue"
import { FIRST_PAGE, PAGE_SIZE_DEFAULT } from "@/constant/pagination";

export default {
  name: "EmployeeList",
  components: {
    MenuMassDelete,
    MIcon,
    InputTextFieldIconSearch,
    MToast,
    MPagination,
    MButton,
    DialogEmployee,
    PopupEmployeeDelete,
    PopupDeleteMassData,
    MTable,
    MLoading
  },
  mixins: [handleApiErrorMixin],
  created() {
    this.fetchData();
  },
  data() {
    return {
      items: [],
      totalRecord: 0,
      totalPage: FIRST_PAGE,
      currentPage: FIRST_PAGE,
      CurrentPageRecords: PAGE_SIZE_DEFAULT,
      pageSize: PAGE_SIZE_DEFAULT,
      contentDelete: "",
      search: "",
      isLoading: false,
      employeeIds: [],
      checkboxEmployees: [],
      downloadUrl: "",
      isLoadingPage: false
    };
  },

  computed: {
    /**
     * headers: header của table Employee
     * @author: tmthuc (12/06/2023)
     */
    headers() {
      return [
        {
          text: "MÃ NHÂN VIÊN",
          class: "text--left",
          value: "EmployeeCode",
          width: 160,
        },
        {
          text: "TÊN NHÂN VIÊN",
          class: "text--left",
          value: "FullName",
          width: 160,
        },
        {
          text: "GIỚI TÍNH",
          class: "text--left",
          value: "Gender",
          width: 160,
        },
        {
          text: "NGÀY SINH",
          class: "text--center",
          align: "center",
          value: "DateOfBirth",
          width: 160,
        },
        {
          text: "SỐ CMND",
          tooltip: "SỐ CHỨNG MINH NHÂN DÂN",
          value: "IdentityNumber",
          class: "text--right",
          align: "right",
          width: 160,
        },
        {
          text: "CHỨC DANH",
          class: "text--left",
          value: "PositionName",
          width: 160,
        },
        {
          text: "TÊN ĐƠN VỊ",
          class: "text--left",
          value: "DepartmentName",
          width: 160,
        },
        {
          text: "SỐ TÀI KHOẢN",
          class: "text--right",
          align: "right",
          value: "BankAccount",
          width: 160,
        },
        {
          text: "TÊN NGÂN HÀNG",
          class: "text--left",
          value: "BankName",
          width: 160,
        },
        {
          text: "CHI NHÁNH TK NGÂN HÀNG",
          tooltip: "CHI NHÁNH TÀI KHOẢN NGÂN HÀNG",
          class: "text--left",
          value: "BankBranch",
          width: 260,
        },
        {
          text: "CHỨC NĂNG",
          class: "text--left",
          value: "action",
          width: 160,
        },
      ];
    },
    /**
     * isDeleteMass: kiểm tra xem button xoá hàng loạt
     * @author: tmthuc (02/07/2023)
     */
    isDeleteMass() {
      return this.checkboxEmployees.length > 0;
    },
    /**
     * params: Tham số để truyền vào api
     * @author: tmthuc (26/06/2023)
     */
    params() {
      if (this.search) {
        return {
          pageNumber: this.currentPage,
          pageSize: this.pageSize,
          textSearch: this.search,
        };
      }
      return {
        pageNumber: this.currentPage,
        pageSize: this.pageSize,
      };
    },
  },
  methods: {
    async getMaxEmployeeCode(){
      try {
        const res = await api.get("/Employees/code-max");
        return res.data;
      }catch(err){
        this.handleApiError(err);
      }
    },
    /**
     * handleDownFile: Hàm xử lí download file excel
     * @author: tmthuc (02/07/2023)
     */
    async handleDownFile() {    
      this.isLoadingPage = true;
      try {
        const res = await api.get('/Employees/export', { responseType: 'blob' });

        downloadFile(res, "employees.xlsx")

      }catch(error){
        this.handleApiError(error);

      }
      this.isLoadingPage = false;
    },
    /**
     * submitmDeleteMass: hàm xử lí xác nhận xoá nhiều bản ghi
     * @author: tmthuc (01/07/2023)
     */
    async submitmDeleteMass() {
      try {
        // 1. Gọi api xoá nhiều nhân viên
        await api.post("Employees/mass-delete", this.checkboxEmployees);
        // 2. Sau khi gọi xong gán checkbox của table về mảng rỗng
        this.checkboxEmployees = [];
        // 3. Ẩn popup xác nhận xoá nhiều
        this.$refs.popupDeleteMassDataRef.hidden();
        //4. Hiển thị toast xoá thành công
        this.$refs.toastRef.show({
        icon: MResource.TOAST.ICON.SUCCESS,
        title: MResource.TOAST.TITLE.SUCCESS,
        content: MResource.TOAST.CONTENT.DELETE_MASS_EMPLOYEE_SUCCESS,
        status: MResource.TOAST.STATUS.SUCCESS,
      });
      // Gọi lại api và cho current page về trang đầu tiên
      if (this.currentPage == FIRST_PAGE) {
        await this.fetchData();
      }
      else {
        this.currentPage = FIRST_PAGE;
      }
      }catch(err) {
        this.handleApiError(err);
      }
    },
    /**
     * handleConfirmMassDelete: Hàm xử lí xoá bản ghi
     * @author: tmthuc (02/07/2023)
     */
    handleConfirmMassDelete() {

      this.$refs.popupDeleteMassDataRef.show();
    },
    handleInput(e) {
      this.search = e.target.value;
    },
    async refreshData() {
      this.search = "";

      // this.currentPage = 1;
      // this.pageSize = 10;
    },
    /**
     * handleUpdateSuccess: Thực hiện show toast message và gọi lại api sau khi sưa
     * @author: TMThuc (28/05/2023)
     */
    handleUpdateSuccess() {
      this.$refs.toastRef.show({
        icon: MResource.TOAST.ICON.SUCCESS,
        title: MResource.TOAST.TITLE.SUCCESS,
        content: MResource.TOAST.CONTENT.UPDATE_EMPLOYEE_SUCCESS,
        status: MResource.TOAST.STATUS.SUCCESS,
      });
      // Gọi lại api
      this.fetchData();
    },
    /**
     * handleSubmitSuccess: Thực hiện show toast message và gọi lại api sau khi sưa
     * @author: TMThuc (28/05/2023)
     */
    handleSubmitSuccess() {
      //1. Hiển thị toast message sau khi thêm nhân viên thành công
      this.$refs.toastRef.show({
        icon: MResource.TOAST.ICON.SUCCESS,
        title: MResource.TOAST.TITLE.SUCCESS,
        content: MResource.TOAST.CONTENT.ADD_EMPLOYEE_SUCCESS,
        status: MResource.TOAST.STATUS.SUCCESS,
      });
      //2. Gọi lại api sau khi thêm thành công
      this.fetchData();
    },
    /**
     * handleSelectItemPage: Hàm đươc emit lên từ component pagination
     * @author: TMThuc (28/05/2023)
     *  @param {*} page là só lượng item của 1 pagec
     *  @param {*} currentPage là số lương page hiện tại
     */
    handleSelectItemPage(page, currentPage) {
      this.pageSize = page;
      this.currentPage = currentPage;
    },
    /**
     * ShowFormDialog: Hiện form dialog để thêm mới nhân viên
     * @author: TMThuc (28/05/2023)
     */
    async fetchData() {
      this.isLoading = true;
      try {
        // 1. Gọi api danh sách nhân viên và truyền params
        const res = await api.get("/Employees/filter", { params: this.params });
        
        if (res.status === 200) {
          // 2. Gọi api thành công gán isLoading về kết quả false
          this.isLoading = false;
          // 3. gán itemts bằng danh sách nhân viên trả về
          this.items = res.data.Data;
          // 4. Láy giá trị của tất cả bản ghi có trong database
          this.totalRecord = res.data.TotalRecord;
          // 5. Lấy tổng page từ back-end trả về
          this.totalPage = res.data.TotalPage;
        }
      } catch (err) {
        this.isLoading = false;
       // 6. Hàm sử lí lỗi
        this.handleApiError(err);
        
      }
    },
    /**
     * ShowFormDialog: Hiện form dialog để thêm mới nhân viên
     * @author: TMThuc (28/05/2023)
     */
    async ShowFormDialog() {
      this.$refs.formDialogRef.show();
    },
    /**
     * handleEditEmployee: Hiện form dialog để sửa thông tin nhân viên
     * @author: TMThuc (28/05/2023)
     */
    handleEditEmployee(item) {
      this.$refs.formDialogRef.show(item);
    },
    /**
     * handleDeleteEmployee: Thực hiện xoá nhân viện và hiển thi lên popup xoá nhân viên
     * @author: TMThuc (28/05/2023)
     * @param {*} item là nhân viên chọn để xoá
     */
    handleDeleteEmployee(item) {
      this.$refs.popupEmployeeRef.show(item);
      if (item) {
        this.contentDelete = `Bạn có thực sực muốn xoá nhân viên <${item.EmployeeCode}> không ?`;
      }
    },
    async handleCloneEmployee(item){
      const newItem = {...item};
      const maxCode = await this.getMaxEmployeeCode();
      newItem.EmployeeCode = maxCode;
      this.$refs.formDialogRef.show(newItem, true);
    },
    /**
     * onPageChange: thực hiện hành động emit truyền lên
     * @author: TMThuc (28/05/2023)
     * @param {*} page là page hiện tại được chọn
     */
    onPageChange(page) {
      this.currentPage = page;
    },
    /**
     * ConfirmDeleteEmployee: Thực hiện gọi api xoá nhân viên
     * @author: TMThuc (31/05/2023)
     * @param {*} item là nhân viên chọn để xoá
     */
    async ConfirmDeleteEmployee(item) {
      try {
        //1. Gọi api xoá
        await api.delete(`/Employees/${item.EmployeeId}`);
        //2. ẩn popup đi sau khi gọi thành công
        this.$refs.popupEmployeeRef.hidden();
        //3. Hiện message thông báo xoá thành công
        this.$refs.toastRef.show({
          icon: MResource.TOAST.ICON.SUCCESS,
          title: MResource.TOAST.TITLE.SUCCESS,
          content: "Thực hiện xoá nhân viên thành công",
          status: MResource.TOAST.STATUS.SUCCESS,
        });
        //4. Gọi lại api và cho currentPage về trang đầu tiên
        
        
        if (this.currentPage == FIRST_PAGE) {
          await this.fetchData();
        }
        else {
          this.currentPage = FIRST_PAGE;
        }
      } catch (err) {
        // 5. Hàm xử lí lỗi
        this.handleApiError(err)
      }
    },
  },
  watch: {
    /**
     * 
     * @param {*} newValue 
     * @param {*} oldValue 
     * @author: tmthuc (16/06/2023)
     */
    params(newValue, oldValue) {
      if (oldValue !== newValue) {
        // 1. Gọi lại api nếu tham số params thay đổi
        this.fetchData();
      }
    },
    /**
     * 
     * @param {*} val 
     * @author: tmthuc (16/06/2023)
     */
    search(val) {
      if (val) {
        // 1. Nếu search thì cho về trang đầu tiên
        this.currentPage = 1;
        this.pageSize = 10;
      }
    },
  },
};
</script>
<style scoped>
@import "employeeList.css";
</style>
