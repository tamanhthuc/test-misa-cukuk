<template>
  <m-popup ref="popupRef" :icon="icon">
    <template #content>
      {{ content }}
    </template>
    <template #actions>
      <div class="popup__action__left">
        <m-button class="btn" @click="handleClose">Huỷ</m-button>
      </div>

      <div class="popup__action__right">
        <m-button class="btn" @click="handleCancel">Không</m-button>
        <m-button class="btn--primary ml-2 btn" @click="handleSave">Có</m-button>
      </div>
    </template>
  </m-popup>
</template>
<script>
import MButton from "../base/button/MButton.vue";
import MPopup from "../base/popup/MPopup.vue";

export default {
  name: "PopupConfirmSaveData",
  components: {
    MButton,
    MPopup,
  },
  props: {
    icon: {
      type: String,
      default: "icon-question-xl",
    },
    content: {
      type: String,
      default: "Dữ liệu thay đổi bạn có muốn cất không ?",
    },
  },
  data() {
    return {
      isShow: true,
      itemDelete: null,
    };
  },
  methods: {
    /**
     * show: hiện thị popup
     * @author: TMThuc (28/05/2023)
     */
    show() {
      this.$refs.popupRef.show();
    },
    /**
     * hidden: ẩn popup đi
     * @author: TMThuc (28/05/2023)
     */
    hidden() {
      this.$refs.popupRef.hidden();
    },
    /**
     * handleClose: Xử lí action đóng form
     * @author: TMThuc (28/05/2023)
     */
    handleClose() {
        this.hidden();
        this.$emit("close-form");
    },
    /**
     * handleCancel: xử lí huỷ form
     * @author: TMThuc (28/05/2023)
     */
    handleCancel(){
        this.hidden();
        this.$emit("cancel-form");
    },
    /**
     * handleSave: xử lí lưu dữ liệu form
     * @author: TMThuc (28/05/2023)
     */
    handleSave() {
        this.hidden();
        this.$emit("save-form");
    }
  },
};
</script>
<style></style>
