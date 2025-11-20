<template>
  <button
      @click="toggleTheme"
      class="p-3 rounded-full bg-primary dark:bg-primary-dark hover:bg-primary-dark dark:hover:bg-primary transition"
  >
    <component
        :is="isDark ? MoonIcon : SunIcon"
        class="w-6 h-6 text-gray-800 dark:text-gray-200"
    />
  </button>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import {MoonIcon, SunIcon} from "@heroicons/vue/16/solid/index.js";

const isDark = ref(false);

const toggleTheme = () => {
  isDark.value = !isDark.value;
  if (isDark.value) {
    document.documentElement.classList.add('dark');
    localStorage.setItem('theme', 'dark');
  } else {
    document.documentElement.classList.remove('dark');
    localStorage.setItem('theme', 'light');
  }
};

onMounted(() => {
  const savedTheme = localStorage.getItem('theme');
  if (savedTheme === 'dark') {
    isDark.value = true;
    document.documentElement.classList.add('dark');
  }
});
</script>