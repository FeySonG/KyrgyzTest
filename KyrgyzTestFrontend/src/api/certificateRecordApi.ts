import axios from "axios";
import type {CertificateRecord} from "@/types/certificateRecord";

// Отдельный axios-клиент для всех методов CertRecordController.
const api = axios.create({
    baseURL: "http://localhost:5227/api-cert-record",
    withCredentials: true,
});

export async function getCertificateRecords(): Promise<CertificateRecord[]> {
    // GET /get-all — исходные данные для таблицы и клиентской фильтрации.
    const response = await api.get<CertificateRecord[]>("/get-all");
    return response.data;
}

export async function getCertificateRecord(id: number): Promise<CertificateRecord> {
    // GET /get-by-id — загружаем полную запись перед открытием модального окна.
    const response = await api.get<CertificateRecord>("/get-by-id", {params: {id}});
    return response.data;
}

export async function importCertificateRecords(file: File): Promise<CertificateRecord[]> {
    // Контроллер ожидает multipart/form-data с полем "file".
    const formData = new FormData();
    formData.append("file", file);

    const response = await api.post<CertificateRecord[]>("/add-from-excel", formData);
    return response.data;
}

export async function deleteCertificateRecord(id: number): Promise<void> {
    // DELETE /{id} удаляет запись и возвращает HTTP 204 без тела ответа.
    await api.delete(`/${id}`);
}
