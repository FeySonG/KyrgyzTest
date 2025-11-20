import axios from "axios";
import {
    TextRequest,
    TextAnalysisDto,
    WordFrequency,
    AnalysisResponseDto,
    SearchArgs,
} from "../types/types";

const api = axios.create({
    baseURL: "http://localhost:5227/api/text-works",
});

export async function analyzeText(request: TextRequest): Promise<AnalysisResponseDto> {
    const response = await api.post<TextAnalysisDto>("/text-analyze", request, {
        headers: { "Content-Type": "application/json" }
    });

    // Преобразуем Dictionary<string,int> в массив для таблицы
    const freqArray: WordFrequency[] = Object.entries(response.data.frequency).map(([word, count]) => ({
        word,
        count
    }));

    const analysisResult: AnalysisResponseDto = {
        wordCount: response.data.wordCount,
        charCount: response.data.charCount,
        words: response.data.words,
        frequencies: freqArray   // <-- здесь исправлено с frequency на frequencies
    };

    return analysisResult;
}

export async function downloadAnalysisReport(request: TextRequest) {
    const response = await api.post("/text-analyze-report", request, {
        headers: { "Content-Type": "application/json" },
        responseType: "blob" // важно для бинарного Word-файла
    });

    const blob = new Blob([response.data], {
        type: "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
    });

    const url = window.URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = "ЧастотныйАнализ.docx";
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    window.URL.revokeObjectURL(url);
}


export async function downloadExcelReport(request: TextRequest) {
    const response = await api.post(
        "/text-analyze-excel-report",
        request,
        {
            headers: {
                "Content-Type": "application/json",
            },
            responseType: "blob",
        }
    );

    const url = window.URL.createObjectURL(new Blob([response.data]));
    const link = document.createElement("a");
    link.href = url;
    link.setAttribute("download", "frequency-report.xlsx");
    document.body.appendChild(link);
    link.click();
}

export async function searchText(request: SearchArgs): Promise<WordFrequency[]> {
    try {
        const { data } = await api.post<WordFrequency[]>("/text-search", request);
        return data; // ✅ возвращаем данные напрямую
    } catch (error: any) {
        // Axios выбрасывает ошибку, если статус не 2xx
        if (error.response?.data?.error) {
            throw new Error(error.response.data.error);
        }
        throw new Error(error.message || "Ошибка при выполнении поиска");
    }
}