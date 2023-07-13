<template>
  <div
    class="col_action_table"
    @click="handleShowDrop"
    ref="dropdown"
    v-click-outside="handleClickOutside"
    v-bind="$attrs"
  >
    <div class="icon__container">
      <div class="icon__dropdown__round"></div>
    </div>
  </div>
  <!-- <drop-down-action-table v-show="isShowDrop" /> -->
  <div class="dropdown__col__action__container" v-show="isShowDrop">
    <div class="dropdown__col__action">
      <div
        class="dropdown__col__action__container__text"
        v-for="action in actions"
        :key="action.name"
        @click.stop.prevent="handleClickItem(action, item)"
        @dblclick.stop=""
      >
        {{ action.name }}
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MenuActionTable",
  emits: ["edit-item", "delete", "stop-used", "clone-item"],
  props: {
    modelValue: {
      type: String,
      default: "",
    },
    item: {
      type: Object,
      default: () => {},
    },
    actions: {
      type: Array,
      default: () => [],
    },
  },
  components: {},
  data() {
    return {
      isShowDrop: false,
    };
  },
  mounted() {},
  methods: {
    /**
     * handleClickOutside: ẩn menu khi click ra ngoài
     *  @author: TMThuc (28/05/2023)
     */
    handleClickOutside() {
      this.isShowDrop = false;
    },
    /**
     * handleShowDrop: ẩn hiện menu
     * @author: TMThuc (28/05/2023)
     */
    handleShowDrop() {
      this.isShowDrop = !this.isShowDrop;
    },
    /**
     * handleShowDrop: ẩn hiện menu
     * @author: TMThuc (28/05/2023)
     */
    selectItem(item) {
      this.$emit(item.value, this.item);
    },
    /**
     * @author: TMThuc (28/05/2023)
     *
     */
    handleClickItem(action, item) {
      // 1. emit lên component cha giá giá trị của acitin và item được chọn
      this.$emit(action.value, item);
      // 2. Ẩn menu đi
      this.isShowDrop = false;
    },
  },
};
</script>
<style scoped>
@import url("./MenuActionTable.css");
</style>
