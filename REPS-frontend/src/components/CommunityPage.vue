<script setup lang="ts">
import { ref } from 'vue';
import CommunityHeader from './community/CommunityHeader.vue';
import CommunityValues from './community/CommunityValues.vue';
import CommunityOffers from './community/CommunityOffers.vue';
import CommunityStats from './community/CommunityStats.vue';
import CommunityCTA from './community/CommunityCTA.vue';
import NavBar from './landing/NavBar.vue';
import FooterSection from './landing/FooterSection.vue';
import SignUpDialog from './landing/SignUpDialog.vue'; // Reuse signup dialog

const showSignUp = ref(false);
const openSignUp = () => {
    showSignUp.value = true;
};
</script>

<template>
  <v-main class="community-page-bg text-white">
    <!-- Navigation -->
    <NavBar @open-signup="openSignUp" />

    <div class="community-container pt-16 mt-16 pb-16">
        <!-- Blue Glow top left -->
        <div class="glow-effect blue-glow"></div>
        <!-- Red Glow bottom right -->
        <div class="glow-effect red-glow"></div>
        
        <!-- Particle Grid Overlay (simulated) -->
        <div class="particle-grid"></div>

        <CommunityHeader />
        
        <CommunityValues />
        
        <CommunityOffers />
        
        <CommunityStats />
        
        <CommunityCTA @open-signup="openSignUp" />
    </div>

    <!-- Footer -->
    <FooterSection />

    <!-- Sign Up Dialog -->
    <SignUpDialog v-model="showSignUp" />
  </v-main>
</template>

<style scoped>
.community-page-bg {
    background: linear-gradient(180deg, #0A0A15 0%, #050508 50%, #000000 100%);
    min-height: 100vh;
    position: relative;
    overflow-x: hidden; /* Prevent horizontal scroll from glows */
}

.community-container {
    max-width: 1360px; /* Max width 1360px as requested */
    margin-left: auto;
    margin-right: auto;
    padding-left: 24px;
    padding-right: 24px;
    position: relative;
    z-index: 1;
}

/* Glow Effects */
.glow-effect {
    position: absolute;
    width: 600px;
    height: 600px;
    border-radius: 50%;
    filter: blur(120px);
    z-index: -1;
    pointer-events: none;
}

.blue-glow {
    top: 0;
    left: -200px;
    background: rgba(37, 99, 235, 0.05); /* Blue op 0.05 - using hex #2563EB equivalent */
}

.red-glow {
    bottom: 0;
    right: -200px;
    background: rgba(220, 38, 38, 0.05); /* Red op 0.05 */
}

/* Particle/Grid Pattern simulation */
.particle-grid {
    position: absolute;
    top: 0; left: 0; right: 0; bottom: 0;
    background-image: radial-gradient(#3B82F6 1px, transparent 1px);
    background-size: 40px 40px;
    opacity: 0.03;
    z-index: -1;
    pointer-events: none;
}
</style>
