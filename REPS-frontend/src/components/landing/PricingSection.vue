<script setup lang="ts">
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
    <section id="pricing" class="pricing-section">
        <div class="container">
            <!-- Header -->
            <div class="header-container text-center">
                <h2 class="section-title">
                    Elige tu <span class="text-red">Plan</span>
                </h2>
                <p class="section-subtitle">
                    Empieza gratis, evoluciona cuando quieras
                </p>
            </div>

            <!-- Plans Grid -->
            <div class="plans-grid">
                <!-- Free Plan -->
                <div class="plan-card free-card">
                    <div class="card-header">
                        <h3 class="plan-name">Gratis</h3>
                        <div class="plan-price">$0</div>
                        <p class="plan-description">Para empezar tu viaje</p>
                    </div>

                    <div class="features-list">
                        <div v-for="(feature, i) in freeFeatures" :key="i" class="feature-item">
                            <!-- Star Outline Icon -->
                            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#DC2626" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                                <polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"></polygon>
                            </svg>
                            <span class="feature-text">{{ feature }}</span>
                        </div>
                    </div>

                    <button class="cta-button free-button">
                        EMPEZAR GRATIS
                    </button>
                </div>

                <!-- Pro Plan -->
                <div class="plan-card pro-card">
                    <!-- Crown Icon -->
                    <div class="crown-icon">
                        <svg width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="#EAB308" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                            <path d="m2 4 3 12h14l3-12-6 7-4-7-4 7-6-7zm3 16h14"></path>
                        </svg>
                    </div>
                    
                    <div class="card-header">
                        <h3 class="plan-name text-red">Pro</h3>
                        <div class="price-container">
                            <span class="plan-price">$9.99</span>
                            <span class="plan-period">/mes</span>
                        </div>
                        <p class="plan-description">Para resultados profesionales</p>
                    </div>

                    <div class="features-list">
                        <div v-for="(feature, i) in proFeatures" :key="i" class="feature-item">
                            <!-- Star Fill Icon -->
                            <svg width="20" height="20" viewBox="0 0 24 24" fill="#DC2626" stroke="#DC2626" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                                <polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"></polygon>
                            </svg>
                            <span class="feature-text">{{ feature }}</span>
                        </div>
                    </div>

                    <button class="cta-button pro-button">
                        HAZTE PRO
                    </button>
                </div>
            </div>

            <!-- Footer Note -->
            <p class="footer-note">
                Puedes cambiar o cancelar tu plan en cualquier momento
            </p>
        </div>
    </section>
</template>

<style scoped>
/* Reset & Base */
.pricing-section {
    /* Blend from HowItWorks (#111827) to pure black (#000000) quickly */
    /* The gradient should finish well before the cards, so we fade to black by 20% */
    background: linear-gradient(to bottom, #111827 0%, #000000 20%, #000000 100%);
    padding-top: 128px;
    padding-bottom: 128px;
    width: 100%;
}

.container {
    max-width: 1280px;
    margin: 0 auto;
    padding-left: 48px;
    padding-right: 48px;
}

/* Header */
.header-container {
    margin-bottom: 64px;
}

.section-title {
    font-size: 56px;
    font-weight: 700;
    color: #FFFFFF;
    line-height: 1.1;
    margin-bottom: 16px;
}

.text-red {
    color: #DC2626 !important;
}

.section-subtitle {
    font-size: 20px;
    font-weight: 400;
    color: #9CA3AF;
}

/* Plans Grid */
.plans-grid {
    display: grid;
    grid-template-columns: 1fr;
    gap: 32px;
    max-width: 900px;
    margin: 0 auto;
}

@media (min-width: 768px) {
    .plans-grid {
        grid-template-columns: repeat(2, 1fr);
    }
}

/* Cards Common */
.plan-card {
    background-color: #0A0A0A;
    border-radius: 20px;
    padding: 40px;
    position: relative;
    transition: all 300ms ease;
    display: flex;
    flex-direction: column;
    align-items: center; /* Center everything */
    text-align: center;
}

.card-header {
    margin-bottom: 24px;
    display: flex;
    flex-direction: column;
    align-items: center;
}

.plan-name {
    font-size: 24px;
    font-weight: 400;
    color: #FFFFFF;
    margin-bottom: 8px;
}

.plan-price {
    font-size: 48px;
    font-weight: 700;
    color: #FFFFFF;
    line-height: 1;
    margin-bottom: 4px;
}

.plan-description {
    font-size: 14px;
    font-weight: 400;
    color: #9CA3AF;
}

.features-list {
    display: flex;
    flex-direction: column;
    gap: 16px;
    margin-bottom: 32px;
    flex-grow: 1;
    width: 100%;
    align-items: flex-start; /* List items align left relative to container? No, screenshot shows left-aligned list content but centered in card? Or simply left aligned? Screenshot shows stars aligned. */
    /* Screenshot: Stars are aligned left, text is left. The BLOCK is centered or left? */
    /* Screenshot shows items aligned to the left side of the card content area. */
    align-items: flex-start; 
    padding-left: 12px;
}

.feature-item {
    display: flex;
    align-items: center;
    gap: 12px;
    text-align: left;
}

.feature-text {
    font-size: 15px;
    color: #D1D5DB;
    font-weight: 400;
}

.cta-button {
    width: 100%;
    font-size: 15px;
    font-weight: 700;
    padding: 14px 24px;
    border-radius: 8px;
    text-align: center;
    cursor: pointer;
    transition: all 300ms ease;
    text-transform: uppercase;
    letter-spacing: 0.5px;
}

/* Free Card Specifics */
.free-card {
    /* Stronger Blue Border */
    border: 2px solid #2563EB;
    /* Dark Blue background for Free Card only */
    background: linear-gradient(180deg, #0f172a 0%, #020617 100%);
}

.free-button {
    background: transparent;
    border: 1px solid #374151;
    color: #FFFFFF;
}

.free-button:hover {
    background: #1F2937;
    border-color: #1F2937;
}

/* Pro Card Specifics */
.pro-card {
    border: 1px solid #DC2626;
    background: linear-gradient(180deg, rgba(220, 38, 38, 0.1) 0%, rgba(0, 0, 0, 0) 100%); /* Subtle red gradient top */
    box-shadow: 0 0 20px rgba(220, 38, 38, 0.1);
}

.pro-card:hover {
    box-shadow: 0 0 30px rgba(220, 38, 38, 0.2);
}

.crown-icon {
    position: absolute;
    top: 24px;
    right: 24px;
}

.price-container {
    display: flex;
    align-items: baseline;
    gap: 4px;
    justify-content: center;
}

.plan-period {
    font-size: 18px;
    font-weight: 400;
    color: #9CA3AF;
}

.pro-button {
    background: #DC2626;
    border: none;
    color: #FFFFFF;
}

.pro-button:hover {
    background: #B91C1C;
}

/* Footer Note */
.footer-note {
    font-size: 14px;
    color: #6B7280;
    text-align: center;
    margin-top: 48px;
}

/* Layout adjustments */
.divider { display: none; }

</style>
