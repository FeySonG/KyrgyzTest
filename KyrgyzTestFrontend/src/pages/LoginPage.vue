<template>
  <MainLayout>
    <div class="min-h-screen flex items-center justify-center ">
      <div class="bg-white mb-72 p-8 rounded-lg shadow-lg w-full max-w-md">
        <h2 class="text-2xl font-bold mb-6 text-center">
          {{ isRegister ? 'Регистрация' : 'Авторизация' }}
        </h2>

        <!-- Форма авторизации -->
        <form v-if="!isRegister" @submit.prevent="login">
          <div class="mb-4">
            <label class="block mb-1 font-medium">Логин</label>
            <input
                v-model="username"
                type="text"
                class="w-full border border-blue-2 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
                placeholder="Введите логин"
                required
            />
          </div>

          <div class="mb-4">
            <label class="block mb-1 font-medium">Пароль</label>
            <input
                v-model="password"
                type="password"
                class="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
                placeholder="Введите пароль"
                required
            />
          </div>

          <button
              type="submit"
              class="w-full bg-primary text-white py-2 rounded hover:bg-primary-dark transition-colors"
          >
            Войти
          </button>

          <button
              type="button"
              class="mt-1 w-full bg-primary text-white py-2 rounded hover:bg-primary-dark transition-colors"
              @click="isRegister = true"
          >
            Регистрация
          </button>
        </form>

        <!-- Компонент регистрации -->
        <RegisterForm v-else @back="isRegister = false"/>
      </div>
    </div>
  </MainLayout>
</template>

<script setup lang="ts">
import {ref} from "vue";
import {useRouter} from "vue-router";
import RegisterForm from './RegisterForm.vue';
import {useAuthStore} from "@/store/auth";
import MainLayout from "@/layouts/MainLayout.vue";


const username = ref("");
const password = ref("");
const isRegister = ref(false);

const router = useRouter();
const authStore = useAuthStore();

async function login() {
  try {
    await authStore.login(username.value, password.value);
    await router.push("/home"); // Переход на карту после входа
  } catch (error: any) {
    alert(error.message || "Неверный логин или пароль");
  }
}
</script>