<template>
  <div class="fixed inset-0 z-[60] flex items-center justify-center p-4">
    <div class="absolute inset-0 bg-black/80 backdrop-blur-sm" @click="$emit('close')"></div>
    
    <div class="relative w-full max-w-[900px] bg-[#111827] border border-[#1F2937] rounded-2xl overflow-hidden flex flex-col shadow-2xl animate-scale-in">
      <!-- HEADER -->
      <div class="p-6 border-b border-[#1F2937] flex justify-between items-center">
        <h2 class="text-2xl font-bold text-white">Crear Nueva Rutina</h2>
        <button @click="$emit('close')" class="p-2 text-[#9CA3AF] hover:text-white transition-colors">
          <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M18 6L6 18M6 6l12 12"/></svg>
        </button>
      </div>

      <!-- BODY -->
      <div class="p-6 overflow-y-auto max-h-[70vh] custom-scrollbar">
        <!-- Campo Nombre -->
        <div class="mb-6">
          <label class="block text-sm font-semibold text-white mb-2">Nombre de la rutina</label>
          <input 
            type="text" 
            v-model="name"
            placeholder="Ej: Empuje Superior A"
            class="w-full bg-[#0A0A0A] border border-[#374151] rounded-lg p-3 text-white focus:border-[#DC2626] outline-none transition-all"
          >
        </div>

        <!-- Selección de Ejercicios -->
        <div class="mb-6">
          <label class="block text-sm font-semibold text-white mb-3">Añadir Ejercicios</label>
          <div class="relative mb-4">
            <svg class="absolute left-3 top-1/2 -translate-y-1/2 text-[#9CA3AF]" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
              <circle cx="11" cy="11" r="8"/><path d="m21 21-4.3-4.3"/>
            </svg>
            <input 
              type="text" 
              placeholder="Buscar ejercicios..." 
              class="w-full bg-[#0A0A0A] border border-[#374151] rounded-lg py-3 pl-10 pr-4 text-sm text-white focus:border-[#DC2626] outline-none transition-all"
            >
          </div>

          <!-- Lista ejercicios disponibles -->
          <div class="bg-[#0A0A0A] border border-[#374151] rounded-lg p-2 max-h-48 overflow-y-auto custom-scrollbar mb-6">
            <div 
              v-for="ex in availableExercises" 
              :key="ex.name"
              class="p-2.5 flex justify-between items-center rounded-md hover:bg-[#1F2937] cursor-pointer group transition-colors"
              @click="addExercise(ex)"
            >
              <span class="text-sm text-white">{{ ex.name }}</span>
              <div class="flex items-center gap-3">
                <span class="bg-[rgba(220,38,38,0.15)] px-2 py-0.5 rounded text-[10px] text-[#FCA5A5] uppercase font-bold">{{ ex.muscle }}</span>
                <div class="w-7 h-7 bg-[#DC2626] rounded flex items-center justify-center text-white font-bold transition-transform group-hover:scale-110">+</div>
              </div>
            </div>
          </div>
        </div>

        <!-- Ejercicios Seleccionados -->
        <div>
          <label class="block text-sm font-semibold text-white mb-3">Ejercicios seleccionados</label>
          
          <div v-if="selectedExercises.length === 0" class="py-12 border-2 border-dashed border-[#374151] rounded-xl text-center text-[#6B7280]">
            <p>Añade ejercicios a tu rutina</p>
          </div>

          <div v-else class="flex flex-col gap-4">
            <div 
              v-for="(item, index) in selectedExercises" 
              :key="index"
              class="bg-[#0A0A0A] border border-[#374151] rounded-xl p-4 animate-fade-in"
            >
              <div class="flex justify-between items-center mb-4">
                <div class="flex items-center gap-2">
                  <svg class="text-[#6B7280] cursor-grab" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                    <circle cx="9" cy="5" r="1"/><circle cx="9" cy="12" r="1"/><circle cx="9" cy="19" r="1"/>
                    <circle cx="15" cy="5" r="1"/><circle cx="15" cy="12" r="1"/><circle cx="15" cy="19" r="1"/>
                  </svg>
                  <span class="font-bold text-white">{{ item.name }}</span>
                </div>
                <div class="flex gap-2">
                  <button class="p-2 border border-[#374151] rounded-md text-[#9CA3AF] hover:border-[#DC2626] transition-colors">
                    <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M12 20h9"/><path d="M16.5 3.5a2.121 2.121 0 0 1 3 3L7 19l-4 1 1-4L16.5 3.5z"/></svg>
                  </button>
                  <button @click="removeExercise(index)" class="p-2 border border-[#374151] rounded-md text-[#9CA3AF] hover:border-[#DC2626] hover:text-[#DC2626] transition-colors">
                    <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M3 6h18"/><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"/></svg>
                  </button>
                </div>
              </div>

              <!-- Configuración Ejercicio -->
              <div class="grid grid-cols-2 sm:grid-cols-4 gap-3">
                <div>
                  <label class="block text-[11px] text-[#9CA3AF] uppercase font-bold mb-1">Series</label>
                  <input type="number" v-model="item.sets" class="w-full bg-[#1F2937] border border-[#374151] rounded-md p-2 text-sm text-white outline-none">
                </div>
                <div>
                  <label class="block text-[11px] text-[#9CA3AF] uppercase font-bold mb-1">Reps</label>
                  <input type="text" v-model="item.reps" class="w-full bg-[#1F2937] border border-[#374151] rounded-md p-2 text-sm text-white outline-none">
                </div>
                <div>
                  <label class="block text-[11px] text-[#9CA3AF] uppercase font-bold mb-1">Descanso</label>
                  <input type="text" v-model="item.rest" class="w-full bg-[#1F2937] border border-[#374151] rounded-md p-2 text-sm text-white outline-none">
                </div>
                <div>
                  <label class="block text-[11px] text-[#9CA3AF] uppercase font-bold mb-1">Peso</label>
                  <input type="text" v-model="item.weight" class="w-full bg-[#1F2937] border border-[#374151] rounded-md p-2 text-sm text-white outline-none">
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- FOOTER -->
      <div class="p-6 border-t border-[#1F2937] flex justify-end gap-3">
        <button @click="$emit('close')" class="bg-transparent border border-[#374151] hover:border-[#DC2626] rounded-lg px-6 py-2.5 text-[15px] font-semibold text-[#9CA3AF] transition-all">
          Cancelar
        </button>
        <button 
          @click="saveWorkout" 
          :disabled="!name || selectedExercises.length === 0"
          class="bg-[#DC2626] hover:bg-[#B91C1C] disabled:opacity-50 disabled:cursor-not-allowed rounded-lg px-6 py-2.5 flex items-center gap-2 text-[15px] font-semibold text-white transition-all hover:scale-105 active:scale-95"
        >
          <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5"><path d="M20 6L9 17l-5-5"/></svg>
          Crear Rutina
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const emit = defineEmits(['close', 'save']);

const name = ref('');
const selectedExercises = ref([]);

const availableExercises = [
  { name: 'Press de Banca con Barra', muscle: 'Pecho' },
  { name: 'Sentadilla Libre', muscle: 'Piernas' },
  { name: 'Peso Muerto Rumano', muscle: 'Isquios' },
  { name: 'Press Militar', muscle: 'Hombros' },
  { name: 'Dominadas Pronas', muscle: 'Espalda' },
  { name: 'Curl de Bíceps con Barra', muscle: 'Brazos' },
  { name: 'Press de Banca Inclinado', muscle: 'Pecho' },
  { name: 'Remo con Barra', muscle: 'Espalda' }
];

const addExercise = (ex) => {
  selectedExercises.value.push({
    ...ex,
    sets: 3,
    reps: '10-12',
    rest: '90s',
    weight: '0 kg'
  });
};

const removeExercise = (index) => {
  selectedExercises.value.splice(index, 1);
};

const saveWorkout = () => {
  emit('save', { name: name.value, exercises: selectedExercises.value });
};
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
.custom-scrollbar::-webkit-scrollbar-thumb:hover {
  background: #4B5563;
}
</style>
