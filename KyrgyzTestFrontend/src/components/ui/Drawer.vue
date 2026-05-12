<template>
  <teleport to="body">
    <transition name="fade">
      <div v-if="modelValue" class="fixed inset-0 z-50">

        <!-- OVERLAY -->
        <div
            class="absolute inset-0
                 bg-black/50 backdrop-blur-sm
                 transition"
            @click="close"
        />

        <!-- DRAWER -->
        <transition name="slide">
          <div
              v-if="modelValue"
              class="absolute right-0 top-0 h-full w-[520px]
                   bg-white/95 dark:bg-gray-900/95
                   backdrop-blur-xl
                   border-l border-white/10
                   shadow-2xl
                   flex flex-col"
          >

            <!-- HEADER -->
            <div
                class="px-5 py-4
                     flex justify-between items-center
                     border-b border-gray-200 dark:border-gray-700
                     bg-gradient-to-r
                     from-primary/90 to-primary-dark/90
                     text-white"
            >
              <slot name="title">
                <span class="font-semibold tracking-wide">
                  {{ mode }}
                </span>
              </slot>

              <!-- CLOSE BUTTON -->
              <button
                  @click="close"
                  class="w-8 h-8 flex items-center justify-center
                       rounded-lg
                       hover:bg-white/20
                       hover:scale-110
                       active:scale-95
                       transition-all"
              >
                ✕
              </button>
            </div>

            <!-- CONTENT -->
            <div
                class="flex-1 overflow-y-auto p-5
                     space-y-4
                     text-gray-700 dark:text-gray-700"
            >
              <slot :mode="mode" />
            </div>

            <!-- FOOTER -->
            <div
                v-if="$slots.footer"
                class="px-5 py-4
                     border-t border-gray-200 dark:border-gray-700
                     bg-gray-50 dark:bg-gray-800/60"
            >
              <slot name="footer" :mode="mode" />
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
