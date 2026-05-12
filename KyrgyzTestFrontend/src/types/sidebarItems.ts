import {UsersIcon, UserIcon, MagnifyingGlassIcon} from "@heroicons/vue/24/outline";
import {UserRole} from "@/types/userTypes/userRole";
import type {Component} from "vue";


export interface SidebarItem {
    label: string;
    to: string;
    icon?: Component;
    roles?: UserRole[];
}

export const adminSidebarItems: SidebarItem[] = [
    {
        label: "Пользователи",
        to: "/admin/users",
        icon: UsersIcon,
        roles: [UserRole.Admin], // ⚠️ логично только админу
    },
];

export const userProfileItems: SidebarItem[] = [
    {
        label: "Профиль",
        to: "/profile",
        icon: UserIcon,
        roles: [UserRole.User, UserRole.Admin],
    },
];

export const archiveItems: SidebarItem[] = [
    {
        label: "Поиск результатов",
        to: "/archive/search",
        icon: MagnifyingGlassIcon,
        roles: [UserRole.User, UserRole.Admin],
    },

]