<script setup lang="ts">
const stats = [
  { number: "2024", title: "Año de Fundación" },
  { number: "50K+", title: "Usuarios Activos" },
  { number: "100M+", title: "Entrenamientos Completados" }
];
</script>

<template>
  <div class="mission-section">
    <v-container>
      <!-- Title -->
      <h2 class="section-title text-center text-white mb-8">
        Nuestra <span class="text-red">Misión</span>
      </h2>

      <!-- Description -->
      <p class="mission-text text-center text-grey-lighten-2 mx-auto mb-16">
        En REPS creemos que cada persona merece alcanzar su máximo potencial físico. Por eso, combinamos la última tecnología en inteligencia artificial con décadas de conocimiento deportivo para crear la plataforma de entrenamiento más avanzada y personalizada del mercado.
      </p>

      <!-- Stats Grid -->
      <div class="stats-grid mb-16">
        <div v-for="(stat, index) in stats" :key="index" class="stat-card">
           <div class="stat-number">{{ stat.number }}</div>
           <div class="stat-title">{{ stat.title }}</div>
        </div>
      </div>
    </v-container>
  </div>
</template>

<style scoped>
.mission-section {
    background-color: transparent;
    margin-bottom: 128px; /* Spec */
}

.text-red { color: #DC2626 !important; }
.text-grey-lighten-2 { color: #D1D5DB !important; }

.section-title {
    font-size: 44px;
    font-weight: 700;
}

.mission-text {
  font-size: 18px;
  line-height: 1.7;
  max-width: 900px;
}

/* Grid */
.stats-grid {
  display: grid;
  grid-template-columns: 1fr;
  gap: 24px;
}

@media (min-width: 960px) {
  .stats-grid {
    grid-template-columns: repeat(3, 1fr);
  }
}

/* Cards */
.stat-card {
  background-color: rgba(17, 24, 39, 0.5); /* Dark blue/gray tint */
  backdrop-filter: blur(4px);
  border: 1px solid #1F2937;
  border-radius: 12px;
  padding: 48px 32px;
  text-align: center;
  transition: all 0.3s ease;
}

.stat-card:hover {
  background-color: rgba(17, 24, 39, 0.7);
  border-color: #DC2626;
  transform: translateY(-5px);
}

.stat-number {
  font-size: 48px;
  font-weight: 700;
  color: #DC2626;
  margin-bottom: 16px;
}

.stat-title {
  font-size: 18px;
  font-weight: 500;
  color: #D1D5DB;
}
</style>
