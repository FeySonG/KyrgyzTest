<script setup lang="ts">

import MainLayout from "@/layouts/MainLayout.vue";
import Sidebar from "@/components/Sidebar.vue";
import {adminSidebarItems} from "@/types/sidebarItems";
</script>

<template>
  <MainLayout>
    <div class="bg-pearl dark:bg-warm-gray w-full flex min-h-screen">

      <!-- Sidebar слева -->
      <Sidebar :items="adminSidebarItems" />

      <!-- Контент справа -->
      <div class="flex-1 p-6">
        <RouterView />
      </div>

    </div>
  </MainLayout>
</template>

<style scoped>

</style>