<script setup lang="ts">
import NavBar from '@/components/landing/NavBar.vue';
import FooterSection from '@/components/landing/FooterSection.vue';
import AboutHeader from './about/AboutHeader.vue';
import AboutMission from './about/AboutMission.vue';
import AboutTeam from './about/AboutTeam.vue';
import AboutContactInfo from './about/AboutContactInfo.vue';
import AboutContactForm from './about/AboutContactForm.vue';
import AuthDialog from '@/components/landing/AuthDialog.vue';
import { ref } from 'vue';

const showSignUp = ref(false);
</script>

<template>
  <v-app class="bg-black">
    <NavBar @open-signup="showSignUp = true" />
    
    <v-main class="pt-0"> <!-- Removed default padding, handled by components -->
        <div class="page-wrapper">
            <AboutHeader />
            <AboutMission />
            <AboutTeam />
            
            <!-- Contact Section (Shared Wrapper) -->
            <div class="contact-section mb-16 px-4">
                 <v-container>
                    <h2 class="section-title text-center text-white mb-4">
                        Contacta con <span class="text-red">Nosotros</span>
                    </h2>
                    <p class="subtitle text-center text-grey-lighten-1 mb-16 mx-auto">
                        ¿Tienes alguna pregunta o sugerencia? Nos encantaría escucharte
                    </p>

                    <div class="grid lg:grid-cols-2 gap-8 items-start max-w-7xl mx-auto">
                        <!-- Left: Info Cards -->
                        <div class="contact-left">
                             <AboutContactInfo />
                        </div>
                        <!-- Right: Form -->
                        <div class="contact-right">
                             <AboutContactForm />
                        </div>
                    </div>
                 </v-container>
            </div>
        </div>
    </v-main>

    <FooterSection />
    
    <AuthDialog v-model="showSignUp" />
  </v-app>
</template>

<style scoped>
.bg-black {
    background-color: #000000 !important;
}

.page-wrapper {
    /* Extending a bit more per request "un pelin menos recortado" */
    background: linear-gradient(to bottom, #111827 0%, #020205 20%, #000000 40%);
    min-height: 100vh;
    width: 100%;
    overflow-x: hidden;
}

.text-red { color: #DC2626 !important; }
.text-grey-lighten-1 { color: #9CA3AF !important; }

.section-title {
    font-size: 3rem;
    font-weight: 700;
    line-height: 1.2;
}

.subtitle {
    font-size: 1.125rem;
    max-width: 40rem;
}

.grid {
    display: grid;
}

.gap-8 { gap: 2rem; }
.items-start { align-items: start; }

@media (min-width: 1024px) {
    .lg\:grid-cols-2 { grid-template-columns: repeat(2, 1fr); }
}
</style>
