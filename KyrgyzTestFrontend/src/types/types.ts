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