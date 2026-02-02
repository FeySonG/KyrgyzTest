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

    <BaseButton @click="analyze" class="w-52 h-10 text-lg mt-1">Анализировать</BaseButton>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useTextStore } from "@/store/textStore";
import BaseButton from "@/components/ui/buttons/BaseButton.vue";

const text = ref("");
const store = useTextStore();

function analyze() {
  if (text.value.trim()) {
    store.analyze(text.value);
  }
}
</script>