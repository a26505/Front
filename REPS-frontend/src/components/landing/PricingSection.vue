<script setup lang="ts">
defineEmits(['open-signup']);

const freeFeatures = [
    "Rutinas básicas",
    "Seguimiento de progreso",
    "Estadísticas básicas",
    "Videos explicativos",
    "Acceso a comunidad"
];

const proFeatures = [
    "Todo lo de Gratis",
    "Rutinas con IA ilimitadas",
    "Entrenador virtual 24/7",
    "Análisis avanzado de progreso",
    "Calentamiento inteligente",
    "Descuentos exclusivos"
];
</script>

<template>
    <section id="pricing" class="py-16" style="padding-top: 96px !important; padding-bottom: 96px !important; background: linear-gradient(to bottom, #111827, #000000);">
        <v-container>
            <div class="text-center mb-16">
                <h2 class="text-h4 text-md-h3 text-lg-h2 font-weight-bold mb-4">
                    Elige tu <span class="text-red">Plan</span>
                </h2>
                <p class="text-h6 text-grey-lighten-1 font-weight-regular" style="color: #9CA3AF !important;">
                    Empieza gratis, evoluciona cuando quieras
                </p>
            </div>

            <div class="d-flex flex-column flex-md-row justify-center gap-8 mx-auto" style="max-width: 900px;">
                <!-- Free Plan -->
                <div class="pricing-card rounded-xl pa-8 w-100 bg-grey-darken-4 border-grey">
                    <div class="text-center mb-6">
                        <h3 class="text-h5 font-weight-regular mb-2">Gratis</h3>
                        <div class="text-h3 font-weight-bold mb-2">$0</div>
                        <p class="text-grey text-body-2">Para empezar tu viaje</p>
                    </div>

                    <div class="d-flex flex-column gap-3 mb-8">
                        <div v-for="(feature, i) in freeFeatures" :key="i" class="d-flex align-center gap-3">
                            <v-icon icon="mdi-star" color="red" size="small"></v-icon>
                            <span class="text-body-1">{{ feature }}</span>
                        </div>
                    </div>

                    <v-btn block variant="outlined" class="border-2" size="x-large" rounded="xl" @click="$emit('open-signup')">
                        EMPEZAR GRATIS
                    </v-btn>
                </div>

                <!-- Pro Plan -->
                <div class="pricing-card rounded-xl pa-8 w-100 position-relative border-red overflow-hidden" 
                     style="background: linear-gradient(135deg, rgba(220, 38, 38, 0.2), rgba(127, 29, 29, 0.2));">
                    
                    <div class="position-absolute top-0 right-0 ma-4">
                        <v-icon icon="mdi-crown" color="yellow" size="large"></v-icon>
                    </div>

                    <div class="text-center mb-6">
                        <h3 class="text-h5 font-weight-regular mb-2">Pro</h3>
                        <div class="d-flex align-baseline justify-center gap-1 mb-2">
                            <span class="text-h3 font-weight-bold">$9.99</span>
                            <span class="text-h6 font-weight-regular">/mes</span>
                        </div>
                        <p class="text-grey text-body-2">Para resultados profesionales</p>
                    </div>

                    <div class="d-flex flex-column gap-3 mb-8">
                        <div v-for="(feature, i) in proFeatures" :key="i" class="d-flex align-center gap-3">
                            <v-icon icon="mdi-star" color="red" size="small"></v-icon>
                            <span class="text-body-1">{{ feature }}</span>
                        </div>
                    </div>

                    <v-btn block color="red" size="x-large" rounded="xl" @click="$emit('open-signup')">
                        HAZTE PRO
                    </v-btn>
                </div>
            </div>
        </v-container>
    </section>
</template>

<style scoped>
.text-red {
    color: #DC2626 !important;
}

.border-grey {
    border: 1px solid #1F2937 !important;
}

.border-red {
    border: 1px solid #DC2626 !important;
}

.bg-grey-darken-4 {
    background-color: rgba(17, 24, 39, 0.5) !important;
}
</style>
