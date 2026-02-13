<script setup lang="ts">
</script>

<template>
  <div class="header-container text-center text-white">
    <h1 class="text-h3 text-md-h2 font-weight-bold mb-4">
      Nuestra <span class="text-red">Comunidad</span>
    </h1>
    <p class="subtitle text-body-1 text-grey-lighten-1 mx-auto">
      Buscamos construir una comunidad sana, respetuosa y motivadora donde todos puedan alcanzar sus objetivos
    </p>
  </div>
</template>

<style scoped>
.header-container {
  margin-bottom: 80px;
  position: relative;
  z-index: 2;
}

.text-red {
    color: #DC2626 !important;
}

.subtitle {
    max-width: 768px;
    font-size: 1.25rem !important; /* 20px */
    line-height: 1.6;
}

/* Responsive adjustments */
@media (max-width: 600px) {
    .header-container h1 {
        font-size: 3rem !important; /* 48px */
    }
}
</style>
