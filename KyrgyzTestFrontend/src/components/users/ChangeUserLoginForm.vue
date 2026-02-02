<template>
  <form class="space-y-4" @submit.prevent="submit">
    <!-- Логин -->
    <div>
      <label class="block text-sm text-gray-600 dark:text-pearl mb-1">
        Логин
      </label>

      <input
          v-model="form.login"
          class="w-full border px-3 py-2 rounded"
          required
      />
    </div>

    <!-- Кнопки -->
    <div class="flex justify-end gap-2 pt-4">
      <CancelButton @click="$emit('close')"/>

      <SaveButton/>
    </div>
  </form>
</template>

<script setup lang="ts">
import {reactive, watch} from "vue";
import {CurrentUser} from "@/types/types";
import {ChangeUserLoginDto} from "@/types/userTypes/userType";
import SaveButton from "@/components/ui/buttons/SaveButton.vue";
import CancelButton from "@/components/ui/buttons/CancelButton.vue";

const props = defineProps<{
  user: CurrentUser | null;
}>();

const emit = defineEmits<{
  (e: "save", payload: ChangeUserLoginDto): void;
  (e: "close"): void;
}>();

const form = reactive<ChangeUserLoginDto>({
  login: "",
});

// при приходе пользователя — подставляем логин
watch(
    () => props.user,
    (u) => {
      if (!u) return;
      form.login = u.login;
    },
    {immediate: true}
);

function submit() {
  emit("save", {login: form.login});
}
</script>
