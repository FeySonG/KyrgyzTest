<template>
  <MainLayout>
    <div class="bg-pearl dark:bg-warm-gray w-full flex min-h-screen">

      <!-- Sidebar -->
      <Sidebar :items="userProfileItems">
        <template #footer>
          <button
              @click="handleLogout"
              class="text-red-600 px-6 py-3 border-2 border-red-600 rounded
                   hover:bg-red-600 hover:text-pearl"
          >
            <span class="flex items-center gap-2">
              <ArrowLeftStartOnRectangleIcon class="w-6 h-6" />
              <span>Выйти</span>
            </span>
          </button>
        </template>
      </Sidebar>

      <!-- Content -->
      <div class="w-full mx-auto py-8 px-4">
        <h1 class="text-2xl font-semibold mb-6 dark:text-pearl">
          Профиль пользователя {{ user?.firstName }}
        </h1>

        <BaseCard width="w-1/2">
          <InfoRow label="Имя">{{ user?.firstName }}</InfoRow>
          <InfoRow label="Фамилия">{{ user?.lastName }}</InfoRow>
          <InfoRow label="Отчество">{{ user?.middleName }}</InfoRow>
          <InfoRow label="Логин">{{ user?.login }}</InfoRow>
          <InfoRow label="Роль">{{ user?.role }}</InfoRow>

          <template #actions>
            <DropdownMenu>
              <template #menu>
                <button
                    class="block w-full px-4 py-2 text-left text-sm hover:bg-gray-100"
                    @click="openEditUser"
                >
                  Редактировать ФИО
                </button>

                <button
                    class="block w-full px-4 py-2 text-left text-sm hover:bg-gray-100"
                    @click="openChangeLogin"
                >
                  Изменить логин
                </button>

                <button
                    class="block w-full px-4 py-2 text-left text-sm text-red-600 hover:bg-red-50"
                    @click="openChangePassword"
                >
                  Изменить пароль
                </button>
              </template>
            </DropdownMenu>
          </template>
        </BaseCard>
      </div>

      <!-- Drawer -->
      <Drawer v-model="drawerOpen" :mode="drawerTitle">
        <component
            :is="drawerComponent"
            :user="selectedUser"
            @save="handleSave"
            @close="closeDrawer"
        />
      </Drawer>

    </div>
  </MainLayout>
</template>

<script setup lang="ts">
import {ref, shallowRef} from "vue";
import { storeToRefs } from "pinia";
import { useRouter } from "vue-router";

import MainLayout from "@/layouts/MainLayout.vue";
import Sidebar from "@/components/Sidebar.vue";
import DropdownMenu from "@/components/ui/DropdownMenu.vue";
import Drawer from "@/components/ui/Drawer.vue";
import BaseCard from "@/components/ui/cards/BaseCard.vue";
import InfoRow from "@/components/ui/cards/InfoRow.vue";

import UserEditForm from "@/components/users/UserEditForm.vue";
import UserChangePasswordForm from "@/components/users/UserChangePasswordForm.vue";

import { ArrowLeftStartOnRectangleIcon } from "@heroicons/vue/16/solid";
import { userProfileItems } from "@/types/sidebarItems";
import type { CurrentUser } from "@/types/types";
import type {ChangeUserLoginDto, ChangeUserNameDto, ChangeUserPasswordDto} from "@/types/userTypes/userType";

import { useAuthStore } from "@/store/auth";
import { useUserStore } from "@/store/userStore";
import ChangeUserLoginForm from "@/components/users/ChangeUserLoginForm.vue";

type DrawerType = "edit-user" | "change-password" | "change-login" | null;


const authStore = useAuthStore();
const router = useRouter();
const userStore = useUserStore();

const { user } = storeToRefs(authStore);

const drawerOpen = ref(false);
const drawerType = ref<DrawerType>(null);
const drawerComponent = shallowRef<typeof UserEditForm | typeof UserChangePasswordForm | typeof ChangeUserLoginForm | null>(null);
const selectedUser = ref<CurrentUser | null>(null);
const drawerTitle = ref<string>("<UNK>");

// ---------- Drawer logic ----------

function openEditUser() {
  if (!user.value) return;

  drawerTitle.value = "Редактировать ФИО";
  selectedUser.value = user.value;
  drawerType.value = "edit-user";
  drawerComponent.value = UserEditForm;
  drawerOpen.value = true;
}

function openChangePassword() {
  if (!user.value) return;

  drawerTitle.value = "Изменить пароль";
  selectedUser.value = user.value;
  drawerType.value = "change-password";
  drawerComponent.value = UserChangePasswordForm;
  drawerOpen.value = true;
}

function openChangeLogin(){
  if(!user.value) return;

  drawerTitle.value = "Изменить логин";
  selectedUser.value = user.value;
  drawerType.value = "change-login";
  drawerComponent.value = ChangeUserLoginForm;
  drawerOpen.value = true;

}

async function handleSave(
    payload: ChangeUserNameDto | ChangeUserPasswordDto | ChangeUserLoginDto
) {
  if (!drawerType.value) return;

  if (drawerType.value === "edit-user") {
    await userStore.updateName(payload as ChangeUserNameDto);
    await authStore.fetchUserFromServer();
  }

  if (drawerType.value === "change-password") {
    await userStore.updatePassword(payload as ChangeUserPasswordDto);
  }

  if (drawerType.value === "change-login") {
    const request = payload as ChangeUserLoginDto;
    request.id = user.value!.id;

    await userStore.updateLogin(request);
    await authStore.fetchUserFromServer();
  }

  closeDrawer();
}

function closeDrawer() {
  drawerOpen.value = false;
  drawerType.value = null;
  drawerComponent.value = null;
  selectedUser.value = null;
}

// ---------- Auth ----------

async function handleLogout() {
  await authStore.logout();
  await router.push("/login");
}
</script>
