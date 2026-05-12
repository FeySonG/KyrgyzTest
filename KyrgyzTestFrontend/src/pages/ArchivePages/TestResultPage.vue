<script setup lang="ts">
import { useRoute } from "vue-router";
import { ref, computed, onMounted } from "vue";
import { useArchiveStore } from "@/store/archiveStore";

const route = useRoute();
const archiveStore = useArchiveStore();

onMounted(async () => {
  const id = Number(route.params.id);

  if (!id) return;

  await archiveStore.getStudentResult(id);
});

const results = computed(() => archiveStore.studentResults ?? []);

const sortDesc = ref(true);

const sortedResults = computed(() => {
  return [...results.value].sort((a, b) => {
    const dateA = new Date(a.generateDate).getTime();
    const dateB = new Date(b.generateDate).getTime();

    return sortDesc.value ? dateB - dateA : dateA - dateB;
  });
});

const toggleSort = () => {
  sortDesc.value = !sortDesc.value;
};

const formatDate = (dateString: string) => {
  const date = new Date(dateString);

  return date.toLocaleString("ru-RU", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
  });
};
</script>

<template>
  <div
      v-if="sortedResults.length"
      class="mt-6 animate-fade-in"
  >
    <!-- Заголовок -->
    <div
        class="text-lg font-semibold mb-4 dark:text-gray-300"
    >
      Результаты тестов:
      <span class="text-primary">
        {{ sortedResults[0]?.nameStudent }}
      </span>
    </div>

    <!-- Таблица -->
    <div
        class="
        custom-scrollbar
        overflow-y-auto
        overflow-x-hidden
        max-h-[800px]
        rounded-2xl
        shadow-lg
        bg-white
        dark:bg-gray-900
        border
        border-gray-200
        dark:border-gray-700
      "
    >
      <table class="w-full text-sm border-collapse">
        <!-- HEADER -->
        <thead
            class="
            bg-gray-50
            dark:bg-gray-800
            text-gray-600
            dark:text-gray-300
            sticky top-0 z-10
          "
        >
        <tr>
          <th class="px-5 py-3 font-medium">
            Место тестирования
          </th>

          <th class="px-5 py-3 font-medium">
            Тест
          </th>

          <th class="px-5 py-3 font-medium">
            Уровень
          </th>

          <th class="px-5 py-3 font-medium text-center">
            Баллы
          </th>

          <th
              @click="toggleSort"
              class="
              px-5 py-3
              font-medium
              cursor-pointer
              select-none
              hover:text-primary
              transition-colors
              duration-300
            "
          >
            <div class="flex items-center justify-center gap-1">
              Дата

              <span
                  class="
                  inline-block
                  transition-transform
                  duration-300
                "
                  :class="sortDesc ? 'rotate-0' : 'rotate-180'"
              >
                  ↓
                </span>
            </div>
          </th>
        </tr>
        </thead>

        <!-- BODY -->
        <transition-group
            tag="tbody"
            name="table"
        >
          <tr
              v-for="item in sortedResults"
              :key="item.id"
              class="
              border-t
              border-gray-100
              dark:border-gray-700
              hover:bg-gray-50
              dark:hover:bg-gray-800
              transition-all
              duration-300
              hover:scale-[1.002]
              dark:text-pearl
            "
          >
            <td
                class="
                px-5 py-3
                border-r
                border-gray-200
                dark:border-gray-700
              "
            >
              {{ item.nameFacultet }}
            </td>

            <td
                class="
                px-5 py-3
                border-r
                border-gray-200
                dark:border-gray-700
              "
            >
              {{ item.nameDiscipline }}
            </td>

            <td
                class="
                px-5 py-3
                border-r
                border-gray-200
                dark:border-gray-700
              "
            >
              {{ item.shRegulation }}
            </td>

            <td
                class="
                px-5 py-3
                text-center
                font-bold
                border-r
                border-gray-200
                dark:border-gray-700
              "
            >
              <span
                  class="
                  px-2
                  py-1
                  rounded-lg
                  transition-all
                  duration-300
                "
                  :class="
                  item.ball == null
                    ? 'bg-gray-100 text-gray-400'
                    : item.ball >= 60
                    ? 'bg-green-100 text-green-600'
                    : 'bg-red-100 text-red-600'
                "
              >
                {{ item.ball ?? "—" }}
              </span>
            </td>

            <td
                class="
                px-5 py-3
                text-gray-500
                dark:text-gray-400
              "
            >
              {{ formatDate(item.generateDate) }}
            </td>
          </tr>
        </transition-group>
      </table>
    </div>
  </div>

  <!-- empty -->
  <div
      v-else
      class="
      text-gray-400
      mt-6
      text-center
      animate-pulse
    "
  >
    Нет результатов
  </div>
</template>

<style scoped>
/* появление контейнера */
.animate-fade-in {
  animation: fadeIn 0.4s ease;
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(8px);
  }

  to {
    opacity: 1;
    transform: translateY(0);
  }
}

/* transition-group */
.table-enter-active,
.table-leave-active,
.table-move {
  transition: all 0.35s ease;
}

.table-enter-from {
  opacity: 0;
  transform: translateY(12px);
}

.table-leave-to {
  opacity: 0;
  transform: translateY(-12px);
}
</style>