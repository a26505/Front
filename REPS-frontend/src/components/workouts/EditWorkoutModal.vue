<template>
  <div class="fixed inset-0 z-[60] flex items-center justify-center p-4">
    <div class="absolute inset-0 bg-black/80 backdrop-blur-sm" @click="$emit('close')"></div>
    
    <div class="relative w-full max-w-[900px] bg-[#111827] border border-[#1F2937] rounded-2xl overflow-hidden flex flex-col shadow-2xl animate-scale-in">
      <!-- HEADER -->
      <div class="p-6 border-b border-[#1F2937] flex justify-between items-center">
        <h2 class="text-2xl font-bold text-white">Editar Rutina</h2>
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

        <!-- Nivel de Dificultad -->
        <div class="mb-6">
          <label class="block text-sm font-semibold text-white mb-2">Dificultad</label>
          <div class="grid grid-cols-3 gap-3">
            <button 
              v-for="(label, value) in { 0: 'Principiante', 1: 'Intermedio', 2: 'Avanzado' }" 
              :key="value"
              @click="difficulty = parseInt(value)"
              type="button"
              class="py-2.5 rounded-lg text-xs font-bold transition-all border uppercase tracking-widest"
              :class="difficulty === parseInt(value) ? 'bg-[#DC2626] border-[#DC2626] text-white shadow-[0_0_15px_rgba(220,38,38,0.4)]' : 'bg-[#0A0A0A] border-[#374151] text-[#9CA3AF] hover:border-white'"
            >
              {{ label }}
            </button>
          </div>
        </div>

        <div class="mb-6">
          <label class="block text-sm font-semibold text-white mb-2">Imagen de la rutina</label>
          <div class="flex flex-col gap-3">
            <div class="flex gap-3">
              <input 
                type="text" 
                v-model="imagenUrl"
                placeholder="URL de la imagen o selecciona un archivo"
                class="flex-1 bg-[#0A0A0A] border border-[#374151] rounded-lg p-3 text-sm text-white focus:border-[#DC2626] outline-none transition-all"
              >
              <div v-if="imagenUrl" class="w-12 h-12 shrink-0 rounded-lg overflow-hidden border border-[#374151]">
                <img :src="imagenUrl" class="w-full h-full object-cover" />
              </div>
            </div>
            
            <div class="flex items-center gap-3">
              <button 
                @click="triggerFileInput" 
                type="button"
                class="flex-1 bg-[#1F2937] hover:bg-[#374151] border border-[#374151] rounded-lg p-2.5 text-xs font-bold text-white transition-all flex items-center justify-center gap-2 uppercase tracking-widest"
                :disabled="isUploading"
              >
                <svg v-if="!isUploading" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                  <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path>
                  <polyline points="17 8 12 3 7 8"></polyline>
                  <line x1="12" y1="3" x2="12" y2="15"></line>
                </svg>
                <span v-if="isUploading" class="w-4 h-4 border-2 border-white/30 border-t-white rounded-full animate-spin"></span>
                {{ isUploading ? 'Subiendo...' : 'Subir Imagen' }}
              </button>
              <input 
                type="file" 
                ref="fileInput" 
                class="hidden" 
                @change="onFileSelected" 
                accept="image/*"
              >
            </div>
          </div>
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
              v-model="searchQuery"
              placeholder="Buscar ejercicios..." 
              class="w-full bg-[#0A0A0A] border border-[#374151] rounded-lg py-3 pl-10 pr-4 text-sm text-white focus:border-[#DC2626] outline-none transition-all"
            >
          </div>

          <!-- Lista ejercicios disponibles -->
          <div class="flex justify-between items-center mb-2">
            <span class="text-xs font-bold text-[#9CA3AF] uppercase">Resultados</span>
            <button @click="showCreateExModal = true" class="text-[10px] font-black text-[#DC2626] uppercase tracking-widest border border-[#DC2626]/30 px-2 py-1 rounded hover:bg-[#DC2626] hover:text-white transition-all">+ Crear Ejercicio</button>
          </div>
          <div class="bg-[#0A0A0A] border border-[#374151] rounded-lg p-2 max-h-48 overflow-y-auto custom-scrollbar mb-6">
            <div 
              v-for="ex in filteredExercises" 
              :key="ex.id"
              class="p-2.5 flex justify-between items-center rounded-md hover:bg-[#1F2937] cursor-pointer group transition-colors"
              @click="addExercise(ex)"
            >
              <span class="text-sm text-white">{{ ex.name }}</span>
              <div class="flex items-center gap-2">
                <span class="bg-[rgba(220,38,38,0.15)] px-2 py-0.5 rounded text-[10px] text-[#FCA5A5] uppercase font-bold">{{ ex.muscle }}</span>
                <button v-if="ex.esMio" @click.stop="deleteCustomExercise(ex)" class="w-6 h-6 bg-transparent border border-[#DC2626]/30 hover:bg-[#DC2626] hover:border-[#DC2626] rounded flex items-center justify-center text-[#DC2626] hover:text-white transition-all" title="Eliminar ejercicio">
                  <svg width="10" height="10" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3"><path d="M18 6L6 18M6 6l12 12"/></svg>
                </button>
                <div @click.stop="addExercise(ex)" class="w-7 h-7 bg-[#DC2626] rounded flex items-center justify-center text-white font-bold transition-transform group-hover:scale-125 hover:bg-red-500 shadow-lg shadow-red-900/20 active:scale-90">+</div>
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
      <div class="p-6 border-t border-[#1F2937] flex flex-col gap-3">
        <p v-if="saveError" class="text-[#FCA5A5] text-sm text-center">{{ saveError }}</p>
        <div class="flex justify-end gap-3">
          <button @click="$emit('close')" class="bg-transparent border border-[#374151] hover:border-[#DC2626] rounded-lg px-6 py-2.5 text-[15px] font-semibold text-[#9CA3AF] transition-all">
            Cancelar
          </button>
          <button 
            @click="saveWorkout" 
            :disabled="!name || selectedExercises.length === 0 || isSaving"
            class="bg-[#DC2626] hover:bg-[#B91C1C] disabled:opacity-50 disabled:cursor-not-allowed rounded-lg px-6 py-2.5 flex items-center gap-2 text-[15px] font-semibold text-white transition-all hover:scale-105 active:scale-95"
          >
            <span v-if="isSaving" class="w-4 h-4 border-2 border-white/30 border-t-white rounded-full animate-spin"></span>
            <svg v-else width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5"><path d="M20 6L9 17l-5-5"/></svg>
            {{ isSaving ? 'Guardando...' : 'Guardar Cambios' }}
          </button>
        </div>
      </div>
    </div>
    <CreateEjercicioModal 
      v-if="showCreateExModal" 
      @close="showCreateExModal = false" 
      @created="handleExerciseCreated"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { rutinasApi, ejerciciosApi } from '../../api';
import { useUIStore } from '../../stores/ui';
import CreateEjercicioModal from './CreateEjercicioModal.vue';

const uiStore = useUIStore();

const props = defineProps<{
  workoutToEdit: any;
}>();

const emit = defineEmits(['close', 'save']);

// --- STATE ---
const searchQuery = ref('');
const showCreateExModal = ref(false);
const name = ref('');
const difficulty = ref(1);
const imagenUrl = ref('');
const selectedExercises = ref<any[]>([]);
const availableExercises = ref<any[]>([]);
const isSaving = ref(false);
const isUploading = ref(false);
const saveError = ref('');
const fileInput = ref<HTMLInputElement | null>(null);

// --- DRILL DOWN ---
const filteredExercises = computed(() => {
  if (!availableExercises.value) return [];
  const q = searchQuery.value.toLowerCase().trim();
  return availableExercises.value.filter(ex => {
    const nameStr = ex.name ? String(ex.name).toLowerCase() : '';
    const muscleStr = ex.muscle !== undefined && ex.muscle !== null ? String(ex.muscle).toLowerCase() : '';
    return nameStr.includes(q) || muscleStr.includes(q);
  });
});

// --- METHODS ---
const triggerFileInput = () => {
  fileInput.value?.click();
};

const onFileSelected = async (event: any) => {
  const file = event.target.files[0];
  if (!file) return;
  
  isUploading.value = true;
  try {
    const formData = new FormData();
    formData.append('file', file);
    formData.append('upload_preset', 'ml_default');
    
    const response = await fetch('https://api.cloudinary.com/v1_1/dgtahwqpj/upload', {
      method: 'POST',
      body: formData
    });
    
    const data = await response.json();
    if (data.secure_url) {
      imagenUrl.value = data.secure_url;
      saveError.value = '';
    } else {
      console.error('Cloudinary upload error:', data);
      saveError.value = `Error al subir: ${data.error?.message || 'Preset inválido'}`;
    }
  } catch (err: any) {
    console.error('Error uploading to Cloudinary:', err);
    saveError.value = `Error de conexión: ${err.message}`;
  } finally {
    isUploading.value = false;
  }
};

const loadExercises = async () => {
    try {
        const res = await ejerciciosApi.getAll();
        const muscleGroupMap: Record<number, string> = {
            0: 'Pecho', 1: 'Espalda', 2: 'Pierna', 3: 'Hombro', 4: 'Bíceps',
            5: 'Tríceps', 6: 'Abdomen', 7: 'Cardio', 8: 'Full Body', 9: 'Otro'
        };

        availableExercises.value = (res.data as any[]).map((e: any) => {
            let muscleName = 'Otro';
            if (typeof e.grupoMuscular === 'number') {
                muscleName = muscleGroupMap[e.grupoMuscular] || 'Otro';
            } else if (typeof e.grupoMuscular === 'string') {
                muscleName = e.grupoMuscular;
            }

            return { id: e.id, name: e.nombre, muscle: muscleName, esMio: e.esMio || false };
        });
    } catch (e) {
        console.error("Error loading exercises", e);
    }
}

const addExercise = (ex: any) => {
  selectedExercises.value.push({
    ...ex,
    sets: 3,
    reps: '10-12',
    rest: '90s',
    weight: '0 kg'
  });
  searchQuery.value = '';
};

const removeExercise = (index: number) => {
  selectedExercises.value.splice(index, 1);
};

const handleExerciseCreated = async (newEx: any) => {
    const muscleGroupMap: Record<number, string> = {
      0: 'Pecho', 1: 'Espalda', 2: 'Pierna', 3: 'Hombro', 4: 'Bíceps',
      5: 'Tríceps', 6: 'Abdomen', 7: 'Cardio', 8: 'Full Body', 9: 'Otro'
    };
    
    const mappedEx = {
        id: newEx.id,
        name: newEx.nombre,
        muscle: typeof newEx.grupoMuscular === 'number' ? muscleGroupMap[newEx.grupoMuscular] : newEx.grupoMuscular,
        esMio: true
    };
    
    availableExercises.value.unshift(mappedEx);
    addExercise(mappedEx);
};

const deleteCustomExercise = async (ex: any) => {
    try {
        await ejerciciosApi.eliminar(ex.id);
        availableExercises.value = availableExercises.value.filter(e => e.id !== ex.id);
        uiStore.showToast('Ejercicio eliminado', 'success');
    } catch {
        uiStore.showToast('Error al eliminar ejercicio', 'error');
    }
};

const saveWorkout = async () => {
  if (!name.value || selectedExercises.value.length === 0) return;
  isSaving.value = true;
  saveError.value = '';
  try {
    const dto = {
      nombre: name.value,
      imagenUrl: imagenUrl.value,
      nivel: difficulty.value,
      ejercicios: selectedExercises.value.map((e: any) => ({
        ejercicioId: e.id,
        series: parseInt(e.sets) || 3,
        repeticiones: String(e.reps) || '10-12',
        descansoSegundos: parseInt(e.rest) || 90,
        tipo: 0
      }))
    };
    await rutinasApi.actualizar(props.workoutToEdit.id, dto);
    emit('save', { name: name.value, exercises: selectedExercises.value });
    emit('close');
  } catch (err: any) {
    saveError.value = err.response?.data?.message ?? 'Error al actualizar';
  } finally {
    isSaving.value = false;
  }
};

onMounted(async () => {
    if (props.workoutToEdit) {
      name.value = props.workoutToEdit.title || props.workoutToEdit.nombre || '';
      imagenUrl.value = props.workoutToEdit.image || props.workoutToEdit.urlImagen || '';
      difficulty.value = props.workoutToEdit.difficultyNum ?? props.workoutToEdit.nivel ?? 1;
      
      if (props.workoutToEdit.exerciseList) {
          selectedExercises.value = props.workoutToEdit.exerciseList.map((e: any) => ({
              id: e.ejercicioId || e.id,
              name: e.name || e.nombreEjercicio,
              muscle: e.muscle || e.grupoMuscular || '',
              sets: e.sets || e.series || 3,
              reps: e.reps || e.repeticiones || '10-12',
              rest: e.rest || e.descansoSegundos || 90,
              weight: e.weight || '0 kg'
          }));
      } else if (props.workoutToEdit.id) {
          try {
              const res = await rutinasApi.getById(props.workoutToEdit.id);
              if (res.data?.ejercicios) {
                   selectedExercises.value = res.data.ejercicios.map((e: any) => ({
                      id: e.ejercicioId,
                      name: e.nombreEjercicio,
                      muscle: e.grupoMuscular,
                      sets: e.series,
                      reps: e.repeticiones,
                      rest: e.descansoSegundos,
                      weight: '0 kg'
                  }));
              }
          } catch(err) {
              console.error("Error loading workout detail", err);
          }
      }
    }
    await loadExercises();
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
@keyframes fadeIn {
  from { opacity: 0; transform: translateY(10px); }
  to { opacity: 1; transform: translateY(0); }
}
.animate-fade-in {
  animation: fadeIn 0.4s ease-out forwards;
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
