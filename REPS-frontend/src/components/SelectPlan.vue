<script setup lang="ts">
import { useRouter } from 'vue-router'
// Si tienes un store de auth, impórtalo aquí para obtener el token. 
// Como no vi el store completo, asumiremos que axios tiene el interceptor o 
// que podemos importar el token desde un store.
import axios from 'axios' 

const router = useRouter()

// SVG Components
const ArrowLeftIcon = { template: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M19 12H5M12 19l-7-7 7-7"/></svg>` }
const CheckGreenIcon = { template: `<svg width="20" height="20" viewBox="0 0 20 20" fill="none"><circle cx="10" cy="10" r="10" fill="#10B981"/><path d="M6 10l2.5 2.5L14 7" stroke="#FFFFFF" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/></svg>` }
const XRedIcon = { template: `<svg width="20" height="20" viewBox="0 0 20 20" fill="none"><circle cx="10" cy="10" r="10" fill="#DC2626"/><path d="M7 7l6 6M13 7l-6 6" stroke="#FFFFFF" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/></svg>` }
const SparklesIcon = { template: `<svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M12 3v18m9-9H3m15.364 6.364L5.636 5.636m12.728 0L5.636 18.364"/></svg>` }
const BrainIcon = { template: `<svg width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M12 4.5a2.5 2.5 0 0 0-4.96-.46 2.5 2.5 0 0 0-1.98 3 2.5 2.5 0 0 0-1.32 4.24 3 3 0 0 0 .34 5.58 2.5 2.5 0 0 0 2.96 3.08 2.5 2.5 0 0 0 4.91.05L12 20V4.5z"/><path d="M16 8V5c0-1.1.9-2 2-2"/><path d="M12 13h4"/><path d="M12 18h6a2 2 0 0 0 2-2v-5"/></svg>` }
const ChartIcon = { template: `<svg width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M3 3v18h18"/><path d="M18 17V9"/><path d="M13 17V5"/><path d="M8 17v-3"/></svg>` }
const UsersIcon = { template: `<svg width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/><circle cx="9" cy="7" r="4"/><path d="M23 21v-2a4 4 0 0 0-3-3.87"/><path d="M16 3.13a4 4 0 0 1 0 7.75"/></svg>` }
const GiftIcon = { template: `<svg width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="#EF4444" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="8" width="18" height="4" rx="1"/><path d="M12 8v13"/><path d="M19 12v7a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2v-7"/><path d="M7.5 8a2.5 2.5 0 0 1 0-5A4.5 4.5 0 0 1 12 7.5"/><path d="M16.5 8a2.5 2.5 0 0 0 0-5A4.5 4.5 0 0 0 12 7.5"/></svg>` }

const selectPlan = async (planId: number) => {
  try {
    // API Call
    // Assuming Axios is configured with base URL and Authorization header if needed globally
    // or manually add headers here if token is stored in localStorage/Pinia
    const token = localStorage.getItem('token'); 
    await axios.put('http://localhost:5038/api/usuarios/plan', planId, {
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    })
    
    // Redirect to Dashboard or Home on success
    // Asumimos que la página principal después del login es el Home o Dashboard
    router.push('/') 
    
  } catch (error) {
    console.error('Error selecting plan:', error)
    alert('Hubo un error al seleccionar el plan. Por favor intente de nuevo.')
  }
}

const goBack = () => {
  router.back()
}
</script>

<template>
  <div class="min-h-screen bg-black py-16 px-6 flex flex-col items-center select-plan-page">
  
    <!-- Botón Volver -->
    <button @click="goBack" class="fixed top-6 left-6 flex items-center gap-2 text-gray-400 hover:text-white transition-colors z-50">
      <component :is="ArrowLeftIcon" />
      <span>Volver</span>
    </button>

    <!-- Encabezado -->
    <div class="max-w-[1280px] w-full mb-16 text-center">
      <h1 class="text-4xl md:text-5xl font-bold text-white mb-4">
        Elige Tu Plan de <span class="text-red-600">Entrenamiento</span>
      </h1>
      <p class="text-lg md:text-xl text-gray-400">
        Selecciona el plan que mejor se adapte a tus objetivos
      </p>
    </div>

    <!-- Grid de Planes -->
    <div class="max-w-[1100px] w-full grid grid-cols-1 lg:grid-cols-2 gap-8 mb-12">
      
      <!-- Plan Free -->
      <div class="bg-[#0A0A0A] border-2 border-[#1A1A1A] rounded-3xl p-8 md:p-10 hover:border-red-600/20 transition-all duration-300 relative group">
        <h3 class="text-2xl font-bold text-white mb-2">Plan Free</h3>
        <div class="mb-2">
          <span class="text-4xl font-bold text-white">$0</span>
          <span class="text-gray-400 ml-1">/mes</span>
        </div>
        <p class="text-gray-400 mb-8">Perfecto para comenzar tu viaje fitness</p>
        
        <div class="border-t border-[#1A1A1A] mb-8"></div>
        
        <ul class="space-y-3 mb-8">
          <!-- Habilitadas -->
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Rutinas básicas pre-diseñadas</span>
          </li>
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Seguimiento de entrenamientos</span>
          </li>
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Estadísticas básicas</span>
          </li>
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Biblioteca de ejercicios</span>
          </li>
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Videos explicativos</span>
          </li>
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Seguimiento de progreso</span>
          </li>
          
          <!-- Deshabilitadas -->
          <li class="flex items-center gap-3 opacity-50">
            <component :is="XRedIcon" />
            <span class="text-gray-600 line-through text-sm">Rutinas con IA generativa</span>
          </li>
           <li class="flex items-center gap-3 opacity-50">
            <component :is="XRedIcon" />
            <span class="text-gray-600 line-through text-sm">Entrenador virtual adaptativo</span>
          </li>
           <li class="flex items-center gap-3 opacity-50">
            <component :is="XRedIcon" />
            <span class="text-gray-600 line-through text-sm">Análisis avanzado de rendimiento</span>
          </li>
           <li class="flex items-center gap-3 opacity-50">
            <component :is="XRedIcon" />
            <span class="text-gray-600 line-through text-sm">Calentamiento inteligente</span>
          </li>
           <li class="flex items-center gap-3 opacity-50">
            <component :is="XRedIcon" />
            <span class="text-gray-600 line-through text-sm">Comparación con amigos</span>
          </li>
           <li class="flex items-center gap-3 opacity-50">
            <component :is="XRedIcon" />
            <span class="text-gray-600 line-through text-sm">Sistema de recompensas premium</span>
          </li>
        </ul>
        
        <button @click="selectPlan(0)" class="w-full border-2 border-red-600 text-red-600 font-bold py-3 rounded-xl hover:bg-red-600 hover:text-white transition-all duration-300 cursor-pointer">
          Seleccionar Free
        </button>
      </div>

      <!-- Plan Pro -->
      <div class="relative bg-[#0A0A0A] border-2 border-red-600 rounded-3xl p-8 md:p-10 shadow-[0_0_40px_rgba(220,38,38,0.15)] hover:shadow-[0_0_60px_rgba(220,38,38,0.25)] transition-all duration-300 mt-8 lg:mt-0">
        
        <!-- Badge -->
        <div class="absolute -top-4 left-1/2 -translate-x-1/2 bg-red-600 text-white text-xs font-bold px-5 py-1.5 rounded-full uppercase tracking-wider">
          MÁS POPULAR
        </div>
        
        <h3 class="text-2xl font-bold text-red-600 mb-2">Plan Pro</h3>
        <div class="mb-1">
          <span class="text-4xl font-bold text-white">$9.99</span>
          <span class="text-gray-400 ml-1">/mes</span>
        </div>
        <p class="text-emerald-500 text-sm font-semibold mb-2">Ahorra 20% anual</p>
        <p class="text-gray-400 mb-8">Desbloquea todo el potencial de la IA</p>
        
        <div class="border-t border-red-600/30 mb-8"></div>
        
        <ul class="space-y-3 mb-8">
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Todo lo incluido en Free</span>
          </li>
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Rutinas con IA generativa personalizadas</span>
          </li>
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Entrenador virtual con adaptación en tiempo real</span>
          </li>
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Análisis avanzado por grupo muscular</span>
          </li>
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Cálculo automático de calentamiento óptimo</span>
          </li>
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Comparación y competencia con amigos</span>
          </li>
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Sistema de recompensas y descuentos exclusivos</span>
          </li>
          <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Estadísticas detalladas y predicciones</span>
          </li>
           <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Prioridad en nuevas funciones</span>
          </li>
           <li class="flex items-center gap-3">
            <component :is="CheckGreenIcon" />
            <span class="text-gray-300 text-sm">Soporte premium 24/7</span>
          </li>
        </ul>
        
        <button @click="selectPlan(1)" class="w-full bg-red-600 text-white font-bold py-3 rounded-xl hover:bg-red-700 hover:-translate-y-0.5 hover:shadow-[0_8px_24px_rgba(220,38,38,0.3)] transition-all duration-300 cursor-pointer border-none">
          Seleccionar Pro
        </button>
      </div>

    </div>

    <!-- Nota Prueba -->
    <p class="text-gray-400 flex items-center gap-2 mb-20 text-base">
      <component :is="SparklesIcon" /> Prueba gratis 14 días • Cancela cuando quieras
    </p>

    <!-- Beneficios Pro -->
    <div class="max-w-[1100px] w-full mb-16">
      <h2 class="text-3xl md:text-4xl font-bold text-white text-center mb-12">
        ¿Por qué elegir <span class="text-red-600">REPS Pro</span>?
      </h2>
      
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        
        <div class="bg-[#0A0A0A] border border-[#1A1A1A] rounded-2xl p-8 text-center hover:border-red-600 transition-all duration-300">
          <div class="mx-auto mb-5 flex justify-center"><component :is="BrainIcon" /></div>
          <h4 class="text-xl font-bold text-white mb-3">IA Avanzada</h4>
          <p class="text-gray-400 text-sm">Rutinas personalizadas que se adaptan a ti</p>
        </div>
        
         <div class="bg-[#0A0A0A] border border-[#1A1A1A] rounded-2xl p-8 text-center hover:border-red-600 transition-all duration-300">
           <div class="mx-auto mb-5 flex justify-center"><component :is="ChartIcon" /></div>
          <h4 class="text-xl font-bold text-white mb-3">Análisis Detallado</h4>
          <p class="text-gray-400 text-sm">Conoce tu progreso en profundidad</p>
        </div>
         <div class="bg-[#0A0A0A] border border-[#1A1A1A] rounded-2xl p-8 text-center hover:border-red-600 transition-all duration-300">
           <div class="mx-auto mb-5 flex justify-center"><component :is="UsersIcon" /></div>
          <h4 class="text-xl font-bold text-white mb-3">Social</h4>
          <p class="text-gray-400 text-sm">Compite y comparte con amigos</p>
        </div>
         <div class="bg-[#0A0A0A] border border-[#1A1A1A] rounded-2xl p-8 text-center hover:border-red-600 transition-all duration-300">
           <div class="mx-auto mb-5 flex justify-center"><component :is="GiftIcon" /></div>
          <h4 class="text-xl font-bold text-white mb-3">Recompensas</h4>
          <p class="text-gray-400 text-sm">Descuentos y premios exclusivos</p>
        </div>
        
      </div>
    </div>

    <!-- Botón Continuar Free -->
    <button @click="selectPlan(0)" class="border-2 border-gray-700 text-gray-400 font-semibold px-12 py-3 rounded-xl hover:border-red-600 hover:text-red-600 transition-all duration-300 mb-6 cursor-pointer bg-transparent">
      Continuar con Free
    </button>

    <!-- Nota Final -->
    <p class="text-gray-600 text-sm text-center">
      Por favor, selecciona un plan para continuar
    </p>

  </div>
</template>

<style scoped>
/* Asegúrate de que Tailwind esté activo o estos estilos no se aplicarán correctamente */
.select-plan-page {
    font-family: 'Inter', 'Outfit', sans-serif; /* Asumiendo que usas estas fuentes */
}
</style>
