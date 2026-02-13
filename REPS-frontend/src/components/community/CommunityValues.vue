<script setup lang="ts">
import { ref } from 'vue';

const values = ref([
  {
    title: "Respeto Mutuo",
    description: "Celebramos las diferencias y apoyamos a cada miembro en su viaje personal",
    icon: "respeto"
  },
  {
    title: "Espacio Seguro",
    description: "Tolerancia cero al acoso, bullying o cualquier comportamiento negativo",
    icon: "seguro"
  },
  {
    title: "Motivación Positiva",
    description: "Inspiramos y celebramos los logros de todos, sin importar el nivel",
    icon: "motivacion"
  },
  {
    title: "Crecimiento Continuo",
    description: "Fomentamos el aprendizaje constante y el intercambio de conocimientos",
    icon: "crecimiento"
  },
  {
    title: "Competencia Sana",
    description: "Promovemos la superación personal, no la comparación destructiva",
    icon: "competencia"
  },
  {
    title: "Inclusividad",
    description: "Todos son bienvenidos, independientemente de su nivel o experiencia",
    icon: "inclusividad"
  }
]);
</script>

<template>
  <div class="values-section mb-16">
    <h2 class="text-h4 text-md-h3 font-weight-bold text-center text-white mb-16">
      Nuestros <span class="text-red">Valores</span>
    </h2>

    <v-row>
      <v-col cols="12" sm="6" md="4" v-for="(value, index) in values" :key="index">
        <div class="value-card h-100">
          <!-- Decorative Top Bar -->
          <div class="card-top-bar d-flex justify-center align-center">
             <!-- SVG Icon Container -->
             <div class="icon-wrapper">
                <!-- Inline SVGs based on type -->
               <svg v-if="value.icon === 'respeto'" class="w-10 h-10 text-red-500" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2" />
                  <circle cx="9" cy="7" r="4" />
                  <path d="M22 21v-2a4 4 0 0 0-3-3.87" />
                  <path d="M16 3.13a4 4 0 0 1 0 7.75" />
                  <!-- Heartbeat line connector simulation -->
                  <path d="M9 12 h1 l1 -2 l1 4 l1 -2 h1" stroke-width="1.5" opacity="0.8"/> 
               </svg>

               <svg v-if="value.icon === 'seguro'" class="w-10 h-10 text-red-500" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z" />
                  <path d="m9 12 2 2 4-4" />
               </svg>

               <svg v-if="value.icon === 'motivacion'" class="w-10 h-10 text-red-500" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2" />
                  <circle cx="4" cy="4" r="1" fill="#EF4444" stroke="none"/>
                  <circle cx="20" cy="4" r="1" fill="#EF4444" stroke="none"/>
               </svg>

               <svg v-if="value.icon === 'crecimiento'" class="w-10 h-10 text-red-500" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <path d="M12 22v-14" />
                  <path d="M5 22v-8" />
                  <path d="M19 22v-10" />
                  <path d="M2 22h20" />
                  <path d="M12 8l-3-3m6 0l-3 3" /> <!-- Leaves simulation -->
               </svg>

               <svg v-if="value.icon === 'competencia'" class="w-10 h-10 text-red-500" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <path d="M6 9H4.5a2.5 2.5 0 0 1 0-5H6" />
                  <path d="M18 9h1.5a2.5 2.5 0 0 0 0-5H18" />
                  <path d="M4 22h16" />
                  <path d="M10 14.66V17c0 .55-.47.98-.97 1.21C7.85 18.75 7 20.24 7 22" />
                  <path d="M14 14.66V17c0 .55.47.98.97 1.21C16.15 18.75 17 20.24 17 22" />
                  <path d="M18 2H6v7a6 6 0 0 0 12 0V2Z" />
               </svg>

               <svg v-if="value.icon === 'inclusividad'" class="w-10 h-10 text-red-500" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2" />
                  <circle cx="9" cy="7" r="4" />
                  <path d="M22 21v-2a4 4 0 0 0-3-3.87" />
                  <path d="M16 3.13a4 4 0 0 1 0 7.75" />
               </svg>
             </div>
          </div>
          
          <div class="card-content pa-8">
            <h3 class="text-h6 font-weight-bold text-white mb-4">{{ value.title }}</h3>
            <p class="text-body-1 text-grey-lighten-1" style="line-height: 1.6;">{{ value.description }}</p>
          </div>
        </div>
      </v-col>
    </v-row>
  </div>
</template>

<style scoped>
.text-red {
    color: #DC2626 !important;
}

.w-10 { width: 40px; }
.h-10 { height: 40px; }

.value-card {
    background: linear-gradient(180deg, #12121C 0%, #0A0A12 100%);
    border: 1px solid rgba(31, 41, 55, 0.7);
    border-radius: 12px;
    overflow: hidden;
    transition: all 0.3s ease-in-out;
    position: relative;
}

.value-card:hover {
    border-color: rgba(220, 38, 38, 0.4);
    box-shadow: 0 0 20px rgba(220, 38, 38, 0.1);
}

.card-top-bar {
    height: 96px;
    background: transparent;
    position: relative;
}

/* Vertical gradient overlay on top bar */


.text-grey-lighten-1 {
    color: #9CA3AF !important;
}
</style>
