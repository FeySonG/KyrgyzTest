import { createApp } from "vue";
import { createPinia } from "pinia";
import App from "./App.vue";
import router from "./router"; // путь к твоему router/index.ts
import { useAuthStore } from "@/store/auth";

import './assets/main.css';

const app = createApp(App);
const pinia = createPinia();
app.use(pinia);

const authStore = useAuthStore();

// Ждём fetchUserFromServer перед подключением роутера
(async () => {
    await authStore.fetchUserFromServer(); // подтягиваем пользователя с сервера через куки

    app.use(router);
    app.mount("#app");
})();