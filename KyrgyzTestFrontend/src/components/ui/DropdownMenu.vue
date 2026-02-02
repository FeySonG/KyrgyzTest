<template>
  <div class="relative inline-block" ref="root">
    <!-- Trigger -->
    <button
        type="button"
        @click="toggle"
        class="flex items-center justify-center"
    >
      <slot name="trigger">
        <EllipsisVerticalIcon
            class="w-8 h-8 text-gray-800 dark:text-gray-400"
        />
      </slot>
    </button>

    <!-- Menu -->
    <div
        v-if="open"
        class="absolute right-0 mt-2 w-60 rounded-md bg-white shadow-lg ring-1 ring-black/5 z-50"
    >
      <slot name="menu" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onBeforeUnmount } from "vue";
import { EllipsisVerticalIcon } from "@heroicons/vue/24/outline";

const open = ref(false);
const root = ref<HTMLElement | null>(null);

function toggle() {
  open.value = !open.value;
}

function onClickOutside(e: MouseEvent) {
  if (root.value && !root.value.contains(e.target as Node)) {
    open.value = false;
  }
}

onMounted(() => {
  document.addEventListener("click", onClickOutside);
});

onBeforeUnmount(() => {
  document.removeEventListener("click", onClickOutside);
});
</script>
