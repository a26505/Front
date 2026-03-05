<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../stores/auth'
import { usuariosApi } from '../api'

const router = useRouter()
const authStore = useAuthStore()

// Estado para el plan seleccionado localmente
// 0 = Gratis, 1 = Pro
const selectedPlanId = ref<number | null>(null)
const isSubmitting = ref(false)

// Cargar el plan actual del usuario al montar
onMounted(async () => {
    if (!authStore.profile) {
        await authStore.fetchProfile()
    }
    // Si el perfil ya tiene esPro, marcamos Pro, si no Gratis
    selectedPlanId.value = authStore.isPro ? 1 : 0
})

const selectPlanLocal = (id: number) => {
    selectedPlanId.value = id
}

const confirmPlan = async () => {
  if (selectedPlanId.value === null) return

  isSubmitting.value = true
  try {
    // Usar la API centralizada
    await usuariosApi.updatePlan(selectedPlanId.value);
    
    // Recargar perfil para actualizar el estado esPro en el store
    await authStore.fetchProfile();
    
    // Redirigir al inicio o dashboard
    router.push('/dashboard') 
    
  } catch (error) {
    console.error('Error selecting plan:', error)
    alert('Hubo un error al seleccionar el plan. Por favor intente de nuevo.')
  } finally {
    isSubmitting.value = false
  }
}
</script>

<template>
  <div class="min-h-screen bg-app-gradient py-16 px-6 flex flex-col items-center select-plan-page text-white relative overflow-x-hidden">
    
    <!-- Decoración de fondo -->
    <div class="absolute inset-0 pointer-events-none">
        <div class="absolute top-[-10%] left-[-10%] w-[50%] h-[50%] bg-blue-600/15 blur-[120px] rounded-full"></div>
        <div class="absolute bottom-[20%] right-[-5%] w-[40%] h-[40%] bg-red-600/10 blur-[120px] rounded-full"></div>
    </div>

    <!-- Encabezado -->
    <div class="max-w-[1280px] w-full mb-16 text-center z-10">
      <h1 class="text-5xl md:text-6xl font-black mb-4 tracking-tight">
        Elige Tu Plan de <span class="text-red-gradient">Entrenamiento</span>
      </h1>
      <p class="text-xl text-white/90 font-medium">
        Empieza gratis, evoluciona cuando quieras
      </p>
    </div>

    <!-- Grid de Planes -->
    <div class="max-w-[1100px] w-full grid grid-cols-1 lg:grid-cols-2 gap-10 mb-20 z-10">
      
      <!-- Plan Gratis -->
      <div 
        class="pricing-card blue group mt-4 relative cursor-pointer transition-all duration-300"
        :class="{ 'active-plan blue-border': selectedPlanId === 0 }"
        @click="selectPlanLocal(0)"
      >
        <div class="flex flex-col h-full relative z-10">
            <div class="mb-8 pt-4">
                <div class="flex justify-between items-start">
                    <h3 class="text-2xl font-black text-blue-400 uppercase tracking-widest mb-4">Gratis</h3>
                    <div v-if="selectedPlanId === 0" class="bg-blue-600 text-[10px] font-bold px-3 py-1 rounded-full uppercase">Seleccionado</div>
                </div>
                <div class="flex items-baseline gap-1">
                    <span class="text-6xl font-black text-white">$0</span>
                </div>
                <p class="text-white/60 mt-2 font-medium">Para empezar tu viaje</p>
            </div>
            
            <div class="divider blue mb-10"></div>
            
            <ul class="space-y-6 mb-12 flex-grow">
                <li class="plan-item">
                    <svg class="w-5 h-5 text-blue-500 shrink-0" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>
                    <span class="text-white">Rutinas básicas</span>
                </li>
                <li class="plan-item">
                    <svg class="w-5 h-5 text-blue-500 shrink-0" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>
                    <span class="text-white">Seguimiento de progreso</span>
                </li>
                <li class="plan-item">
                    <svg class="w-5 h-5 text-blue-500 shrink-0" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>
                    <span class="text-white">Estadísticas básicas</span>
                </li>
                <li class="plan-item">
                    <svg class="w-5 h-5 text-blue-500 shrink-0" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>
                    <span class="text-white">Videos explicativos</span>
                </li>
                <li class="plan-item">
                    <svg class="w-5 h-5 text-blue-500 shrink-0" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>
                    <span class="text-white">Acceso a comunidad</span>
                </li>
            </ul>
            
            <button class="btn-plan blue" :class="{ 'bg-blue-600': selectedPlanId === 0 }">
              {{ selectedPlanId === 0 ? 'SELECCIONADO' : 'ELEGIR GRATIS' }}
            </button>
        </div>
      </div>

      <!-- Plan Pro -->
      <div 
        class="pricing-card red group relative shadow-red-glow mt-4 cursor-pointer transition-all duration-300"
        :class="{ 'active-plan red-border': selectedPlanId === 1 }"
        @click="selectPlanLocal(1)"
      >
        <div class="absolute -top-5 left-1/2 -translate-x-1/2 bg-red-600 text-white text-[12px] font-black px-8 py-2.5 rounded-full uppercase tracking-[0.2em] z-30 shadow-[0_4px_20px_rgba(220,38,38,0.5)] whitespace-nowrap">
          MÁS POPULAR
        </div>
        
        <div class="flex flex-col h-full relative z-10">
            <div class="mb-8 pt-4">
                <div class="flex justify-between items-start">
                    <h3 class="text-2xl font-black text-red-500 uppercase tracking-widest mb-4">Pro</h3>
                    <div v-if="selectedPlanId === 1" class="bg-red-600 text-[10px] font-bold px-3 py-1 rounded-full uppercase">Seleccionado</div>
                </div>
                <div class="flex items-baseline gap-1">
                    <span class="text-6xl font-black text-white">$9.99</span>
                    <span class="text-white/40 font-bold">/mes</span>
                </div>
                <p class="text-white/60 mt-2 font-medium">Para resultados profesionales</p>
            </div>
            
            <div class="divider red mb-10"></div>
            
            <ul class="space-y-6 mb-12 flex-grow">
                <li class="plan-item featured">
                    <svg class="w-5 h-5 text-red-500 shrink-0" viewBox="0 0 24 24" fill="currentColor"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>
                    <span class="font-black text-white uppercase text-xs tracking-wider">Todo lo de Gratis</span>
                </li>
                <li class="plan-item">
                    <svg class="w-5 h-5 text-red-500 shrink-0" viewBox="0 0 24 24" fill="currentColor"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>
                    <span class="text-white">Rutinas con IA ilimitadas</span>
                </li>
                <li class="plan-item">
                    <svg class="w-5 h-5 text-red-500 shrink-0" viewBox="0 0 24 24" fill="currentColor"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>
                    <span class="text-white">Entrenador virtual 24/7</span>
                </li>
                <li class="plan-item">
                    <svg class="w-5 h-5 text-red-500 shrink-0" viewBox="0 0 24 24" fill="currentColor"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>
                    <span class="text-white">Análisis avanzado de progreso</span>
                </li>
                <li class="plan-item">
                    <svg class="w-5 h-5 text-red-500 shrink-0" viewBox="0 0 24 24" fill="currentColor"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>
                    <span class="text-white">Calentamiento inteligente</span>
                </li>
                <li class="plan-item">
                    <svg class="w-5 h-5 text-red-500 shrink-0" viewBox="0 0 24 24" fill="currentColor"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>
                    <span class="text-white">Descuentos exclusivos</span>
                </li>
            </ul>
            
            <button class="btn-plan red" :class="{ 'bg-red-700': selectedPlanId === 1 }">
              {{ selectedPlanId === 1 ? 'SELECCIONADO' : 'ELEGIR PRO' }}
            </button>
        </div>
      </div>

    </div>

    <!-- Beneficios Pro -->
    <div class="max-w-[1200px] w-full mb-32 z-10">
      <h2 class="text-4xl font-black text-center mb-16 uppercase tracking-tight text-white">
        ¿Por qué elegir <span class="text-red-gradient">REPS Pro</span>?
      </h2>
      
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
        
        <div class="benefit-card">
          <div class="benefit-icon">
            <svg class="w-8 h-8 text-white" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M9.5 2A2.5 2.5 0 0 1 12 4.5v15a2.5 2.5 0 0 1-4.96-.44 2.5 2.5 0 0 1-1.98-3 2.5 2.5 0 0 1-1.32-4.24 3 3 0 0 1 .34-5.58 2.5 2.5 0 0 1 2.96-3.08A2.5 2.5 0 0 1 9.5 2Z"/><path d="M14.5 2A2.5 2.5 0 0 0 12 4.5v15a2.5 2.5 0 0 0 4.96-.44 2.5 2.5 0 0 0 1.98-3 2.5 2.5 0 0 0 1.32-4.24 3 3 0 0 0-.34-5.58 2.5 2.5 0 0 0-2.96-3.08A2.5 2.5 0 0 0 14.5 2Z"/></svg>
          </div>
          <h4 class="text-lg font-black mb-3 text-white uppercase tracking-wider">IA Avanzada</h4>
          <p class="text-white/60 text-sm font-medium leading-relaxed">Rutinas personalizadas que se adaptan a ti</p>
        </div>
        
        <div class="benefit-card">
          <div class="benefit-icon">
            <svg class="w-8 h-8 text-white" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M3 3v18h18"/><path d="m19 9-5 5-4-4-3 3"/></svg>
          </div>
          <h4 class="text-lg font-black mb-3 text-white uppercase tracking-wider">Análisis Detallado</h4>
          <p class="text-white/60 text-sm font-medium leading-relaxed">Conoce tu progreso en profundidad</p>
        </div>

        <div class="benefit-card">
          <div class="benefit-icon">
            <svg class="w-8 h-8 text-white" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2"/><circle cx="9" cy="7" r="4"/><path d="M22 21v-2a4 4 0 0 0-3-3.87"/><path d="M16 3.13a4 4 0 0 1 0 7.75"/></svg>
          </div>
          <h4 class="text-lg font-black mb-3 text-white uppercase tracking-wider">Social</h4>
          <p class="text-white/60 text-sm font-medium leading-relaxed">Compite y comparte con amigos</p>
        </div>

        <div class="benefit-card">
          <div class="benefit-icon">
            <svg class="w-8 h-8 text-white" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="8" width="18" height="4" rx="1"/><path d="M12 8v13"/><path d="M19 12v7a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2v-7"/><path d="M7.5 8a2.5 2.5 0 0 1 0-5A4.5 4.5 0 0 1 12 7.5"/><path d="M16.5 8a2.5 2.5 0 0 0 0-5A4.5 4.5 0 0 0 12 7.5"/></svg>
          </div>
          <h4 class="text-lg font-black mb-3 text-white uppercase tracking-wider">Recompensas</h4>
          <p class="text-white/60 text-sm font-medium leading-relaxed">Descuentos y premios exclusivos</p>
        </div>
        
      </div>
    </div>

    <!-- Botón Continuar Inferior -->
    <div class="w-full flex flex-col items-center gap-6 mt-auto z-10">
        <button 
            @click="confirmPlan" 
            class="continuar-btn bg-white text-black hover:bg-red-500 hover:text-white"
            :disabled="selectedPlanId === null || isSubmitting"
        >
            {{ isSubmitting ? 'GUARDANDO...' : 'CONTINUAR CON PLAN ELEGIDO' }}
        </button>
        <p class="text-white/40 text-[10px] font-bold uppercase tracking-[0.2em] mb-12">
            Cancela cuando quieras
        </p>
    </div>

  </div>
</template>

<style scoped>
.bg-app-gradient {
    background: radial-gradient(circle at center, #0B1221 0%, #000000 100%);
    background-color: black;
}

.text-red-gradient {
    background: linear-gradient(135deg, #FF4D4D 0%, #B91C1C 100%);
    background-clip: text;
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
}

/* Pricing Cards */
.pricing-card {
    background: rgba(13, 18, 30, 0.75);
    backdrop-filter: blur(30px);
    border-radius: 2.5rem;
    padding: 3rem;
    transition: all 0.4s cubic-bezier(0.16, 1, 0.3, 1);
    height: 100%;
    position: relative;
    border: 2px solid rgba(255, 255, 255, 0.05);
}

.pricing-card.blue:hover {
    border-color: rgba(37, 99, 235, 0.4);
    transform: translateY(-5px);
}

.pricing-card.red:hover {
    border-color: rgba(220, 38, 38, 0.4);
    transform: translateY(-5px);
}

.active-plan.blue-border {
    border-color: #2563EB !important;
    background: rgba(37, 99, 235, 0.1);
    box-shadow: 0 0 40px rgba(37, 99, 235, 0.2);
}

.active-plan.red-border {
    border-color: #DC2626 !important;
    background: rgba(220, 38, 38, 0.1);
    box-shadow: 0 0 40px rgba(220, 38, 38, 0.2);
}

/* Items */
.plan-item {
    display: flex;
    align-items: center;
    gap: 1rem;
    font-size: 0.95rem;
    font-weight: 500;
}

.plan-item.featured {
    transform: scale(1.05);
}

/* Dividers */
.divider {
    height: 1px;
    width: 100%;
}
.divider.blue { background: linear-gradient(90deg, transparent, rgba(37, 99, 235, 0.4), transparent); }
.divider.red { background: linear-gradient(90deg, transparent, rgba(220, 38, 38, 0.6), transparent); }

/* Buttons inside cards */
.btn-plan {
    width: 100%;
    padding: 1rem;
    border-radius: 1rem;
    font-weight: 900;
    letter-spacing: 0.1em;
    transition: all 0.3s ease;
    cursor: pointer;
    text-transform: uppercase;
    font-size: 0.8rem;
    border: none;
    background: rgba(255, 255, 255, 0.05);
    color: white;
}

/* Main Continue Button */
.continuar-btn {
    padding: 1.25rem 4rem;
    border-radius: 1.25rem;
    font-weight: 900;
    letter-spacing: 0.2em;
    font-size: 0.9rem;
    cursor: pointer;
    transition: all 0.4s cubic-bezier(0.175, 0.885, 0.32, 1.275);
    border: none;
    box-shadow: 0 10px 30px rgba(0,0,0,0.5);
}

.continuar-btn:hover:not(:disabled) {
    transform: scale(1.05) translateY(-5px);
    box-shadow: 0 20px 40px rgba(220, 38, 38, 0.3);
}

.continuar-btn:disabled {
    opacity: 0.5;
    cursor: not-allowed;
    background: #333;
    color: #666;
}

/* Benefits */
.benefit-card {
    background: rgba(15, 15, 15, 0.85);
    border-radius: 1.5rem;
    padding: 2.5rem 2rem;
    text-align: center;
    transition: all 0.4s ease;
    border: 1px solid rgba(255, 255, 255, 0.08);
    backdrop-filter: blur(15px);
}

.benefit-card:hover { 
    border-color: rgba(225, 29, 72, 0.5); 
    transform: translateY(-8px);
    background: rgba(30, 30, 30, 0.95);
}

.benefit-icon {
    width: 60px;
    height: 60px;
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 0 auto 1.5rem;
    border-radius: 1rem;
    background: rgba(255, 255, 255, 0.05);
    transition: all 0.3s ease;
}

.select-plan-page {
    font-family: 'Outfit', sans-serif;
}
</style>
