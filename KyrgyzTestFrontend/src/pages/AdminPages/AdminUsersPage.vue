<template>
  <div class="p-6">

    <!-- CARD -->
    <div
        class="
        rounded-2xl
        shadow-xl
        bg-white
        dark:bg-gray-900
        border
        border-gray-200
        dark:border-gray-700
        overflow-visible
      "
    >

      <!-- HEADER -->
      <div
          class="
          px-6 py-4
          border-b
          dark:border-gray-700
          flex
          justify-between
          items-center
        "
      >
        <h2 class="text-lg font-semibold text-gray-800 dark:text-gray-200">
          Пользователи
        </h2>
      </div>

      <!-- TABLE -->
      <div class="custom-scrollbar overflow-x-auto overflow-y-visible">

        <table class="w-full text-l">

          <!-- THEAD -->
          <thead
              class="
              bg-gray-50
              dark:bg-gray-800
              text-gray-500
              dark:text-gray-400
            "
          >
          <tr>
            <th class="px-6 py-3 text-left">
              ФИО
            </th>

            <th class="px-6 py-3 text-left">
              Роль
            </th>

            <th class="px-6 py-3 text-left">
              Логин
            </th>

            <th class="px-6 py-3 text-right">
              Действия
            </th>
          </tr>
          </thead>

          <!-- TBODY -->
          <tbody class="divide-y dark:divide-gray-700">

          <!-- Loading -->
          <tr v-if="userStore.loading">
            <td
                colspan="4"
                class="px-6 py-10 text-center text-gray-400"
            >
              ⏳ Загрузка пользователей...
            </td>
          </tr>

          <!-- Empty -->
          <tr v-else-if="userStore.users.length === 0">
            <td
                colspan="4"
                class="px-6 py-10 text-center text-gray-400"
            >
              Пользователи не найдены
            </td>
          </tr>

          <!-- DATA -->
          <tr
              v-else
              v-for="user in userStore.users"
              :key="user.id"
              class="
              transition-all
              duration-200
              hover:bg-gray-50
              dark:hover:bg-gray-800
            "
          >

            <!-- NAME -->
            <td
                class="
                px-6 py-4
                font-medium
                text-gray-800
                dark:text-gray-200
              "
            >
              {{ fullName(user) }}
            </td>

            <!-- ROLE -->
            <td class="px-6 py-4">
              <span
                  class="
                  px-2 py-1
                  rounded-lg
                  text-xs
                  font-medium
                "
                  :class="
                  user.role === 'Admin'
                    ? 'bg-red-100 text-red-600'
                    : 'bg-blue-100 text-blue-600'
                "
              >
                {{ user.role }}
              </span>
            </td>

            <!-- LOGIN -->
            <td
                class="
                px-6 py-4
                text-gray-600
                dark:text-gray-300
              "
            >
              {{ user.login }}
            </td>

            <!-- ACTIONS -->
            <td
                class="
                px-6 py-4
                text-right
                relative
              "
            >
              <div class="relative inline-block z-50">

                <DropdownMenu>

                  <template #menu>

                    <button
                        class="
                        block
                        w-full
                        text-left
                        px-4 py-2
                        text-sm
                        hover:bg-gray-100
                        dark:hover:bg-gray-700
                      "
                        @click="openEditRole(user)"
                    >
                      Изменить роль
                    </button>

                    <button
                        class="
                        block
                        w-full
                        text-left
                        px-4 py-2
                        text-sm
                        text-red-600
                        hover:bg-red-50
                        dark:hover:bg-red-900/30
                      "
                        @click="confirmDelete(user)"
                    >
                      Удалить
                    </button>

                  </template>

                </DropdownMenu>

              </div>
            </td>

          </tr>

          </tbody>
        </table>
      </div>
    </div>

    <!-- DRAWER -->
    <Drawer
        v-model="drawerOpen"
        :mode="drawerTitle"
    >
      <component
          :is="drawerComponent"
          :user="selectedUser"
          @close="closeDrawer"
          @saved="onSaved"
      />
    </Drawer>

    <!-- CONFIRM -->
    <ConfirmWindow
        :visible="confirmVisible"
        title="Подтверждение удаления"
        :message="
        userToDelete
          ? `Вы уверены, что хотите удалить пользователя ${fullName(userToDelete)}?`
          : ''
      "
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
