<script setup lang="ts">
import { ref } from 'vue';
import NavBar from './landing/NavBar.vue';
import HeroSection from './landing/HeroSection.vue';
import FeaturesSection from './landing/FeaturesSection.vue';
import HowItWorksSection from './landing/HowItWorksSection.vue';
import PricingSection from './landing/PricingSection.vue';
import CTASection from './landing/CTASection.vue';
import FooterSection from './landing/FooterSection.vue';
import AuthDialog from './landing/AuthDialog.vue';

const showSignUp = ref(false);

const openSignUp = () => {
    showSignUp.value = true;
};
</script>

<template>
  <v-main class="bg-black text-white">
    <!-- Navigation -->
    <NavBar @open-signup="openSignUp" />

    <!-- Hero Section -->
    <HeroSection @open-signup="openSignUp" />

    <!-- Features Section -->
    <FeaturesSection />

    <!-- How It Works -->
    <HowItWorksSection />

    <!-- Pricing -->
    <PricingSection @open-signup="openSignUp" />

    <!-- CTA Section -->
    <CTASection @open-signup="openSignUp" />

    <!-- Footer -->
    <FooterSection />

    <!-- Auth Dialog (Login/Register) -->
    <AuthDialog v-model="showSignUp" />

  </v-main>
</template>

<style scoped>
/* Global landing page specific overrides if needed */
</style>
