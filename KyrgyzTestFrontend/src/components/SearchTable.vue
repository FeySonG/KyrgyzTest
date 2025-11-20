<template>
  <div class="p-4
              w-1/2
              min-h-screen
              ">
    <div class="
              p-4
              border-4
              overflow-y-auto
              max-h-[492px]
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

      <div class="searchArea">
        <div class="w-full max-w-md mx-auto ">
          <div class="relative">
            <!-- Иконка поиска -->
            <div class="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
              <svg xmlns="http://www.w3.org/2000/svg"
                   fill="none" viewBox="0 0 24 24"
                   stroke-width="2" stroke="currentColor"
                   class="w-5 h-5 text-gray-500 dark:text-gray-400">
                <path stroke-linecap="round" stroke-linejoin="round"
                      d="M21 21l-4.35-4.35M5.5 11a5.5 5.5 0 1111 0 5.5 5.5 0 01-11 0z" />
              </svg>
            </div>

            <!-- Поле ввода -->
            <input
                v-model="query"
                type="text"
                placeholder="Введите слово..."
                class="block w-full pl-10 pr-16 py-2 text-gray-900 dark:text-gray-100
             bg-white dark:bg-gray-800 border border-gray-300 dark:border-gray-700
             rounded-xl focus:ring-2 focus:ring-blue-500 focus:border-blue-500
             outline-none"
            />

            <!-- Кнопка поиска -->
            <button
                @click="searchWord"
                class="absolute inset-y-0 right-0 flex items-center px-4 bg-blue-600
             text-white rounded-r-xl hover:bg-blue-700 active:bg-blue-800
             focus:outline-none focus:ring-2 focus:ring-blue-400 transition">
              <svg xmlns="http://www.w3.org/2000/svg"
                   fill="none" viewBox="0 0 24 24"
                   stroke-width="2" stroke="currentColor"
                   class="w-5 h-5 mr-1">
                <path stroke-linecap="round" stroke-linejoin="round"
                      d="M21 21l-4.35-4.35M5.5 11a5.5 5.5 0 1111 0 5.5 5.5 0 01-11 0z" />
              </svg>
              Найти
            </button>
          </div>
        </div>
      </div>

      <table class="min-w-10 p-2 border-collapse shadow-lg my-2">
        <thead class="bg-gray-200">
        <tr>
          <th class="border px-3 py-2 text-left">Слово</th>
          <th class="border px-3 py-2 text-left">Частота</th>
        </tr>
        </thead>
        <tbody>
        <tr
            v-for="(f, i) in store.searchResults"
            :key="i"
            class="odd:bg-white even:bg-gray-100 hover:bg-blue-50 transition"
        >
          <td class="border px-3 py-2">{{ f.word }}</td>
          <td class="border px-3 py-2">{{ f.count }}</td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
<script setup lang="ts">
import {useTextStore} from "@/store/textStore";
import {ref} from "vue";

const store = useTextStore();
const query = ref("");

// Метод для кнопки поиска
const searchWord = async () => {
  if (!query.value.trim()) return;
  await store.findWord(query.value);
};
</script>