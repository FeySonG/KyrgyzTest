import {defineStore} from "pinia";
import {ref} from "vue";
import {useAlertStore} from "@/store/alertStore";
import {RegulationDto, StudentResultResponse, TestResultDto, TestStudent} from "@/types/types";
import {getRegulationById, getStudentResults, searchArchiveResult} from "@/api/archiveApi";

export const useArchiveStore = defineStore("archiveStore", () => {

    const searchName = ref<string>("");
    const regulation = ref<RegulationDto>();
    const searchResults = ref<TestResultDto[]>([]);
    const studentResults = ref<TestResultDto[]>([]);
    const studentNames = ref<TestStudent[]>([]);
    const loading = ref<boolean>(false);
    const alertStore = useAlertStore();

    async function searchResult(text: string) {
        try {
            loading.value = true;

            const result = await searchArchiveResult({name: text});
            searchName.value = text
            searchResults.value = result.testResults
            studentNames.value =  result.students

        } catch (e: any) {
            alertStore.error(e.error || "Ошибка при поиске")
        } finally {
            loading.value = false;
        }
    }

    async function getRegulation(id: number) {
        try{
            loading.value = true;
            regulation.value = await getRegulationById(id)

        } catch(e: any) {
            alertStore.error(e.error)
        } finally {
            loading.value = false;
        }
    }

    async function getStudentResult(id: number) {
        try{
            loading.value = true;
            const response = await getStudentResults(id);
            studentResults.value = response.testResults

        } catch(e: any) {
            alertStore.error(e.error)
        } finally {
            loading.value = false;
        }
    }

    return {
        searchResult,
        getRegulation,
        getStudentResult,
        testResults: searchResults,
        studentResults,
        searchName,
        regulation,
        studentNames,
        loading
    }
});