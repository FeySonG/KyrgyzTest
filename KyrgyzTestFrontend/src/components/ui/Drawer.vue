<template>
  <teleport to="body">
    <transition name="fade">
      <div
          v-if="modelValue"
          class="fixed inset-0 z-50"
      >
        <!-- overlay -->
        <div
            class="absolute inset-0 bg-black/40"
            @click="close"
        />

        <!-- drawer -->
        <transition name="slide">
          <div
              v-if="modelValue"
              class="absolute
                 right-0
                 top-0
                 h-full
                 w-[540px]
                 bg-pearl
                 border-l-4
                 border-l-primary
                 dark:bg-warm-light
                 shadow-xl
                 flex
                 flex-col"
          >
            <!-- header -->
            <div class="px-4 py-3 border-b flex justify-between items-center bg-primary dark:primary-dark">
              <slot name="title">
              <span class="font-semibold capitalize text-pearl">
                {{ mode }}
              </span>
              </slot>

              <button @click="close">✕</button>
            </div>

            <!-- content -->
            <div class="flex-1 overflow-y-auto p-4 ">
              <slot :mode="mode"/>
            </div>

            <!-- footer -->
            <div v-if="$slots.footer" class="border-t px-4 py-3">
              <slot name="footer" :mode="mode"/>
            </div>
          </div>
        </transition>
      </div>
    </transition>
  </teleport>

</template>

<script setup lang="ts">
withDefaults(
    defineProps<{
      modelValue: boolean;
      mode?: string;
    }>(),
    {
      mode: "Окно редактирования",
    }
);

const emit = defineEmits<{
  (e: "update:modelValue", value: boolean): void;
}>();

function close() {
  emit("update:modelValue", false);
}
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.4s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

.slide-enter-active,
.slide-leave-active {
  transition: transform 0.25s ease;
}

.slide-enter-from,
.slide-leave-to {
  transform: translateX(100%);
}
</style>
