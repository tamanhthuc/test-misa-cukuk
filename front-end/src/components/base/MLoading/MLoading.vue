<template>
    <div class="loading" v-if="isLoading">
        <embed type="image/svg+xml" src="loading.svg" />
    </div>
</template>

<script>
export default {
    name: "MLoading",
    props: {
        isLoading: {
            type: Boolean,
            default: false
        }
    }
}
</script>

<style scoped>
@import url('./loading.css');
</style>