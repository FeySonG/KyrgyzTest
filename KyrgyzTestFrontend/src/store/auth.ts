import { defineStore } from "pinia";
import {registerUser, loginUser, logoutUser, type RegisterRequest, fetchCurrentUser} from "../api/authApi";

export const useAuthStore = defineStore("auth", {
    state: () => ({
        user: undefined as undefined | null | { login: string }, // undefined при старте
        loading: false,
        error: ""
    }),
    actions: {
        async register(payload: RegisterRequest) {
            this.loading = true;
            this.error = "";
            try {
                await registerUser(payload);
                this.user = { login: payload.login };
            } catch (err: any) {
                this.error = err.response?.data || "Ошибка регистрации";
            } finally {
                this.loading = false;
            }
        },

        async login(login: string, password: string) {
            try {
                this.error = "";
                await loginUser({ login, password });
                this.user = { login };
            } catch (err: any) {
                this.error = err.response?.data || "Ошибка авторизации";
                throw err;
            }
        },

        async logout() {
            await logoutUser();
            this.user = null;
        },

        // 🔹 Подтягиваем текущего пользователя с сервера через cookie
        async fetchUserFromServer() {
            this.loading = true;
            try {
                const data = await fetchCurrentUser();
                this.user = data ? { login: data.login } : null;
            } catch {
                this.user = null;
            } finally {
                this.loading = false;
            }
        }
    }
});