<template>
  <form class="space-y-4" @submit.prevent="submit">
    <!-- Фамилия -->
    <div>
      <label class="block text-sm text-gray-600 dark: text-pearl mb-1">Фамилия</label>
      <input
          v-model="form.lastName"
          class="w-full border px-3 py-2 rounded"
          required
      />
    </div>

    <!-- Имя -->
    <div>
      <label class="block text-sm text-gray-600 dark: text-pearl mb-1">Имя</label>
      <input
          v-model="form.firstName"
          class="w-full border px-3 py-2 rounded"
          required
      />
    </div>

    <!-- Отчество -->
    <div>
      <label class="block text-sm text-gray-600 dark: text-pearl mb-1">Отчество</label>
      <input
          v-model="form.middleName"
          class="w-full border px-3 py-2 rounded"
          placeholder="Необязательно"
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
import { reactive, watch } from "vue";
import { CurrentUser } from "@/types/types";
import { ChangeUserNameDto } from "@/types/userTypes/userType";
import CancelButton from "@/components/ui/buttons/CancelButton.vue";
import SaveButton from "@/components/ui/buttons/SaveButton.vue";

const props = defineProps<{
  user: CurrentUser | null;
}>();

const emit = defineEmits<{
  (e: "save", payload: ChangeUserNameDto): void;
  (e: "close"): void;
}>();

// форма полностью соответствует интерфейсу CurrentUser
const form = reactive<ChangeUserNameDto>({
  firstName: "",
  lastName: "",
  middleName: "",
});

// когда приходит пользователь — заполняем форму
watch(
    () => props.user,
    (u) => {
      if (!u) return;
      Object.assign(form, u);
    },
    { immediate: true }
);

function submit() {
  emit("save", { ...form }); // отправляем наружу полный CurrentUser
}
</script>
