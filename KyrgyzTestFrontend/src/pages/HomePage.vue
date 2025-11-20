<template>
  <MainLayout>
    <div class="bg-pearl dark:bg-warm-gray w-full flex justify-between p-4">
      <TextForm/>
      <FrequencyTable/>
    </div>
  </MainLayout>
</template>

<script setup lang="ts">
import TextForm from "../components/TextForm.vue";
import FrequencyTable from "../components/FrequencyTable.vue";
import {useAuthStore} from "@/store/auth";
import {useRouter} from "vue-router";
import MainLayout from "@/layouts/MainLayout.vue";

const router = useRouter();
const authStore = useAuthStore();

// функция для кнопки
async function handleLogout() {
  try {
    await authStore.logout();
    await router.push("/login");
  } catch (err: any) {
    alert(err.message || "Ошибка выхода");
  }
}
</script>