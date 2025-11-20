<template>
  <div class="p-4 w-1/2 min-h-screen ">
    <textarea
        v-model="text"
        rows="5"
        class="w-full
               h-[50vh]
               p-4
               border-4
               border-primary-light
               dark:border-primary
               rounded-lg
               focus:outline-none
               focus:ring-2
               focus:ring-accent
               resize-none
               bg-gray-50
               dark:bg-gray-800
               text-gray-800
               dark:text-gray-50"
        placeholder="Введите текст для анализа..."
    ></textarea>

    <button
        @click="analyze"
        class="bg-blue-600 text-white px-6 py-2 mt-3 rounded-md hover:bg-blue-700 hover:shadow-lg transition duration-200"
    >
      Анализировать
    </button>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useTextStore } from "../store/textStore";

const text = ref("");
const store = useTextStore();

function analyze() {
  if (text.value.trim()) {
    store.analyze(text.value);
  }
}
</script>