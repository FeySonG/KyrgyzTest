<template>
  <section class="mx-auto w-full max-w-[1440px] space-y-6">
    <!-- Заголовок страницы. -->
    <div class="flex flex-wrap items-center justify-between gap-4">
      <div>
        <h1 class="text-3xl font-bold text-text-dark dark:text-white">Выдачи сертификатов</h1>
        <p class="mt-1 text-sm text-silver dark:text-gray-400">Реестр выданных сертификатов</p>
      </div>
    </div>

    <!-- Статистика и действия импорта. -->
    <div class="grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
      <article v-for="card in statCards" :key="card.label" class="flex min-h-32 items-center gap-4 rounded-2xl border border-gray-100 bg-white p-5 shadow-sm dark:border-gray-800 dark:bg-gray-900">
        <div class="rounded-full bg-primary/10 p-4 text-primary">
          <component :is="statCardIcons[card.icon]" class="h-7 w-7" />
        </div>
        <div>
          <p class="text-sm font-medium text-silver">{{ card.label }}</p>
          <p class="mt-1 text-2xl font-bold text-primary-dark dark:text-primary-light">{{ card.value }}</p>
          <p class="mt-1 text-xs text-silver">{{ card.description }}</p>
        </div>
      </article>

      <a
          href="/templates/Шаблон_импорта_сертификатов.xlsx"
          download="Шаблон_импорта_сертификатов.xlsx"
          class="flex min-h-32 items-center gap-4 rounded-2xl border border-gray-100 bg-white p-5 shadow-sm transition hover:border-primary hover:shadow-md dark:border-gray-800 dark:bg-gray-900"
      >
        <div class="rounded-full bg-primary/10 p-4 text-primary">
          <ArrowDownTrayIcon class="h-7 w-7" />
        </div>
        <div>
          <p class="text-lg font-bold text-text-dark dark:text-white">Скачать шаблон</p>
          <p class="mt-1 text-sm text-silver">Excel-файл для импорта сертификатов</p>
        </div>
      </a>

      <label
          class="flex min-h-32 cursor-pointer items-center gap-4 rounded-2xl border border-gray-100 bg-white p-5 shadow-sm transition hover:border-primary hover:shadow-md dark:border-gray-800 dark:bg-gray-900"
          :class="{'pointer-events-none opacity-60': store.loading}"
      >
        <div class="rounded-full bg-primary/10 p-4 text-primary">
          <ArrowUpTrayIcon class="h-7 w-7" />
        </div>

        <div>
          <p class="text-lg font-bold text-text-dark dark:text-white">Импорт Excel</p>
          <p class="mt-1 text-sm text-silver">Выберите заполненный файл</p>
        </div>

        <input class="hidden" type="file" accept=".xlsx" @change="onFileSelected" />
      </label>
    </div>

    <!-- Фильтры применяются по нажатию «Найти», чтобы не пересчитывать таблицу при каждом вводе. -->
    <form class="rounded-2xl border border-gray-100 bg-white p-5 shadow-sm dark:border-gray-800 dark:bg-gray-900" @submit.prevent="applyFilters">
      <div class="grid gap-4 xl:grid-cols-12">


        <label class="space-y-2
                      xl:col-span-4
                       text-gray-900
                        dark:text-pearl
                      ">
          <span class="text-sm font-semibold

                      ">Поиск по ФИО</span>
          <div class="
                      relative
                      ">
            <UserIcon class="pointer-events-none
                             absolute
                             left-3
                             top-1/2
                             h-5
                             w-5
                             -translate-y-1/2

                            " />
            <input v-model="filters.received" class="input-field
                                                     !pl-10
                                                    " placeholder="Введите ФИО..." />
          </div>
        </label>

        <label class="space-y-2 xl:col-span-3 dark:text-gray-100">
          <span class="text-sm font-semibold text-text-dark dark:text-white">Номер сертификата</span>
          <div class="relative">
            <HashtagIcon class="pointer-events-none absolute left-3 top-1/2 h-5 w-5 -translate-y-1/2 dark:text-pearl" />
            <input v-model="filters.number" class="input-field !pl-10" placeholder="Введите номер..." />
          </div>
        </label>

        <label class="space-y-2 xl:col-span-2">
          <span class="text-sm font-semibold text-text-dark dark:text-pearl">Дата с</span>
          <input v-model="filters.startDate" type="date" class="input-field" />
        </label>

        <label class="space-y-2 xl:col-span-2">
          <span class="text-sm font-semibold text-text-dark dark:text-pearl">Дата по</span>
          <input v-model="filters.endDate" type="date" class="input-field" />
        </label>

        <div class="flex items-end gap-2 xl:col-span-1">
          <button type="submit" class="inline-flex h-12 flex-1 items-center justify-center gap-2 rounded-xl bg-primary px-4 font-semibold text-white shadow-sm transition hover:bg-primary-dark">
            <MagnifyingGlassIcon class="h-5 w-5" />
            <span class="hidden 2xl:inline">Найти</span>
          </button>

        </div>
      </div>
    </form>

    <!-- Таблица с состояниями загрузки, пустого результата и пагинацией. -->
    <div class="overflow-hidden rounded-2xl border border-gray-100 bg-white shadow-sm dark:border-gray-800 dark:bg-gray-900">
      <div class=" custom-scrollbar max-h-[60vh] overflow-auto">
        <table class="w-full min-w-[850px] text-left text-#0033cc">
          <thead class="sticky top-0 z-10 border-b bg-gray-50
                                  dark:bg-gray-800
                                  text-gray-600
                                  dark:text-gray-300">
            <tr>
              <th class="px-6 py-4 font-semibold">ФИО</th>
              <th class="px-6 py-4 font-semibold">Номер сертификата</th>
              <th class="px-6 py-4 font-semibold">Дата выдачи</th>
              <th class="px-6 py-4 font-semibold">Организация</th>
              <th class="px-6 py-4 font-semibold">КомментариЙ</th>
              <th class="px-6 py-4 text-center font-semibold">Действия</th>
            </tr>
          </thead>
          <tbody>
            <tr v-if="store.loading">
              <td colspan="6" class="px-6 py-12 text-center text-silver">Загрузка реестра…</td>
            </tr>
            <tr v-else-if="paginatedRecords.length === 0">
              <td colspan="6" class="px-6 py-12 text-center text-silver">По вашему запросу ничего не найдено.</td>
            </tr>
            <tr v-for="record in paginatedRecords" :key="record.id" class="border-b border-gray-100 last:border-0 dark:border-gray-800 dark:text-pearl">
              <td class="px-6 py-4 font-medium">{{ record.received }}</td>
              <td class="px-6 py-4">{{ record.certificateNumber }}</td>
              <td class="px-6 py-4">{{ formatDate(record.issueDate) }}</td>
              <td class="px-6 py-4">{{ record.organization || '—' }}</td>
              <td class="px-6 py-4">{{ record.additionalInfo || '—' }}</td>
              <td class="px-6 py-4">
                <div class="flex justify-center gap-2">
                  <button title="Просмотреть" class="action-button hover:text-primary" @click="store.show(record.id)"><EyeIcon class="h-5 w-5" /></button>
                  <button title="Удалить" class="action-button text-error hover:bg-red-50" @click="confirmDelete(record.id)"><TrashIcon class="h-5 w-5" /></button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <div class="flex
                  flex-wrap
                  items-center
                  justify-between
                  gap-4
                  border-t
                  border-gray-100
                  px-6
                  py-4
                  text-sm
                  dark:border-gray-800
                  dark:text-pearl
                  ">
        <span>Всего записей: <b>{{ filteredRecords.length.toLocaleString('ru-RU') }}</b></span>
        <div class="flex items-center gap-2">
          <button class="pagination-button" :disabled="page === 1" @click="page--">‹</button>
          <span>Страница {{ page }} из {{ totalPages }}</span>
          <button class="pagination-button" :disabled="page === totalPages" @click="page++">›</button>
        </div>
      </div>
    </div>

    <!-- Модальное окно: открывается только после успешного запроса записи по id. -->
    <div v-if="store.selectedRecord" class="fixed inset-0 z-50 flex items-center justify-center bg-black/45 p-4" @click.self="store.selectedRecord = null">
      <article class="w-full max-w-lg rounded-2xl bg-white p-6 shadow-2xl dark:bg-gray-900">
        <div class="mb-5 flex items-start justify-between gap-4">
          <div>
            <h2 class="text-xl font-bold dark:text-pearl">Сертификат № {{ store.selectedRecord.certificateNumber }}</h2>
            <p class="mt-1 text-sm text-silver dark:text-pearl">{{ store.selectedRecord.received }}</p>
          </div>
          <button class="action-button" title="Закрыть" @click="store.selectedRecord = null"><XMarkIcon class="h-6 w-6" /></button>
        </div>
        <dl class="grid grid-cols-2 gap-x-5 gap-y-4 text-sm">
          <template v-for="item in recordDetails" :key="item.label">
            <dt class="text-silver ">{{ item.label }}</dt><dd class="font-medium dark:text-pearl">{{ item.value || '—' }}</dd>
          </template>
        </dl>
      </article>
    </div>
  </section>
</template>

<script setup lang="ts">
import {computed, onMounted, reactive, ref, watch} from "vue";
import {
  ArrowDownTrayIcon, ArrowUpTrayIcon, CalendarDaysIcon, DocumentCheckIcon, EyeIcon,
  HashtagIcon, MagnifyingGlassIcon, TrashIcon, UserIcon, XMarkIcon,
} from "@heroicons/vue/24/outline";
import {useCertificateRecordStore} from "@/store/certificateRecordStore";

// Pinia-store инкапсулирует все HTTP-запросы и состояния загрузки страницы.
const store = useCertificateRecordStore();
// Номер страницы и максимальное число строк на странице таблицы.
const page = ref(1);
const pageSize = 25;
// Черновые значения полей формы до нажатия «Найти».
const filters = reactive({received: "", number: "", startDate: "", endDate: ""});
// Значения, по которым сейчас фактически отфильтрована таблица.
const appliedFilters = reactive({received: "", number: "", startDate: "", endDate: ""});

// Сопоставление строкового ключа с Vue-компонентом иконки.
const statCardIcons = {
  document: DocumentCheckIcon,
  calendar: CalendarDaysIcon,
  user: UserIcon,
};
type StatCardIcon = keyof typeof statCardIcons;

// Находим первую и последнюю даты выдачи среди всех записей реестра.
const certificatePeriod = computed(() => {
  const dates = store.records
      .map(record => record.issueDate.slice(0, 10))
      .filter(Boolean)
      .sort();

  if (dates.length === 0) {
    return {value: "—", description: "Записей пока нет"};
  }

  const firstDate = formatDate(dates[0]);
  const lastDate = formatDate(dates[dates.length - 1]);
  return {
    value: `с ${firstDate} по ${lastDate}`,
    description: "Дата первого и последнего сертификата",
  };
});

// Статистические карточки, рассчитанные по актуальному реестру.
const statCards = computed<Array<{
  label: string;
  value: string;
  description: string;
  icon: StatCardIcon;
}>>(() => [
  {label: "Всего сертификатов", value: store.records.length.toLocaleString("ru-RU"), description: "В реестре", icon: "document"},
  {label: "Период выдачи", value: certificatePeriod.value.value, description: certificatePeriod.value.description, icon: "calendar"},
]);

// Одновременная фильтрация по ФИО, номеру и включительному диапазону даты.
const filteredRecords = computed(() => store.records.filter(record => {
  const received = record.received.toLocaleLowerCase().includes(appliedFilters.received.toLocaleLowerCase().trim());
  const number = record.certificateNumber.toLocaleLowerCase().includes(appliedFilters.number.toLocaleLowerCase().trim());
  const date = record.issueDate.slice(0, 10);
  const start = !appliedFilters.startDate || date >= appliedFilters.startDate;
  const end = !appliedFilters.endDate || date <= appliedFilters.endDate;
  return received && number && start && end;
}));
// Вычисляем границы пагинации после фильтрации.
const totalPages = computed(() => Math.max(1, Math.ceil(filteredRecords.value.length / pageSize)));
const paginatedRecords = computed(() => filteredRecords.value.slice((page.value - 1) * pageSize, page.value * pageSize));

// Набор полей для компактного вывода в модальном окне просмотра.
const recordDetails = computed(() => {
  const record = store.selectedRecord;
  if (!record) return [];
  return [
    {label: "Дата выдачи", value: formatDate(record.issueDate)},
    {label: "Организация", value: record.organization},
    {label: "Уровень", value: record.level},
    {label: "Дополнительно", value: record.additionalInfo},
  ];
});

function formatDate(value: string) {
  // API возвращает ISO-дату; отображаем её в привычном русском формате.
  return value ? new Intl.DateTimeFormat("ru-RU").format(new Date(value)) : "—";
}
function applyFilters() {
  // Копируем значения формы, чтобы поиск был явным действием пользователя.
  Object.assign(appliedFilters, filters);
  page.value = 1;
}
function resetFilters() {
  // Очищаем форму и тут же возвращаем таблицу к полному реестру.
  Object.assign(filters, {received: "", number: "", startDate: "", endDate: ""});
  applyFilters();
}
function onFileSelected(event: Event) {
  // После передачи файла очищаем input: тот же файл можно выбрать повторно.
  const input = event.target as HTMLInputElement;
  const file = input.files?.[0];
  if (file) void store.importExcel(file);
  input.value = "";
}
function confirmDelete(id: number) {
  // Удаление необратимо, поэтому запрашиваем подтверждение до вызова API.
  if (window.confirm("Удалить эту запись сертификата?")) void store.remove(id);
}

// Если после фильтрации текущая страница исчезла, возвращаемся на последнюю доступную.
watch(totalPages, value => { if (page.value > value) page.value = value; });
// При первом открытии страницы получаем актуальный реестр.
onMounted(() => void store.load());
</script>

<style scoped>
/* Общие классы, чтобы поля и кнопки таблицы выглядели единообразно. */
.input-field { @apply h-12 w-full rounded-xl border border-gray-200 bg-white px-3 text-text-dark outline-none transition placeholder:text-silver focus:border-primary focus:ring-2 focus:ring-primary/20 dark:border-gray-700 dark:bg-gray-900 dark:text-white; }
.action-button { @apply inline-flex h-9 w-9 items-center justify-center rounded-lg text-text-dark transition hover:bg-gray-100 dark:text-white dark:hover:bg-gray-800; }
.pagination-button { @apply inline-flex h-9 w-9 items-center justify-center rounded-lg border border-gray-200 text-xl transition hover:border-primary hover:text-primary disabled:cursor-not-allowed disabled:opacity-40 dark:border-gray-700; }
</style>
