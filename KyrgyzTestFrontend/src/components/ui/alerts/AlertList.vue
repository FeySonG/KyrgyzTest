<template>
  <div class="fixed top-4 left-1/2 -translate-x-1/2 z-50 w-80">
    <TransitionGroup
        name="alert"
        tag="div"
        class="space-y-2"
    >
      <AlertItem
          v-for="alert in alerts"
          :key="alert.id"
          :alert="alert"
          @close="remove(alert.id)"
      />
    </TransitionGroup>
  </div>
</template>

<script setup lang="ts">
import { storeToRefs } from "pinia";
import { useAlertStore } from "@/store/alertStore";
import AlertItem from "./AlertItem.vue";

const alertStore = useAlertStore();
const { alerts } = storeToRefs(alertStore);
const { remove } = alertStore;
</script>

<style scoped>
.alert-enter-active,
.alert-leave-active {
  transition: all 0.25s ease;
}

.alert-enter-from {
  opacity: 0;
  transform: translateY(-10px);
}

.alert-leave-to {
  opacity: 0;
  transform: translateY(-10px);
}
</style>
