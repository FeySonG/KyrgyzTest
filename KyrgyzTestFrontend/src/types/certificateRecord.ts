/** Запись реестра сертификатов, возвращаемая CertRecordController. */
export interface CertificateRecord {
    /** Идентификатор записи для просмотра и удаления. */
    id: number;
    /** ФИО получателя сертификата. */
    received: string;
    /** Организация получателя, если она была указана при импорте. */
    organization: string | null;
    /** Уникальный номер сертификата. */
    certificateNumber: string;
    /** Уровень сертификата, если он передан в исходном файле. */
    level: string | null;
    /** Дата выдачи в ISO-формате, которую возвращает API. */
    issueDate: string;
    /** Дополнительные сведения из реестра. */
    additionalInfo: string | null;
}
