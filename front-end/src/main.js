import { createApp } from 'vue';
import App from './App.vue';
import { clickOutsideDirective } from './customDirective/clickOutsideDirective';
import router from './router';
import { TOAST } from './resource/toast/toast';
import MEnum from './script/enum';
import MResource from './script/resource';
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'
import Popper from "vue3-popper";
import { filterDate, filterGender, formatSpaceNumber } from './helpers/filter';
const app = createApp(App);

app.component('MPopper', Popper);
app.component('VueDatePicker', VueDatePicker);
app.directive('click-outside', clickOutsideDirective );

app.config.globalProperties.$_MEnum = MEnum;
app.config.globalProperties.$_Toast = TOAST;
app.config.globalProperties.$_MResource = MResource;



app.config.globalProperties.$filters = {
    date: filterDate,
    gender: filterGender,
    formatSpaceNumber: formatSpaceNumber
};
router.beforeEach((to, from, next) => {
  // Thiết lập tiêu đề dựa trên thuộc tính "meta.title" trong route
  document.title = to.meta.title || 'Tiêu đề mặc định'; 
  next();
});
 
app.use(router);
app.mount('#app'); 
