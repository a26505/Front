<script setup lang="ts">
// Icon SVGs
const icons = {
  Brain: `<svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M12 5a3 3 0 1 0-5.997.125 4 4 0 0 0-2.526 5.77 4 4 0 0 0 .556 6.588A4 4 0 1 0 12 18Z"/><path d="M12 5a3 3 0 1 1 5.997.125 4 4 0 0 1 2.526 5.77 4 4 0 0 1-.556 6.588A4 4 0 1 1 12 18Z"/><path d="M15 13a4.5 4.5 0 0 1-3-4 4.5 4.5 0 0 1-3 4"/><path d="M17.599 6.5a3 3 0 0 0 .399-1.375"/><path d="M6.003 5.125A3 3 0 0 0 6.401 6.5"/><path d="M3.477 10.896a4 4 0 0 1 .585-.396"/><path d="M19.938 10.5a4 4 0 0 1 .585.396"/><path d="M6 18a4 4 0 0 1-1.967-.516"/><path d="M19.967 17.484A4 4 0 0 1 18 18"/></svg>`,
  BarChart3: `<svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M3 3v18h18"/><path d="M18 17V9"/><path d="M13 17V5"/><path d="M8 17v-3"/></svg>`,
  Target: `<svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10"/><circle cx="12" cy="12" r="6"/><circle cx="12" cy="12" r="2"/></svg>`,
  Trophy: `<svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M6 9H4.5a2.5 2.5 0 0 1 0-5H6"/><path d="M18 9h1.5a2.5 2.5 0 0 0 0-5H18"/><path d="M4 22h16"/><path d="M10 14.66V17c0 .55-.47.98-.97 1.21C7.85 18.75 7 20.24 7 22"/><path d="M14 14.66V17c0 .55.47.98.97 1.21C16.15 18.75 17 20.24 17 22"/><path d="M18 2H6v7a6 6 0 0 0 12 0V2Z"/></svg>`,
  Video: `<svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="m16 13 5.223 3.482a.5.5 0 0 0 .777-.416V7.87a.5.5 0 0 0-.752-.432L16 10.5"/><rect x="2" y="6" width="14" height="12" rx="2"/></svg>`,
  Users: `<svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2"/><circle cx="9" cy="7" r="4"/><path d="M22 21v-2a4 4 0 0 0-3-3.87"/><path d="M16 3.13a4 4 0 0 1 0 7.75"/></svg>`,
  Award: `<svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="8" r="6"/><path d="M15.477 12.89 17 22l-5-3-5 3 1.523-9.11"/></svg>`,
  Zap: `<svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M4 14a1 1 0 0 1-.78-1.63l9.9-10.2a.5.5 0 0 1 .86.46l-1.92 6.02A1 1 0 0 0 13 10h7a1 1 0 0 1 .78 1.63l-9.9 10.2a.5.5 0 0 1-.86-.46l1.92-6.02A1 1 0 0 0 11 14z"/></svg>`,
  TrendingUp: `<svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><polyline points="22 7 13.5 15.5 8.5 10.5 2 17"/><polyline points="16 7 22 7 22 13"/></svg>`
};

const features = [
  { icon: icons.Brain, title: "Rutinas con IA", description: "Generación automática de entrenamientos personalizados según tus objetivos y nivel" },
  { icon: icons.BarChart3, title: "Análisis de Progreso", description: "Estadísticas detalladas por grupo muscular y seguimiento de evolución" },
  { icon: icons.Target, title: "Objetivos Inteligentes", description: "Define metas realistas y recibe recomendaciones basadas en tu rendimiento" },
  { icon: icons.Trophy, title: "Sistema de Rachas", description: "Mantén tu constancia y desbloquea logros por tu dedicación" },
  { icon: icons.Video, title: "Videos Explicativos", description: "Aprende la técnica correcta con tutoriales profesionales" },
  { icon: icons.Users, title: "Comunidad Activa", description: "Compara resultados con amigos y compite sanamente" },
  { icon: icons.Award, title: "Recompensas", description: "Obtén descuentos y beneficios exclusivos por tus logros" },
  { icon: icons.Zap, title: "Entrenador Virtual", description: "Asistente IA que adapta tu entrenamiento en tiempo real" },
  { icon: icons.TrendingUp, title: "Calentamiento Óptimo", description: "Cálculo inteligente del calentamiento perfecto para cada rutina" }
];
</script>

<template>
    <section id="features" class="features-section">
        <v-container>
            <div class="header-container text-center mb-16">
                 <h2 class="section-title mb-4 font-weight-bold">
                     Características <span class="text-red">Revolucionarias</span>
                 </h2>
                 <p class="section-subtitle max-w-2xl mx-auto">
                     Tecnología de vanguardia para transformar tu entrenamiento
                 </p>
            </div>

            <div class="features-grid">
                <div v-for="(feature, i) in features" :key="i" class="feature-card group">
                    <div class="icon-container mb-4" v-html="feature.icon"></div>
                    <h3 class="feature-title mb-3 font-weight-medium">{{ feature.title }}</h3>
                    <p class="feature-description">{{ feature.description }}</p>
                </div>
            </div>
        </v-container>
    </section>
</template>

<style scoped>
/* Colors */
.text-red { color: #DC2626 !important; }

/* Section */
.features-section {
    padding-top: 96px;
    padding-bottom: 96px;
    background-color: #000000;
}

/* Header */
.section-title {
    color: white;
    font-size: 2.25rem; /* Mobile */
    line-height: 1.2;
}
@media (min-width: 960px) { .section-title { font-size: 3rem; } }
@media (min-width: 1280px) { .section-title { font-size: 3.75rem; } }

.section-subtitle {
    font-size: 1.25rem;
    color: #9CA3AF;
    max-width: 42rem;
    margin-left: auto;
    margin-right: auto;
}

/* Grid */
.features-grid {
    display: grid;
    gap: 24px;
    grid-template-columns: 1fr;
}
@media (min-width: 960px) { .features-grid { grid-template-columns: repeat(2, 1fr); } }
@media (min-width: 1280px) { .features-grid { grid-template-columns: repeat(3, 1fr); } }

/* Cards */
.feature-card {
    background-color: rgba(17, 24, 39, 0.5);
    backdrop-filter: blur(4px);
    border: 1px solid #1F2937;
    border-radius: 12px;
    padding: 32px;
    transition: all 0.15s ease;
}

.feature-card:hover {
    background-color: rgba(17, 24, 39, 0.7);
    border-color: rgba(220, 38, 38, 0.5);
    transform: scale(1.05);
}

/* Icons */
.icon-container {
    color: #DC2626;
    transition: transform 0.15s ease;
}
.group:hover .icon-container {
    transform: scale(1.1);
}

/* Feature Text */
.feature-title {
    font-size: 1.25rem; /* 20px */
    color: #FFFFFF;
}

.feature-description {
    font-size: 1rem; /* 16px */
    color: #9CA3AF;
    line-height: 1.625;
}
</style>
