<template>
  <div class="min-h-screen flex flex-col bg-background text-text-main">
    <!-- Header -->
    <header class="bg-primary dark:bg-primary-dark text-pearl py-4 shadow-md">
      <div class="container max-w-full px-4 flex justify-between">
        <div class="w-52 flex items-center">
          <RouterLink to="/home" class="hover:no-underline ">

            <h1 class="text-xl font-semibold text-pearl">🌿 Кыргызтест</h1>
          </RouterLink>
        </div>
        <div class="flex justify-between items-center text-lg">
          <RouterLink
              v-if="user?.role === 'User' || user?.role === 'Admin'"
              to="/home"
              class="p-3 rounded-xl
           transition-colors duration-500 ease-in-out
           hover:bg-gray-100 dark:hover:bg-primary/20"
          >
            Частотный словарь
          </RouterLink>

          <RouterLink
              v-if="user?.role === 'Admin'"
              to="/admin/users"
              class="p-3 rounded-xl
           transition-colors duration-500 ease-in-out
           hover:bg-gray-100 dark:hover:bg-primary/20"
          >
            Админ
          </RouterLink>
        </div>


        <div class="flex items-center gap-2 flex-shrink-0">
          <ThemeToggle/>

          <RouterLink to="/profile">
            <div class="w-12 h-12
                        rounded-full
                        flex items-center justify-center
                        transition-all duration-500 ease-out
                        hover: hover:scale-110
                      dark:hover:bg-primary
                        ">
              <UserCircleIcon class="w-8 h-8 text-pearl"/>
            </div>
          </RouterLink>

        </div>
      </div>
    </header>

    <!-- Main content -->
    <main class="min-h-screen dark:bg-warm-gray">
      <slot/>
    </main>

    <!-- Footer -->
    <footer class="bg-primary-dark text-white py-3 text-center text-sm">
      © {{ new Date().getFullYear() }} KyrgyzTest system
    </footer>
  </div>
</template>
<script setup lang="ts">
import {useRouter} from "vue-router";
import {useAuthStore} from "@/store/auth";
import ThemeToggle from "@/components/ThemeToggle.vue";
import {storeToRefs} from "pinia";
import {UserCircleIcon} from "@heroicons/vue/16/solid/index.js";

const router = useRouter();
const authStore = useAuthStore();
const {user} = storeToRefs(authStore);


</script>