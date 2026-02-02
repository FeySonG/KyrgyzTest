<template>
  <form class="space-y-4" @submit.prevent="submit">
    <div>
      <label class="block text-sm mb-1 dark:text-pearl">Старый пароль</label>
      <input
          v-model="form.oldPassword"
          type="password"
          class="w-full border px-3 py-2 rounded"
          required
      />
    </div>

    <div>
      <label class="block text-sm mb-1 dark:text-pearl">Новый пароль</label>
      <input
          v-model="form.newPassword"
          type="password"
          class="w-full border px-3 py-2 rounded"
          required
      />
    </div>

    <div class="flex justify-end gap-2 pt-4">
      <CancelButton @click="$emit('close')"/>

      <SaveButton/>
    </div>
  </form>
</template>

<script setup lang="ts">
import { reactive } from "vue";
import {ChangeUserPasswordDto} from "@/types/userTypes/userType";
import CancelButton from "@/components/ui/buttons/CancelButton.vue";
import SaveButton from "@/components/ui/buttons/SaveButton.vue";

const emit = defineEmits<{
  (e: "save", payload: ChangeUserPasswordDto): void;
  (e: "close"): void;
}>();

const form = reactive<ChangeUserPasswordDto>({
  oldPassword: "",
  newPassword: "",
});

function submit() {
  emit("save", { ...form });
}
</script>
