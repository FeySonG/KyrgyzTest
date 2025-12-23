import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import HomePage from "@/pages/HomePage.vue";
import LoginPage from "@/pages/LoginPage.vue";
import RegisterPage from "@/pages/RegisterForm.vue";
import { useAuthStore } from "@/store/auth";

const routes: Array<RouteRecordRaw> = [
    { path: "/", redirect: "/login" },
    { path: "/login", component: LoginPage },
    { path: "/register", component: RegisterPage },
    { path: "/home", component: HomePage, meta: { requiresAuth: true } },
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

// 🔹 Guard проверяет store, store уже синхронизирован в main.ts
router.beforeEach((to, from, next) => {
    const authStore = useAuthStore();

    if (to.meta.requiresAuth && !authStore.user) {
        next({ path: "/login", query: { redirect: to.fullPath } });
    } else {
        next();
    }
});

export default router;