<template>
  <div class="flex justify-between items-center py-2 border-b border-gray-100 dark:border-gray-700">

    <!-- LABEL -->
    <span class="text-gray-400 text-sm shrink-0">
      {{ label }}
    </span>

    <!-- VALUE -->
    <span class="font-medium text-right dark:text-white max-w-[60%] truncate">
      <slot />
    </span>

  </div>
</template>

<script setup lang="ts">
defineProps<{
  label: string;
}>();
</script>
