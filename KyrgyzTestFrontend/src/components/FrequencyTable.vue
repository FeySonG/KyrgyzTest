<template>
  <div class="p-4 w-1/2 min-h-screen flex flex-row">

    <div v-if="store.loading">Загрузка...</div>
    <div v-else-if="store.error" class="text-red-500">{{ store.error }}</div>
    <div v-else-if="store.frequencies.length" class="
              p-4
              border-4
              overflow-y-auto
              max-h-[492px]
              w-1/2
              flex flex-col
              border-primary-light
              dark:border-primary
              rounded-lg
              focus:outline-none
              focus:ring-2
              focus:ring-accent
              resize-none
              bg-gray-50
              dark:bg-gray-800
              text-gray-800">


      <div class="min-w-10 mb-3 border-collapse bg-silver shadow-lg">
        <div class="flex justify-between">
          <button
              @click="store.downloadReport"
              class="bg-blue-400 w-full text-white px-4 py-2 hover:bg-blue-900"
          >
            Скачать в Word
          </button>
          <button
              @click="store.downloadExcelReport"
              class="bg-primary w-full text-white px-4 py-2 hover:bg-primary-dark"
          >
            Скачать в Excel
          </button>
        </div>
        <p class="p-3">Всего символов: {{ store.charCount }}</p>
        <p class="p-3">Всего слов: {{ store.wordCount }}</p>
      </div>

      <!-- Таблица частот -->
      <table class="min-w-10 p-2 border-collapse shadow-lg mb-3">
        <thead class="bg-gray-200">
        <tr>
          <th class="border px-3 py-2 text-left">Слово</th>
          <th class="border px-3 py-2 text-left">Частота</th>
        </tr>
        </thead>
        <tbody>
        <tr
            v-for="(f, i) in store.frequencies"
            :key="i"
            class="odd:bg-white even:bg-gray-100 hover:bg-blue-50 transition"
        >
          <td class="border px-3 py-2">{{ f.word }}</td>
          <td class="border px-3 py-2">{{ f.count }}</td>
        </tr>
        </tbody>
      </table>
    </div>

    <SearchTable v-if="store.frequencies.length" class="-mt-4"/>
  </div>
</template>

<script setup lang="ts">
import {useTextStore} from "@/store/textStore";
import SearchTable from "@/components/SearchTable.vue";

const store = useTextStore();
</script>