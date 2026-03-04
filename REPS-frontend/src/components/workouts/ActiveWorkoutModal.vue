<template>
  <div class="fixed inset-0 z-[70] flex flex-col bg-[#000000] text-white animate-slide-up">
    <!-- HEADER -->
    <header class="sticky top-0 z-40 bg-[#111827] border-b border-[#1F2937] px-4 py-4 flex items-center justify-between shadow-lg">
      <div>
        <h2 class="text-xl md:text-2xl font-black text-white uppercase tracking-tight bg-gradient-to-r from-white via-red-200 to-red-400 bg-clip-text text-transparent">{{ workout.title || workout.nombre }}</h2>
        <div class="text-sm text-[#9CA3AF] mt-1 flex items-center gap-2 font-medium">
          <div class="w-2.5 h-2.5 rounded-full bg-red-500 shadow-[0_0_10px_#EF4444] animate-pulse"></div>
          Entrenamiento en curso
        </div>
      </div>
      <div class="flex items-center gap-3 md:gap-4">
        <div class="hidden sm:flex items-center bg-[#111827] rounded-xl p-1 border border-[#374151] shadow-inner">
          <button @click="weightUnit = 'kg'" :class="weightUnit === 'kg' ? 'bg-[#DC2626] text-white shadow-[0_0_10px_rgba(220,38,38,0.5)]' : 'text-gray-400 hover:text-white'" class="px-4 py-1.5 rounded-lg text-xs font-black tracking-widest transition-all">KG</button>
          <button @click="weightUnit = 'lbs'" :class="weightUnit === 'lbs' ? 'bg-[#DC2626] text-white shadow-[0_0_10px_rgba(220,38,38,0.5)]' : 'text-gray-400 hover:text-white'" class="px-4 py-1.5 rounded-lg text-xs font-black tracking-widest transition-all">LBS</button>
        </div>
        <button @click="onFinalizar" class="bg-gradient-to-r from-[#DC2626] to-[#991B1B] hover:from-[#B91C1C] hover:to-[#7F1D1D] text-white px-6 py-2.5 rounded-xl font-black transition-all shadow-[0_0_20px_rgba(220,38,38,0.3)] active:scale-95 text-sm uppercase tracking-wider border border-red-500/30">
          Finalizar
        </button>
      </div>
    </header>

    <!-- BODY -->
    <main class="flex-1 overflow-y-auto p-4 md:p-6 custom-scrollbar">
      <div class="max-w-[800px] mx-auto">
        <div class="mb-8 flex flex-col md:flex-row md:justify-between md:items-end gap-2">
          <h3 class="text-xl font-black text-gray-200 tracking-tight">Selecciona el ejercicio por el cual empezar o continuar:</h3>
          <span class="text-xs text-[#DC2626] font-bold uppercase tracking-widest bg-red-500/10 px-3 py-1 rounded-full border border-red-500/20">Puedes finalizar sin completarlos todos</span>
        </div>

        <div class="flex flex-col gap-4">
          <div 
            v-for="(ex, i) in exercises" 
            :key="i"
            class="bg-[#0A0A0A] border rounded-2xl p-5 transition-all duration-500 relative overflow-hidden group"
            :class="ex.completed ? 'border-green-500/50 bg-gradient-to-r from-[#052e16]/40 to-[#0A0A0A] opacity-80' : (currentExerciseIndex === i ? 'border-[#DC2626] ring-2 ring-[#DC2626]/20 shadow-[0_4px_30px_rgba(220,38,38,0.15)] bg-[#111827]' : 'border-[#1F2937] hover:border-gray-500/50 hover:bg-[#111827]/50')"
            @click="currentExerciseIndex = i"
          >
            <!-- Card Header -->
            <div class="flex justify-between items-start mb-4 cursor-pointer">
              <div class="flex items-center gap-3">
                <!-- Check Circle -->
                <button 
                  @click.stop="toggleExerciseCompletion(Number(i))"
                  class="w-10 h-10 rounded-full border-2 flex items-center justify-center transition-all bg-[#0A0A0A] relative overflow-hidden"
                  :class="ex.completed ? 'border-green-500 text-green-500 shadow-[0_0_15px_rgba(34,197,94,0.4)]' : 'border-[#374151] text-transparent hover:border-gray-400'"
                >
                  <div v-if="ex.completed" class="absolute inset-0 bg-green-500/20 animate-pulse"></div>
                  <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round" class="relative z-10">
                    <polyline points="20 6 9 17 4 12"></polyline>
                  </svg>
                </button>
                <div>
                  <h4 class="font-bold text-white text-lg">{{ ex.name || ex.ejercicio?.nombre || ex.nombreEjercicio }}</h4>
                  <template v-if="ex.muscle || ex.ejercicio?.grupoMuscular || ex.grupoMuscular">
                    <span class="text-[10px] uppercase font-black tracking-widest text-[#9CA3AF]">{{ ex.muscle || ex.ejercicio?.grupoMuscular || ex.grupoMuscular }}</span>
                  </template>
                </div>
              </div>
              
              <!-- Expand icon when selected -->
              <div v-if="currentExerciseIndex === i" class="text-[#DC2626]">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><polyline points="6 9 12 15 18 9"></polyline></svg>
              </div>
            </div>

            <!-- Card Body (Only if selected) -->
            <div v-show="currentExerciseIndex === i" class="pl-11 pt-2 border-t border-[#1F2937]/50 mt-2">
               <div class="grid grid-cols-3 gap-4 mb-4 mt-2">
                 <div>
                   <label class="block text-[10px] text-gray-500 uppercase font-bold mb-1">Series</label>
                   <div class="text-white font-medium">{{ ex.sets || ex.series }}</div>
                 </div>
                 <div>
                   <label class="block text-[10px] text-gray-500 uppercase font-bold mb-1">Reps Obj.</label>
                   <div class="text-white font-medium">{{ ex.reps || ex.ejercicio?.repeticiones || '10-12' }}</div>
                 </div>
                  <div>
                    <label class="block text-[10px] text-[#DC2626] uppercase font-black tracking-widest mb-1">Peso Sug. ({{ weightUnit }})</label>
                    <div class="text-white font-bold text-lg">{{ ex.weight || 'Smart Weight' }}</div>
                  </div>
               </div>

               <!-- Si aún no empieza -->
                 <div v-if="!ex.isStarted && !ex.completed" class="mt-6">
                   <button 
                      @click.stop="startExercise(i)" 
                      class="w-full py-4 rounded-xl bg-gradient-to-r from-[#DC2626] to-[#991B1B] text-white font-black text-sm md:text-base uppercase tracking-widest shadow-[0_0_20px_rgba(220,38,38,0.4)] hover:shadow-[0_0_30px_rgba(220,38,38,0.6)] transition-all active:scale-95 group/btn overflow-hidden relative"
                    >
                     <div class="absolute inset-0 bg-white/20 translate-y-full group-hover/btn:translate-y-0 transition-transform duration-300 ease-out"></div>
                     <span class="relative z-10 flex items-center justify-center gap-2">
                       <svg width="20" height="20" viewBox="0 0 24 24" fill="currentColor"><path d="M5 3l14 9-14 9V3z"/></svg>
                       Empezar Ejercicio
                     </span>
                   </button>
                 </div>

               <!-- Descanso -->
               <div v-else-if="ex.isResting" class="mt-6 flex flex-col items-center justify-center py-8 bg-[#111827] rounded-2xl border border-[#374151] shadow-inner">
                  <svg width="32" height="32" viewBox="0 0 24 24" fill="none" class="text-blue-500 mb-2 animate-pulse" stroke="currentColor" stroke-width="2"><circle cx="12" cy="12" r="10"/><polyline points="12 6 12 12 16 14"/></svg>
                  <h4 class="text-xs font-bold text-gray-400 uppercase tracking-widest mb-1">Descanso</h4>
                  <div class="text-6xl font-black text-white mb-6 font-mono tracking-tighter">{{ formatTime(ex.restRemaining) }}</div>
                  <button @click.stop="skipRest(i)" class="px-8 py-2.5 rounded-xl border-2 border-transparent bg-white/10 text-white hover:bg-white/20 font-bold text-sm uppercase transition-all shadow-md active:scale-95">
                      Saltar Descanso
                  </button>
               </div>

               <!-- Series -->
               <div v-else-if="ex.isStarted && !ex.completed" class="mt-6">
                 <h5 class="text-[11px] font-black text-gray-500 uppercase tracking-widest mb-3 flex items-center justify-between">
                   <span>Registrar Series</span>
                   <span class="text-[#DC2626]">Serie {{ ex.currentSet }} / {{ ex.seriesData.length }}</span>
                 </h5>
                 
                 <div class="space-y-3">
                   <div v-for="(serie, sIdx) in ex.seriesData" :key="sIdx">
                     <!-- Mostrar solo las series hasta la actual (inclusive) -->
                     <div v-if="Number(sIdx) < ex.currentSet" class="flex items-center gap-3 bg-[#111827] p-2 rounded-xl border transition-all" :class="Number(sIdx) === ex.currentSet - 1 ? 'border-[#374151] shadow-lg scale-[1.02]' : 'border-transparent opacity-60'">
                       <div class="w-8 flex justify-center text-sm font-black text-gray-400">{{ Number(sIdx) + 1 }}</div>
                       
                       <div class="relative flex-1 group/input">
                         <input type="number" v-model="serie.peso" :disabled="Number(sIdx) !== ex.currentSet - 1" :placeholder="ex.weight || 'Smart'" class="w-full bg-[#1F2937] border border-[#374151] focus:bg-[#111827] rounded-lg text-white font-black text-center text-lg py-2 focus:outline-none focus:border-[#DC2626] focus:ring-2 focus:ring-[#DC2626]/50 transition-all placeholder:text-gray-600 disabled:opacity-50 disabled:cursor-not-allowed shadow-inner">
                         <span class="absolute right-3 top-1/2 -translate-y-1/2 text-[10px] text-gray-400 font-bold uppercase tracking-widest">{{ weightUnit }}</span>
                       </div>
                       
                       <div class="relative flex-1 group/input">
                         <input type="number" v-model="serie.reps" :disabled="Number(sIdx) !== ex.currentSet - 1" :placeholder="ex.reps || ex.ejercicio?.repeticiones || '10'" class="w-full bg-[#1F2937] border border-[#374151] focus:bg-[#111827] rounded-lg text-white font-black text-center text-lg py-2 focus:outline-none focus:border-[#DC2626] focus:ring-2 focus:ring-[#DC2626]/50 transition-all placeholder:text-gray-600 disabled:opacity-50 disabled:cursor-not-allowed shadow-inner">
                         <span class="absolute right-3 top-1/2 -translate-y-1/2 text-[10px] text-gray-400 font-bold uppercase tracking-widest">reps</span>
                       </div>
                       
                       <!-- Boton de check -->
                       <button v-if="Number(sIdx) === ex.currentSet - 1" @click.stop="finishSeries(Number(i), Number(sIdx))" class="w-12 h-10 rounded-lg bg-[#DC2626] flex items-center justify-center text-white hover:bg-red-700 transition-all shadow-lg active:scale-95 shrink-0">
                         <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><polyline points="20 6 9 17 4 12"></polyline></svg>
                       </button>
                       <div v-else class="w-12 h-10 flex items-center justify-center text-green-500 shrink-0 bg-green-500/10 rounded-lg">
                         <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><polyline points="20 6 9 17 4 12"></polyline></svg>
                       </div>
                     </div>
                   </div>
                 </div>
               </div>

               <!-- Ya completado -->
               <div v-if="ex.completed" class="mt-6">
                 <button 
                    @click.stop="currentExerciseIndex = (Number(i) + 1 < exercises.length) ? Number(i) + 1 : -1" 
                    class="w-full py-3 rounded-xl bg-[#22C55E] text-black font-black text-sm uppercase tracking-wide shadow-lg shadow-green-900/20 hover:bg-green-400 transition-all active:scale-95 flex items-center justify-center gap-2"
                  >
                   <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><polyline points="9 18 15 12 9 6"></polyline></svg>
                   Siguiente Ejercicio
                 </button>
               </div>
            </div>
          </div>
        </div>
      </div>
    </main>
  </div>
</template>

<script setup lang="ts">
import { ref, onBeforeUnmount } from 'vue';
import { entrenamientosApi } from '../../api';

const props = defineProps<{
  workout: any;
}>();

const emit = defineEmits(['close', 'completed']);

const weightUnit = ref('kg');

// Handle internal exercises data with required state for intervals, series, and interactions
const rawExercises = props.workout.ejercicios || props.workout.exerciseList || [];
const exercises = ref<any[]>(rawExercises.map((e: any) => ({
  ...e,
  completed: false,
  isStarted: false,
  isResting: false,
  currentSet: 1,
  restRemaining: 0,
  seriesData: Array.from({ length: e.sets || e.series || 3 }).map((_, sIdx) => ({
      numeroSerie: sIdx + 1,
      peso: null,
      reps: null,
      completada: false
  }))
})));

const currentExerciseIndex = ref(0);
let timerInterval: any = null;

const formatTime = (seconds: number) => {
    const m = Math.floor(seconds / 60);
    const s = seconds % 60;
    return `${m.toString().padStart(2, '0')}:${s.toString().padStart(2, '0')}`;
};

const toggleExerciseCompletion = (index: number) => {
  const ex = exercises.value[index];
  ex.completed = !ex.completed;
  if (ex.completed) {
      ex.isStarted = true;
      ex.isResting = false;
      if (timerInterval) clearInterval(timerInterval);
      ex.seriesData.forEach((s: any) => s.completada = true);
      
      if (currentExerciseIndex.value === index) {
          const nextIncomplete = exercises.value.findIndex((e: any, idx: number) => idx > index && !e.completed);
          if (nextIncomplete !== -1) {
              currentExerciseIndex.value = nextIncomplete;
          }
      }
  } else {
      ex.seriesData.forEach((s: any) => s.completada = false);
      ex.currentSet = 1;
  }
};

const startExercise = (index: number) => {
    exercises.value[index].isStarted = true;
};

const endRest = (index: number) => {
    if (timerInterval) clearInterval(timerInterval);
    const ex = exercises.value[index];
    ex.isResting = false;
    ex.restRemaining = 0;
    ex.currentSet++;
};

const skipRest = (index: number) => {
    endRest(index);
};

const finishSeries = (index: number, sIdx: number) => {
    const ex = exercises.value[index];
    ex.seriesData[sIdx].completada = true;
    
    if (ex.currentSet < ex.seriesData.length) {
        ex.isResting = true;
        ex.restRemaining = ex.descansoSegundos || 90;
        
        if (timerInterval) clearInterval(timerInterval);
        timerInterval = setInterval(() => {
            ex.restRemaining--;
            if (ex.restRemaining <= 0) {
                endRest(index);
            }
        }, 1000);
    } else {
        ex.completed = true;
    }
};

const onFinalizar = async () => {
  try {
    const completedExs = exercises.value.filter((e: any) => e.completed).map((e: any, idx: number) => {
        return {
            ejercicioId: e.ejercicioId || e.id || idx + 1,
            series: e.seriesData.map((s: any) => ({
                numeroSerie: s.numeroSerie,
                peso: parseFloat(s.peso) || 0,
                reps: parseInt(s.reps) || parseInt(e.reps) || e.ejercicio?.repeticiones || 10,
                completada: s.completada
            }))
        }
    });

    const payload = {
        rutinaId: props.workout.id || null, 
        nombre: props.workout.title || props.workout.nombre || 'Entrenamiento libre',
        duracionMinutos: props.workout.duration || props.workout.duracionMinutos || 45,
        ejercicios: completedExs
    };

    await entrenamientosApi.finalizar(payload);
    emit('completed');
  } catch (error) {
    console.error("Error finalizing workout:", error);
    emit('completed');
  }
};

onBeforeUnmount(() => {
    if (timerInterval) clearInterval(timerInterval);
});
</script>

<style scoped>
.animate-slide-up {
  animation: slideUp 0.3s cubic-bezier(0.165, 0.84, 0.44, 1) forwards;
}
@keyframes slideUp {
  from { opacity: 0; transform: translateY(20px); }
  to { opacity: 1; transform: translateY(0); }
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
