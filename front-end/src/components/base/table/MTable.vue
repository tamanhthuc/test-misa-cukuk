<template>
  <div class="table__container">
    <div class="table__data table-fix-left table-fix-right">
      <table class="custom_table" id="table__employee">
        <thead>
          <tr class="table__header">
            <th
              style="width: 40px; min-width: 40px"
              :style="{ zIndex: zIndex + 2 }"
              type="checkbox"
            >
              <input type="checkbox" v-model="selectAll" @change="handleSelectAll" />
            </th>
            <th
              v-for="(header, index) in headers"
              :key="index"
              :style="{
                width: header.width + 'px',
                minWidth: header.width + 'px',
                zIndex: zIndex + 1,
              }"
              :class="header.class"
            >
              <m-popper v-if="header.tooltip" hover arrow placement="top">
                {{ header.text }}

                <template #content> {{ header.tooltip }}</template>
              </m-popper>
              <span v-else> {{ header.text }}</span>
            </th>
          </tr>
        </thead>
       
        <div v-show="isLoading" class="table-loading">
          <embed type="image/svg+xml" src="loading.svg" />
        </div>
        <tbody v-show="isLoading == false">
          <tr
            v-for="(item, index) in items"
            :key="item.EmployeeId"
            @dblclick="handleEditItem(item)"
          >
            <td>
              <input
                type="checkbox"
                v-model="computedCheckboxes"
                :value="item[nameId]"
                @dblclick.stop=""
              />
            </td>

            <td v-for="(cell, index) in tableCells" :key="index">
              <div
                :style="{
                  width: cell.width + 'px',
                  maxWidth: cell.width,
                  textAlign: cell.align,
                }"
                class="cell-table"
              >
                <slot :name="cell.value" :item="item">
                  <m-popper hover arrow placement="top" v-if="isShowITooltip(item, cell)">
                    <div class="cell-table">{{ item[cell.value] }}</div>
                    <template #content> {{ item[cell.value] }}</template>
                  </m-popper>

                  <span v-else >{{ item[cell.value] }}</span>
                  
                </slot>
              </div>
            </td>

            <td :style="{ 'z-index': zIndex - index }">
              <div class="col__action__container" @dblclick.stop="">
                <div
                  class="col__action__text mr-1"
                  @click="() => handleEditItem(item)"
                >
                  {{ $_MResource.EDIT }}
                </div>
                <menu-action-table
                  :actions="actions"
                  :item="item"
                  @delete="handleDelete"
                  :class="{ active: isActive(index) }"
                  @click="setActiveMenu(index)"
                  @clone-item="handleClone"
                />
              </div>
            </td>
          </tr>
        </tbody>
        <div v-if="items.length == 0 && isLoading == false">
          <div class="table-not-data">{{ $_MResource.NO_DATA }}</div>
        </div>
      </table>
    </div>
  </div>
</template>

<script>
import MenuActionTable from "@/components/MenuActionTable/MenuActionTable.vue";
import {  union, difference, every, includes } from "lodash";

export default {
  name: "MTable",
  components: { MenuActionTable },
  emits: ["edit-item", "delete", "stop-used", "update:checkboxs", "clone-item"],
  props: {
    headers: {
      type: Array,
      default: () => [],
    },
    showSelect: {
      type: Boolean,
      default: false,
    },
    items: {
      type: Array,
      default: () => [],
    },
    modelValue: {
      type: String,
      default: "",
    },
    nameId: {
      type: String,
      default: "",
    },
    isLoading: {
      type: Boolean,
      default: false,
    },
    checkboxs: {
      type: Array,
      default: () => []
    }
  },
  mounted() {
    // this.debouncedHandleInput = _.debounce(this.handleInput, 500);\
    
  },

  data() {
    return {
      activeMenu: "",
      actionSelected: "",
      actions: [
        {
          name: "Nhân bản",
          value: "clone-item",
        },
        {
          name: "Xoá",
          value: "delete",
        },
        {
          name: "Ngừng sử dụng",
          value: "stop-used",
        },
      ],
      fakeItem: { name: "fakeItem" },
      selectAll: false,
      innerValue: "",
      
    };
  },
  computed: {
    /**
     * computedCheckboxes: 
     * @author: tmthuc (01/07/2023)
     */
    computedCheckboxes: {
      get() {
        return this.checkboxs;
      },
      set(value) {
        
        this.$emit('update:checkboxs', value); // Phát ra sự kiện để cập nhật prop trong thành phần cha
      }
    },
    /**
     * zIndex:
     * @author: tmthuc (20/06/2023)
     */
    zIndex() {
      return this.items.length;
    },
    /**
     * tableCells: Lấy các cột của table trừ cột action
     * @author: tmthuc (20/06/2023)
     */
    tableCells() {
      return this.headers.filter((header) => header.value !== "action");
    },
    /**
     * idItems: lấy danh sách các id của data trả về
     * @author: tmthuc (25/06/2023)
     */
    idItems() {
      return this.items.map(item => item[this.nameId])
    }
  },
  methods: {
    /**
     * isShowITooltip: hàm kiểm tra có hiển thị tooltip trong các ô của table không
     * @param {*} item: dữ liệu của ô kiểm tra 
     * @param {*} cell : thông tin cấu hình của ô
     * @author: tmthuc (20/06/2023)
     */
    isShowITooltip(item, cell){
     // 1. Lấy độ rộng của cell
      const cellWidth = cell.width;
    // 2. Lấy nội dung
      const content = item[cell.value];
    // 3. Tạo ra 1 thẻ span  
      const element = document.createElement('span');
      // 4. Gán nội dụng đó vào thẻ span
      element.innerHTML = content;
      // 5. Gán vào body
      document.body.appendChild(element)
     // 6. Lấy kích thước của thẻ span vừa gán vào
      const widthElement = element.offsetWidth;
      // 7. Xoá bỏ thẻ span khổi DOM
      document.body.removeChild(element);
      // 8. Kiểm tra độ rộng của nội dung có vượt quá độ của front-end cấu hình không
      if (widthElement > cellWidth) {
        return true;
      }
      return false;
    },
    /**
     * isActive: kiểm tra menu action có được click khong
     * @param {*} index chỉ số của item trong menu action
     * @author tmthuc (20/06/2023)
     */
    isActive(index) {
      return index === this.activeMenu;
    },
    /**
     * setActiveMenu: Hàm chỉ số cho menu được chọn 
     * @param {*} index 
     * @author tmthuc (20/06/2023)
     */
    setActiveMenu(index) {
      this.activeMenu = index;
    },
    /**
     * handleSelectAll: hàm xử lí chọn tất cả checkbox
     * @author tmthuc (01/07/2023)
     */
    handleSelectAll() {
      // 1. Lấy hết dữ liệu của checkbox cũ đã chọn
      const old_checkboxes = this.computedCheckboxes;
      if (this.selectAll) {
        // 2. Gộp hết cả dữ liệu mới và dữ liệu cũ sau khi click vào chọn hết
        this.computedCheckboxes = union(old_checkboxes, this.idItems)
      }
      else {
        // 3. Loại bỏ hết dữ liệu chọn tất cả checkbox
        this.computedCheckboxes = difference(old_checkboxes, this.idItems)
      }
    },
    /**
     * handleRefreshData: thực hiện hành động emit lên component cha để gọi lại api
     * @author TMThuc (28/05/2023)
     */
    handleRefreshData() {
      this.$emit("refresh-data");
    },

    handleEditItem(item) {
      // 1. emit function edit-item lên compnent cha và truyền item mà muốn edit
      this.$emit("edit-item", item);
    },
    /**
     * @author TMThuc (28/05/2023)
     *
     */
    handleDelete(item) {
      // 1. emit function delete lên compnent cha và truyền item mà muốn xoá
      this.$emit("delete", item);
    },
    handleClone(item) {
      this.$emit("clone-item", item)
    },
    // handleStopUsed(item) {
    //   this.$emit("stop-used", item)
    // }
  },
  watch: {
    /**
     * items: kiểm tra sự thay đổi của item để set lại trạng thái của input checkbox tất cả
     * @author tmthuc (01/07/2023)
     */
    items(newItems) {
      // 1. Kiểm tra tất cả phần tử của computedCheckboxes có nằm trong idItems không
      const isExitsSelectAll = every(this.idItems, item => includes(this.computedCheckboxes, item));
      
      if (isExitsSelectAll && newItems.length > 0) {
        // 2. input checkbox all được tích
        this.selectAll = true;
      }
      else {
        // 3. input checkbox all không được tích
        this.selectAll = false;
      }
    },
    /**
     * checkobxs: kiểm tra sự thay đổi của item để set lại trạng thái của input checkbox tất cả
     * @param {*} val Giá trị mới nhất của checkbox
     * @author tmthuc (01/07/2023)
     */
    checkboxs(val){
      const isExitsSelectAll = every(this.idItems, item => includes(val, item));
      if (isExitsSelectAll) {
        this.selectAll = true;
      }
      else {
        this.selectAll = false;
      }
    }
  }
};
</script>

<style scoped>
@import "./table.css";
</style>
