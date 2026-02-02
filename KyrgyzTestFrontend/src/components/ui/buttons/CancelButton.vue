<template>
  <BaseButton variant="secondary" @click="$emit('cancel')">
    <slot>Отмена</slot>
  </BaseButton>
</template>

<script setup lang="ts">
import BaseButton from "./BaseButton.vue";

defineEmits<{
  (e: "cancel"): void;
}>();
</script>
