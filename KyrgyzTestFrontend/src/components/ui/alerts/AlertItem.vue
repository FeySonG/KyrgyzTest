<template>
  <div
      class="flex items-start gap-3 p-4 rounded shadow text-sm text-white text-lg font-semibold"
      :class="bgClass"
  >
    <span class="flex-1">{{ alert.message }}</span>

    <button @click="$emit('close')" class="text-white/70 hover:text-white">
      ✕
    </button>
  </div>
</template>

<script setup lang="ts">
import type { Alert } from "@/types/alert";
import { computed } from "vue";

const props = defineProps<{ alert: Alert }>();

const bgClass = computed(() => {
  switch (props.alert.type) {
    case "success": return "bg-green-600";
    case "error": return "bg-red-600";
    case "warning": return "bg-yellow-500 text-black";
    case "info": return "bg-blue-600";
  }
});
</script>
