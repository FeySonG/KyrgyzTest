<template>
  <div
      class="relative
         rounded-2xl
         bg-white dark:bg-gray-800
         border border-gray-200 dark:border-gray-700
         shadow-lg
         hover:shadow-xl
         transition-all duration-300
         p-6"
      :class="width"
  >
    <!-- Контент -->
    <div class="space-y-2 pr-12">
      <slot />
    </div>

    <!-- Экшены (в углу) -->
    <div
        v-if="$slots.actions"
        class="absolute top-4 right-4 z-10"
    >
      <slot name="actions" />
    </div>
  </div>
</template>


<script setup lang="ts">
defineProps<{
  width?: string; // например: "w-1/2", "w-full"
}>();
</script>
