<template>
  <m-form-dialog :title="title" ref="formDialogRef">
    <template #header>
      <div class="dialog__title">
        <div class="dialog__title__name">{{ title }}</div>
        <slot name="header"></slot>
        <!-- <div class="radio__input">
          <div class="radio">
            <input v-model="account" id="customer" type="checked" />
            <label for="customer" class="radio-label">Là khách hàng</label>
          </div>

          <div class="radio">
            <input v-model="account" id="employee" type="checked" />
            <label for="employee" class="radio-label">Là nhân viên</label>
          </div>
        </div> -->
      </div>

      <div class="dialog__header__action">
        <div class="mx-4">
          <m-icon icon="icon icon-question" tooltip="Gúp đỡ"></m-icon>
        </div>

        <m-icon
          class="close_form"
          icon="icon-close-xl"
          tooltip="Đóng Form"
          @click="handleCloseForm"
        ></m-icon>
      </div>
    </template>
    <template #content>
      <validator-form
        :validation-schema="schema"
        ref="formRef"
        v-slot="{ handleSubmit }"
      >
        <form @submit="handleSubmit($event, onSubmit)" @keydown="handleKeydown">
          <div class="dialog__content__info">
            <div class="dialog__content__info__left">
              <m-input-text-field-with-validation
                v-model="employee.EmployeeCode"
                name="EmployeeCode"
                required
                label="Mã"
                ref="EmployeeCodeRef"
                :tab-index="1"
              />

              <m-input-text-field-with-validation
                v-model="employee.FullName"
                name="FullName"
                ref="FullNameRef"
                required
                label="Tên"
                :tab-index="1"

              />
            </div>

            <div class="dialog__content__info__right">
              <!-- <flat-pickr
                v-model="employee.DateOfBirth"
                :config="flatpickrOptions"
              /> -->
              <m-flat-picker-with-validation
                name="DateOfBirth"
                v-model="employee.DateOfBirth"
                label="Ngày sinh"
              />
              <div class="radio__container">
                <div class="radio__label">{{ $_MResource.GENDER.NAME }}</div>

                <div class="radio__input">
                  <div class="radio">
                    <input
                      v-model="employee.Gender"
                      :value="1"
                      id="male"
                      type="radio"
                      tabindex="2"
                    />
                    <label for="male" class="radio-label">{{
                      $_MResource.GENDER.MALE
                    }}</label>
                  </div>
                  <div class="radio">
                    <input
                      v-model="employee.Gender"
                      :value="0"
                      id="female"
                      type="radio"
                      tabindex="2"
                    />
                    <label for="female" class="radio-label">{{
                      $_MResource.GENDER.FEMALE
                    }}</label>
                  </div>

                  <div class="radio">
                    <input
                      v-model="employee.Gender"
                      :value="2"
                      id="other"
                      type="radio"
                      tabindex="2"
                    />
                    <label for="other" class="radio-label">{{
                      $_MResource.GENDER.DIFFER
                    }}</label>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="dialog__content__info">
            <div class="dialog__content__info__left">
              <m-combobox
                :valueSelected="employee.DepartmentName"
                @updateCombobox="handleUpdateCombobox"
                :items="itemDepartments"
                label="Đơn vị"
                required
                item-text="DepartmentName"
                item-value="DepartmentId"
                name="Unit"
                :tab-index="1"
                
              ></m-combobox>
            </div>

            <div class="dialog__content__info__right">
              <m-input-text-field-with-validation
                label="Số CMND"
                tooltip="Số chứng minh nhân dân"
                name="IdentityNumber"
                v-model="employee.IdentityNumber"
              />
              <m-flat-picker-with-validation
                label="Ngày cấp"
                name="IdentityDate"
                v-model="employee.IdentityDate"
              />
            </div>
          </div>

          <div class="dialog__content__info">
            <div class="dialog__content__info__left">
              <m-input-text-field-with-validation
                v-model="employee.PositionName"
                label="Chức danh"
                name="PositionName"
                tabIndex="1"
              />
            </div>

            <div class="dialog__content__info__right">
              <m-input-text-field-with-validation
                v-model="employee.IdentityPlace"
                label="Nơi cấp"
                name="IdentityPlace"
              />
            </div>
          </div>

          <div class="dialog__content__info">
            <m-input-text-field-with-validation
              v-model="employee.Address"
              label="Địa chỉ"
              name="Address"
            />
          </div>

          <div class="dialog__content__info">
            <m-input-text-field-with-validation
              v-model="employee.PhoneNumber"
              label="ĐT di động"
              tooltip="Điện thoại di động"
              name="PhoneNumber"
            />

            <m-input-text-field-with-validation
              label="ĐT cố định"
              tooltip="Điện thoại cố định"
              name="TelephoneNumber"
              v-model="employee.TelephoneNumber"
            />

            <m-input-text-field-with-validation
              v-model="employee.Email"
              label="Email"
              placeholder="tmthuc@gmail.com"
              name="Email"
            />
          </div>

          <div class="dialog__content__info">
            <m-input-text-field-with-validation
              v-model="employee.BankAccount"
              label="Số tài khoản"
              name="BankAccount"
            />
            <m-input-text-field-with-validation
              v-model="employee.BankName"
              label="Tên ngân hàng"
              name="BankName"
            />
            <m-input-text-field-with-validation
              v-model="employee.BankBranch"
              label="Chi nhánh"
              name="BankBranch"
            />
          </div>

          <div class="dialog__action">
            <div class="dialog__action__left">
              <m-button class="btn cancel-dialog" @click="hidden" type="button">
                {{ $_MResource.BUTTON.CANCEL }}
              </m-button>
            </div>
            <div class="dialog__action__right">
              <m-button
                class="btn"
                :disabled="isDisabledStored"
                type="button"
                @click="handleStoreForm"
              >
                {{ $_MResource.BUTTON.STORE }}</m-button
              >

              <m-button
                class="btn--primary ml-2 btn"
                type="button"
                @click="handleStoreAndAddForm"
                :disabled="isDisabledStored"
              >
                {{ $_MResource.BUTTON.STORE_ADD }}
              </m-button>
            </div>
          </div>
        </form>
      </validator-form>
    </template>
  </m-form-dialog>

  <popup-confirm-save-data
    ref="popupConfirmSavaData"
    @cancel-form="handleCancelForm"
    @save-form="handleSaveForm"
  />

  <popup-message-errors ref="popupEmployeeDeleteRef" :errors="errors" />

  <m-toast ref="toastRef" />
</template>
<script>
import MResource from "@/script/resource";
import MToast from "../base/toast/MToast.vue";
import PopupConfirmSaveData from "@/components/PopupConfirmSaveData/PopupConfirmSaveData.vue";
import PopupMessageErrors from "../PopupMessageErrors/PopupMessageErrors.vue";
import MFlatPickerWithValidation from "../base/flatPicker/MFlatPickerWithValidation.vue";
import MFormDialog from "../base/dialogForm/MFormDialog.vue";
import MCombobox from "../base/combobox/MCombobox.vue";
import { Form as ValidatorForm } from "vee-validate";
import MButton from "../base/button/MButton.vue";
import MInputTextFieldWithValidation from "../base/input/MInputTextFieldWithValidation.vue";
import MIcon from "../base/Icon/MIcon.vue";
import * as Yup from "yup";
import api from "@/api";
// import { isEmptyObject } from "@/helpers/utils";
import { DEFAULT_EMPLOYEE } from "@/resource/data/EmployeeData";
import { isEqualDeep } from "@/helpers/utils";
import handleApiErrorMixin from "@/mixins/handleApiErrorMixin.vue";
import { CODE_SAVE } from "@/constant/keyCode";
export default {
  name: "DialogEmployee",
  components: {
    MToast,
    PopupMessageErrors,
    MFormDialog,
    MCombobox,
    ValidatorForm,
    MButton,
    MInputTextFieldWithValidation,
    MIcon,
    MFlatPickerWithValidation,
    PopupConfirmSaveData,
  },
  mixins: [handleApiErrorMixin],
  data() {
    const schema = Yup.object().shape({
      FullName: Yup.string()
        .required(MResource.VALIDATE.EMPLOYEE_MES.FullName_Requried)
        .max(100),
      EmployeeCode: Yup.string()
        .required(MResource.VALIDATE.EMPLOYEE_MES.EmployeeCode_Required)
        .max(20, MResource.VALIDATE.EMPLOYEE_MES.EmployeeCode_Max20),
      Email: Yup.string().email(MResource.VALIDATE.EMPLOYEE_MES.Email),
      Unit: Yup.string().required(
        MResource.VALIDATE.EMPLOYEE_MES.Unit_Required
      ),
      Address: Yup.string().max(
        255,
        MResource.VALIDATE.EMPLOYEE_MES.Address_Max255
      ),
      BankBranch: Yup.string().max(
        255,
        MResource.VALIDATE.EMPLOYEE_MES.BankBranch_Max255
      ),
      BankName: Yup.string().max(
        255,
        MResource.VALIDATE.EMPLOYEE_MES.BankName_Max255
      ),
      BankAccount: Yup.string().max(25),
      IdentityPlace: Yup.string().max(
        100,
        MResource.VALIDATE.EMPLOYEE_MES.IdentityPlace_Max100
      ),
      PhoneNumber: Yup.string().max(
        50,
        MResource.VALIDATE.EMPLOYEE_MES.PhoneNumber_Max50
      ),
      TelephoneNumber: Yup.string().max(
        50,
        MResource.VALIDATE.EMPLOYEE_MES.TelePhoneNumber_Max50
      ),
      PositionName: Yup.string().max(
        255,
        MResource.VALIDATE.EMPLOYEE_MES.PositionName_Max255
      ),
      IdentityNumber: Yup.string().max(
        25,
        MResource.VALIDATE.EMPLOYEE_MES.IdentityNumber_Max25
      ),
    });
    return {
      date: "",
      schema,
      title: MResource.DIALOG.EMPLOYEE.TITLE.ADD,
      isShow: false,
      selectedOption: "",
      account: "",
      employee: {},
      errors: [],
      isEdit: false,
      isStoreAndAdd: false,
      itemDepartments: [],
      isResetForm: false,
      employeeOld: {},
      isDisabledStored: false,
    };
  },
  
  methods: {
    /**
     * handleKeydown: xử lí phím tắt của form
     * @param {*} e là đối số event của form
     * @author tmthuc (06/07/2023)
     */
    handleKeydown(e) {
      if(e.keyCode === CODE_SAVE && e.ctrlKey && e.shiftKey) {
        e.preventDefault();
        this.handleStoreAndAddForm();
      }
      else if (e.keyCode === CODE_SAVE && e.ctrlKey) {
        e.preventDefault();
        this.handleStoreForm();
      }
     
    },
    /**
     * getMaxEmployeeCode: Gọi api lấy mã code nhân viên lơn nhất
     * @author tmthuc (05/07/2023)
     */
    async getMaxEmployeeCode() {
      try {
        const res = await api.get("/Employees/code-max");
        return res.data;
      } catch (err) {
        this.handleApiError(err);
      }
    },
    /**
     * handleCloseForm: Xử lí đong form
     * @author tmthuc (28/06/2023)
     */
    handleCloseForm() {
      // 1. Kiểm tra giá trị mới và giá trị cũ
      if (isEqualDeep(this.employee, this.employeeOld)) {
        // 2. Nếu giống nhau ẩn luônn popup thông báo
        this.$refs.formDialogRef.hidden();
      } else {
        // 3. Hiện poup cất thông báo ra
        this.$refs.popupConfirmSavaData.show();
      }
    },
    /**
     * reset: Xử lí reset về giá trị mặc định
     * @author tmthuc (28/06/2023)
     */
    reset() {
      this.employee = JSON.parse(JSON.stringify(DEFAULT_EMPLOYEE));
    },
    /**
     * handleCancelForm: Xác nhận ẩn form dialog
     *  @author: TMThuc (31/05/2023)
     */
    handleCancelForm() {
      this.hidden();
    },

    /**
     * handleSaveForm: Xác nhận gửi dữ liệu form
     *  @author: TMThuc (31/05/2023)
     */

    async handleSaveForm() {
      // if (this.isEdit) {
      //   // Nếu là sửa form thì gửi api sửa thông tin nhân vien
      //   await this.EditEmployee();
      // } else {
      //   // Nếu là thêm mới nhân viên thì gửi api thêm mới nhân viên
      //   await this.addEmployee();
      // }
      const isValid = await this.$refs.formRef
        .validate()
        .then((res) => res.valid);
      // 2. Kiểm tra có lỗi không
      if (isValid) {
        // 3. Hiển thi popup xác nhận
        if (this.isEdit) {
          // Nếu là sửa form thì gửi api sửa thông tin nhân vien
          await this.EditEmployee();
        } else {
          // Nếu là thêm mới nhân viên thì gửi api thêm mới nhân viên
          await this.addEmployee();
        }
      }
    },
    /**
     * handleUpdateCombobox: Cập nhâp id của department
     *  @author: TMThuc (31/05/2023)
     * @param {*} data dữ liệu emit lên
     */

    handleUpdateCombobox(data) {
      this.employee.DepartmentId = data;
    },
    /**
     * getitemDepartments: call api để lây danh sách các bộ phận
     * @author: TMThuc (01/06/2023)
     */
    async getitemDepartments() {
      try {
        // 1. Gọi api deparments
        const res = await api.get("/Departments");

        // 2. Gán dữ liệu trả về
        this.itemDepartments = res.data;
      } catch (err) {
        console.log(err);
      }
    },
    /**
     * handleStoreForm: Xác nhận cất dữ liệu
     * @author: TMThuc (31/05/2023)
     *
     * @param {*} errors các lỗi validate
     */
    async handleStoreForm() {
      // 1. Validate form
      const isValid = await this.$refs.formRef
        .validate()
        .then((res) => res.valid);
      // 2. Kiểm tra có lỗi không
      if (isValid) {
        // 3. Hiển thi popup xác nhận
        if (this.isEdit) {
          // Nếu là sửa form thì gửi api sửa thông tin nhân vien
          await this.EditEmployee();
        } else {
          // Nếu là thêm mới nhân viên thì gửi api thêm mới nhân viên
          await this.addEmployee();
        }
      }
    },

    /**
     * handleStoreAndAddForm: Các nhận cất và thêm dữ liệu
     * @author: TMThuc (31/05/2023)
     */
    async handleStoreAndAddForm() {
      // 1. validate form
      this.isResetForm = true;
      const isValid = await this.$refs.formRef
        .validate()
        .then((res) => res.valid);
      // 2. Kiểm tra có lỗi không
      if (isValid) {
        // 3. Hiển thi popup xác nhận
        if (this.isEdit) {
          // Nếu là sửa form thì gửi api sửa thông tin nhân vien
          await this.EditEmployee();
        } else {
          // Nếu là thêm mới nhân viên thì gửi api thêm mới nhân viên
          await this.addEmployee();
        }
      }
      this.isResetForm = false;
    },
    async onSubmit() {
      if (this.isEdit) {
        await this.EditEmployee();
      } else {
        await this.addEmployee();
      }
    },
    /**
     * Show: hàm hiển thị form dialog
     * @author: TMThuc (28/05/2023)
     *
     * @param {*} item là  dữ liểu truyền vào
     * @param {*} isClone kiểm tra xem có phải là nhân bản
     */
    async show(item = null, isClone = false) {
      // Trạng disable button về flase
      this.isDisabledStored = false;
      await this.getitemDepartments();
      this.isEdit = false;
      this.employee = { ...DEFAULT_EMPLOYEE };
      this.employeeOld = { ...DEFAULT_EMPLOYEE };
      if (item && isClone === false) {
        this.isEdit = true;
        //  Gán title lại vào form
        this.title = MResource.DIALOG.EMPLOYEE.TITLE.EDIT;
        // Làm mới dữ liệu ra địa chỉ mới để không bị tham chiếu
        this.employee = { ...item };

        this.employeeOld = { ...item };

        this.$refs.formDialogRef.show();
      } else if (item && isClone) {
        this.isEdit = false;
        this.employee = { ...item };
        this.employeeOld = { ...item };

        this.title = MResource.DIALOG.EMPLOYEE.TITLE.ADD;
        const maxEmployeeCode = await this.getMaxEmployeeCode();
        this.employee.EmployeeCode = maxEmployeeCode;
        this.$refs.formDialogRef.show();
      } else {
        // Gán title lại vào form
        this.title = MResource.DIALOG.EMPLOYEE.TITLE.ADD;
        const maxEmployeeCode = await this.getMaxEmployeeCode();
        this.employee.EmployeeCode = maxEmployeeCode;
        this.$refs.formDialogRef.show();
      }
      this.$nextTick(() => {
        this.$refs.EmployeeCodeRef.focus();
      });
    },
    /**
     * hidden: Hàm này để ẩn form dialog
     * @author: TMThuc (28/05/2023)
     */
    hidden() {
      // 1. Trước khi ẩn reset cái giá trị của form
      this.reset();
      // 2. Thực hiện ẩn form đi
      this.$refs.formDialogRef.hidden();
    },

    /**
     * addEmployee: gọi api để thêm nhân viên
     * @author: TMThuc (28/05/2023)
     *
     */
    async addEmployee() {
      // sau khi click cho trang diabled button về true
      this.isDisabledStored = true;
      try {
        // 1. Gọi api thêm nhân viên
        await api.post("/Employees", this.employee);
        // 2. Sau khi gọi thành công emit lên component cha để load lại data
        this.$emit("submit-success");

        if (this.isResetForm) {
          // 2. Kiểm tra có reset form hay khong
          this.$refs.formRef.resetForm();
          const maxEmployeeCode = await this.getMaxEmployeeCode();
          this.employee.EmployeeCode = maxEmployeeCode;
          this.$refs.FullNameRef.focus();
        } else {
          this.hidden();
        }
      } catch (err) {
        // 3. sử lí lỗi
        this.handleApiError(err);
        // 4. Lấy các lỗi tử backend trả về hiện thị lên front end
        if (err.response.data.Errors) {
          this.$refs.formRef.setErrors(err.response.data.Errors);
        }
      }
      // Gọi api xong và đòng form cho trạng disabled của button về false
      this.isDisabledStored = false;
    },
    /**
     * addEmployee: gọi api để sửa thông tin  nhân viên
     * @author: TMThuc (28/05/2023)
     *
     */
    async EditEmployee() {
      this.isDisabledStored = true;
      try {
        // 1. Lấy id của nhân viên được sửa
        const id = this.employee.EmployeeId;
        // 2. Gọi api sửa nhân viên
        await api.put(`/Employees/${id}`, this.employee);

        // 3. Gọi api sửa thành công emit lên component cha để load lại data
        this.$emit("update-success");

        if (this.isResetForm) {
          // 4. Kiểm tra có reset form lại không
          this.$refs.formRef.resetForm();
          const maxEmployeeCode = await this.getMaxEmployeeCode();
          this.employee.EmployeeCode = maxEmployeeCode;
          this.title = MResource.DIALOG.EMPLOYEE.TITLE.ADD;
          this.$refs.FullNameRef.focus();
        } else {
          this.hidden();
        }
      } catch (err) {
        // 3. sử lí lỗi
        this.handleApiError(err);
        // 4. Lấy các lỗi tử backend trả về hiện thị lên front end
        if (err.response.data.Errors) {
          this.$refs.formRef.setErrors(err.response.data.Errors);
        }
      }
      this.isDisabledStored = false;
    },
  },
};
</script>
<style>
@import url("./dialogEmployee.css");
</style>
