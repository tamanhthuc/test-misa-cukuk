<template>
  <div class="layout">
    <div class="layout__sidebar">
      <the-header-logo />
      <the-side-bar />
    </div>
    <div class="layout__appbar">
      <the-header />
      <the-main />
    </div>
  </div>
</template>

<script>
import TheHeaderLogo from '@/layout/TheHeaderLogo.vue';
import TheSideBar from "@/layout/TheSideBar.vue";
import TheHeader from "@/layout/TheHeader.vue";
import TheMain from "@/layout/TheMain.vue";
export default {
  name: "App",
  components: {
    TheHeaderLogo,
      TheSideBar,
      TheHeader,
      TheMain
  }
};
</script>

<style>
@import "../src/styles/global.css";
</style>

