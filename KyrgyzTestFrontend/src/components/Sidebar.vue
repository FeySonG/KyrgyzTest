<template>
  <aside
      class="w-96
             bg-[#8FB65E]
             dark:bg-warm-light
             h-screen
             border-r-4
             border-r-primary
             dark:border-r-primary-dark
             flex
             flex-col"
  >
    <!-- Навигация -->
    <nav class="p-4 space-y-2 mt-11 flex-1">
      <RouterLink
          v-for="item in filteredItems"
          :key="item.to"
          :to="item.to"
          class="block
                 px-4
                 py-2
                 rounded
                 hover:bg-white/10
                 text-lg
                 font-semibold
                 text-text-dark
                 dark:text-[#939292]
                 hover:text-pearl"
          active-class="bg-white/20"
      >
        <div class="flex items-center gap-2">
          <component
              v-if="item.icon"
              :is="item.icon"
              class="w-12 h-12 p-3"
          />
          <span>{{ item.label }}</span>
        </div>
      </RouterLink>
    </nav>

    <!-- Футер (необязательный) -->
    <div class="p-4 border-t border-white/20">
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
