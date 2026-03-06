<template>
  <div class="fixed inset-0 z-[60] flex items-center justify-center p-4">
    <div class="absolute inset-0 bg-black/80 backdrop-blur-sm" @click="$emit('close')"></div>
    
    <div class="relative w-full max-w-[600px] bg-[#111827] border border-[#1F2937] rounded-2xl overflow-hidden flex flex-col shadow-2xl animate-scale-in">
      <!-- HEADER -->
      <div class="p-6 border-b border-[#1F2937] flex justify-between items-center bg-gradient-to-r from-[rgba(147,51,234,0.1)] to-transparent">
        <div class="flex items-center gap-3">
          <div class="w-10 h-10 bg-[#9333EA] rounded-xl flex items-center justify-center shadow-lg shadow-purple-500/20">
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
              <path d="m12 3 1.912 5.813a2 2 0 0 0 1.275 1.275L21 12l-5.813 1.912a2 2 0 0 0-1.275 1.275L12 21l-1.912-5.813a2 2 0 0 0-1.275-1.275L3 12l5.813-1.912a2 2 0 0 0 1.275-1.275L12 3Z"/>
            </svg>
          </div>
          <h2 class="text-2xl font-bold text-white">Generar con IA</h2>
        </div>
        <button @click="$emit('close')" class="p-2 text-[#9CA3AF] hover:text-white transition-colors">
          <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M18 6L6 18M6 6l12 12"/></svg>
        </button>
      </div>

      <!-- BODY -->
      <div class="p-6 overflow-y-auto max-h-[70vh] custom-scrollbar">
        <div class="grid grid-cols-1 sm:grid-cols-2 gap-4 mb-6">
          <!-- Objetivo -->
          <div class="col-span-1">
            <label class="block text-xs font-bold text-[#9CA3AF] uppercase mb-2">Objetivo Principal</label>
            <select v-model="form.goal" class="w-full bg-[#0A0A0A] border border-[#374151] rounded-lg p-3 text-sm text-white focus:border-[#9333EA] outline-none">
              <option value="hipertrofia">Hipertrofia</option>
              <option value="fuerza">Fuerza</option>
              <option value="resistencia">Resistencia</option>
              <option value="grasa">Pérdida de Grasa</option>
            </select>
          </div>

          <!-- Nivel -->
          <div class="col-span-1">
            <label class="block text-xs font-bold text-[#9CA3AF] uppercase mb-2">Nivel</label>
            <select v-model="form.level" class="w-full bg-[#0A0A0A] border border-[#374151] rounded-lg p-3 text-sm text-white focus:border-[#9333EA] outline-none">
              <option value="principiante">Principiante</option>
              <option value="intermedio">Intermedio</option>
              <option value="avanzado">Avanzado</option>
            </select>
          </div>

          <!-- Días -->
          <div class="col-span-1">
            <label class="block text-xs font-bold text-[#9CA3AF] uppercase mb-2">Días Semanales</label>
            <select v-model="form.days" class="w-full bg-[#0A0A0A] border border-[#374151] rounded-lg p-3 text-sm text-white focus:border-[#9333EA] outline-none">
              <option value="2">2 días</option>
              <option value="3">3 días</option>
              <option value="4">4 días</option>
              <option value="5">5 días</option>
            </select>
          </div>

          <!-- Duración -->
          <div class="col-span-1">
            <label class="block text-xs font-bold text-[#9CA3AF] uppercase mb-2">Duración Sesión</label>
            <select v-model="form.duration" class="w-full bg-[#0A0A0A] border border-[#374151] rounded-lg p-3 text-sm text-white focus:border-[#9333EA] outline-none">
              <option value="30-45">30-45 min</option>
              <option value="45-60">45-60 min</option>
              <option value="60-90">60-90 min</option>
            </select>
          </div>
        </div>

        <!-- Enfoque Muscular -->
        <div class="mb-6">
          <label class="block text-xs font-bold text-[#9CA3AF] uppercase mb-3">Enfoque Muscular (Opcional)</label>
          <div class="flex flex-wrap gap-2">
            <button 
              v-for="m in muscles" 
              :key="m"
              @click="toggleMuscle(m)"
              class="px-4 py-2 rounded-lg text-sm transition-all border"
              :class="form.muscles.includes(m) ? 'bg-[#9333EA] border-[#9333EA] text-white' : 'bg-transparent border-[#374151] text-[#9CA3AF] hover:border-[#9333EA]'"
            >
              {{ m }}
            </button>
          </div>
        </div>

        <!-- Equipo -->
        <div class="mb-6">
          <label class="block text-xs font-bold text-[#9CA3AF] uppercase mb-3">Equipo Disponible</label>
          <div class="grid grid-cols-2 gap-3">
             <label v-for="e in equipment" :key="e" class="flex items-center gap-3 p-3 bg-[#0A0A0A] border border-[#374151] rounded-lg cursor-pointer hover:border-[#9333EA] transition-all">
                <input type="checkbox" :value="e" v-model="form.equipment" class="accent-[#9333EA] w-4 h-4">
                <span class="text-sm text-white">{{ e }}</span>
             </label>
          </div>
        </div>

        <!-- Notas -->
        <div>
          <label class="block text-xs font-bold text-[#9CA3AF] uppercase mb-2">Información Adicional (Opcional)</label>
          <textarea 
            v-model="form.notes"
            placeholder="Ej: Lesión en rodilla, preferencia por ejercicios compuestos..."
            class="w-full bg-[#0A0A0A] border border-[#374151] rounded-lg p-3 text-sm text-white focus:border-[#9333EA] outline-none resize-none h-24"
          ></textarea>
        </div>
      </div>

      <!-- FOOTER -->
      <div class="p-6 border-t border-[#1F2937] flex justify-end gap-3">
        <button @click="$emit('close')" class="bg-transparent border border-[#374151] hover:border-[#9333EA] rounded-lg px-6 py-2.5 text-[15px] font-semibold text-[#9CA3AF] transition-all">
          Cancelar
        </button>
        <button 
          @click="generate" 
          class="bg-[#9333EA] hover:bg-[#7C3AED] rounded-lg px-6 py-2.5 flex items-center gap-2 text-[15px] font-semibold text-white transition-all hover:scale-105 active:scale-95 shadow-lg shadow-purple-500/20"
        >
          <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
            <path d="m12 3 1.912 5.813a2 2 0 0 0 1.275 1.275L21 12l-5.813 1.912a2 2 0 0 0-1.275 1.275L12 21l-1.912-5.813a2 2 0 0 0-1.275-1.275L3 12l5.813-1.912a2 2 0 0 0 1.275-1.275L12 3Z"/>
          </svg>
          Generar con IA
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive } from 'vue';

const emit = defineEmits(['close', 'generate']);

const muscles = ['Pecho', 'Espalda', 'Piernas', 'Hombros', 'Brazos', 'Core'];
const equipment = ['Barra y discos', 'Mancuernas', 'Máquinas', 'Peso corporal', 'Bandas', 'Kettlebells'];

const form = reactive<{
  goal: string;
  level: string;
  days: string;
  duration: string;
  muscles: string[];
  equipment: string[];
  notes: string;
}>({
  goal: 'hipertrofia',
  level: 'principiante',
  days: '3',
  duration: '45-60',
  muscles: [],
  equipment: [],
  notes: ''
});

const toggleMuscle = (m: string) => {
  const index = form.muscles.indexOf(m);
  if (index > -1) form.muscles.splice(index, 1);
  else form.muscles.push(m);
};

const generate = () => {
  emit('generate', { ...form });
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
</style>
