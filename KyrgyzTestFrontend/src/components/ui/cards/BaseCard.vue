<template>
  <div
      class="
        relative
        border-4
        border-primary
        bg-white
        dark:bg-gray-800
        rounded-lg
        shadow
        p-6
      "
      :class="width"
  >
    <!-- Контент -->
    <div class="space-y-2">
      <slot />
    </div>

    <!-- Экшены (в углу) -->
    <div
        v-if="$slots.actions"
        class="absolute top-4 right-4"
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
