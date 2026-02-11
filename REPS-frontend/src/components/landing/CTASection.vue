<script setup lang="ts">
defineEmits(['open-signup']);
</script>

<template>
    <section class="py-16 bg-black text-center position-relative overflow-hidden">
      <!-- Background Glow -->
      <div class="glow-effect"></div>

      <v-container class="position-relative z-10">
        <h2 class="text-h3 text-md-h2 font-weight-bold mb-6 text-white" style="font-family: 'Outfit', sans-serif;">
          ¿Listo para <span class="text-red">Transformarte?</span>
        </h2>
        
        <p class="text-h6 text-grey-lighten-1 mb-10 mx-auto" style="max-width: 700px; font-family: 'Outfit', sans-serif; line-height: 1.6;">
          Únete a miles de atletas que ya están alcanzando sus metas con REPS
        </p>

        <v-btn color="red" size="x-large" rounded="xl" class="px-12 font-weight-bold box-shadow-glowing text-white" @click="$emit('open-signup')">
          COMENZAR AHORA
        </v-btn>
      </v-container>
    </section>
</template>

<style scoped>
.text-red {
    color: #DC2626 !important;
}
.glow-effect {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 600px;
    height: 600px;
    background: radial-gradient(circle, rgba(220, 38, 38, 0.15) 0%, transparent 70%);
    pointer-events: none;
    z-index: 1;
}
</style>
