// src/api/authApi.ts
import axios from "axios";

const api = axios.create({
    baseURL: `${import.meta.env.VITE_API_URL}/api-auth`,
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
export async function fetchCurrentUser() {
    try {
        const response = await api.get("/me"); // эндпоинт на сервере
        return response.data; // { login: string }
    } catch {
        return null; // если не авторизован
    }
}