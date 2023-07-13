<template>
  <div class="popup" v-if="isShow">
    <div class="popup__container">
      <div class="popup__main">
        <div class="popup__header">
          <div class="popup__header__title"></div>
          <div class="popup__header__icon"></div>
        </div>
        <div class="popup__content">
          <div class="mr-4" :class="icon"></div>
          <div class="popup__content__content">
            <slot name="content"></slot>
          </div>
        </div>
      </div>


      <div class="popup__action">
        <slot name="actions"></slot>
      
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MPopup",
  props: {
    icon: {
        type: String,
        default: ""
    },

  },
  data() {
    return {
        isShow: false
    }
  },
  conmputed: {
   
  },
  methods: {
     /**
      *  show: hiển thị popup
     * @author: TMThuc (28/05/2023)
     */
    show() {
        this.isShow = true;
    },
     /**
      *  hidden: đóng popup lại
     * @author: TMThuc (28/05/2023)
     */
    hidden() {
        this.isShow = false;
    },
  }
};
</script>
<style scoped>
    @import url("./popup.css");
</style>
