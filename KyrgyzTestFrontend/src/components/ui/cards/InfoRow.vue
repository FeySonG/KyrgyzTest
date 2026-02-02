<template>
  <div>
    <span class="text-gray-500">{{ label }}:</span>
    <span class="ml-2 font-medium dark:text-pearl">
      <slot />
    </span>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  label: string;
}>();
</script>
