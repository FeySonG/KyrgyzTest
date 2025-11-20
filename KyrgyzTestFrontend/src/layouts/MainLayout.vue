<template>
  <div class="min-h-screen flex flex-col bg-background text-text-main">
    <!-- Header -->
    <header class="bg-primary dark:bg-primary-dark text-pearl py-4 shadow-md">
      <div class="container max-w-full px-4 flex justify-between">
        <div class="w-52 flex items-center">
          <Router-link to="/home" class="hover:no-underline ">

            <h1 class="text-xl font-semibold text-pearl">🌿 Кыргыз тест</h1>
          </Router-link>
        </div>
        <div class=" flex items-center">
          <RouterLink to="/home">
            Частотный словарь
          </RouterLink>
        </div>

        <div class="flex items-center gap-2 flex-shrink-0">
          <button
              @click="handleLogout"
              class="bg-primary text-white px-4 py-2 rounded hover:bg-primary-dark"
          >
            Выйти
          </button>
          <ThemeToggle/>
        </div>
      </div>
    </header>

    <!-- Main content -->
    <main class="min-h-screen dark:bg-warm-gray px-4 py-6">
      <slot/>
    </main>

    <!-- Footer -->
    <footer class="bg-primary-dark text-white py-3 text-center text-sm">
      © {{ new Date().getFullYear() }} Frequency Dictionary
    </footer>
  </div>
</template>
<script setup lang="ts">
import {useRouter} from "vue-router";
import {useAuthStore} from "@/store/auth";
import ThemeToggle from "@/components/ThemeToggle.vue";

const router = useRouter();
const authStore = useAuthStore();

async function handleLogout() {
  try {
    await authStore.logout();
    await router.push("/login");
  } catch (err: any) {
    alert(err.message || "Ошибка выхода");
  }
}
</script>