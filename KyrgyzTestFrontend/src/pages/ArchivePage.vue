<script setup lang="ts">

import {archiveItems} from "@/types/sidebarItems";
import Sidebar from "@/components/Sidebar.vue";
import MainLayout from "@/layouts/MainLayout.vue";
</script>

<template>
  <MainLayout>
    <div class="bg-pearl dark:bg-warm-gray w-full flex min-h-screen">

      <!-- Sidebar слева -->
      <Sidebar :items="archiveItems" />

      <!-- Контент справа -->
      <div class="flex-1 p-6">
        <RouterView />
      </div>

    </div>
  </MainLayout>
</template>
