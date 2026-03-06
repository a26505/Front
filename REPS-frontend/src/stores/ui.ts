import { defineStore } from 'pinia';
import { ref } from 'vue';

export interface ToastOptions {
    message: string;
    type: 'success' | 'error' | 'warning' | 'info';
    duration?: number;
}

export interface DialogOptions {
    title: string;
    message: string;
    confirmText?: string;
    cancelText?: string;
    type?: 'danger' | 'info' | 'warning';
    onConfirm?: () => void;
    onCancel?: () => void;
}

export const useUIStore = defineStore('ui', () => {
    // Toast State
    const toast = ref<{ show: boolean; message: string; type: string }>({
        show: false,
        message: '',
        type: 'success'
    });

    const showToast = (message: string, type: 'success' | 'error' | 'warning' | 'info' = 'success', duration = 3000) => {
        toast.value = { show: true, message, type };
        setTimeout(() => {
            toast.value.show = false;
        }, duration);
    };

    // Dialog State
    const dialog = ref<{
        show: boolean;
        title: string;
        message: string;
        confirmText: string;
        cancelText: string;
        type: string;
        resolve: ((value: boolean) => void) | null;
    }>({
        show: false,
        title: '',
        message: '',
        confirmText: 'Aceptar',
        cancelText: 'Cancelar',
        type: 'info',
        resolve: null
    });

    const showConfirm = (title: string, message: string, options?: Partial<DialogOptions>): Promise<boolean> => {
        return new Promise((resolve) => {
            dialog.value = {
                show: true,
                title,
                message,
                confirmText: options?.confirmText || 'Confirmar',
                cancelText: options?.cancelText || 'Cancelar',
                type: options?.type || 'info',
                resolve
            };
        });
    };

    const showAlert = (title: string, message: string, options?: Partial<DialogOptions>): Promise<boolean> => {
        return new Promise((resolve) => {
            dialog.value = {
                show: true,
                title,
                message,
                confirmText: options?.confirmText || 'Entendido',
                cancelText: '', // Empty means no cancel button (alert mode)
                type: options?.type || 'info',
                resolve
            };
        });
    };

    const closeDialog = (result: boolean) => {
        if (dialog.value.resolve) {
            dialog.value.resolve(result);
        }
        dialog.value.show = false;
        dialog.value.resolve = null;
    };

    return {
        toast,
        showToast,
        dialog,
        showConfirm,
        showAlert,
        closeDialog
    };
});
