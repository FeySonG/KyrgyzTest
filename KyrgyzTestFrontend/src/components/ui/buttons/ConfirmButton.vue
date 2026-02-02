<template>
  <BaseButton variant="danger" @click="$emit('confirm')">
    <slot>Подтвердить</slot>
  </BaseButton>
</template>

<script setup lang="ts">
import BaseButton from "./BaseButton.vue";

defineEmits<{
  (e: "confirm"): void;
}>();
</script>
