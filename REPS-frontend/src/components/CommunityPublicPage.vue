<script setup lang="ts">
import { ref } from 'vue';
import NavBar from '@/components/landing/NavBar.vue';
import FooterSection from '@/components/landing/FooterSection.vue';
import CommunityHeader from './community/CommunityHeader.vue';
import CommunityValues from './community/CommunityValues.vue';
import CommunityOffers from './community/CommunityOffers.vue';
import CommunityCTA from './community/CommunityCTA.vue';
import AuthDialog from '@/components/landing/AuthDialog.vue';

const showSignUp = ref(false);
</script>

<template>
  <v-app class="bg-black">
    <NavBar @open-signup="showSignUp = true" />
    
    <v-main class="pt-0">
        <div class="page-wrapper">
            <v-container>
                <CommunityHeader />
                <CommunityValues />
                <CommunityOffers />
                <CommunityCTA @open-signup="showSignUp = true" />
            </v-container>
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
    background: linear-gradient(to bottom, #111827 0%, #020205 20%, #000000 40%);
    min-height: 100vh;
    width: 100%;
    padding-top: 80px;
    overflow-x: hidden;
}

.text-red { color: #DC2626 !important; }
</style>
