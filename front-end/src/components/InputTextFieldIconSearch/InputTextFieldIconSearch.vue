<template>
    <div class="input__container search mb-0">
        <input v-bind="$attrs"
        :value="modelValue" @input="handleInput" />
        <div class="icon-search input-icon-search icon"></div>  
    </div>
</template>

<script>
import { debounce } from 'lodash'; 
export default {
    name: "InputTextFieldIconSearch",
    components: {  },
    props: {
        modelValue: {
            type: String,
            default: ""
        }
    },
    methods: {
        handleInput: debounce(function (e) {
        this.$emit("update:modelValue", e.target.value);
    }, 500),

    }
}
</script>

<style scoped >
@import "./inputTextFieldIconSearch.css";
</style>
