<script setup lang="ts">
</script>

<template>
  <div class="team-section">
    <v-container>
      <!-- Title -->
      <h2 class="section-title text-center text-white mb-8">
        Nuestro <span class="text-red">Equipo</span>
      </h2>

      <!-- Description -->
      <p class="team-text text-center text-grey-lighten-2 mx-auto">
        Somos un equipo multidisciplinario de desarrolladores, entrenadores certificados, nutricionistas y especialistas en IA. Trabajamos día a día para mejorar la plataforma y ayudarte a conseguir resultados reales de forma inteligente y sostenible.
      </p>
    </v-container>
  </div>
</template>

<style scoped>
.team-section {
    background-color: transparent;
    margin-bottom: 128px; /* Spec */
}

.text-red { color: #DC2626 !important; }
.text-grey-lighten-2 { color: #D1D5DB !important; }

.section-title {
    font-size: 44px;
    font-weight: 700;
}

.team-text {
  font-size: 18px;
  line-height: 1.7;
  max-width: 900px;
}
</style>
