<script setup lang="ts">
import { RouterView } from 'vue-router'
import GlobalUI from './components/common/GlobalUI.vue'
import CookieConsent from './components/common/CookieConsent.vue'
</script>

<template>
  <v-app class="bg-[#000000] min-h-screen text-white selection:bg-[#DC2626] selection:text-white">
    <RouterView />
    <GlobalUI />
    <CookieConsent />
  </v-app>
</template>

<style>

/* Animations for whole web elements */
@keyframes glowPulse {
    0%, 100% { box-shadow: 0 0 15px rgba(220, 38, 38, 0.1); }
    50% { box-shadow: 0 0 25px rgba(220, 38, 38, 0.3); }
}

.animate-glow-pulse {
    animation: glowPulse 3s infinite ease-in-out;
}
</style>
