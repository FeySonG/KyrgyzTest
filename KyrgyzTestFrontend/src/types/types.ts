import {UserRole} from "@/types/userTypes/userRole";

export interface WordFrequency {
    word: string;
    count: number;
}

// Для ответа
export interface TextAnalysisDto {
    wordCount: number;
    charCount: number;
    words: string[];
    text?: string | null;
    frequency: Record<string, number>;
}

export interface AnalysisResponseDto {
    wordCount: number;
    charCount: number;
    words: string[];
    text?: string | null;
    frequencies: WordFrequency[];
}

export interface TextRequest {
    text: string | null | undefined;
}

export interface RegulationDto {
    id: number;
    name?: string;
    shRegulation?: string;
}

export interface ResultSearch {
    name: string | null | undefined;
}

export interface TestStudent {
    studentId: number;
    studentName: string;
}

export interface StudentResultResponse {
    testResults: TestResultDto[];
}
export interface SearchResult {
    students: TestStudent[];
    testResults: TestResultDto[];
}

export interface TestResultDto {
    id: number;

    idTest: number;

    idFacultet: number;
    nameFacultet: string;

    idSemestr: number;
    nameSemestr: string;

    idGroup: number;
    nameGroup: string;

    idStudent: number;
    nameStudent: string;

    idDiscipline: number;
    nameDiscipline: string;

    idExamination: number;
    nameExamination: string;

    idRegulation: number;
    shRegulation: string | null;

    testType: string;
    formatTestType: string;

    attempt: number;
    rightAnswers: number;

    ball: number | null;
    ballForTextAnswers: number | null;
    ballForAudioAnswers: number | null;

    generateDate: string;
    deliveryDate: string | null;

    isHandled: boolean;
    countQuest: number;
}

export interface SearchArgs {
    words: WordFrequency[];
    query: string;
}
export interface SearchResponse {
    words: WordFrequency[];
}

export interface CurrentUser {
    id: number;
    name: string;
    firstName: string;
    lastName: string;
    middleName: string;
    login: string;
    role: UserRole;
}