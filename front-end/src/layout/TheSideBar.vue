<template>
    <div class="sidebar">
        <div class="sidebar__container">
            <div class="menu">
                <router-link :to="item.path" class="menu__item" v-for="(item, index) in sidebars" :key="index">
                    <div class="px-3">
                        <div :class="item.icon"></div>
                    </div>
                    <div class="menu__item__text">{{ item.name }}</div>
                </router-link>
            </div>
        </div>
    </div>
</template>
<script>
import {sidebars} from "@/constant/sidebars";
export default {
    name: "TheSideBar",
    data() {
        return {
            sidebars: sidebars
        }
    }
}
</script>
<style scoped>
@import "@/styles/layout/sidebar.css";


</style>
