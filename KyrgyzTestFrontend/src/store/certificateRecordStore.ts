import {defineStore} from "pinia";
import {ref} from "vue";
import {useAlertStore} from "@/store/alertStore";
import {
    deleteCertificateRecord,
    getCertificateRecord,
    getCertificateRecords,
    importCertificateRecords,
} from "@/api/certificateRecordApi";
import type {CertificateRecord} from "@/types/certificateRecord";

function errorMessage(error: unknown, fallback: string): string {
    // API возвращает текст ошибки в response.data; в остальных случаях показываем понятный запасной текст.
    const response = (error as {response?: {data?: unknown}})?.response?.data;
    return typeof response === "string" ? response : fallback;
}

export const useCertificateRecordStore = defineStore("certificateRecordStore", () => {
    // Реестр, отображаемый в таблице после загрузки с API.
    const records = ref<CertificateRecord[]>([]);
    // Единый индикатор для запросов, импорта и удаления.
    const loading = ref(false);
    // Запись, открытая в модальном окне просмотра; null — окно закрыто.
    const selectedRecord = ref<CertificateRecord | null>(null);
    const alertStore = useAlertStore();

    async function load() {
        // Первичная загрузка реестра и обновление после импорта.
        try {
            loading.value = true;
            records.value = await getCertificateRecords();
        } catch (error) {
            alertStore.error(errorMessage(error, "Не удалось загрузить реестр сертификатов."));
        } finally {
            loading.value = false;
        }
    }

    async function show(id: number) {
        // Запрашиваем запись по id, чтобы модальное окно всегда показывало актуальные данные.
        try {
            selectedRecord.value = await getCertificateRecord(id);
        } catch (error) {
            alertStore.error(errorMessage(error, "Не удалось загрузить данные сертификата."));
        }
    }

    async function importExcel(file: File) {
        // Импортируем Excel, затем повторно загружаем реестр с сервера.
        try {
            loading.value = true;
            const imported = await importCertificateRecords(file);
            await load();
            alertStore.success(`Импортировано записей: ${imported.length}`);
        } catch (error) {
            alertStore.error(errorMessage(error, "Не удалось импортировать Excel-файл."));
        } finally {
            loading.value = false;
        }
    }

    async function remove(id: number) {
        // После успешного удаления обновляем локальный список без дополнительного GET-запроса.
        try {
            loading.value = true;
            await deleteCertificateRecord(id);
            records.value = records.value.filter(record => record.id !== id);
            selectedRecord.value = null;
            alertStore.success("Запись сертификата удалена.");
        } catch (error) {
            alertStore.error(errorMessage(error, "Не удалось удалить запись сертификата."));
        } finally {
            loading.value = false;
        }
    }

    return {records, loading, selectedRecord, load, show, importExcel, remove};
});
