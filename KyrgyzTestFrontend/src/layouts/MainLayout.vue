<template>
  <div class="min-h-screen flex flex-col bg-background text-text-main">

    <!-- HEADER -->
    <header class="sticky top-0 z-50
                   bg-primary/95 backdrop-blur
                   dark:bg-primary-dark/95
                   text-pearl
                   shadow-lg">

      <div class="max-w-full mx-auto px-6 py-3 flex items-center justify-between">

        <!-- LOGO -->
        <RouterLink to="/home" class="flex items-center gap-2 group">
          <h1 class="text-xl font-semibold tracking-wide
                     group-hover:scale-105 transition">
            🌿 Кыргызтест
          </h1>
        </RouterLink>

        <!-- NAV -->
        <nav class="flex items-center gap-2 text-l">

          <RouterLink
              v-if="user?.role === 'User' || user?.role === 'Admin'"
              to="/archive"
              class="px-4 py-2 rounded-xl
                   transition-all duration-200
                   hover:bg-white/10
                   hover:scale-105"
          >
            Архив
          </RouterLink>

          <RouterLink
              v-if="user?.role === 'User' || user?.role === 'Admin'"
              to="/home"
              class="px-4 py-2 rounded-xl
                   transition-all duration-200
                   hover:bg-white/10
                   hover:scale-105"
          >
            Частотный словарь
          </RouterLink>

          <RouterLink
              v-if="user?.role === 'Admin'"
              to="/admin/users"
              class="px-4 py-2 rounded-xl
                   transition-all duration-200
                   hover:bg-white/20
                   hover:scale-105"
          >
            Админ
          </RouterLink>

        </nav>

        <!-- RIGHT SIDE -->
        <div class="flex items-center gap-3">

          <ThemeToggle/>

          <!-- PROFILE -->
          <RouterLink to="/profile">
            <div class="w-11 h-11 rounded-full
                        flex items-center justify-center
                        hover:bg-primary
                        hover:scale-110
                        transition-all duration-200
                        shadow-sm">
              <UserCircleIcon class="w-7 h-7 text-pearl"/>
            </div>
          </RouterLink>

        </div>

      </div>
    </header>

    <!-- MAIN -->
    <main class="flex-1  w-full
                 from-transparent
                 ">
      <slot/>
    </main>

    <!-- FOOTER -->
    <footer class="bg-primary-dark/95 text-white
                   py-4 text-center text-sm
                   border-t border-white/10
                   shadow-inner">
      <div class="opacity-80">
        © {{ new Date().getFullYear() }} KyrgyzTest system
      </div>
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