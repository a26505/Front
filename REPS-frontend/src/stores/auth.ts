import { defineStore } from 'pinia';
import { ref, computed } from 'vue';
import { authApi, usuariosApi } from '../api';

export interface UserProfile {
    id: number;
    nombre: string;
    email: string;
    avatarId: string;
    codigoAmigo: string;
    fechaRegistro: string;
    rol: string;
    puntosTotales: number;
    puntosRangoGeneral: number;
    puntosLogros: number;
    nivel: number;
    rachaDias: number;
    rangoGeneral: string;
    biografia: string;
    esPro: boolean;
    esPerfilPublico: boolean;
    mostrarEstadisticas: boolean;
    rankingVisible: boolean;
}

export const useAuthStore = defineStore('auth', () => {
    const token = ref<string | null>(localStorage.getItem('token'));
    const profile = ref<UserProfile | null>(null);
    const isAuthenticated = ref<boolean>(!!token.value);

    const userName = computed(() => profile.value?.nombre ?? '');
    const userRank = computed(() => profile.value?.rangoGeneral ?? 'Bronce');
    const isPro = computed(() => profile.value?.esPro ?? false);
    const userStreak = computed(() => profile.value?.rachaDias ?? 0);
    const userId = computed(() => {
        if (!token.value) return null;
        try {
            const payload = JSON.parse(atob(token.value.split('.')[1]));
            // El claim estándar de .NET es 'nameid' o el largo
            return (
                payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] ??
                payload['nameid'] ??
                payload['sub'] ??
                null
            );
        } catch {
            return null;
        }
    });

    // Carga el perfil desde el back
    async function fetchProfile() {
        try {
            const res = await usuariosApi.getMiPerfil();
            profile.value = res.data;
        } catch (e) {
            console.warn('No se pudo cargar el perfil', e);
        }
    }

    async function login(email: string, password: string) {
        try {
            const res = await authApi.login(email, password);
            const authToken = res.data.token;
            token.value = authToken;
            localStorage.setItem('token', authToken);
            isAuthenticated.value = true;
            await fetchProfile();
            return true;
        } catch (e) {
            throw e;
        }
    }

    async function register(nombre: string, email: string, password: string) {
        try {
            const res = await authApi.register(nombre, email, password);
            const authToken = res.data.token;
            token.value = authToken;
            localStorage.setItem('token', authToken);
            isAuthenticated.value = true;
            await fetchProfile();
            return true;
        } catch (e) {
            throw e;
        }
    }

    function logout() {
        token.value = null;
        profile.value = null;
        isAuthenticated.value = false;
        localStorage.removeItem('token');
    }

    // Si existe token al inicializar, cargar perfil
    if (token.value) {
        fetchProfile();
    }

    return {
        token,
        profile,
        isAuthenticated,
        userName,
        userRank,
        isPro,
        userStreak,
        userId,
        login,
        register,
        logout,
        fetchProfile,
    };
});
