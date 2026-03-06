<template>
  <div class="fixed inset-0 z-[70] flex items-center justify-center p-4">
    <div class="absolute inset-0 bg-black/40 backdrop-blur-sm" @click="$emit('close')"></div>
    
    <div class="relative w-full max-w-md bg-[#111827] border border-[#1F2937] rounded-2xl shadow-2xl animate-scale-in">
      <div class="p-6 border-b border-[#1F2937] flex justify-between items-center bg-black/20">
        <h3 class="text-xl font-bold text-white uppercase tracking-tight">Nuevo Ejercicio</h3>
        <button @click="$emit('close')" class="text-[#9CA3AF] hover:text-white transition-colors">
          <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M18 6L6 18M6 6l12 12"/></svg>
        </button>
      </div>

      <div class="p-6 space-y-5">
        <div>
          <label class="block text-xs font-black text-gray-400 uppercase tracking-widest mb-2">Nombre del Ejercicio</label>
          <input 
            v-model="nombre"
            type="text" 
            placeholder="Ej: Press de Banca" 
            class="w-full bg-black border border-[#374151] rounded-xl p-3.5 text-white outline-none focus:border-[#DC2626] transition-all"
          >
        </div>

        <div>
          <label class="block text-xs font-black text-gray-400 uppercase tracking-widest mb-2">Grupo Muscular</label>
          <select 
            v-model="grupoMuscular"
            class="w-full bg-black border border-[#374151] rounded-xl p-3.5 text-white outline-none focus:border-[#DC2626] transition-all appearance-none"
          >
            <option v-for="(name, val) in muscleGroupMap" :key="val" :value="val">{{ name }}</option>
          </select>
        </div>

        <div>
          <label class="block text-xs font-black text-gray-400 uppercase tracking-widest mb-2">Descripción (Opcional)</label>
          <textarea 
            v-model="descripcion"
            placeholder="Describe brevemente el ejercicio..." 
            rows="3"
            class="w-full bg-black border border-[#374151] rounded-xl p-3.5 text-white outline-none focus:border-[#DC2626] transition-all resize-none"
          ></textarea>
        </div>
      </div>

      <div class="p-6 bg-black/20 flex gap-3">
        <button 
          @click="$emit('close')" 
          class="flex-1 py-3.5 rounded-xl border border-[#374151] text-gray-400 font-bold hover:bg-gray-800 transition-all uppercase text-xs tracking-widest"
        >
          Cancelar
        </button>
        <button 
          @click="submit"
          :disabled="!nombre || isSaving"
          class="flex-1 py-3.5 rounded-xl bg-[#DC2626] text-white font-bold hover:bg-red-700 transition-all uppercase text-xs tracking-widest flex items-center justify-center gap-2"
        >
          <span v-if="isSaving" class="w-4 h-4 border-2 border-white/30 border-t-white rounded-full animate-spin"></span>
          {{ isSaving ? 'Guardando...' : 'Crear' }}
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { ejerciciosApi } from '../../api';
import { useUIStore } from '../../stores/ui';

const emit = defineEmits(['close', 'created']);
const uiStore = useUIStore();

const nombre = ref('');
const grupoMuscular = ref(10); // Otro
const descripcion = ref('');
const isSaving = ref(false);

const muscleGroupMap = {
  0: 'Pecho',
  1: 'Espalda',
  2: 'Pierna',
  3: 'Hombro',
  4: 'Brazos',
  5: 'Abdomen',
  6: 'Biceps',
  7: 'Triceps',
  8: 'Cardio',
  9: 'FullBody',
  10: 'Otro'
};

const submit = async () => {
  if (!nombre.value) return;
  isSaving.value = true;
  try {
    const res = await ejerciciosApi.crear({
      nombre: nombre.value,
      descripcion: descripcion.value,
      grupoMuscular: parseInt(grupoMuscular.value as any),
      tipoEjercicio: 0 // Fuerza
    });
    uiStore.showToast("Ejercicio creado exitosamente", "success");
    emit('created', res.data);
    emit('close');
  } catch (e) {
    console.error(e);
    uiStore.showToast("Error al crear el ejercicio", "error");
  } finally {
    isSaving.value = false;
  }
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
</style>
