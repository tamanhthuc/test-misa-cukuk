<template>
  <m-popup ref="popupRef" :icon="icon">
    <template #content>
      {{ content }}
    </template>
    <template #actions>
      <div class="popup__action__left">
        <button class="btn" @click="hidden">{{ $_MResource.BUTTON.NO }}</button>
      </div>

      <div class="popup__action__right">
        <button class="btn--primary ml-2 btn" @click="handleDelete">{{ $_MResource.BUTTON.YES }}</button>
      </div>
    </template>
  </m-popup>
</template>
<script>
import MPopup from "../base/popup/MPopup.vue";
export default {
  name: "PopupEmployeeDelete",
  props: {
    icon: {
      type: String,
      default: "icon-warning",
    },
    content: {
      type: String,
      default: "",
    },
  },
  components: {
    MPopup,
  },
  data() {
    return {
      isShow: false,
      itemDelete: null,
    };
  },
  methods: {
    /**
     * show: hiện thị popup
     * @author tmthuc (28/05/2023)
     */
    show(item) {
      this.itemDelete = item;
      this.$refs.popupRef.show();
    },
    /**
     * hidden: ẩn popup đi
     * @author tmthuc (28/05/2023)
     */
    hidden() {
      this.$refs.popupRef.hidden();
    },
    /**
     * handleDelete: Hàm xử lí emit item cần xoá lên compoent cha
     * @author tmthuc (10/08/2023)
     */
    handleDelete() {
      this.$emit("delete-item", this.itemDelete);
    },
  },
};
</script>
<style></style>
