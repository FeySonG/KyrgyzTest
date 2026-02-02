import { defineStore } from "pinia";
import { useAlertStore } from "@/store/alertStore";
import {
    changeUserLogin,
    changeUserName,
    changeUserPassword,
    changeUserRole,
    deleteUser,
    getListUsers,
} from "@/api/userApi";

import type {
    ChangeUserLoginDto,
    ChangeUserNameDto,
    ChangeUserPasswordDto,
    UserDto,
} from "@/types/userTypes/userType";
import type { UserRole } from "@/types/userTypes/userRole";
import {ref} from "vue";

export const useUserStore = defineStore("userStore", () => {
    const alert = useAlertStore();

    const users = ref<UserDto[]>([]);
    const loading = ref(false);

    // -------------------------
    // Получение пользователей
    // -------------------------
    async function getUsers() {
        loading.value = true;
        try {
            users.value = await getListUsers();
        } catch (e: any) {
            alert.error(e.response?.data || "Ошибка загрузки пользователей");
        } finally {
            loading.value = false;
        }
    }

    // -------------------------
    // Удаление пользователя
    // -------------------------
    async function removeUser(userId: number) {
        loading.value = true;
        try {
            await deleteUser(userId);
            users.value = users.value.filter(u => u.id !== userId);
            alert.success("Пользователь удалён");
        } catch (e: any) {
            alert.error(e.response?.data || "Ошибка при удалении пользователя");
            throw e;
        } finally {
            loading.value = false;
        }
    }

    async function changeRole(payload: { userId: number; role: UserRole }) {
        try {
            await changeUserRole(payload);
            alert.success("Роль пользователя обновлена");
        } catch (e: any) {
            alert.error(e.response?.data || "Ошибка смены роли");
            throw e;
        }
    }

    async function updateName(payload: ChangeUserNameDto) {
        try {
            await changeUserName(payload);
            alert.success("Данные пользователя обновлены");
        } catch (e: any) {
            alert.error(e.response?.data || "Ошибка редактирования данных");
            throw e;
        }
    }

    async function updatePassword(payload: ChangeUserPasswordDto) {
        try {
            await changeUserPassword(payload);
            alert.success("Пароль успешно изменён");
        } catch (e: any) {
            alert.error(e.response?.data || "Ошибка смены пароля");
            throw e;
        }
    }

    async function updateLogin(payload: ChangeUserLoginDto) {
        try {
            await changeUserLogin(payload);
            alert.success("Логин успешно изменён");
        } catch (e: any) {
            alert.error(e.response?.data || "Ошибка смены логина");
            throw e;
        }
    }

    return {
        users,
        loading,
        getUsers,
        removeUser,
        changeRole,
        updateName,
        updatePassword,
        updateLogin,
    };
});
