<template>
  <aside
      class="w-[18%] h-screen
           bg-gradient-to-b from-[#8FB65E] to-[#7aa94f]
           dark:from-gray-900 dark:to-gray-800
           border-r border-white/10
           flex flex-col
           shadow-xl"
  >

    <!-- LOGO / TITLE -->
    <div class="px-6 py-5 text-l font-semibold text-pearl tracking-wide">
      Панель инструментов
    </div>

    <!-- NAV -->
    <nav class="flex-1 px-3 space-y-1">

      <RouterLink
          v-for="item in filteredItems"
          :key="item.to"
          :to="item.to"
          class="group flex items-center gap-3
               px-4 py-3 rounded-xl
               text-l font-medium
               text-white/90
               transition-all duration-200
               hover:bg-white/10
               hover:translate-x-1"
          active-class="bg-white/20 shadow-inner"
      >
        <!-- ICON -->
        <component
            v-if="item.icon"
            :is="item.icon"
            class="w-5 h-5
                 text-white/70
                 group-hover:text-white
                 transition"
        />

        <!-- TEXT -->
        <span class="group-hover:text-white transition">
          {{ item.label }}
        </span>

        <!-- ACTIVE INDICATOR -->
        <span
            class="ml-auto w-1.5 h-1.5 rounded-full bg-white opacity-0
                 group-[.router-link-active]:opacity-100 transition"
        />
      </RouterLink>

    </nav>

    <!-- FOOTER -->
    <div class="p-4 border-t border-white/10 text-xs text-white/60">
      <slot name="footer" />
    </div>

  </aside>
</template>

<script setup lang="ts">
import { computed } from "vue";
import { useAuthStore } from "@/store/auth";
import type { SidebarItem } from "@/types/sidebarItems";

const props = defineProps<{
  items: SidebarItem[];
}>();

const authStore = useAuthStore();

const filteredItems = computed(() => {
  const user = authStore.user;
  if (!user) return [];

  return props.items.filter(item => {
    if (!item.roles || item.roles.length === 0) return true;
    return item.roles.includes(user.role);
  });
});
</script>
