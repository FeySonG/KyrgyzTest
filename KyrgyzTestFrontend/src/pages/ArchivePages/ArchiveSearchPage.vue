<template>
  <div class="p-4 w-full">

    <div class="relative w-full max-w-2xl mx-auto">

      <!-- INPUT + ICON -->
      <div class="relative flex items-center">

        <MagnifyingGlassIcon
            class="absolute left-3 w-5 h-5 text-gray-400"
        />

        <input
            v-model="query"
            type="text"
            placeholder="Введите ФИО..."
            class="w-full h-12 pl-10 pr-28
                 text-gray-900 dark:text-gray-100
                 bg-white dark:bg-gray-900
                 border border-gray-200 dark:border-gray-700
                 rounded-2xl
                 shadow-sm
                 focus:ring-2 focus:ring-primary
                 focus:shadow-md
                 transition-all duration-200
                 outline-none"
            @keyup.enter="searchTestResult"
        />

        <button
            @click="searchTestResult"
            class="absolute right-1 h-10 px-4
                 flex items-center gap-2
                 bg-primary text-white
                 rounded-xl
                 shadow-md
                 hover:bg-primary-dark
                 hover:scale-105
                 active:scale-95
                 transition-all duration-200"
        >
          <MagnifyingGlassIcon class="w-4 h-4" />
          <span>Найти</span>
        </button>
      </div>

      <!-- DROPDOWN -->
      <div
          v-if="isSearched"
          class="absolute z-50 mt-2 w-full
               bg-white dark:bg-gray-900
               border border-gray-200 dark:border-gray-700
               rounded-2xl
               shadow-2xl
               p-2
               backdrop-blur-sm"
      >

        <!-- Заголовок -->
        <div class="text-xs text-gray-400 px-3 mb-2">
          Результаты поиска: "{{ resultStore.searchName }}"
        </div>

        <!-- ЕСЛИ ЕСТЬ РЕЗУЛЬТАТЫ -->
        <div v-if="resultStore.studentNames.length">
          <div
              v-for="item in resultStore.studentNames"
              :key="item.studentId"
              @click="goToStudent(item.studentId)"
              class="px-4 py-3 rounded-xl
                   cursor-pointer
                   flex items-center justify-between
                   transition-all duration-200
                   hover:bg-gray-100 dark:hover:bg-gray-800
                   hover:shadow-sm"
          >
            <span class="text-gray-800 dark:text-gray-200">
              {{ item.studentName }}
            </span>

            <span class="text-gray-400 text-sm">→</span>
          </div>
        </div>

        <!-- ЕСЛИ НИЧЕГО НЕ НАЙДЕНО -->
        <div
            v-else
            class="px-4 py-6 text-center text-gray-400"
        >
          Ничего не найдено
        </div>

      </div>

    </div>

  </div>
</template>

<script setup lang="ts">
import { MagnifyingGlassIcon } from "@heroicons/vue/24/outline";
import { useArchiveStore } from "@/store/archiveStore";
import { ref } from "vue";
import router from "@/router";

const resultStore = useArchiveStore();
const query = ref("");
const isSearched = ref(false);

const goToStudent = (id: number) => {
  // resultStore.testResults.forEach((result) => {
  //   if (result.idStudent == id) { resultStore.getRegulation(result.idRegulation) }
  // })
  // resultStore.getStudentResult(id);

  router.push({
    name: "StudentResults",
    params: { id },
  });
};

const searchTestResult = async () => {
  if (!query.value.trim()) return;

  await resultStore.searchResult(query.value);


  isSearched.value = true;
};
</script>