import { defineStore } from "pinia";
import {
    registerUser,
    loginUser,
    logoutUser,
    fetchCurrentUser,
    type RegisterRequest
} from "../api/authApi";
import { CurrentUser } from "@/types/types";
import { useAlertStore } from "@/store/alertStore";

export const useAuthStore = defineStore("auth", {
    state: () => ({
        user: undefined as CurrentUser | null | undefined, // undefined = ещё не проверяли
        loading: false,
        error: ""
    }),

    getters: {
        isAuth: (state) => state.user !== null && state.user !== undefined,
        isAdmin: (state) => state.user?.role === "Admin"
    },

    actions: {

        async register(payload: RegisterRequest) {
            this.loading = true;
            this.error = "";
            const alert = useAlertStore();

            try {
                await registerUser(payload);
                await this.fetchUserFromServer();
                alert.success("Регистрация прошла успешно");
            } catch (err: any) {
                this.error = err.response?.data || "Ошибка регистрации";
                alert.error(this.error);
            } finally {
                this.loading = false;
            }
        },

        async login(login: string, password: string) {
            this.loading = true;
            this.error = "";
            const alert = useAlertStore();
            try {
                await loginUser({ login, password });
                await this.fetchUserFromServer();
                alert.success("Вы успешно вошли в систему");
            } catch (err: any) {
                this.error = "Неверный логин или пароль";
                alert.error(this.error);
            } finally {
                this.loading = false;
            }
        },

        async logout() {
            const alert = useAlertStore();
            try {
                await logoutUser();
                this.user = null;
                alert.success("Вы вышли из системы");
            } catch (err: any) {
                alert.error(err.response?.data || "Ошибка при выходе");
            }
        },

        async fetchUserFromServer() {
            const alert = useAlertStore();
            this.loading = true;
            try {
                const data = await fetchCurrentUser();
                this.user = data;
            } catch (err: any) {
                this.user = null;
                alert.error("Не удалось получить данные пользователя");
            } finally {
                this.loading = false;
            }
        },

    }
});
