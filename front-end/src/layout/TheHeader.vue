<template>
    <div class="header">
        <div class="header__left">
            <div class=""></div>
            <div class="icon__nav icon"></div>
            <div class="header__left__text mr-1">
                <p>{{ $_MResource.PAGE.EMPLOYEE.NAVBAR_TEXT }}</p>
            </div>
            <div class="icon-dropdown icon"></div>
        </div>

        <div class="header__right">
            <m-icon icon="icon-notify" tooltip="Thông báo" />
            <m-icon icon="icon-avatar" c-class="mx-6"></m-icon>
            <div class="header__right__name mr-1">Ta Manh Thuc</div>
            <m-icon icon="icon-dropdown" ></m-icon>

           
        </div>
    </div>
</template>
<script>
import MIcon from '@/components/base/Icon/MIcon.vue';
export default {
    name: "TheHeader",
    components: {
        MIcon
    }

}
</script>
<style scoped>

@import "@/styles/layout/header.css";
</style>


