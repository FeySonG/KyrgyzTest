<template>
  <div class="space-y-4">

    <div>
      <label class="block text-sm font-medium mb-1 dark:text-pearl">Роль</label>
      <select v-model="role" class="w-full border rounded px-3 py-2 text-text-dark">
        <option :value="UserRole.User">User</option>
        <option :value="UserRole.Admin">Admin</option>
      </select>
    </div>

    <div class="flex justify-end gap-2 pt-4">
      <CancelButton @click="$emit('close')"/>
      <SaveButton @click="save" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from "vue";
import { UserRole } from "@/types/userTypes/userRole";
import { useUserStore } from "@/store/userStore";
import type { UserDto } from "@/types/userTypes/userType";
import SaveButton from "@/components/ui/buttons/SaveButton.vue";
import CancelButton from "@/components/ui/buttons/CancelButton.vue";

const props = defineProps<{
  user: UserDto | null;
}>();

const emit = defineEmits<{
  (e: "close"): void;
  (e: "saved"): void;
}>();

const userStore = useUserStore();
const role = ref<UserRole>(UserRole.User);

watch(
    () => props.user,
    (u) => {
      if (u) role.value = u.role as UserRole;
    },
    { immediate: true }
);

async function save() {
  if (!props.user) return;

  await userStore.changeRole({
    userId: props.user.id,
    role: role.value
  });

  emit("saved");
}
</script>
