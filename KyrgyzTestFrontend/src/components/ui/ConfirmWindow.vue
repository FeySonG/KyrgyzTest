<template>
  <div v-if="visible" class="fixed inset-0 flex items-center justify-center bg-black/50 z-50">
    <div class="bg-white dark:bg-gray-800 rounded-lg shadow-lg w-96 p-6">
      <h3 class="text-lg font-semibold text-gray-800 dark:text-gray-200 mb-4">{{ title }}</h3>
      <p class="text-gray-600 dark:text-gray-300 mb-6">{{ message }}</p>

      <div class="flex justify-end gap-3">
        <button
            class="px-4
                   py-2
                   border-4
                   border-gray-700
                   rounded
                   dark:text-pearl
                   hover:bg-gray-100
                   hover:bg-gray-700
                   hover:text-pearl

"
            @click="cancel"
        >
          Отмена
        </button>
        <button
            class="px-4
                   py-2
                   text-lg
                   text-red-700
                   border-4
                   border-red-700
                   rounded
                   hover:text-pearl
                   hover:bg-red-700

"
            @click="confirm"
        >
          Подтвердить
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import {defineProps, defineEmits} from "vue";

const props = defineProps<{
  visible: boolean;
  title?: string;
  message: string;
}>();

const emit = defineEmits<{
  (e: "confirm"): void;
  (e: "cancel"): void;
}>();

function confirm() {
  emit("confirm");
}

function cancel() {
  emit("cancel");
}
</script>
