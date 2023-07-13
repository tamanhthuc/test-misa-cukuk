<template>
  <div class="logo">
      <m-icon icon="icon-config" />
    <div class="logo__web">
        <m-icon icon="icon-logo" />
    </div>
    <div class="logo__text">{{ $_MResource.LOGO.TEXT }}</div>
  </div>
</template>
<script>
import MIcon from "@/components/base/Icon/MIcon.vue";

export default {
  name: "TheHeaderLogo",
    components: {MIcon}
};
</script>
<style scoped>
@import "@/styles/layout/header-logo.css";


</style>
