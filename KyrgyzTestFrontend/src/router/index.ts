import {createRouter, createWebHistory, RouteRecordRaw} from "vue-router";

import HomePage from "@/pages/HomePage.vue";
import LoginPage from "@/pages/LoginPage.vue";
import RegisterPage from "@/pages/RegisterForm.vue";
import AdminPage from "@/pages/AdminPage.vue";
import AdminUsersPage from "@/pages/AdminPages/AdminUsersPage.vue";
import UserProfilePage from "@/pages/UserProfilePage.vue";
import ArchivePage from "@/pages/ArchivePage.vue";

import {useAuthStore} from "@/store/auth";
import ArchiveSearchPage from "@/pages/ArchivePages/ArchiveSearchPage.vue";
import TestResultPage from "@/pages/ArchivePages/TestResultPage.vue";

const routes: Array<RouteRecordRaw> = [
    {
        path: "/login",
        component: LoginPage,
    },
    {
        path: "/register",
        component: RegisterPage,
    },
    {
        path: "/home",
        component: HomePage,
        meta: {requiresAuth: true},
    },
    {
        path: "/archive",
        component: ArchivePage,
        meta: {requiresAuth: true},
        children: [
            {
                path: "search",
                component: ArchiveSearchPage,
            },
            {
                path: "student/:id",   // 👈 ВОТ ЭТО ДОБАВЬ
                name: "StudentResults",
                component: TestResultPage,
            },
        ],
    },
    {
        path: "/profile",
        component: UserProfilePage,
        meta: {requiresAuth: true},
    },
    {
        path: "/admin",
        component: AdminPage,
        meta: {requiresAuth: true, requiresAdmin: true},
        children: [
            {
                path: "users",
                component: AdminUsersPage,
            },
        ],
    },
    {
        path: "/",
        redirect: "/home",
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

//  Глобальный guard
router.beforeEach((to, from, next) => {
    const authStore = useAuthStore();

    if (to.meta.requiresAuth && !authStore.user) {
        next("/login");
        return;
    }

    if (to.meta.requiresAdmin && authStore.user?.role !== "Admin") {
        next("/home");
        return;
    }

    next();
});

export default router;
