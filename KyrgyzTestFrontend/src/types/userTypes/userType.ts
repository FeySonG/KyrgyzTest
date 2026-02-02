import {UserRole} from "@/types/userTypes/userRole";

export interface UserDto {
    id: number;
    firstName: string;
    lastName: string;
    middleName?: string | null;
    login: string;
    role: UserRole;
}

export interface ChangeUserRoleDto {
    userId: number;
    role: UserRole;
}

export interface ChangeUserNameDto {
    firstName: string;
    lastName: string;
    middleName?: string | null;
}

export interface ChangeUserPasswordDto {
    oldPassword: string;
    newPassword: string;
}

export interface ChangeUserLoginDto {
    id?: number;
    login: string;
}