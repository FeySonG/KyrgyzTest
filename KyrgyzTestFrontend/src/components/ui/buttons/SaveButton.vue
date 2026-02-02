<template>
  <BaseButton type="submit" variant="primary">
    <slot>Сохранить</slot>
  </BaseButton>
</template>

<script setup lang="ts">
import BaseButton from "./BaseButton.vue";
</script>
