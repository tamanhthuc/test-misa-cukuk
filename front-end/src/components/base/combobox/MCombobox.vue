<template>
  <div class="dropdown" @click="toggleDropdown">
    <div class="dropdown-label">{{ label }} <span v-if="required">*</span></div>
    <div class="dropdown-toggle">
      <div class="select-input">
        <Field
          :id="name"
          :name="name"
          :type="type"
          v-model="textSelected"
          @input="handleInput"
          v-slot="{ field, errors }"
          validateOnInput
        >
          <input
            @keyup.down="handleKeyupDown"
            @keyup.enter="handleKeyupEnter"
            @keyup.up="handleKeyup"
            v-bind="field"
            ref="inputRef"
            :class="{ 'input-invalid': errors.length > 0 }"
            autocomplete="off"
            :tabindex="tabIndex"
          />
        </Field>
      </div>
      <div class="select-option-icon">
        <div class="icon-select"></div>
      </div>
      <ul v-show="isOpen" class="dropdown-menu">
        <li
          v-for="(option, index) in dataItemFilters"
          :key="index"
          @click.stop="handleSelectOption(option)"
          :class="{ 'dropdown-menu--selected': index == indexSelected }"
        >
          {{ option[itemText] }}
        </li>
      </ul>
    </div>
    <ErrorMessage :name="name" class="error-message" />
  </div>
</template>

<script>
import { Field, ErrorMessage } from "vee-validate";

export default {
  name: "MCombobox",
  components: { Field, ErrorMessage },
  props: {
    modelValue: {
      type: [String, Number],
      default: "",
    },
    type: {
      type: String,
      defaule: "text",
    },
    items: {
      type: Array,
      default: () => [],
    },
    label: {
      type: String,
      default: "",
    },
    required: {
      type: Boolean,
      default: false,
    },
    name: {
      type: String,
      default: "",
    },
    itemText: {
      type: String,
      default: "",
    },
    itemValue: {
      type: String,
      default: "",
    },
    valueSelected: {
      type: String,
      default: "",
    },
    tabIndex: {
      type: String,
      default: "2"
    }
  },
  created() {
    // 1. Gán giá trị hiển thị vào thanh textSearch
    this.textSelected = this.valueSelected;
    // 2. Kiểm tra danh dách items truyền vào chưa
    if (this.items.length > 0) {
      // 3. Gán dữ liệu truyền vào cho dataItems
      this.dataItems = this.items;
      // 4. Gán dữ liệu truyền vào cho dataItemFilters
      this.dataItemFilters = this.items;
    }
  },
  data() {
    return {
      isOpen: false,
      textSelected: "",
      dataItems: [],
      dataItemFilters: [],
      indexSelected: 0,
      itemSeleted: {},
    };
  },

  methods: {
    /**
     * handleKeyup: xử lí nút bấm lên để chọn item
     * @author tmthuc (16/06/2023)
     */
    handleKeyup() {
      if (this.indexSelected > 0) {
        this.indexSelected--;
      }
    },
    /**
     * handleKeyupEnter: Xử lí enter để chọn item
     *  @author tmthuc (16/06/2023)
     */
    handleKeyupEnter() {
      this.itemSeleted = this.dataItemFilters[this.indexSelected];
      this.textSelected = this.itemSeleted[this.itemText];
      this.isOpen = false;

      this.$emit("updateCombobox", this.itemSeleted[this.itemValue]);
    },
    /**
     * handleKeyupDown: Xử lí nút ấn xuống chọn item
     */
    handleKeyupDown() {
      const maxLength = this.dataItemFilters.length;

      if (this.indexSelected < maxLength - 1) {
        this.indexSelected++;
      }
    },
    /**
     * handleInput: Xử lí search combobox
     * @param {*} e 
     * @author tmthuc (16/06/2023)
     */
    handleInput(e) {
      // 1. Lấy value của input
      let value = e.target.value;
      this.isOpen = true;
      // 2. filter dataItemFilters theo giá trị searcg
      this.dataItemFilters = this.dataItems.filter((item) =>
        item[this.itemText].toLowerCase().includes(value)
      );
      // 3. Gán giá trị search được chọn
      this.textSelected = value;
    },
    /**
     * toggleDropdown: ẩn hiện dropdown
     * @author: TMThuc (28/05/2023)
     */
    toggleDropdown() {
      this.isOpen = !this.isOpen;
    },
    /**
     * handleSelectOption: chọn item trong danh sách
     * @author: TMThuc (28/05/2023)
     */
    handleSelectOption(option) {
      // 1. Gán giá trị text search khi item được họn
      this.textSelected = option[this.itemText];
      // 2. Ẩn menu đi
      this.isOpen = false;
      // 3. Emit lên component cha và truyền giá trị
      this.$emit("updateCombobox", option[this.itemValue]);
    },
  }
};
</script>

<style scoped>
@import url("./combobox.css");
</style>
