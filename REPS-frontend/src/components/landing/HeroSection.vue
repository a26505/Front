<script setup lang="ts">
// Placeholder for Athlete Image - Using the uploaded asset
const athleteImageUrl = "/hero-athlete.png"; 

const emit = defineEmits(['open-signup']);

const scrollToFeatures = () => {
    const featuresSection = document.getElementById('features');
    if (featuresSection) {
        featuresSection.scrollIntoView({ behavior: 'smooth' });
    } else {
        window.scrollBy({ top: window.innerHeight, behavior: 'smooth' });
    }
};
</script>

<template>
    <section id="home" class="relative min-h-screen flex items-end pb-0 lg:items-center pt-32 md:pt-24 overflow-hidden bg-black">
        
        <!-- DECORATION LAYER (Z-INDEX 1) -->
        <!-- Red circle decorations - Positioned behind athlete -->
        <div class="absolute right-[5%] top-1/2 -translate-y-1/2 w-[500px] h-[500px] md:w-[700px] md:h-[700px] lg:w-[850px] lg:h-[850px] rounded-full border circle-decoration pointer-events-none z-1 circle-large"></div>
        <div class="absolute right-[8%] top-1/2 -translate-y-1/2 w-[400px] h-[400px] md:w-[600px] md:h-[600px] lg:w-[750px] lg:h-[750px] rounded-full border circle-decoration pointer-events-none z-1 circle-medium"></div>

        <!-- Gradient Bottom moved to back - Z-INDEX 0 -->
        <div class="absolute bottom-0 left-0 right-0 h-48 bg-gradient-to-t from-black to-transparent z-0 pointer-events-none"></div>

        <div class="container mx-auto px-6 py-12 lg:py-12 relative z-10 w-full h-full"> 
            <!-- GRID LAYER (Z-INDEX 10 for text, Z-INDEX 20 for image) -->
            <div class="grid lg:grid-cols-2 gap-8 lg:gap-12 items-end lg:items-center h-full">
                
                <!-- Left Content (Text) - Z-INDEX 30 -->
                <div class="max-w-2xl relative z-30 lg:-ml-8">
                    <h1 class="text-h1-huge font-weight-black mb-6 lg:mb-8 text-white uppercase">
                        REPS, UNA<br />
                        NUEVA FORMA<br />
                        DE <span class="text-red-600">ENTRENAR</span>
                    </h1>
                    
                    <p class="text-base md:text-lg lg:text-xl text-gray-400 max-w-xl leading-relaxed mb-8 lg:mb-12 font-weight-light uppercase" style="letter-spacing: 1px; line-height: 1.625;">
                        UNA PLATAFORMA INTELIGENTE<br />
                        PARA ALCANZAR TU MÁXIMO POTENCIAL
                    </p>

                    <div class="flex flex-row gap-4 mb-8 lg:mb-0">
                        <button 
                            @click="$emit('open-signup')"
                            class="bg-red-600 hover:bg-red-700 text-white px-8 py-6 text-lg rounded-full group flex items-center justify-center transition-colors font-bold"
                        >
                            COMENZAR AHORA
                            <svg class="w-5 h-5 ml-2 group-hover:translate-x-1 transition-transform" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M5 12h14"/><path d="m12 5 7 7-7 7"/></svg>
                        </button>
                        <button 
                            @click="scrollToFeatures"
                            class="border-2 border-white hover:bg-white/10 px-8 py-6 text-lg rounded-full text-white font-bold transition-colors"
                        >
                            VER MÁS
                        </button>
                    </div>

                    <!-- Stats - Desktop Only -->
                    <div class="hidden lg:grid lg:grid-cols-3 gap-8 mt-16 max-w-xl relative z-30">
                        <div>
                            <div class="text-5xl mb-2 font-black text-red-600">50K+</div>
                            <div class="text-[13px] font-bold tracking-[0.15em] uppercase text-[#888888]">USUARIOS ACTIVOS</div>
                        </div>
                        <div>
                            <div class="text-5xl mb-2 font-black text-red-600">100M+</div>
                            <div class="text-[13px] font-bold tracking-[0.15em] uppercase text-[#888888]">ENTRENAMIENTOS</div>
                        </div>
                        <div>
                            <div class="text-5xl mb-2 font-black text-red-600">4.9★</div>
                            <div class="text-[13px] font-bold tracking-[0.15em] uppercase text-[#888888]">VALORACIÓN</div>
                        </div>
                    </div>
                </div>

                <!-- Right Content (Athlete Image) - Z-INDEX 20 -->
                <div class="relative flex justify-center lg:justify-end -mb-0 z-20 pointer-events-none">
                        <img 
                            :src="athleteImageUrl" 
                            alt="Athlete" 
                            class="w-full max-w-md sm:max-w-lg md:max-w-xl lg:max-w-4xl h-auto object-contain drop-shadow-2xl origin-bottom athlete-image"
                            style="mask-image: linear-gradient(to bottom, black 90%, transparent 100%);"
                        />
                    </div>
                </div>
            </div>
    </section>
</template>

<style scoped>
/* Utility Mimics for Exact Tailwind Match */
.relative { position: relative; }
.absolute { position: absolute; }
.fixed { position: fixed; }
.flex { display: flex; }
.grid { display: grid; }
.hidden { display: none; }
.block { display: block; }
.items-center { align-items: center; }
.items-end { align-items: flex-end; }
.justify-between { justify-content: space-between; }
.justify-center { justify-content: center; }
.justify-end { justify-content: flex-end; }
.flex-col { flex-direction: column; }
.flex-row { flex-direction: row; }
.rounded-full { border-radius: 9999px; }
.border-2 { border-width: 2px; }
.border { border-width: 1px; }
.z-1 { z-index: 1; }
.z-10 { z-index: 10; }
.z-20 { z-index: 20; }
.z-50 { z-index: 50; }
.overflow-hidden { overflow: hidden; }
.w-full { width: 100%; }
.h-full { height: 100%; }
.h-auto { height: auto; }
.min-h-screen { min-height: 100vh; }
.pointer-events-none { pointer-events: none; }
.uppercase { text-transform: uppercase; }

/* Colors */
.text-white { color: #FFFFFF; }
.text-red-600 { color: #DC2626; }
.text-red-bright { color: #FF4444; }
.text-gray-400 { color: #9CA3AF; }
.text-gray-200 { color: #E5E7EB; }
.drop-shadow-red { filter: drop-shadow(0 0 8px rgba(255, 68, 68, 0.6)); text-shadow: 0 0 15px rgba(255, 68, 68, 0.4); }
.drop-shadow-red-neon { filter: drop-shadow(0 0 10px rgba(220, 38, 38, 0.8)); text-shadow: 0 0 20px rgba(220, 38, 38, 1), 0 0 40px rgba(220, 38, 38, 0.8); color: #FFFFFF; }
.font-medium { font-weight: 500; }
.font-bold { font-weight: 700; }
.font-black { font-weight: 900; }
.text-5xl { font-size: 3rem; line-height: 1; }
.text-base { font-size: 1rem; line-height: 1.5rem; }
.tracking-wider { letter-spacing: 0.05em; }
.mb-2 { margin-bottom: 0.5rem; }
.bg-black { background-color: #000000; }
.bg-red-600 { background-color: #DC2626; }
.border-red-600\/20 { border-color: rgba(220, 38, 38, 0.2); }
.border-red-600\/15 { border-color: rgba(220, 38, 38, 0.15); }
.border-red-900\/30 { border-color: rgba(127, 29, 29, 0.3); }
.border-red-900\/20 { border-color: rgba(127, 29, 29, 0.2); }
.border-white { border-color: #FFFFFF; }
.hover\:bg-red-700:hover { background-color: #B91C1C; }
.hover\:bg-white\/10:hover { background-color: rgba(255, 255, 255, 0.1); }
.bg-gradient-to-t { background: linear-gradient(to top, #000000, transparent); }
.from-black { --tw-gradient-from: #000000; }
.to-transparent { --tw-gradient-to: transparent; }

/* Spacing & Sizes (Approximate mapping to style block for cleaner Vuetify/standard mixing) */
/* Note: Since we are using scoped styles, some tailwind classes might need this manual definition if standard tailwind isn't fully configured or to ensure PRECISE override of Vuetify defaults */

/* Layout Extras */
.container { width: 100%; max-width: 1280px; margin-left: auto; margin-right: auto; }
.mx-auto { margin-left: auto; margin-right: auto; }
.px-6 { padding-left: 1.5rem; padding-right: 1.5rem; }
.px-8 { padding-left: 2rem; padding-right: 2rem; }
.py-6 { padding-top: 1.5rem; padding-bottom: 1.5rem; }
.py-12 { padding-top: 3rem; padding-bottom: 3rem; }
.lg\:-ml-8 { margin-left: -2rem; }

/* Max Widths */
.max-w-md { max-width: 28rem; }
.max-w-lg { max-width: 32rem; }
.max-w-xl { max-width: 36rem; }
.max-w-2xl { max-width: 42rem; }
.max-w-4xl { max-width: 56rem; }

/* Margins */
.mb-1 { margin-bottom: 0.25rem; }
.mb-6 { margin-bottom: 1.5rem; }
.mb-8 { margin-bottom: 2rem; }
.mt-16 { margin-top: 4rem; }
.gap-4 { gap: 1rem; }
.gap-8 { gap: 2rem; }
.ml-2 { margin-left: 0.5rem; }

/* Responsive adjustments */
@media (min-width: 640px) {
    .sm\:text-5xl { font-size: 3rem; line-height: 1; }
    .sm\:flex-row { flex-direction: row; }
    .sm\:max-w-lg { max-width: 32rem; }
}

@media (min-width: 768px) {
    .md\:text-6xl { font-size: 3.75rem; line-height: 1; }
    .md\:pt-24 { padding-top: 6rem; }
    .md\:max-w-xl { max-width: 36rem; }
    .md\:text-4xl { font-size: 2.25rem; line-height: 2.5rem; }
    .md\:text-lg { font-size: 1.125rem; line-height: 1.75rem; }
    .md\:text-sm { font-size: 0.875rem; line-height: 1.25rem; }
    .md\:h-14 { height: 3.5rem; }
    .md\:w-[600px] { width: 600px; }
    .md\:h-[600px] { height: 600px; }
}

@media (min-width: 1024px) {
    .lg\:grid-cols-2 { grid-template-columns: repeat(2, minmax(0, 1fr)); }
    .lg\:justify-end { justify-content: flex-end; }
    .lg\:block { display: block; }
    .lg\:hidden { display: none; }
    .lg\:grid { display: grid; }
    .lg\:py-12 { padding-top: 3rem; padding-bottom: 3rem; }
    .lg\:gap-12 { gap: 3rem; }
    .lg\:mb-8 { margin-bottom: 2rem; }
    .lg\:mb-12 { margin-bottom: 3rem; }
    .lg\:mb-0 { margin-bottom: 0; }
    .lg\:max-w-4xl { max-width: 56rem; }
    .lg\:w-[850px] { width: 850px; }
    .lg\:h-[850px] { height: 850px; }
    .lg\:w-[750px] { width: 750px; }
    .lg\:h-[750px] { height: 750px; }
    .lg\:-ml-8 { margin-left: -2rem; }
    .lg\:grid-cols-3 { grid-template-columns: repeat(3, minmax(0, 1fr)); }
}

.text-4xl { font-size: 2.25rem; line-height: 2.5rem; }
.tracking-tight { letter-spacing: -0.025em; }
.leading-\[0\.95\] { line-height: 0.95; }
.pt-32 { padding-top: 8rem; }
.pb-0 { padding-bottom: 0px; }

/* Custom Circles */
.circle-decoration {
    border-width: 12px;
    border-color: rgba(220, 38, 38, 0.45) !important;
    box-shadow: 0 0 40px rgba(220, 38, 38, 0.2), inset 0 0 40px rgba(220, 38, 38, 0.1);
}

.circle-large {
    width: 500px; height: 500px;
}
@media (min-width: 768px) { .circle-large { width: 700px; height: 700px; } }
@media (min-width: 1024px) { .circle-large { width: 850px; height: 850px; } }

.circle-medium {
    width: 400px; height: 400px;
}
@media (min-width: 768px) { .circle-medium { width: 600px; height: 600px; } }
@media (min-width: 1024px) { .circle-medium { width: 750px; height: 750px; } }

/* Transformations & Animations */
.group:hover .group-hover\:translate-x-1 { transform: translateX(0.25rem); }
.origin-bottom { transform-origin: bottom; }
.-translate-y-1\/2 { transform: translateY(-50%); }
.top-1\/2 { top: 50%; }
.right-\[5\%\] { right: 5%; }
.right-\[8\%\] { right: 8%; }

/* Exact Text Matches */
.text-h1-huge {
    font-size: 3.5rem;
    line-height: 0.95;
    letter-spacing: -0.025em;
    margin-left: -1rem; /* Pull text slightly left on mobile */
}
@media (min-width: 768px) {
    .text-h1-huge { font-size: 3.75rem; margin-left: 0; }
}
@media (min-width: 1024px) {
    .text-h1-huge { 
        font-size: 6rem; 
        line-height: 0.95; 
        margin-left: 0; /* Reset - handled by container now */
    }
}

/* Athlete Image Specifics */
.athlete-image {
    transform: scale(1.1) translateY(80px); /* Mobile */
}
@media (min-width: 768px) {
    .athlete-image { transform: scale(1.25) translateY(112px); } /* Tablet */
}
@media (min-width: 1024px) {
    .athlete-image { transform: scale(1.65) translateY(20px) translateX(70px); } /* Desktop - Lowered further */
}
</style>
