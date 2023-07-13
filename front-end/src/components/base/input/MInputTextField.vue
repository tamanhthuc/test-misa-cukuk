<template>
  <div class="input__container">
    <label v-if="label" :for="name">{{ label }}<span v-if="required">*</span></label>
    <input
      :id="name"
      :name="name"
      :type="type"
      :placeholder="placeholder"
      :value="modelValue"
      @input="handleUpdateInput"
      ref="inputRef"
    />
  </div>
</template>

<script>
export default {
  name: "MInputTextField",
  emits: ["update:modelValue"],
  components: {},
  props: {
    modelValue: {
      type: [String, Number],
      default: "",
    },
    id: {
      type: String,
      default: "",
    },
    name: {
      type: String,
      default: ""
    },
    label: {
      type: String,
      default: "",
    },
    required: {
      type: Boolean,
      default: false,
    },
    placeholder: {
      type: String,
      default: "",
    },
    type: {
      type: String,
      default: "text",
    },
  },
  methods: {
    /** 
     * Author: TMThuc (28/05/2023)
     * uppdateInput: bind dữ liệu lên component cha
     * @param {} event 
     */
    handleUpdateInput(event) {
      this.$emit("update:modelValue", event.target.value);
    },
    focus() {
      // this.$refs.inputRef.focus()
       console.log(this.$refs.inputRef)
    }
  }
};
</script>

<style>
@import "./input.css";
</style>
