<script setup lang="ts">
defineEmits(['open-signup']);
</script>

<template>
    <section id="community" class="py-16 bg-grey-darken-4">
      <v-container>
        <div class="text-center mb-16">
          <h2 class="text-h3 font-weight-bold mb-4" style="font-family: 'Outfit', sans-serif;">
            Únete a la <span class="text-red">Comunidad</span>
          </h2>
          <p class="text-h6 text-grey" style="max-width: 700px; margin: 0 auto; font-family: 'Outfit', sans-serif;">
            Forma parte de una comunidad de atletas comprometidos con alcanzar sus objetivos
          </p>
        </div>

        <v-row class="mb-12">
          <v-col cols="12" md="4">
            <v-card class="bg-black pa-8 text-center h-100 hover-card" variant="flat">
              <v-icon color="red" size="48" class="mb-4">mdi-account-group</v-icon>
              <h3 class="text-h5 font-weight-bold mb-3">Conecta con Amigos</h3>
              <p class="text-grey">Añade amigos, comparte entrenamientos y motívense mutuamente</p>
            </v-card>
          </v-col>
          <v-col cols="12" md="4">
            <v-card class="bg-black pa-8 text-center h-100 hover-card" variant="flat">
              <v-icon color="red" size="48" class="mb-4">mdi-trophy</v-icon>
              <h3 class="text-h5 font-weight-bold mb-3">Compite Sanamente</h3>
              <p class="text-grey">Participa en desafíos y sube en las tablas de clasificación</p>
            </v-card>
          </v-col>
          <v-col cols="12" md="4">
            <v-card class="bg-black pa-8 text-center h-100 hover-card" variant="flat">
              <v-icon color="red" size="48" class="mb-4">mdi-sparkles</v-icon>
              <h3 class="text-h5 font-weight-bold mb-3">Comparte Logros</h3>
              <p class="text-grey">Celebra tus victorias y inspira a otros con tu progreso</p>
            </v-card>
          </v-col>
        </v-row>

        <div class="text-center">
          <v-btn color="red" size="x-large" rounded="xl" class="px-10" @click="$emit('open-signup')">
            ÚNETE AHORA
          </v-btn>
        </div>
      </v-container>
    </section>
</template>

<style scoped>
.hover-card {
  transition: all 0.3s ease;
  border: 1px solid rgba(255,255,255,0.05);
}
.hover-card:hover {
  transform: translateY(-5px);
  border-color: rgba(220, 38, 38, 0.5);
  background-color: rgba(30, 30, 30, 0.8) !important;
}
.text-red {
    color: #DC2626 !important;
}
</style>
