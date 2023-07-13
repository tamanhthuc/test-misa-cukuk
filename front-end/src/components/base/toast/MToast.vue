<template>
  <div class="toast" v-show="isShow"  :class="status">
    <div class="toast__container">
      <div class="toast__notice">
        <div class="mr-2" :class="icon"></div>
        <div class="toast__notice__text">
          <span class="toast__notice__text--strong">{{ title }}</span>
          {{ content }}
        </div>
      </div>
      <div class="icon icon-close" @click="hidden"></div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MToast",
  props: {
    duration: {
        type: Number,
        default: 5000
    }
  },
  data() {
    return {
      isShow: false,
      icon: "",
      title: "",
      content: "",
      status: ""
    };
  },
  methods: {
    show(item) {
        this.isShow = true;
      if (item) {
        this.icon = item.icon;
        this.title = item.title;
        this.content = item.content;
        this.status = item.status;
        setTimeout(() => {
          this.isShow = false;
        }, this.duration);
      }
     
    },
    stopTimer() {
      clearTimeout(this.duration); // Hủy bỏ setTimeout
    },
    hidden() {
      this.isShow = false;
    },
  },
  beforeUnmount() {
    this.stopTimer();
  },
};
</script>
<style scoped>
@import url("./toast.css");
</style>
