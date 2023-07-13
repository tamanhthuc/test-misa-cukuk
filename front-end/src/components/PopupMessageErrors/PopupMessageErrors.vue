<template>
  <m-popup :icon="icon" ref="popupRef">
    <template #content>
      <ul class="list-error">
        <li v-for="(error, index) in errors" :key="index"> {{ error }}</li>
      </ul>
      
    </template>
    <template #actions>
      <div class="popup__action__button">
        <button class="btn btn--primary" @click="hidden">{{ $_MResource.BUTTON.AGREE }}</button>
      </div>
    </template>
  </m-popup>
</template>
<script>
import MPopup from "../base/popup/MPopup.vue";
export default {
  name: "PopupMessageErrors",
  components: {
    MPopup,
  },
  props: {
    icon: {
      type: String,
      default: "icon-warning",
    },
    errors: {
      type: String,
      default: "",
    },
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
  },
};
</script>
<style scoped>
@import url("./popupMessageErrors.css");
</style>
