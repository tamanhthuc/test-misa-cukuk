<template>
  <div class="panigation">
    <div class="panigation__quantity">
      {{ $_MResource.PAGINATION.SUMMARY }}:
      <span class="number">
        <span class="txt-strong">{{ $filters.formatSpaceNumber(totalRecord) }}</span>
        {{ $_MResource.PAGINATION.RECORD }}</span
      >
    </div>
    <div class="panigation__page">
      <div class="panigation__page__num" v-click-outside="handleClickOutside">
        <div class="panigation__page__num__wrap" @click="handleMenuPage">
          <div class="num__text">
            {{ $_MResource.PAGINATION.PAGE_NUMBER_TEXT }}:
            <span> {{ selectedItemPerPage }}</span>
          </div>
          <div class="panigation__page__icon center__icon icon">
            <div class="icon__chevron__down"></div>
          </div>
        </div>
        <div class="pagination-menu" v-show="isShowMenuPage">
          <div class="pagination-menu__container" ref="dropdown">
            <div
              class="pagination-menu__item"
              v-for="(page, index) in itemPerPages"
              :key="index"
              @click="handleSelectedMenuPage(page)"
              @dblclick.stop="handleSelectedMenuPage(page)"
            >
              <div
                class="pagination-menu__item__text"
                :class="{ active: selectedItemPerPage == page }"
              >
                {{ page }}
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="num__page">
        <span class="txt-strong">{{ $filters.formatSpaceNumber(currentPage) }}</span>
         -
        <span class="txt-strong">{{ $filters.formatSpaceNumber(totalPage) }}</span> {{ $_MResource.PAGINATION.PAGE_TEXT }} 
      </div>
      <div class="panigation__icon">
        <div
          class="panigation__icon__left center__icon icon"
          :class="[isFirstPage ? 'not-allowed' : '']"
          @click="onClickPreviousPage"
        >
          <div
            :class="[
              isFirstPage ? 'icon-chevron-left--disable' : 'icon-chevron-left',
            ]"
          ></div>
        </div>
        <div
          class="panigation__icon__right center__icon icon"
          :class="[isLastPage ? 'not-allowed' : '']"
          @click="onClickNextPage"
        >
          <div
            :class="[
              isLastPage ? 'icon-chevron-right--disable' : 'icon-chevron-right',
            ]"
          ></div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MPagination",
  props: {
    totalRecord: {
      type: Number,
      required: true,
    },
    totalPage: {
      type: Number,
      default: 1,
    },
    itemPerPages: {
      type: Array,
      default: () => [10, 20, 30, 40],
    },
    page: {
      type: Number,
      default: 1,
    },
  },
  created() {
    this.currentPage = this.page;
  },
  watch: {
    page(val) {
      this.currentPage = val;
    },
  },
  data() {
    return {
      selectedItemPerPage: 10,
      isShowMenuPage: false,
      currentPage: 1,
    };
  },
  mounted() {
    // document.addEventListener('click', this.handleOutsideClick);
  },
  computed: {
    /**
     * @author: TMThuc (28/05/2023)
     * totalPages: trả về  tổng số page
     */
    totalPages() {
      return Math.ceil(this.totalRecord / this.selectedItemPerPage);
    },
    /**
     * isFirstPage:kiểm tra có phải trang page hiện tại có phải trang đầu không
     * @author: TMThuc (28/05/2023)
     */
    isFirstPage() {
      return this.currentPage === 1;
    },
    /**
     *isLastPage : kiểm tra có phải trang page hiện tại có phải trang cuối không
     * @author: TMThuc (28/05/2023)
     */
    isLastPage() {
      return this.currentPage === this.totalPage;
    },
  },
  methods: {
    /**
     *  handleMenuPage: thực hiện đóng mở menu pagination
     * @author: TMThuc (28/05/2023)
     */
    handleMenuPage() {
      this.isShowMenuPage = !this.isShowMenuPage;
    },
    handleClickOutside() {
      this.isShowMenuPage = false;
    },
    /**
     *
     * @author: TMThuc (28/05/2023)
     *
     */
    handleSelectedMenuPage(item) {
      // 1. Chọn số lượng item muốn hiển thị trên table
      this.selectedItemPerPage = item;
      this.currentPage = 1;
      console.log("currentPage", this.currentPage);
      this.$emit("select-item-page", item, this.currentPage);
      // 2. Chọn xong đóng menu lại
      this.isShowMenuPage = false;
    },
    /**
     * handleOutsideClick: xử lí xự kiện click outside ra ngoài menu
     * @author: TMThuc (28/05/2023)
     */
    handleOutsideClick(event) {
      const dropdown = this.$refs.dropdown;

      if (dropdown && !dropdown.contains(event.target)) {
        this.isShowMenuPage = false;
      }
    },
    /**
     * onClickPreviousPage: xử lí click lùi page hiện tại đi 1
     * @author: TMThuc (28/05/2023)
     */
    onClickPreviousPage() {
      if (this.currentPage > 1) {
        this.currentPage -= 1;
        // 1. truyền dữ page hiên tại lên component cha
        this.$emit("pagechanged", this.currentPage);
      }
    },
    /**
     * onClickPreviousPage: xử lí click tiến page hiện tại lên 1
     * @author: TMThuc (28/05/2023)
     */
    onClickNextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage += 1;
        // 1. truyền dữ page hiên tại lên component cha
        this.$emit("pagechanged", this.currentPage);
      }
    },
  },
};
</script>

<style scoped>
@import "./pagination.css";
</style>
