<template>
  <m-toast ref="toastRef" />
  <popup-message-errors ref="popupEmployeeDeleteRef" :errors="errors" />
</template>

<script>
import MToast from "@/components/base/toast/MToast.vue";
import MResource from "@/script/resource";
import PopupMessageErrors from "@/components/PopupMessageErrors/PopupMessageErrors.vue";
import MEnum from "@/script/enum.js";
export default {
  name: "handleApiErrorMixin",
  components: {
    MToast,
    PopupMessageErrors
  },
  data() {
    return {
        errors: ""
    }
  },
  methods: {
    handleApiError(error) {
      if (error.response) {
        const { status } = error.response;

        if (status === MEnum.STATUS_CODE.HTTP_CODE_404) {
            this.$refs.toastRef.show({
            icon: MResource.TOAST.ICON.ERROR,
            title: MResource.TOAST.TITLE.ERROR,
            content: MResource.TOAST.CONTENT,
            status: MResource.TOAST.STATUS.ERROR,
          });
        } 
        else if ( status === MEnum.STATUS_CODE.HTTP_CODE_400) {
            /// 4. Lây lỗi từ phía backend trả về hiện thi lên popup
            this.errors = { ...error.response.data.Errors };
            // 5. Hiển thị popup lên
            this.$refs.popupEmployeeDeleteRef.show();
        }
        else if (status === MEnum.STATUS_CODE.HTTP_CODE_500) {
            this.$refs.toastRef.show({
            icon: MResource.TOAST.ICON.ERROR,
            title: MResource.TOAST.TITLE.ERROR,
            content: MResource.TOAST.CONTENT,
            status: MResource.TOAST.STATUS.ERROR,
          });
        } else {
          // Xử lý lỗi mặc định
          this.$refs.toastRef.show({
            icon: MResource.TOAST.ICON.ERROR,
            title: MResource.TOAST.TITLE.ERROR,
            content: MResource.TOAST.CONTENT,
            status: MResource.TOAST.STATUS.ERROR,
          });
        }
      } else if (error.request) {
        // Không nhận được phản hồi từ server
        console.log("Request Error:", error.request);
      } else {
        // Xảy ra lỗi khi xử lý request
        console.log("Error:", error.message);
      }
    },
  },
};
</script>
