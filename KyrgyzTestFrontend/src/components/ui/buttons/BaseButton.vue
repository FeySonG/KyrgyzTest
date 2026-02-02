<template>
  <button
      :type="type"
      :disabled="disabled"
      :class="[
      'inline-flex items-center justify-center gap-2 px-2 py-2 rounded  font-medium transition',
      variantClasses,
      disabled && 'opacity-50 cursor-not-allowed'
    ]"
      @click="$emit('click')"
  >
    <slot />
  </button>
</template>

<script setup lang="ts">
import { computed } from "vue";

type Variant = "primary" | "secondary" | "danger" | "ghost";

const props = withDefaults(
    defineProps<{
      type?: "button" | "submit";
      variant?: Variant;
      disabled?: boolean;
    }>(),
    {
      type: "button",
      variant: "primary",
      disabled: false,
    }
);

defineEmits<{
  (e: "click"): void;
}>();

const variantClasses = computed(() => {
  switch (props.variant) {
    case "primary":
      return "bg-primary dark:bg-primary-dark text-pearl hover:bg-primary-dark dark:hover:bg-primary";
    case "secondary":
      return "border text-text-dark dark:text-pearl border-gray-300 hover:bg-gray-400";
    case "danger":
      return "bg-red-600 text-white hover:bg-red-700";
    case "ghost":
      return "text-gray-600 hover:bg-gray-100";
  }
});
</script>
