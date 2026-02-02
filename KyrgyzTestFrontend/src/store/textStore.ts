// import { defineStore } from "pinia";
// import {analyzeText, downloadAnalysisReport, downloadExcelReport, searchText} from "@/api/textApi";
// import {SearchArgs} from "@/types/types";
//
// interface WordFrequency {
//     word: string;
//     count: number;
// }
//
//
//
// interface TextStoreState {
//     mainText: string;
//     wordCount: number;
//     charCount: number;
//     words: string[];
//     frequencies: WordFrequency[];
//     searchResults: WordFrequency[];
//     loading: boolean;
//     error: string;
// }
//
// export const useTextStore = defineStore("textStore", {
//     state: (): TextStoreState => ({
//         mainText: "",
//         wordCount: 0,
//         charCount: 0,
//         words: [],
//         frequencies: [],
//         searchResults: [],
//         loading: false,
//         error: "",
//     }),
//     actions: {
//         async analyze(text: string) {
//             try {
//                 this.loading = true;
//                 this.error = "";
//
//                 const result = await analyzeText({ text });
//
//                 this.wordCount = result.wordCount;
//                 this.charCount = result.charCount;
//                 this.words = result.words;
//                 this.frequencies = result.frequencies;
//                 this.mainText = text;
//             } catch {
//                 this.error = "Ошибка при анализе текста";
//             } finally {
//                 this.loading = false;
//             }
//         },
//
//         async downloadReport() {
//             try {
//                 this.loading = true;
//                 this.error = "";
//                 await downloadAnalysisReport({ text: this.mainText });
//             } catch {
//                 this.error = "Ошибка при получении отчёта";
//             } finally {
//                 this.loading = false;
//             }
//         },
//
//         async downloadExcelReport() {
//             try {
//                 this.loading = true;
//                 this.error = "";
//                 await downloadExcelReport({ text: this.mainText });
//             } catch {
//                 this.error = "Ошибка при скачивании Excel отчёта";
//             } finally {
//                 this.loading = false;
//             }
//         },
//
//         // 🔹 Новый метод для поиска по тексту
//         async findWord(query: string) {
//             try {
//                 this.loading = true;
//                 this.error = "";
//
//                 const request = {
//                     query,
//                     words: this.frequencies,
//                 };
//
//                 const response = await searchText(request);
//
//                 console.log("Backend response:", response);
//
//                 // ✅ сохраняем найденные слова
//                 this.searchResults = response || [];
//             } catch (err: any) {
//                 this.error = err.message || "Ошибка при поиске";
//             } finally {
//                 this.loading = false;
//             }
//         }
//     }
// });

import {defineStore} from "pinia";
import {analyzeText, downloadAnalysisReport, downloadExcelReport, searchText} from "@/api/textApi";
import {ref} from "vue";
import {useAlertStore} from "@/store/alertStore";

interface WordFrequency {
    word: string;
    count: number;
}

export const useTextStore = defineStore("textStore", () => {

    const charCount = ref(0);
    const wordCount = ref(0);
    const mainText = ref<string>("");
    const frequencies = ref<WordFrequency[]>([]);
    const searchResults = ref<WordFrequency[]>([]);
    const loading = ref<boolean>(false);
    const words = ref<string[]>([]);
    const alertStore = useAlertStore();


    async function analyze(text: string) {
        try {
            loading.value = true;

            const result = await analyzeText({text});

            wordCount.value = result.wordCount;
            charCount.value = result.charCount;
            words.value = result.words;
            frequencies.value = result.frequencies;
            mainText.value = text;
        } catch (e: any) {
            alertStore.error(e.error || "Ошибка при анализе текста")
        } finally {
            loading.value = false;
        }
    }


    async function downloadReport() {
        try {
            loading.value = true;

            await downloadAnalysisReport({text: mainText.value});

        } catch (e: any) {
            alertStore.error(e.error || "Ошибка при получении отчёта")
        } finally {
            loading.value = false;
        }
    }


    async function downloadExlReport() {
        try {
            loading.value = true;

            await downloadExcelReport({text: mainText.value});
        } catch (e: any) {
            alertStore.error(e.error || "Ошибка при скачивании Excel отчёта")
        } finally {
            loading.value = false;
        }
    }


    async function findWord(query: string) {
        try {
            loading.value = true;

            const request = {
                query,
                words: frequencies.value,
            };

            const response = await searchText(request);


            // сохраняем найденные слова
            searchResults.value = response || [];
        } catch (err: any) {
            alertStore.error(err.error || "Ошибка при поиске")
        } finally {
            loading.value = false;
        }
    }

    return {
        charCount,
        wordCount,
        mainText,
        frequencies,
        searchResults,
        loading,
        words,
        analyze,
        downloadReport,
        downloadExlReport,
        findWord

    }
});