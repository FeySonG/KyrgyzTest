import axios from "axios";
import {
    ChangeUserLoginDto,
    ChangeUserNameDto,
    ChangeUserPasswordDto,
    ChangeUserRoleDto,
    UserDto
} from "@/types/userTypes/userType";

const api = axios.create({
    baseURL: `${import.meta.env.VITE_USER_API_URL}/api-users`,
    withCredentials: true,
})

export async function getListUsers() : Promise<UserDto[]>{
    const  response = await api.get<UserDto[]>("/get-all");
    return response.data
}

export async function updateUser(data:UserDto) {
       const response = await api.put("/update-user", data);
       return response.data;
}

export async function deleteUser(id: number) {
    const response = await api.delete("/delete-user", {
        params: { id }
    });
    return response.data;
}

export async function changeUserRole(payload: ChangeUserRoleDto): Promise<void> {
    await api.put("/change-user-role", payload);
}

export async function changeUserName(payload: ChangeUserNameDto): Promise<UserDto> {
    const response = await api.put("/update-user-name", payload);
    return response.data;
}

export async function changeUserPassword(payload: ChangeUserPasswordDto): Promise<void> {
    await api.put("/change-user-password", payload);
}

export async function changeUserLogin(payload: ChangeUserLoginDto): Promise<void> {
    await api.put("/change-user-login", payload);
}









