<script setup lang="ts">
import { ref } from 'vue';

const offers = ref([
  {
    title: "Conexión con Atletas",
    description: "Conecta con personas que comparten tus mismos objetivos. Añade amigos, comparte tu progreso y motívense mutuamente.",
    items: [
        "Sistema de amigos y seguimiento",
        "Comparte entrenamientos y logros",
        "Tablas de clasificación amistosas"
    ],
    icon: "conexion"
  },
  {
    title: "Rutinas Compartidas",
    description: "Accede a rutinas creadas por la comunidad y comparte tus propios entrenamientos. Inspira a otros con tu conocimiento.",
    items: [
        "Miles de rutinas para probar",
        "Comparte tus entrenamientos favoritos",
        "Valoraciones y comentarios"
    ],
    icon: "rutinas"
  },
  {
    title: "Desafíos y Retos",
    description: "Participa en desafíos semanales y mensuales. Compite sanamente y celebra tus victorias junto a la comunidad.",
    items: [
        "Desafíos variados para todos los niveles",
        "Recompensas por participación",
        "Celebración de logros comunitarios"
    ],
    icon: "desafios"
  },
  {
    title: "Apoyo y Motivación",
    description: "Recibe y da apoyo a otros miembros. Celebramos cada pequeña victoria y nos ayudamos en los momentos difíciles.",
    items: [
        "Mensajes de aliento diarios",
        "Comunidad activa y solidaria",
        "Experiencias compartidas"
    ],
    icon: "apoyo"
  }
]);
</script>

<template>
  <div class="offers-section mb-16">
    <h2 class="text-h4 text-md-h3 font-weight-bold text-center text-white mb-16">
      Qué <span class="text-red">Ofrecemos</span>
    </h2>

    <v-row>
      <v-col cols="12" md="6" v-for="(offer, index) in offers" :key="index">
        <div class="offer-card h-100">
          <div class="card-top-bar d-flex justify-center align-center">
             <div class="icon-wrapper">
               <svg v-if="offer.icon === 'conexion'" class="w-12 h-12 text-red-500" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2"></path>
                  <circle cx="9" cy="7" r="4"></circle>
                  <path d="M22 21v-2a4 4 0 0 0-3-3.87"></path>
                  <path d="M16 3.13a4 4 0 0 1 0 7.75"></path>
               </svg>

               <svg v-if="offer.icon === 'rutinas'" class="w-12 h-12 text-red-500" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"></path>
                  <polyline points="14 2 14 8 20 8"></polyline>
                  <line x1="16" y1="13" x2="8" y2="13"></line>
                  <line x1="16" y1="17" x2="8" y2="17"></line>
                  <polyline points="10 9 9 9 8 9"></polyline>
               </svg>

               <svg v-if="offer.icon === 'desafios'" class="w-12 h-12 text-red-500" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"></polygon>
               </svg>

               <svg v-if="offer.icon === 'apoyo'" class="w-12 h-12 text-red-500" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z"></path>
               </svg>
             </div>
          </div>
          
          <div class="card-content pa-8">
            <h3 class="text-h5 font-weight-bold text-white mb-4">{{ offer.title }}</h3>
            <p class="text-body-1 text-grey-lighten-1 mb-6" style="line-height: 1.6;">{{ offer.description }}</p>
            
            <ul class="offer-list pl-0">
                <li v-for="(item, i) in offer.items" :key="i" class="d-flex align-start mb-2 text-grey-lighten-2">
                    <span class="text-red mr-2">•</span>
                    {{ item }}
                </li>
            </ul>
          </div>
        </div>
      </v-col>
    </v-row>
  </div>
</template>

<style scoped>
.text-red { color: #DC2626 !important; }
.w-12 { width: 48px; }
.h-12 { height: 48px; }

.text-grey-lighten-1 { color: #9CA3AF !important; }
.text-grey-lighten-2 { color: #D1D5DB !important; }

.offer-card {
    background: linear-gradient(180deg, #12121C 0%, #0A0A12 100%);
    border: 1px solid rgba(31, 41, 55, 0.7);
    border-radius: 12px;
    overflow: hidden;
    transition: all 0.3s ease-in-out;
}

.offer-card:hover {
    border-color: rgba(220, 38, 38, 0.4);
    box-shadow: 0 0 20px rgba(220, 38, 38, 0.1); /* Glow effect */
}

/* Taller top bar for Offers */
.card-top-bar {
    height: 112px;
    background: transparent;
    position: relative;
}



.offer-list {
    list-style-type: none;
}
</style>
