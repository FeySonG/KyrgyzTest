<template>
  <div class="p-4 w-1/2 min-h-screen">
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

    <!-- Анализ обычного текста -->
    <BaseButton
        @click="analyze"
        class="w-52 h-10 text-lg mt-2"
        :disabled="store.loading"
    >
      Анализировать
    </BaseButton>

    <!-- Word файл -->
    <div class="mt-4 pt-5 flex gap-3 items-center">
      <input
          type="file"
          accept=".docx"
          @change="handleFileChange"
          class="file:mr-4 file:py-2 file:px-4
             file:rounded file:border-0
             file:text-sm file:font-semibold
             file:bg-primary
             dark:file:bg-primary-dark
             file:text-pearl
             dark:hover:file:bg-primary
             hover:file:bg-primary-dark
             cursor-pointer
             border-4 rounded p-2
             border-primary
             font-semibold
             dark:text-white
             "
      />

      <BaseButton
          @click="analyzeWord"
          :disabled="!selectedFile || store.loading"
      >
        Анализ Word файла
      </BaseButton>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useTextStore } from "@/store/textStore";
import BaseButton from "@/components/ui/buttons/BaseButton.vue";

const text = ref("");
const store = useTextStore();

const selectedFile = ref<File | null>(null);

function analyze() {
  if (text.value.trim()) {
    store.analyze(text.value);
  }
}

function handleFileChange(event: Event) {
  const input = event.target as HTMLInputElement;
  if (!input.files?.length) return;

  selectedFile.value = input.files[0];
}

async function analyzeWord() {
  if (!selectedFile.value) return;

  await store.analyzeWordDocument(selectedFile.value);

  selectedFile.value = null;
}
</script>