<script setup lang="ts">
</script>

<template>
  <div class="about-header text-center">
    <v-container class="position-relative z-10">
      <!-- Title -->
      <h1 class="text-h2 md:text-h1 font-weight-bold text-white mb-6">
        Quiénes <span class="text-red">Somos</span>
      </h1>
      
      <!-- Subtitle -->
      <p class="subtitle text-grey-lighten-1 mx-auto">
        Revolucionando el fitness con tecnología e inteligencia artificial
      </p>
    </v-container>
  </div>
</template>

<style scoped>
.about-header {
  padding-top: 160px;
  padding-bottom: 96px;
  background-color: transparent;
  position: relative;
  overflow: hidden;
}

.text-red {
  color: #DC2626 !important;
}

.text-grey-lighten-1 {
  color: #9CA3AF !important;
}

.subtitle {
    font-size: 20px;
    line-height: 1.5;
    max-width: 700px;
}

/* Typography Overrides for exact match */
@media (min-width: 768px) {
    .text-h1 {
        font-size: 56px !important;
        line-height: 1.1 !important;
    }
}
</style>
