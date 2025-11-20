<template>
  <form @submit.prevent="onRegister">
    <div class="mb-3">
      <label class="block font-medium mb-1">Имя</label>
      <input v-model="firstName" class="input" required />
    </div>

    <div class="mb-3">
      <label class="block font-medium mb-1">Фамилия</label>
      <input v-model="lastName" class="input" required />
    </div>

    <div class="mb-3">
      <label class="block font-medium mb-1">Отчество</label>
      <input v-model="middleName" class="input" />
    </div>

    <div class="mb-3">
      <label class="block font-medium mb-1">Логин</label>
      <input v-model="login" class="input" required />
    </div>

    <div class="mb-3">
      <label class="block font-medium mb-1">Пароль</label>
      <input v-model="password" type="password" class="input" required />
    </div>

    <button type="submit" class="btn-primary w-full">
      {{ auth.loading ? "Регистрация..." : "Зарегистрироваться" }}
    </button>

    <button type="button" class="btn-gray w-full mt-2" @click="$emit('back')">
      Назад к входу
    </button>

    <p v-if="auth.error" class="text-red-500 text-center mt-3">{{ auth.error }}</p>
  </form>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useAuthStore } from "@/store/auth";

const emit = defineEmits(["back"]);
const auth = useAuthStore();

const firstName = ref("");
const lastName = ref("");
const middleName = ref("");
const login = ref("");
const password = ref("");

const onRegister = async () => {
  await auth.register({
    firstName: firstName.value,
    lastName: lastName.value,
    middleName: middleName.value || "",
    login: login.value,
    password: password.value
  });

  if (!auth.error) {
    console.log("✅ Регистрация успешна:", auth.user);
    emit("back");
  }
};
</script>

<style scoped>
.input {
  @apply w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500;
}
.btn-primary {
  @apply bg-primary text-white py-2 rounded hover:bg-primary-dark transition-colors;
}
.btn-gray {
  @apply bg-gray-300 text-black py-2 rounded hover:bg-gray-400 transition-colors;
}
</style>