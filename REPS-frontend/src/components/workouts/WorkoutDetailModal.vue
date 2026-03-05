<template>
  <div class="fixed inset-0 z-[60] flex items-center justify-center p-4">
    <div class="absolute inset-0 bg-black/80 backdrop-blur-sm" @click="$emit('close')"></div>
    
    <div class="relative w-full max-w-[800px] bg-[#111827] border border-[#1F2937] rounded-2xl overflow-hidden flex flex-col shadow-2xl animate-scale-in">
      <!-- HEADER CON IMAGEN -->
      <div class="relative h-[200px] w-full">
        <img :src="workout.image" class="w-full h-full object-cover" alt="workout image">
        <div class="absolute inset-0 bg-gradient-to-t from-[#111827] to-transparent"></div>
        
        <button @click="$emit('close')" class="absolute top-4 right-4 w-9 h-9 bg-black/50 backdrop-blur-md rounded-full flex items-center justify-center text-white hover:bg-black/70 transition-all">
          <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5"><path d="M18 6L6 18M6 6l12 12"/></svg>
        </button>

        <h2 class="absolute bottom-6 left-6 text-3xl font-bold text-white uppercase tracking-tight">{{ workout.title }}</h2>
        
        <div 
          class="absolute top-4 left-6 px-3 py-1 rounded-lg text-xs font-bold text-white uppercase"
          :class="diffClasses"
        >
          {{ workout.difficulty }}
        </div>
      </div>

      <!-- BODY -->
      <div class="p-6 overflow-y-auto max-h-[50vh] custom-scrollbar">
        <!-- Metadata -->
        <div class="flex flex-wrap gap-8 items-center py-4 border-b border-[#1F2937] mb-6">
          <div class="flex items-center gap-2 text-[#9CA3AF]">
            <span class="text-white font-semibold">{{ workout.exercises }} ejercicios</span>
          </div>
          <div class="flex items-center gap-2 text-[#9CA3AF]">
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><circle cx="12" cy="12" r="10"/><path d="M12 6v6l4 2"/></svg>
            <span class="text-white font-semibold">{{ workout.duration }} min</span>
          </div>
          <div v-if="workout.author" class="flex items-center gap-2 text-[#9CA3AF]">
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/><circle cx="12" cy="7" r="4"/></svg>
            <span class="text-white font-semibold">Por {{ workout.author }}</span>
          </div>
        </div>

        <!-- Grupo Musculares -->
        <div class="flex flex-wrap gap-2 mb-8">
           <span v-for="m in workout.muscles" :key="m" class="bg-[rgba(220,38,38,0.15)] border border-[rgba(220,38,38,0.3)] px-3.5 py-1.5 rounded-lg text-sm text-[#FCA5A5] font-bold uppercase tracking-wide">
             {{ m }}
           </span>
        </div>

        <!-- Lista de Ejercicios -->
        <h3 class="text-xl font-bold text-white mb-4 uppercase tracking-wider">Ejercicios</h3>
        <div v-if="!workout.exerciseList || workout.exerciseList.length === 0" class="py-8 text-center text-[#9CA3AF] bg-[rgba(31,41,55,0.2)] rounded-xl border border-dashed border-[#374151]">
          Esta rutina no tiene ejercicios listados de momento.
        </div>
        <div class="flex flex-col gap-4">
          <div 
            v-for="(ex, i) in workout.exerciseList" 
            :key="i"
            class="bg-[#0A0A0A] border border-[#374151] rounded-xl p-4 flex gap-4"
          >
            <div class="w-8 h-8 shrink-0 bg-[#DC2626] rounded-full flex items-center justify-center font-bold text-sm text-white">
              {{ (i as number) + 1 }}
            </div>
            <div class="flex-1">
              <div class="flex justify-between items-start mb-2">
                <div class="flex flex-col gap-1">
                  <h4 class="font-bold text-white">{{ ex.name || ex.nombreEjercicio }}</h4>
                  <div v-if="ex.muscle || ex.grupoMuscular" class="flex">
                    <span class="bg-[#1F2937] text-blue-400 text-[10px] font-black px-2 py-0.5 rounded border border-blue-900/50 uppercase tracking-widest">
                      {{ (ex.muscle || ex.grupoMuscular) === 'OTRO' ? 'Sin Especificar' : (ex.muscle || ex.grupoMuscular) }}
                    </span>
                  </div>
                </div>
                <span v-if="ex.unilateral" class="bg-[rgba(59,130,246,0.15)] px-2 py-0.5 rounded text-[10px] text-[#93C5FD] uppercase font-bold">Unilateral</span>
              </div>
              <div class="grid grid-cols-2 md:grid-cols-4 gap-4 text-[13px] text-[#9CA3AF]">
                <div class="flex flex-col">
                  <span class="text-[10px] uppercase font-bold text-[#6B7280]">Series</span>
                  <span class="text-white font-medium">{{ ex.sets }}</span>
                </div>
                <div class="flex flex-col">
                  <span class="text-[10px] uppercase font-bold text-[#6B7280]">Reps</span>
                  <span class="text-white font-medium">{{ ex.reps }}</span>
                </div>
                <div class="flex flex-col">
                  <span class="text-[10px] uppercase font-bold text-[#6B7280]">Descanso</span>
                  <span class="text-white font-medium">{{ ex.rest || `${ex.descansoSegundos}s` }}</span>
                </div>
                <div class="flex flex-col">
                  <span class="text-[10px] uppercase font-bold text-[#6B7280]">
                    Último Peso
                  </span>
                  <span class="text-white font-medium">{{ ex.weight && String(ex.weight).toLowerCase() !== 'smart weight' && String(ex.weight) !== '0' && String(ex.weight) !== '0 kg' ? ex.weight : '0' }}</span>
                </div>
              </div>
              <p v-if="ex.notes" class="mt-3 text-xs italic text-[#6B7280]">"{{ ex.notes }}"</p>
            </div>
          </div>
        </div>
      </div>

      <!-- FOOTER -->
      <div class="p-6 border-t border-[#1F2937] flex gap-3">
        <button @click="startTraining" class="flex-[2] bg-[#DC2626] hover:bg-[#B91C1C] rounded-xl py-3.5 flex items-center justify-center gap-2 text-[15px] font-bold text-white transition-all hover:scale-105 active:scale-95 shadow-lg shadow-red-500/20">
          <svg width="20" height="20" viewBox="0 0 24 24" fill="white"><path d="M5 3l14 9-14 9V3z"/></svg>
          Comenzar Entrenamiento
        </button>
      </div>
    </div>
    <ActiveWorkoutModal 
      v-if="showActiveWorkout" 
      :workout="workout" 
      @completed="handleWorkoutCompleted"
      @close="showActiveWorkout = false"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue';
import ActiveWorkoutModal from './ActiveWorkoutModal.vue';

const props = defineProps<{
  workout: any;
}>();

const emit = defineEmits(['close', 'completed']);

const showActiveWorkout = ref(false);

const startTraining = () => {
    showActiveWorkout.value = true;
};

const handleWorkoutCompleted = () => {
    showActiveWorkout.value = false;
    emit('completed');
    emit('close');
};

const diffClasses = computed(() => {
  switch (props.workout.difficulty) {
    case 'Principiante': return 'bg-[#10B981]';
    case 'Intermedio': return 'bg-[#DC2626]';
    case 'Avanzado': return 'bg-[#DC2626]';
    case 'Personalizado': return 'bg-[#9333EA]';
    default: return 'bg-gray-600';
  }
});
</script>

<style scoped>
.animate-scale-in {
  animation: scaleIn 0.3s cubic-bezier(0.165, 0.84, 0.44, 1) forwards;
}
@keyframes scaleIn {
  from { opacity: 0; transform: scale(0.95); }
  to { opacity: 1; transform: scale(1); }
}
.custom-scrollbar::-webkit-scrollbar {
  width: 6px;
}
.custom-scrollbar::-webkit-scrollbar-track {
  background: transparent;
}
.custom-scrollbar::-webkit-scrollbar-thumb {
  background: #374151;
  border-radius: 10px;
}
</style>
