import axios from 'axios';

// Usamos ruta relativa '/api' para que el proxy de Vite reenvíe las peticiones al backend
// Esto elimina completamente los problemas de CORS
export const BASE_URL = '/api';

const api = axios.create({
    baseURL: BASE_URL,
});

// Interceptor: adjunta token JWT a cada petición
api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

// Interceptor: si 401 → limpiar sesión y redirigir (excepto en login/registro)
api.interceptors.response.use(
    (res) => res,
    (error) => {
        const isAuthRequest = error.config?.url?.includes('/Auth/login') || error.config?.url?.includes('/Auth/register');

        if (error.response?.status === 401 && !isAuthRequest) {
            localStorage.removeItem('token');
            // Solo redirigir si no estamos ya en la home
            if (window.location.pathname !== '/') {
                window.location.href = '/';
            }
        }
        return Promise.reject(error);
    }
);

// ─── AUTH ────────────────────────────────────────────────────────────
export const authApi = {
    login: (email: string, password: string) =>
        api.post('/Auth/login', { email, password }),
    register: (nombre: string, email: string, password: string) =>
        api.post('/Auth/register', { nombre, email, password }),
};

// ─── USUARIOS ────────────────────────────────────────────────────────
export const usuariosApi = {
    getMiPerfil: () => api.get('/Usuarios/perfil'),
    updatePerfil: (data: {
        nombre?: string;
        avatarId?: string;
        biografia?: string;
        esPerfilPublico?: boolean;
        mostrarEstadisticas?: boolean;
        rankingVisible?: boolean;
    }) => api.put('/Usuarios/perfil', data),
    buscarAmigo: (codigo: string) => api.get(`/Usuarios/buscar/${codigo}`),
    agregarAmigo: (codigo: string) => api.post(`/Usuarios/amigos/agregar/${codigo}`),
    getMisAmigos: () => api.get('/Usuarios/amigos'),
    getSolicitudes: () => api.get('/Usuarios/amigos/solicitudes'),
    responderSolicitud: (codigoAmigo: string, aceptar: boolean) =>
        api.post('/Usuarios/amigos/responder', { codigoAmigo, aceptar }),
    updatePlan: (planId: number) => api.put('/Usuarios/plan', { planId }),
};

// ─── DASHBOARD ────────────────────────────────────────────────────────
export const dashboardApi = {
    getHome: () => api.get('/Dashboard/home'),
};

// ─── RUTINAS ────────────────────────────────────────────────────────
export const rutinasApi = {
    getMisRutinas: () => api.get('/Rutinas'),
    getComunidad: () => api.get('/Rutinas/comunidad'),
    getById: (id: number) => api.get(`/Rutinas/${id}`),
    crear: (data: any) => api.post('/Rutinas', data),
    actualizar: (id: number, data: any) => api.put(`/Rutinas/${id}`, data),
    generarIA: (data: any) => api.post('/Rutinas/generar-ia', data),
    like: (id: number) => api.post(`/Rutinas/${id}/like`),
    copiar: (id: number) => api.post(`/Rutinas/${id}/copiar`),
    eliminar: (id: number) => api.delete(`/Rutinas/${id}`),
    publicar: (id: number) => api.put(`/Rutinas/${id}/publicar`),
};

// ─── EJERCICIOS ────────────────────────────────────────────────────────
export const ejerciciosApi = {
    getAll: () => api.get('/Ejercicios'),
    crear: (data: any) => api.post('/Ejercicios', data),
    actualizar: (id: number, data: any) => api.put(`/Ejercicios/${id}`, data),
    eliminar: (id: number) => api.delete(`/Ejercicios/${id}`),
};

// ─── ENTRENAMIENTOS ────────────────────────────────────────────────────
export const entrenamientosApi = {
    getHistorial: () => api.get('/Entrenamientos'),
    finalizar: (data: any) => api.post('/Entrenamientos/finalizar', data),
};

// ─── PROGRESO ────────────────────────────────────────────────────────
export const progresoApi = {
    getMuscular: () => api.get('/Progreso/muscular'),
    getGeneral: () => api.get('/Progreso/general'),
    getAnalitica: () => api.get('/Progreso/analitica'),
};

// ─── RANKING ────────────────────────────────────────────────────────
export const rankingApi = {
    getLeaderboard: () => api.get('/Ranking/leaderboard'),
    getMiProgreso: () => api.get('/Ranking/mi-progreso'),
};

// ─── RECORDS ────────────────────────────────────────────────────────
export const recordsApi = {
    getMisRecords: () => api.get('/RecordPersonal'),
};

// ─── LOGROS ────────────────────────────────────────────────────────
export const logrosApi = {
    getMisLogros: (userId: number) => api.get(`/Logro/user/${userId}`),
};

export default api;
