<script setup lang="ts">
const steps = [
  {
    step: "01",
    title: "Crea tu Perfil",
    description: "Define tus objetivos, nivel y preferencias de entrenamiento"
  },
  {
    step: "02",
    title: "Rutina Personalizada",
    description: "La IA genera tu plan de entrenamiento perfecto"
  },
  {
    step: "03",
    title: "Entrena y Registra",
    description: "Sigue tu rutina y registra tu progreso automáticamente"
  },
  {
    step: "04",
    title: "Evoluciona",
    description: "Recibe ajustes inteligentes basados en tu rendimiento"
  }
];
</script>

<template>
    <section id="how-it-works" class="how-it-works-section">
        <v-container>
            <!-- Header -->
            <div class="text-center mb-16 header-container">
                <h2 class="section-title mb-4 font-weight-bold">
                    Cómo <span class="text-red">Funciona</span>
                </h2>
                <p class="section-subtitle">
                    Tu camino hacia el éxito en 4 pasos simples
                </p>
            </div>

            <!-- Steps Grid -->
            <div class="steps-grid">
                <div v-for="(item, index) in steps" :key="index" class="text-center">
                    <div class="step-number mb-4">{{ item.step }}</div>
                    <h3 class="step-title mb-3 font-weight-bold">{{ item.title }}</h3>
                    <p class="step-description">{{ item.description }}</p>
                </div>
            </div>
        </v-container>
    </section>
</template>

<style scoped>
/* Colors */
.text-red {
    color: #DC2626 !important;
}

/* Section Background & Padding */
.how-it-works-section {
    padding-top: 96px;
    padding-bottom: 96px;
    background: linear-gradient(to bottom, #000000, #111827);
}

/* Header */
.header-container {
    margin-bottom: 64px;
}

.section-title {
    color: white;
    /* Responsive font sizes handled by media queries if not using clamp/fluid typography, 
       but here mapped to closest Vuetify/standard breakpoints for the requested sizes */
    font-size: 2.25rem; /* Mobile default */
    line-height: 1.2;
}

.section-subtitle {
    font-size: 1.25rem; /* 20px */
    color: #9CA3AF;
}

/* Grid Layout */
.steps-grid {
    display: grid;
    gap: 32px;
    grid-template-columns: 1fr; /* Mobile */
}

/* Responsive Styles matching Tailwind breakpoints */
@media (min-width: 960px) { /* md equivalent in Vuetify terms roughly */
    .steps-grid {
        grid-template-columns: repeat(2, 1fr);
    }
    .section-title {
        font-size: 3rem;
    }
}

@media (min-width: 1280px) { /* lg equivalent */
    .steps-grid {
        grid-template-columns: repeat(4, 1fr);
    }
    .section-title {
        font-size: 3.75rem; /* 60px */
    }
}

/* Step Items */
.step-number {
    font-size: 60px; /* 3.75rem */
    color: #DC2626;
    line-height: 1;
}

.step-title {
    font-size: 24px; /* 1.5rem */
    color: white;
}

.step-description {
    font-size: 16px; /* 1rem */
    color: #9CA3AF;
}
</style>
