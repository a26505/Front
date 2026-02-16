import { defineStore } from 'pinia';
import { ref } from 'vue';
import axios from 'axios';

// Define the API URL based on launchSettings.json usually, or environment variable.
// For now, hardcoding based on previous research: http://localhost:5148
const API_URL = 'http://localhost:5148/api/Auth';

export const useAuthStore = defineStore('auth', () => {
    const user = ref(null);
    const isAuthenticated = ref(false);
    const token = ref(localStorage.getItem('token') || null);

    if (token.value) {
        isAuthenticated.value = true;
        // Optionally valid token or fetch user profile here
    }

    async function login(email: string, password: string) {
        try {
            const response = await axios.post(`${API_URL}/login`, {
                email,
                password
            });

            const authToken = response.data.token;
            token.value = authToken;
            localStorage.setItem('token', authToken);
            isAuthenticated.value = true;

            // Decode token or fetch user details if needed. For now just setting a placeholder or derived data if possible.
            // Assuming the token is all we need for now.
            user.value = { email } as any;

            return true;
        } catch (error: any) {
            console.error('Login failed', error);
            throw new Error(error.response?.data?.mensaje || 'Error al iniciar sesión');
        }
    }

    async function register(userData: any) {
        try {
            const response = await axios.post(`${API_URL}/register`, {
                nombre: userData.nombre,
                email: userData.email,
                password: userData.password
            });

            const authToken = response.data.token;
            token.value = authToken;
            localStorage.setItem('token', authToken);
            isAuthenticated.value = true;

            user.value = { name: userData.nombre, email: userData.email } as any;
            return true;
        } catch (error: any) {
            console.error('Registration failed', error);
            throw new Error(error.response?.data?.mensaje || 'Error al registrarse');
        }
    }

    function logout() {
        token.value = null;
        localStorage.removeItem('token');
        isAuthenticated.value = false;
        user.value = null;
    }

    return {
        user,
        isAuthenticated,
        token,
        login,
        register,
        logout
    };
});
