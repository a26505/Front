<script setup lang="ts">
// No script needed for footer currently
</script>

<template>
    <v-footer id="about" color="black" class="py-16 border-t border-grey-darken-3">
      <v-container>
        <v-row>
          <v-col cols="12" md="4">
            <v-img src="/logo_placeholder.svg" max-width="100" contain class="mb-4"></v-img>
            <p class="text-grey text-body-2">Revolucionando el entrenamiento con inteligencia artificial.</p>
          </v-col>
          <v-col cols="6" md="2">
            <h4 class="font-weight-bold mb-4 text-white" style="font-family: 'Outfit', sans-serif;">Producto</h4>
            <div class="d-flex flex-column gap-2">
                <a href="#features" class="text-decoration-none text-grey hover-white text-body-2">Características</a>
                <a href="#how-it-works" class="text-decoration-none text-grey hover-white text-body-2">Cómo Funciona</a>
                <a href="#pricing" class="text-decoration-none text-grey hover-white text-body-2">Precios</a>
                <a href="/community" class="text-decoration-none text-grey hover-white text-body-2">Comunidad</a>
                <a href="/about" class="text-decoration-none text-grey hover-white text-body-2">Quiénes Somos</a>
            </div>
          </v-col>
          <v-col cols="6" md="2">
            <h4 class="font-weight-bold mb-4 text-white" style="font-family: 'Outfit', sans-serif;">Recursos</h4>
            <div class="d-flex flex-column gap-2">
                <a href="#community" class="text-decoration-none text-grey hover-white text-body-2">Comunidad</a>
                <a href="#how-it-works" class="text-decoration-none text-grey hover-white text-body-2">Cómo Funciona</a>
            </div>
          </v-col>
          <v-col cols="6" md="2">
            <h4 class="font-weight-bold mb-4 text-white" style="font-family: 'Outfit', sans-serif;">Legal</h4>
             <div class="d-flex flex-column gap-2">
                <span class="text-grey hover-white text-body-2 cursor-pointer">Privacidad</span>
                <span class="text-grey hover-white text-body-2 cursor-pointer">Términos</span>
                <span class="text-grey hover-white text-body-2 cursor-pointer">Contacto</span>
            </div>
          </v-col>
        </v-row>
        <div class="border-t border-grey-darken-3 mt-8 pt-8 text-center text-grey-darken-1 text-caption">
          &copy; 2025 REPS. Todos los derechos reservados.
        </div>
      </v-container>
    </v-footer>
</template>

<style scoped>
.cursor-pointer {
    cursor: pointer;
}
.hover-white:hover {
    color: white !important;
}
</style>
