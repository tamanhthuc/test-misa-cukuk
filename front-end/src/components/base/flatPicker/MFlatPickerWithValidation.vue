<template>
  <div class="input__container" :class="{ 'input-invalid': messageError }">
    <label v-if="label" :for="name">{{ label }}<span v-if="required">*</span></label>

    <input v-model="dateData" class="hidden" />

    <div class="input-date-container">
      <flat-pickr :config="flatpickerOptions" :value="dateFormat" @input="handleUpdateInput" :placeholder="placeholder" :tabindex="tabIndex">
      </flat-pickr>
      <div class="icon-flat-picker-container">
        <div class="icon-flat-picker"></div>
      </div>
    </div>


  </div>
</template>
<script>
import "flatpickr/dist/flatpickr.css";
import { filterDateFormat } from '@/helpers/filter';
import { DATE, DATA_DATE_SEND } from '@/constant/date';
import flatPickr from "vue-flatpickr-component";
import "flatpickr/dist/l10n/vn";
import "flatpickr/dist/flatpickr.css";
import moment from "moment";
export default {
  name: "MFlatPickerWithValidation",
  components: { flatPickr },
  props: {
    modelValue: {
      type: [String, Number],
      default: "",
    },
    id: {
      type: String,
      default: "",
    },
    name: {
      type: String,
      default: "",
    },
    label: {
      type: String,
      default: "",
    },
    required: {
      type: Boolean,
      default: false,
    },
    placeholder: {
      type: String,
      default: "DD/MM/YYYY",
    },
    type: {
      type: String,
      default: "text",
    },
    errors: {
      type: Object,
      default: () => { },
    },
    options: {
      type: Object,
      default: () => { },
    },
    tabIndex: {
      type: String,
      default: "2"
    }
  },
  data() {
    return {
      dateData: "",
      dateFormat: ""
    }
  },
  computed: {
    /**
      * messageError: Hiển thị lỗi đúng trường input
     * @author: TMThuc (31/05/2023)
     */
    messageError() {
      if (this.errors && this.errors[this.name]) {
        return this.errors[this.name];
      }
      return null;
    },
    /**
      * flatpickerOptions: kế thường các option từ component cha truyền vào và dịch thời gian sang tiêng việt, và định dữ liệu kiẻu d/m//y
     * @author: TMThuc (31/05/2023)
     */
    flatpickerOptions() {
      return {
        ...this.options,
        locale: "vn", // Sử dụng ngôn ngữ tiếng Việt
        dateFormat: "d/m/Y",
        maxDate: Date.now(),
        allowInput: true
      };
    },
  },
  created() {

    if (this.modelValue) {

      this.dateFormat = filterDateFormat(this.modelValue, DATE);

      this.dateData = filterDateFormat(this.modelValue, DATA_DATE_SEND);
    }
  },
  methods: {
    /**
     * updateInput: emit dữ liệu lên component cha.
    * @author: TMThuc (31/05/2023)
    */
    handleUpdateInput(event) {
      const value = event.target.value; 
      if (value) {
        this.dateData = moment(value, 'D/M/YYYY').format("YYYY-MM-DD");
        this.dateFormat = value;
      }
      else {
        this.dateData = null;
      }

      this.$emit("update:modelValue", this.dateData);
    },
  },
};
</script>

<style>
@import url("./flatPickerWithValidation.css");
</style>
