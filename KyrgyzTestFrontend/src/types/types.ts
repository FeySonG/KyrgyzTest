export interface WordFrequency {
    word: string;
    count: number;
}

// Для ответа
export interface TextAnalysisDto {
    wordCount: number;
    charCount: number;
    words: string[];
    frequency: Record<string, number>;
}

export interface AnalysisResponseDto {
    wordCount: number;
    charCount: number;
    words: string[];
    frequencies: WordFrequency[];
}

export interface TextRequest {
    text: string;
}

export interface SearchArgs {
    words: WordFrequency[];
    query: string;
}
export interface SearchResponse {
    words: WordFrequency[];
}