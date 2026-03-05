<template>
  <div class="fixed inset-0 z-[60] flex items-center justify-center p-4">
    <div class="absolute inset-0 bg-black/80 backdrop-blur-sm" @click="$emit('close')"></div>
    
    <div class="relative w-full max-w-[500px] bg-[#111827] border border-[#1F2937] rounded-2xl overflow-hidden flex flex-col shadow-2xl">
      <!-- HEADER -->
      <div class="p-6 border-b border-[#1F2937] flex justify-between items-center bg-[#07090E]">
        <div class="flex items-center gap-3">
          <div class="w-10 h-10 rounded-xl bg-blue-500/10 flex items-center justify-center text-blue-500 border border-blue-500/20">
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><rect x="3" y="4" width="18" height="18" rx="2" ry="2"/><line x1="16" y1="2" x2="16" y2="6"/><line x1="8" y1="2" x2="8" y2="6"/><line x1="3" y1="10" x2="21" y2="10"/></svg>
          </div>
          <div>
            <h2 class="text-xl font-bold text-white uppercase tracking-wider">Mi Ciclo</h2>
            <p class="text-xs text-[#9CA3AF]">Ordena tus rutinas para saber qué te toca cada día</p>
          </div>
        </div>
        <button @click="$emit('close')" class="text-gray-400 hover:text-white transition-colors">
          <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5"><path d="M18 6L6 18M6 6l12 12"/></svg>
        </button>
      </div>

      <!-- BODY -->
      <div class="p-6 overflow-y-auto max-h-[60vh] flex flex-col gap-4">
        
        <div v-if="cycle.length === 0" class="text-center py-8 text-[#6B7280] text-sm">
          Aún no has añadido rutinas a tu ciclo. ¡Selecciona de la lista de abajo!
        </div>

        <!-- Ciclo Actual -->
        <h3 class="text-sm font-bold text-white mb-2" v-if="cycle.length > 0">Rutinas en tu Ciclo (Arrastra para ordenar si tuvieras D&D, aquí usa los botones)</h3>
        <div class="flex flex-col gap-2 mb-4">
          <div v-for="(r, idx) in cycle" :key="r.id" class="flex items-center justify-between bg-[#0A0A0A] border border-[#374151] p-3 rounded-lg">
            <div class="flex items-center gap-3">
              <div class="bg-[#1F2937] font-black text-[#9CA3AF] w-6 h-6 rounded flex items-center justify-center text-xs">
                {{ idx + 1 }}
              </div>
              <span class="text-white font-bold text-sm">{{ getRoutineName(r.id) }}</span>
            </div>
            <div class="flex items-center gap-1">
              <button @click="moveUp(idx)" :disabled="idx === 0" class="p-1 text-gray-400 hover:text-white disabled:opacity-30">
                <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M18 15l-6-6-6 6"/></svg>
              </button>
              <button @click="moveDown(idx)" :disabled="idx === cycle.length - 1" class="p-1 text-gray-400 hover:text-white disabled:opacity-30">
                <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M6 9l6 6 6-6"/></svg>
              </button>
              <button @click="removeFromCycle(idx)" class="p-1 text-red-400 hover:text-red-500 ml-2">
                <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M18 6L6 18M6 6l12 12"/></svg>
              </button>
            </div>
          </div>
        </div>

        <h3 class="text-sm font-bold text-gray-400 mt-4 border-t border-[#1F2937] pt-4 mb-2">Mis Rutinas Disponibles</h3>
        <div class="flex flex-col gap-2">
          <div v-for="w in myWorkouts" :key="w.id" class="flex items-center justify-between bg-[#1F2937] p-3 rounded-lg border border-[rgba(255,255,255,0.05)]">
            <span class="text-white text-sm font-medium">{{ w.title }}</span>
            <button @click="addToCycle(w.id)" class="text-xs font-bold text-blue-400 hover:text-blue-300 border border-blue-500/30 px-2 py-1 rounded bg-blue-500/10 transition-colors">
              + Añadir
            </button>
          </div>
          <div v-if="myWorkouts.length === 0" class="text-xs text-gray-500 text-center py-2">No tienes rutinas propias creadas.</div>
        </div>
      </div>

      <!-- FOOTER -->
      <div class="p-6 border-t border-[#1F2937] flex gap-3">
        <button @click="$emit('close')" class="flex-1 bg-transparent border border-[#374151] hover:bg-[#1F2937] rounded-xl py-3 text-sm font-bold text-white transition-colors">
          Cancelar
        </button>
        <button @click="save" class="flex-[2] bg-[#2563EB] hover:bg-[#1D4ED8] rounded-xl py-3 flex items-center justify-center gap-2 text-sm font-bold text-white transition-all shadow-lg shadow-blue-500/20">
          Guardar Ciclo
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';

const props = defineProps<{
  myWorkouts: any[];
}>();

const emit = defineEmits(['close', 'save']);

const cycle = ref<any[]>([]);

onMounted(() => {
  const stored = localStorage.getItem('reps_cycle');
  if (stored) {
    try {
      cycle.value = JSON.parse(stored);
    } catch {}
  }
});

const getRoutineName = (id: string | number) => {
  const found = props.myWorkouts.find(w => w.id === id);
  return found ? found.title : 'Rutina Desconocida';
};

const addToCycle = (id: string | number) => {
  cycle.value.push({ id });
};

const removeFromCycle = (idx: number) => {
  cycle.value.splice(idx, 1);
};

const moveUp = (idx: number) => {
  if (idx > 0) {
    const temp = cycle.value[idx - 1];
    cycle.value[idx - 1] = cycle.value[idx];
    cycle.value[idx] = temp;
  }
};

const moveDown = (idx: number) => {
  if (idx < cycle.value.length - 1) {
    const temp = cycle.value[idx + 1];
    cycle.value[idx + 1] = cycle.value[idx];
    cycle.value[idx] = temp;
  }
};

const save = () => {
    localStorage.setItem('reps_cycle', JSON.stringify(cycle.value));
    // Asignamos qué dia del ciclo le toca ahora (0 al empezar)
    if (!localStorage.getItem('reps_cycle_index')) {
        localStorage.setItem('reps_cycle_index', '0');
    }
    emit('save');
    emit('close');
}
</script>
