<template>
  <div 
    class="bg-[#0A0D14] border border-[#1F2937] rounded-xl overflow-hidden cursor-pointer transition-all duration-300 mb-4 group p-6 flex justify-between items-center relative"
    :class="type === 'friends' || type === 'community' ? 'hover:border-[#3B82F6]' : (type === 'ai' ? 'hover:border-[#9333EA]' : 'hover:border-[#DC2626]')"
  >
    <!-- CONTENIDO IZQUIERDO -->
    <div class="flex flex-col gap-3 flex-1">
      <div class="flex flex-col gap-1">
        <h3 class="text-xl font-semibold text-white tracking-wide">{{ workout.title }}</h3>
        
        <!-- Grupos Musculares -->
        <div class="flex flex-wrap gap-2">
          <span 
            v-for="muscle in workout.muscles" 
            :key="muscle"
            class="px-2.5 py-0.5 rounded-full text-[10px] font-bold uppercase tracking-wider border transition-all hover:brightness-110"
            :class="muscleColorClass"
          >
            {{ muscle }}
          </span>
        </div>
      </div>

      <!-- Metadata Principal -->
      <div class="flex flex-wrap gap-4 items-center">
        <div class="flex items-center gap-1.5 text-sm text-[#9CA3AF]">
          <!-- Icono de Mancuerna Inclinada Exacta -->
          <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <path d="M14.4 14.4 9.6 9.6"/>
            <path d="M18.657 21.485a2 2 0 1 1-2.829-2.828l-1.768 1.768a2 2 0 1 1-2.829-2.829l6.364-6.364a2 2 0 1 1 2.829 2.828l-1.768 1.768a2 2 0 1 1 2.828 2.829z"/>
            <path d="m21.5 21.5-1.4-1.4"/>
            <path d="M3.9 3.9 2.5 2.5"/>
            <path d="M6.404 12.768a2 2 0 1 1-2.829-2.829l1.768-1.767a2 2 0 1 1-2.828-2.829l2.828-2.828a2 2 0 1 1 2.829 2.828l1.767-1.768a2 2 0 1 1 2.829 2.829z"/>
          </svg>
          <span class="font-medium">{{ workout.exercises }} ejercicios</span>
        </div>
        
        <!-- Mostrar LIKES si es comunidad -->
        <div v-if="type === 'community'" class="flex items-center gap-1.5 text-sm text-[#10B981]">
          <svg width="15" height="15" viewBox="0 0 24 24" fill="currentColor">
            <path d="M12 21.35l-1.45-1.32C5.4 15.36 2 12.28 2 8.5 2 5.42 4.42 3 7.5 3c1.74 0 3.41.81 4.5 2.09C13.09 3.81 14.76 3 16.5 3 19.58 3 22 5.42 22 8.5c0 3.78-3.4 6.86-8.55 11.54L12 21.35z"/>
          </svg>
          <span class="font-bold underline decoration-dotted">{{ workout.likes || '2.4k' }}</span>
        </div>

        <div class="flex items-center gap-1.5 text-sm" :class="diffTextClass">
          <!-- Icono de Diana/Objetivo -->
          <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
            <circle cx="12" cy="12" r="10"></circle>
            <circle cx="12" cy="12" r="3"></circle>
          </svg>
          <span class="font-bold">{{ type === 'ai' ? 'Personalizado' : workout.difficulty }}</span>
        </div>
      </div>

      <!-- Footer Info -->
      <div class="mt-4 pt-4 border-t border-[#1F2937]/50">
        <span class="text-[13px] text-[#6B7280]">
          {{ type === 'my' ? `Última vez: ${workout.lastUsed}` : (type === 'friends' ? `Usado por ${workout.author}: ${workout.lastUsed}` : `Añadido hace 1 día`) }}
        </span>
      </div>
    </div>

    <!-- CONTENIDO DERECHA -->
    <div class="flex flex-col items-end justify-between self-stretch py-1">
      <!-- Chevron para detalles -->
      <div class="text-[#6B7280] group-hover:text-white transition-colors">
        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
          <polyline points="9 18 15 12 9 6"></polyline>
        </svg>
      </div>

      <!-- Botón Comenzar -->
      <button 
        class="text-white px-4 py-2 rounded-lg flex items-center gap-2 text-sm font-bold transition-all hover:scale-105 active:scale-95 shadow-lg mt-auto"
        :class="buttonClass"
      >
        <svg width="16" height="16" viewBox="0 0 24 24" fill="currentColor">
          <path d="M5 3l14 9-14 9V3z"/>
        </svg>
        Comenzar
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue';

const props = defineProps<{
  workout: any;
  type: 'my' | 'friends' | 'community' | 'ai';
}>();

const diffTextClass = computed(() => {
  if (props.type === 'ai') return 'text-[#9333EA]';
  if (props.type === 'friends') return 'text-[#3B82F6]';
  if (props.type === 'community') return 'text-[#10B981]';
  
  switch (props.workout.difficulty) {
    case 'Principiante': return 'text-[#10B981]';
    case 'Intermedio': return 'text-[#9CA3AF]';
    case 'Avanzado': return 'text-[#DC2626]';
    default: return 'text-gray-600';
  }
});

const muscleColorClass = computed(() => {
  if (props.type === 'friends') {
    return 'bg-[rgba(59,130,246,0.1)] border-[rgba(59,130,246,0.2)] text-[#3B82F6]';
  }
  if (props.type === 'community') {
    return 'bg-[rgba(16,185,129,0.1)] border-[rgba(16,185,129,0.2)] text-[#10B981]';
  }
  if (props.type === 'ai') {
    return 'bg-[rgba(147,51,234,0.1)] border-[rgba(147,51,234,0.2)] text-[#9333EA]';
  }
  return 'bg-[rgba(220,38,38,0.1)] border-[rgba(220,38,38,0.2)] text-[#DC2626]';
});

const buttonClass = computed(() => {
  if (props.type === 'friends') {
    return 'bg-[#3B82F6] hover:bg-[#2563EB] shadow-blue-500/10';
  }
  if (props.type === 'community') {
    return 'bg-[#10B981] hover:bg-[#059669] shadow-emerald-500/10';
  }
  if (props.type === 'ai') {
    return 'bg-[#9333EA] hover:bg-[#7C3AED] shadow-purple-500/10';
  }
  return 'bg-[#DC2626] hover:bg-[#B91C1C] shadow-red-500/10';
});
</script>
