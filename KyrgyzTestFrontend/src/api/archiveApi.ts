import axios from "axios";
import {
    AnalysisResponseDto,
    RegulationDto,
    ResultSearch,
    SearchResult,
    StudentResultResponse,
    TestResultDto
} from "@/types/types";

const api = axios.create({
    baseURL: "http://localhost:5227/api-archive",
    withCredentials: true,
})

export async function searchArchiveResult(query: ResultSearch) : Promise<SearchResult> {

    const res = await api.get<SearchResult>("/search-by-name", {
        params: query
    });

    return res.data;
}

export async function getRegulationById(id: number) : Promise<RegulationDto> {
    const res = await api.get<RegulationDto>("/get-regulation-by-id", { params: id });
    return res.data;
}

export async function getStudentResults(studentId: number): Promise<StudentResultResponse> {
    const res = await api.get<StudentResultResponse>(
        "/get-by-student-id",
        {
            params: { id: studentId }
        }
    );

    return res.data;
}