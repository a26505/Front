<template>
  <div 
    class="bg-[#0A0D14] border border-[#1F2937] rounded-xl overflow-hidden cursor-pointer transition-all duration-300 mb-4 group p-6 flex justify-between items-center relative"
    :class="{
      'hover:border-[#3B82F6]': type === 'friends',
      'hover:border-[#10B981]': type === 'community' || type === 'community_saved',
      'hover:border-[#9333EA]': type === 'ai',
      'hover:border-[#DC2626]': type === 'my'
    }"
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
          <span class="font-medium">{{ workout.exercises }} ejercicios</span>
        </div>

        <!-- Tiempo de la rutina -->
        <div v-if="workout.duration" class="flex items-center gap-1.5 text-sm text-[#9CA3AF]">
          <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <circle cx="12" cy="12" r="10"/><path d="M12 6v6l4 2"/>
          </svg>
          <span class="font-medium">{{ workout.duration }} min</span>
        </div>
        

        <div class="flex items-center gap-1.5 text-sm" :class="diffTextClass">
          <!-- Icono de Diana/Objetivo -->
          <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
            <circle cx="12" cy="12" r="10"></circle>
            <circle cx="12" cy="12" r="3"></circle>
          </svg>
          <span class="font-bold">{{ type === 'ai' ? 'Personalizado' : workout.difficulty }}</span>
        </div>

        <!-- Botón de Like (Corazón) -->
        <button 
          v-if="type === 'community'"
          @click.stop="$emit('like', workout.id)"
          class="flex items-center gap-1.5 px-3 py-1.5 rounded-full bg-[#10B981]/10 border border-[#10B981]/20 text-[#10B981] hover:bg-[#10B981] hover:text-white transition-all duration-300 group/like shadow-lg shadow-emerald-500/10"
        >
          <svg width="16" height="16" viewBox="0 0 24 24" :fill="workout.liked ? 'currentColor' : 'none'" stroke="currentColor" stroke-width="2.5" class="transition-transform group-hover/like:scale-125">
            <path d="M12 21.35l-1.45-1.32C5.4 15.36 2 12.28 2 8.5 2 5.42 4.42 3 7.5 3c1.74 0 3.41.81 4.5 2.09C13.09 3.81 14.76 3 16.5 3 19.58 3 22 5.42 22 8.5c0 3.78-3.4 6.86-8.55 11.54L12 21.35z"/>
          </svg>
          <span class="font-black text-[12px] uppercase tracking-tighter">{{ workout.likes || 0 }}</span>
        </button>
      </div>

      <!-- Footer Info -->
      <div v-if="workout.lastUsed" class="mt-4 pt-4 border-t border-[#1F2937]/50">
        <span class="text-[13px] text-[#6B7280]">
          {{ type === 'my' ? `Última vez: ${workout.lastUsed}` : (type === 'friends' ? `Usado por ${workout.author}: ${workout.lastUsed}` : `Añadido hace 1 día`) }}
        </span>
      </div>
    </div>

    <!-- CONTENIDO DERECHA -->
    <div class="flex flex-col items-end justify-between self-stretch py-1">
      <!-- Botón de Borrar, Publicar y Chevron para detalles -->
      <div class="flex items-center gap-2">
        <button 
          v-if="type === 'my' && !workout.public"
          @click.stop="$emit('publish', workout.id)"
          class="text-gray-500 hover:text-blue-500 transition-colors p-1.5 rounded-full hover:bg-blue-500/10"
          title="Publicar en Comunidad"
        >
          <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <polyline points="16 16 12 12 8 16"></polyline>
            <line x1="12" y1="12" x2="12" y2="21"></line>
            <path d="M20.39 18.39A5 5 0 0 0 18 9h-1.26A8 8 0 1 0 3 16.3"></path>
            <polyline points="16 16 12 12 8 16"></polyline>
          </svg>
        </button>
        <!-- Editar -->
        <button 
          v-if="type === 'my' || type === 'ai' || type === 'community_saved'"
          @click.stop="$emit('edit', workout)"
          class="text-gray-500 hover:text-[#DC2626] transition-colors p-1.5 rounded-full hover:bg-red-500/10"
          title="Editar rutina"
        >
          <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <path d="M12 20h9"></path>
            <path d="M16.5 3.5a2.121 2.121 0 0 1 3 3L7 19l-4 1 1-4L16.5 3.5z"></path>
          </svg>
        </button>
        <!-- Borrar -->
        <button 
          v-if="type === 'my' || type === 'ai' || type === 'community_saved'"
          @click.stop="$emit('delete', workout.id)"
          class="text-gray-500 hover:text-[#DC2626] transition-colors p-1.5 rounded-full hover:bg-red-500/10"
          title="Eliminar rutina"
        >
          <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <polyline points="3 6 5 6 21 6"></polyline>
            <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path>
            <line x1="10" y1="11" x2="10" y2="17"></line>
            <line x1="14" y1="11" x2="14" y2="17"></line>
          </svg>
        </button>
        <div class="text-[#6B7280] group-hover:text-white transition-colors">
          <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
            <polyline points="9 18 15 12 9 6"></polyline>
          </svg>
        </div>
      </div>

      <!-- Botón Comenzar / Guardar / Ver -->
      <div class="flex items-center gap-2 mt-auto">
        <button 
          v-if="type === 'community'"
          @click.stop="$emit('save', workout.id)"
          class="bg-[#1F2937] hover:bg-[#374151] text-white px-4 py-2 rounded-lg flex items-center gap-2 text-sm font-bold transition-all hover:scale-105 active:scale-95 border border-[#374151]"
        >
          <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
            <path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"></path>
            <polyline points="17 21 17 13 7 13 7 21"></polyline>
            <polyline points="7 3 7 8 15 8"></polyline>
          </svg>
          Guardar
        </button>

        <button 
          class="text-white px-4 py-2 rounded-lg flex items-center gap-2 text-sm font-bold transition-all hover:scale-105 active:scale-95 shadow-lg"
          :class="buttonClass"
        >
          <svg width="16" height="16" viewBox="0 0 24 24" fill="currentColor">
            <path d="M5 3l14 9-14 9V3z"/>
          </svg>
          {{ type === 'community' ? 'Ver Rutina' : 'Comenzar' }}
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue';

const props = defineProps<{
  workout: any;
  type: 'my' | 'friends' | 'community' | 'community_saved' | 'ai';
}>();

defineEmits(['delete', 'publish', 'edit', 'like', 'save']);

const diffTextClass = computed(() => {
  if (props.type === 'ai') return 'text-[#9333EA]';
  if (props.type === 'friends') return 'text-[#3B82F6]';
  if (props.type === 'community' || props.type === 'community_saved') return 'text-[#10B981]';
  
  switch (props.workout.difficulty) {
    case 'Principiante': return 'text-[#10B981]';
    case 'Intermedio': return 'text-[#F59E0B]';
    case 'Avanzado': return 'text-[#DC2626]';
    default: return 'text-[#DC2626]';
  }
});

const muscleColorClass = computed(() => {
  if (props.type === 'friends') return 'bg-[#3B82F6]/10 border-[#3B82F6]/20 text-[#3B82F6]';
  if (props.type === 'community' || props.type === 'community_saved') return 'bg-[#10B981]/10 border-[#10B981]/20 text-[#10B981]';
  if (props.type === 'ai') return 'bg-[#9333EA]/10 border-[#9333EA]/20 text-[#9333EA]';
  return 'bg-[#DC2626]/10 border-[#DC2626]/20 text-[#DC2626]';
});

const buttonClass = computed(() => {
  if (props.type === 'friends') return 'bg-[#3B82F6] hover:bg-[#2563EB] shadow-blue-500/10';
  if (props.type === 'community' || props.type === 'community_saved') return 'bg-[#10B981] hover:bg-[#059669] shadow-emerald-500/10';
  if (props.type === 'ai') return 'bg-[#9333EA] hover:bg-[#7C3AED] shadow-purple-500/10';
  return 'bg-[#DC2626] hover:bg-[#B91C1C] shadow-red-500/10';
});
</script>
