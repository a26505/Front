<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();

const emit = defineEmits(['open-signup']);

// State
const isMenuOpen = ref(false);




const handleSectionClick = async (sectionId: string) => {
    isMenuOpen.value = false;
    
    if (router.currentRoute.value.name === 'home') {
        // We are already on home, just scroll
        const el = document.getElementById(sectionId);
        if (el) el.scrollIntoView({ behavior: 'smooth' });
    } else {
        // We are on another page, go to home
        await router.push('/');
        // Wait for DOM to update then scroll
        setTimeout(() => {
            const el = document.getElementById(sectionId);
            if (el) el.scrollIntoView({ behavior: 'smooth' });
        }, 100);
    }
};

const openSignUp = () => {
    emit('open-signup');
    isMenuOpen.value = false;
};
</script>

<template>
    <header class="fixed top-0 left-0 right-0 z-50 bg-black/60 backdrop-blur-md border-b border-gray-800/50">
        <nav class="container mx-auto px-6 py-4 flex items-center justify-between">
            <!-- Logo and Desktop Navigation -->
            <div class="flex items-center gap-8">
                <!-- Real Logo -->
                <router-link to="/" class="d-flex align-center text-decoration-none">
                     <img src="/potential_logo_2.png" alt="REPS Logo" style="height: 48px; width: auto;" />
                </router-link>
                
                <!-- Desktop Navigation -->
                <div class="hidden md:flex items-center gap-8">
                    <button @click="handleSectionClick('home')" class="text-sm font-medium text-white hover:text-red-600 transition-colors">Inicio</button>
                    <button @click="handleSectionClick('features')" class="text-sm font-medium text-white hover:text-red-600 transition-colors">Características</button>
                    <button @click="handleSectionClick('how-it-works')" class="text-sm font-medium text-white hover:text-red-600 transition-colors">Cómo Funciona</button>
                    <button @click="handleSectionClick('pricing')" class="text-sm font-medium text-white hover:text-red-600 transition-colors">Precios</button>
                    <button @click="router.push('/community-info')" class="text-sm font-medium text-white hover:text-red-600 transition-colors">Comunidad</button>
                    <button @click="router.push('/about')" class="text-sm font-medium text-white hover:text-red-600 transition-colors">Quiénes Somos</button>
                </div>
            </div>

            <!-- Log In Button - Desktop -->
            <button 
                @click="openSignUp"
                class="hidden md:block bg-red-600 hover:bg-red-700 rounded-full px-8 py-2 text-white font-medium transition-colors"
            >
                INICIAR SESIÓN
            </button>

            <!-- Mobile Menu Button -->
            <button 
                @click="isMenuOpen = !isMenuOpen"
                class="md:hidden text-white"
            >
                <svg v-if="isMenuOpen" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M18 6 6 18"/><path d="m6 6 12 12"/></svg>
                <svg v-else xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><line x1="4" x2="20" y1="12" y2="12"/><line x1="4" x2="20" y1="6" y2="6"/><line x1="4" x2="20" y1="18" y2="18"/></svg>
            </button>
        </nav>

        <!-- Mobile Menu -->
        <div v-if="isMenuOpen" class="md:hidden bg-black/98 border-t border-gray-800">
            <div class="container mx-auto px-6 py-4 flex flex-col gap-4">
                <button @click="handleSectionClick('home')" class="text-left text-white hover:text-red-600 transition-colors py-2">Inicio</button>
                <button @click="handleSectionClick('features')" class="text-left text-white hover:text-red-600 transition-colors py-2">Características</button>
                <button @click="handleSectionClick('how-it-works')" class="text-left text-white hover:text-red-600 transition-colors py-2">Cómo Funciona</button>
                <button @click="handleSectionClick('pricing')" class="text-left text-white hover:text-red-600 transition-colors py-2">Precios</button>
                <button @click="router.push('/community-info'); isMenuOpen = false" class="text-left text-white hover:text-red-600 transition-colors py-2">Comunidad</button>
                <button @click="router.push('/about'); isMenuOpen = false" class="text-left text-white hover:text-red-600 transition-colors py-2">Quiénes Somos</button>
                <button 
                    @click="openSignUp"
                    class="bg-red-600 hover:bg-red-700 w-full rounded-full py-2 text-white font-medium transition-colors"
                >
                    INICIAR SESIÓN
                </button>
            </div>
        </div>
    </header>
</template>

<style scoped>
/* Ensure standard link behavior isn't overridden by global styles strangely */
a, button {
    text-decoration: none;
    font-size: 1rem;
    font-family: inherit;
    background: none;
    border: none;
    cursor: pointer;
}

/* Layout & Display */
.fixed { position: fixed; }
.top-0 { top: 0; }
.left-0 { left: 0; }
.right-0 { right: 0; }
.z-50 { z-index: 50; }
.container { width: 100%; max-width: 1280px; margin-left: auto; margin-right: auto; }
.mx-auto { margin-left: auto; margin-right: auto; }
.flex { display: flex; }
.flex-col { flex-direction: column; }
.items-center { align-items: center; }
.justify-between { justify-content: space-between; }
.block { display: block; }
.hidden { display: none; }
.w-full { width: 100%; }
.w-auto { width: auto; }
.h-12 { height: 3rem; }

/* Spacing */
.px-6 { padding-left: 1.5rem; padding-right: 1.5rem; }
.py-4 { padding-top: 1rem; padding-bottom: 1rem; }
.px-8 { padding-left: 2rem; padding-right: 2rem; }
.py-2 { padding-top: 0.5rem; padding-bottom: 0.5rem; }
.gap-4 { gap: 1rem; }
.gap-6 { gap: 1.5rem; }
.gap-8 { gap: 2rem; }

/* Typography & Visuals */
.rounded-full { border-radius: 9999px; }
.font-medium { font-weight: 500; }
.transition-colors { transition: color 0.15s, background-color 0.15s; }
.border-b { border-bottom-width: 1px; border-bottom-style: solid; }
.border-t { border-top-width: 1px; border-top-style: solid; }

/* Colors */
.text-white { color: #FFFFFF; }
.text-left { text-align: left; }
.text-red-600 { color: #DC2626 !important; }
.hover\:text-red-600:hover { color: #DC2626 !important; }
.bg-red-600 { background-color: #DC2626 !important; }
.hover\:bg-red-700:hover { background-color: #B91C1C !important; }
.bg-black\/95 { background-color: rgba(0, 0, 0, 0.95); }
.bg-black\/60 { background-color: rgba(0, 0, 0, 0.6); } /* Increased transparency */
.bg-black\/98 { background-color: rgba(0, 0, 0, 0.98); }
.border-gray-800\/50 { border-color: rgba(31, 41, 55, 0.5); }
.border-gray-800 { border-color: #1F2937; }
.backdrop-blur-sm { backdrop-filter: blur(4px); -webkit-backdrop-filter: blur(4px); }
.backdrop-blur-md { backdrop-filter: blur(12px); -webkit-backdrop-filter: blur(12px); } /* Increased blur */

/* Responsive */
@media (min-width: 768px) {
    .md\:flex { display: flex !important; }
    .md\:block { display: block !important; }
    .md\:hidden { display: none !important; }
    .md\:h-14 { height: 3.5rem; }
}
</style>
