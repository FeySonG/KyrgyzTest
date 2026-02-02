// store/alertStore.ts
import { defineStore } from "pinia";
import type { Alert, AlertType } from "@/types/alert";

let id = 0;

export const useAlertStore = defineStore("alert", {
    state: () => ({
        alerts: [] as Alert[],
    }),

    actions: {
        show(type: AlertType, message: string, timeout = 3000) {
            const alertId = ++id;

            this.alerts.push({
                id: alertId,
                type,
                message,
            });

            if (timeout) {
                setTimeout(() => {
                    this.remove(alertId);
                }, timeout);
            }
        },

        success(message: string, timeout?: number) {
            this.show("success", message, timeout);
        },

        error(message: string, timeout?: number) {
            this.show("error", message, timeout);
        },

        warning(message: string, timeout?: number) {
            this.show("warning", message, timeout);
        },

        info(message: string, timeout?: number) {
            this.show("info", message, timeout);
        },

        remove(id: number) {
            this.alerts = this.alerts.filter(a => a.id !== id);
        },
    },
});
