import { defineStore } from 'pinia';
import { ref } from 'vue';

export const useAuthStore = defineStore('auth', () => {
    const user = ref(null);
    const isAuthenticated = ref(false);

    async function login(email: string, password: string) {
        // Simulate API call
        console.log('Logging in with', email, password);
        isAuthenticated.value = true;
        user.value = { name: 'User', email } as any;
    }

    async function register(userData: any) {
        // Simulate API call
        console.log('Registering user', userData);
        isAuthenticated.value = true;
        user.value = { name: userData.name, email: userData.email } as any;
    }

    function logout() {
        isAuthenticated.value = false;
        user.value = null;
    }

    return {
        user,
        isAuthenticated,
        login,
        register,
        logout
    };
});
