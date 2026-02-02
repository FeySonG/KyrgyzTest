<template>
  <div class="p-6">
    <div class="shadow rounded-lg bg-gray-50 border-4 border-primary-light dark:border-primary">
      <table class="w-full divide-y divide-gray-200 dark:bg-pearl text-gray-800">
        <thead class="bg-gray-50 dark:bg-gray-800">
        <tr>
          <th class="px-6 py-3 text-left text-sm font-medium text-gray-400">ФИО</th>
          <th class="px-6 py-3 text-left text-sm font-medium text-gray-400">Роль</th>
          <th class="px-6 py-3 text-left text-sm font-medium text-gray-400">Логин</th>
          <th class="px-6 py-3 text-right text-sm font-medium text-gray-400">Действия</th>
        </tr>
        </thead>

        <tbody class="divide-y divide-gray-200">

        <!-- Loading -->
        <tr v-if="userStore.loading">
          <td colspan="4" class="px-6 py-8 text-center text-gray-500">
            Загрузка пользователей...
          </td>
        </tr>

        <!-- Empty -->
        <tr v-else-if="userStore.users.length === 0">
          <td colspan="4" class="px-6 py-8 text-center text-gray-400">
            Пользователи не найдены
          </td>
        </tr>

        <!-- Data -->
        <tr
            v-else
            v-for="user in userStore.users"
            :key="user.id"
            class="hover:bg-gray-300"
        >
          <td class="px-6 py-4 font-medium text-gray-900">
            {{ fullName(user) }}
          </td>

          <td class="px-6 py-4 text-gray-700">
            {{ user.role }}
          </td>

          <td class="px-6 py-4 text-gray-700">
            {{ user.login }}
          </td>

          <td class="px-6 py-4 text-right">
            <DropdownMenu>
              <template #menu>
                <button
                    class="block w-full text-left px-4 py-2 text-sm hover:bg-gray-100"
                    @click="openEditRole(user)"
                >
                  Изменить роль
                </button>

                <button
                    class="block w-full text-left px-4 py-2 text-sm text-red-600 hover:bg-red-50"
                    @click="confirmDelete(user)"
                >
                  Удалить
                </button>
              </template>
            </DropdownMenu>
          </td>
        </tr>

        </tbody>
      </table>
    </div>

    <!-- Drawer -->
    <Drawer v-model="drawerOpen" :mode="drawerTitle">
      <component
          :is="drawerComponent"
          :user="selectedUser"
          @close="closeDrawer"
          @saved="onSaved"
      />
    </Drawer>

    <!-- Confirm delete -->
    <ConfirmWindow
        :visible="confirmVisible"
        title="Подтверждение удаления"
        :message="userToDelete
        ? `Вы уверены, что хотите удалить пользователя ${fullName(userToDelete)}?`
        : ''"
        @confirm="onConfirmDelete"
        @cancel="onCancelDelete"
    />
  </div>
</template>


<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useUserStore } from "@/store/userStore";
import type { UserDto } from "@/types/userTypes/userType";

import DropdownMenu from "@/components/ui/DropdownMenu.vue";
import Drawer from "@/components/ui/Drawer.vue";
import ConfirmWindow from "@/components/ui/ConfirmWindow.vue";

// 🔹 Drawer content
import RoleEditForm from "@/components/users/RoleEditForm.vue";

const userStore = useUserStore();

const drawerOpen = ref(false);
const drawerComponent = ref<any>(null);
const selectedUser = ref<UserDto | null>(null);
const drawerTitle = ref<string>("UNK");

const confirmVisible = ref(false);
const userToDelete = ref<UserDto | null>(null);

onMounted(() => {
  userStore.getUsers();
});

function fullName(user?: UserDto | null): string {
  if (!user) return "";
  return [user.lastName, user.firstName, user.middleName]
      .filter(Boolean)
      .join(" ");
}

// 🔹 Drawer logic
function openEditRole(user: UserDto) {
  drawerTitle.value = "Изменить роль"
  selectedUser.value = user;
  drawerComponent.value = RoleEditForm;
  drawerOpen.value = true;
}

// 🔹 Delete logic
function confirmDelete(user: UserDto) {
  userToDelete.value = user;
  confirmVisible.value = true;
}


function closeDrawer() {
  drawerOpen.value = false;
  drawerComponent.value = null;
  selectedUser.value = null;
}

function onSaved() {
  userStore.getUsers();
  closeDrawer();
}

async function onConfirmDelete() {
  if (!userToDelete.value) return;

  await userStore.removeUser(userToDelete.value.id);
  await userStore.getUsers();

  confirmVisible.value = false;
  userToDelete.value = null;
}

function onCancelDelete() {
  confirmVisible.value = false;
  userToDelete.value = null;
}
</script>
