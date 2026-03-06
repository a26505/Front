<template>
  <transition name="cookie-fade">
    <div v-if="show" class="fixed bottom-6 left-6 right-6 md:left-auto md:right-8 md:max-w-md z-[2000] animate-in slide-in-from-bottom duration-500">
      <div class="relative bg-black/95 backdrop-blur-xl border border-white/10 rounded-[28px] p-6 shadow-[0_20px_50px_rgba(0,0,0,0.8)] overflow-hidden group">
        
        <!-- Glow accent -->
        <div class="absolute -top-10 -left-10 w-32 h-32 bg-[#DC2626]/10 blur-[40px] rounded-full group-hover:bg-[#DC2626]/20 transition-all"></div>
        
        <div class="relative z-10">
          <div class="flex items-center gap-4 mb-4">
            <div class="w-12 h-12 rounded-2xl bg-[#DC2626]/10 flex items-center justify-center border border-[#DC2626]/20">
              <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#DC2626" stroke-width="2">
                <path d="M12 2a10 10 0 1 0 10 10 4 4 0 0 1-5-5 4 4 0 0 1-5-5" />
                <path d="M8.5 8.5v.01" />
                <path d="M16 15.5v.01" />
                <path d="M12 12v.01" />
                <path d="M11 17v.01" />
                <path d="M7 14v.01" />
              </svg>
            </div>
            <div>
              <h3 class="text-lg font-black italic uppercase tracking-tighter text-white">Configuración de Cookies</h3>
              <p class="text-[10px] font-bold text-[#DC2626] uppercase tracking-widest leading-none">Tu privacidad es prioridad</p>
            </div>
          </div>

          <p class="text-sm text-gray-400 mb-6 leading-relaxed">
            Utilizamos cookies para optimizar tu rendimiento, analizar el tráfico y personalizar tu experiencia REPS. Al aceptar, nos ayudas a mejorar tus resultados.
          </p>

          <div class="flex flex-col gap-3">
             <button @click="acceptAll" class="w-full py-3.5 bg-[#DC2626] hover:bg-red-700 text-white rounded-xl font-black text-xs uppercase tracking-widest transition-all shadow-lg shadow-red-950/20 active:scale-95">
                Aceptar Todo
             </button>
             <div class="grid grid-cols-2 gap-3">
                <button @click="declineAll" class="py-3 border border-white/10 text-gray-400 hover:text-white hover:bg-white/5 rounded-xl font-bold text-xs uppercase tracking-widest transition-all active:scale-95">
                  Rechazar
                </button>
                <button @click="close" class="py-3 border border-white/10 text-gray-400 hover:text-white hover:bg-white/5 rounded-xl font-bold text-xs uppercase tracking-widest transition-all active:scale-95">
                  Configurar
                </button>
             </div>
          </div>
          
          <div class="mt-4 text-center">
            <a href="#" class="text-[10px] font-bold text-gray-600 hover:text-gray-400 transition-colors uppercase tracking-widest">Política de Cookies & Privacidad</a>
          </div>
        </div>
      </div>
    </div>
  </transition>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { getCookie, setCookie } from '../../utils/cookies';

const show = ref(false);

onMounted(() => {
  const consent = getCookie('reps_cookie_consent');
  if (!consent) {
    // Delay slightly for dramatic effect
    setTimeout(() => {
      show.value = true;
    }, 1500);
  }
});

const acceptAll = () => {
  setCookie('reps_cookie_consent', 'all', 365);
  show.value = false;
};

const declineAll = () => {
  setCookie('reps_cookie_consent', 'none', 365);
  show.value = false;
};

const close = () => {
  // Simulating configuration
  acceptAll();
};
</script>

<style scoped>
.cookie-fade-enter-active,
.cookie-fade-leave-active {
  transition: all 0.5s cubic-bezier(0.175, 0.885, 0.32, 1.275);
}

.cookie-fade-enter-from,
.cookie-fade-leave-to {
  transform: translateY(100%) scale(0.9);
  opacity: 0;
}
</style>
