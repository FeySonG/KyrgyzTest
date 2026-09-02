import {defineStore} from "pinia";
import {ref} from "vue";
import {useAlertStore} from "@/store/alertStore";
import {RegulationDto, StudentResultResponse, TestResultDto, TestResultSearchDto, TestStudent} from "@/types/types";
import {getRegulationById, getStudentResults, searchArchiveResult} from "@/api/archiveApi";

export const useArchiveStore = defineStore("archiveStore", () => {

    const searchName = ref<string>("");
    const regulation = ref<RegulationDto>();
    const searchResults = ref<TestResultSearchDto[]>([]);
    const studentResults = ref<TestResultDto[]>([]);
    const studentNames = ref<TestStudent[]>([]);
    const loading = ref<boolean>(false);
    const searchCompleted = ref<boolean>(false);
    const alertStore = useAlertStore();

    async function searchResult(text: string) {
        searchCompleted.value = false;

        try {
            loading.value = true;

            const result = await searchArchiveResult({name: text});
            searchName.value = text
            searchResults.value = result.testResults
            studentNames.value =  result.students
            searchCompleted.value = true;

        } catch (e: any ) {
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

    async function getStudentResult(id: number, source: string) {

       const student = {idStudent: id, source: source};

        try{
            loading.value = true;
            const response = await getStudentResults(student);
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
        searchCompleted,
        loading
    }
});
