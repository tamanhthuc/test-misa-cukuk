<template>
    <m-popup ref="popupRef" :icon="icon">
      <template #content>
        {{ content }}
      </template>
      <template #actions>
        <div class="popup__action__left">
          <m-button class="btn" @click="handleCancel">{{ $_MResource.BUTTON.NO }}</m-button>
        </div>
  
        <div class="popup__action__right">
          <m-button class="btn--primary ml-2 btn" @click="handleSave">{{ $_MResource.BUTTON.YES }}</m-button>
        </div>
      </template>
    </m-popup>
  </template>
  <script>
  import MButton from "../base/button/MButton.vue";
  import MPopup from "../base/popup/MPopup.vue";
  
  export default {
    name: "PopupDeleteMassData",
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
        default: "Bạn có thực sự muốn xoá nhân viên đã chọn không ?",
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
       * @author TMThuc (28/05/2023)
       */
      show() {
        this.$refs.popupRef.show();
      },
      /**
       * hidden: ẩn popup đi
       * @author TMThuc (28/05/2023)
       */
      hidden() {
        this.$refs.popupRef.hidden();
      },
      /**
       * handleCancel: Xử lí huỷ xoá hàng loaatj
       * @author tmthuc (01/07/2023)
       */
      handleCancel(){
          this.hidden();
          this.$emit("cancel-form");
      },
      /**
       * handleSave: Xử lí xoá hàng loạt
       * @author tmthuc (01/07/2023)
       */
      handleSave() {
          this.$emit("confirm-delete-mass");
      }
    },
  };
  </script>
  <style></style>
  