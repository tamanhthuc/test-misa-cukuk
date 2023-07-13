import { createWebHistory, createRouter } from "vue-router";
import EmployeeList from "@/view/EmployeeList.vue";
const routes = [
    {
        path: "/",
        name: "EmployeeListRouter",
        component: EmployeeList,
        meta: {
            title: 'Nhân viên' 
          }
    },
    
];

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router;