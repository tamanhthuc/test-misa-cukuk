<template>
    <div class="menu-delete" @click="handleClickMenu">
        <div clas="menu-delete-container">
            Thực hiện hàng loạt
            <div class="icon-menu menu-icon"></div>
        </div>
        <div class="menu-list" v-show="isShow" @click="handleDelete" v-if="isShowDelete">{{ $_MResource.BUTTON.DELETE }}</div>
    </div>
</template>

<script>
export default {
    name: "MenuMassDelete",
    props: {
        isShowDelete: {
            type: Boolean,
            default: false
        }
    },
    data(){
        return { 
            isShow: false
        }
    },
    methods: {
        /**
         * handleClickMenu: xử lí toogle menu
         * @author tmthuc (01/07/2023)
         */
        handleClickMenu() {
            this.isShow = !this.isShow
        },
          /**
         * handleDelete: Xứ lí action xoá nhiều 
         * @author tmthuc (01/07/2023)
         */
        handleDelete() {
            this.isShow = false;
            this.$emit("confirm-delete")
        }
    }
}
</script>

<style scoped>
    @import url("./menuMassDelete.css");
</style>



