<script setup lang="ts">
import { ref } from 'vue';

const stats = ref([
  { number: "50K+", title: "Miembros Activos", description: "Entrenan juntos cada día" },
  { number: "10K+", title: "Rutinas Compartidas", description: "Creadas por la comunidad" },
  { number: "100M+", title: "Entrenamientos", description: "Completados en total" }
]);
</script>

<template>
  <div class="stats-section mb-16">
    <h2 class="text-h4 text-md-h3 font-weight-bold text-center text-white mb-16">
      Una Comunidad en <span class="text-red">Crecimiento</span>
    </h2>

    <v-row>
      <v-col cols="12" md="4" v-for="(stat, index) in stats" :key="index">
        <div class="stat-card text-center pa-10 h-100">
           <div class="stat-number font-weight-bold mb-2">{{ stat.number }}</div>
           <h3 class="text-h6 font-weight-bold text-white mb-2">{{ stat.title }}</h3>
           <p class="text-body-1 text-grey-lighten-1">{{ stat.description }}</p>
        </div>
      </v-col>
    </v-row>
  </div>
</template>

<style scoped>
.text-red { color: #DC2626 !important; }
.text-grey-lighten-1 { color: #9CA3AF !important; }

.stat-card {
    background: linear-gradient(180deg, #12121C 0%, #0A0A12 100%);
    border: 1px solid rgba(31, 41, 55, 0.7);
    border-radius: 12px;
    position: relative;
    /* Red glow behind via box-shadow or similar */
    box-shadow: 0 0 60px rgba(220, 38, 38, 0.15); /* approximated glow */
}

.stat-number {
    font-size: 60px;
    line-height: 1;
    color: #DC2626;
}
</style>
