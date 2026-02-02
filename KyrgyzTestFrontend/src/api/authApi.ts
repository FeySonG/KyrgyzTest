// src/api/authApi.ts
import axios from "axios";
import {CurrentUser} from "@/types/types";

const api = axios.create({
    baseURL: "http://localhost:5227/api-auth",
    withCredentials: true // если у тебя cookie-авторизация
});

export interface RegisterRequest {
    firstName: string;
    lastName: string;
    middleName?: string;
    login: string;
    password: string;
}

export interface LoginRequest {
    login: string;
    password: string;
}

// 🔹 Регистрация
export async function registerUser(data: RegisterRequest) {
    const response = await api.post("/registration", data);
    return response.data;
}

// 🔹 Логин
export async function loginUser(data: LoginRequest) {
    const response = await api.post("/login", data);
    return response.data;
}

// 🔹 Логаут
export async function logoutUser() {
    const response = await api.post("/logout");
    return response.data;
}

// 🔹 Проверка текущего пользователя по куки
export async function fetchCurrentUser(): Promise<CurrentUser | null> {
    try {
        const response = await api.get<CurrentUser>("/me");
        return response.data;
    } catch {
        return null;
    }
}