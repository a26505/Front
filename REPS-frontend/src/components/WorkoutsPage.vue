<template>
  <div class="flex min-h-screen bg-[#000000] text-white">
    <!-- SIDEBAR UNIFICADO -->
    <Sidebar active="workouts" />

    <!-- 2️⃣ CONTENIDO PRINCIPAL -->
    <div class="flex-1 md:ml-[256px] min-h-screen flex flex-col">
      <!-- HEADER STICKY -->
      <header class="sticky top-0 z-40 w-full h-[80px] bg-black/95 backdrop-blur-md px-6 flex items-center justify-between border-b border-[#1F2937]/50">
        <h1 class="text-2xl md:text-3xl font-bold text-white tracking-tight">Mis Entrenamientos</h1>
        
        <div class="flex gap-2 md:gap-3">
          <button @click="showCiclosModal = true" class="bg-[#2563EB] hover:bg-[#1D4ED8] rounded-lg px-3 md:px-4 py-2 flex items-center gap-2 text-xs md:text-sm font-bold transition-all hover:scale-105 active:scale-95 shadow-lg shadow-blue-500/20">
            <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
              <rect x="3" y="4" width="18" height="18" rx="2" ry="2"></rect><line x1="16" y1="2" x2="16" y2="6"></line><line x1="8" y1="2" x2="8" y2="6"></line><line x1="3" y1="10" x2="21" y2="10"></line>
            </svg>
            <span class="hidden sm:inline">Mi Ciclo</span>
            <span class="sm:hidden">Ciclo</span>
          </button>
          
          <button @click="openIAGenerator" class="bg-[#9333EA] hover:bg-[#7C3AED] rounded-lg px-3 md:px-4 py-2 flex items-center gap-2 text-xs md:text-sm font-bold transition-all hover:scale-105 active:scale-95">
            <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
              <path d="m12 3 1.912 5.813a2 2 0 0 0 1.275 1.275L21 12l-5.813 1.912a2 2 0 0 0-1.275 1.275L12 21l-1.912-5.813a2 2 0 0 0-1.275-1.275L3 12l5.813-1.912a2 2 0 0 0 1.275-1.275L12 3Z"/>
            </svg>
            <span class="hidden sm:inline">Generar con IA</span>
            <span class="sm:hidden">IA</span>
          </button>
          
          <button @click="showCreateModal = true" class="bg-[#DC2626] hover:bg-[#B91C1C] rounded-lg px-3 md:px-4 py-2 flex items-center gap-2 text-xs md:text-sm font-bold transition-all hover:scale-105 active:scale-95">
            <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2.5"><path d="M12 5v14m-7-7h14"/></svg>
            <span class="hidden sm:inline">Nueva Rutina</span>
            <span class="sm:hidden">Nueva</span>
          </button>
        </div>
      </header>

      <!-- CONTENIDO PRINCIPAL -->
      <main class="flex-1 p-6 bg-black overflow-x-hidden">

        <!-- BUSCADOR Y FILTROS INTEGRADOS -->
        <div class="flex items-center gap-4 mb-4">
          <div class="flex-1 relative">
            <svg class="absolute left-3 top-1/2 -translate-y-1/2 text-[#374151]" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5">
              <circle cx="11" cy="11" r="8"/><path d="m21 21-4.3-4.3"/>
            </svg>
            <input 
              v-model="searchQuery"
              type="text" 
              placeholder="Buscar entrenamientos..." 
              class="w-full bg-[#0A0D14] border border-[#1F2937] rounded-lg py-2.5 pl-10 pr-4 text-sm text-white focus:border-[#DC2626] outline-none transition-all placeholder-[#374151]"
            >
          </div>
          
          <button @click="showFilters = !showFilters" class="bg-[#0A0D14] border border-[#1F2937] hover:border-[#DC2626] rounded-lg px-4 py-2.5 flex items-center gap-2 text-xs font-bold text-white transition-all uppercase tracking-widest">
            <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5">
              <line x1="4" y1="21" x2="4" y2="14"/><line x1="4" y1="10" x2="4" y2="3"/>
              <line x1="12" y1="21" x2="12" y2="12"/><line x1="12" y1="8" x2="12" y2="3"/>
              <line x1="20" y1="21" x2="20" y2="16"/><line x1="20" y1="12" x2="20" y2="3"/>
              <line x1="1" y1="14" x2="7" y2="14"/><line x1="9" y1="8" x2="15" y2="8"/><line x1="17" y1="16" x2="23" y2="16"/>
            </svg>
            Filtros
          </button>
        </div>

        <!-- PANEL DE FILTROS DESPLEGABLE (Basado en captura) -->
        <transition name="expand">
          <div v-if="showFilters" class="mb-8 p-6 bg-[#0A0D14] border border-[#1F2937] rounded-xl animate-fade-in overflow-hidden shadow-2xl">
            <div class="space-y-6">
              <!-- Dificultad -->
              <div>
                <h4 class="text-xs font-bold text-white uppercase tracking-wider mb-4">Dificultad</h4>
                <div class="flex flex-wrap gap-2">
                  <button 
                    v-for="d in difficulties" 
                    :key="d"
                    @click="filterDifficulty = d"
                    class="px-4 py-2 rounded-lg text-xs font-bold transition-all border"
                    :class="filterDifficulty === d ? 'bg-[#DC2626] border-[#DC2626] text-white shadow-lg shadow-red-900/20' : 'bg-black border-[#1F2937] text-[#9CA3AF] hover:border-[#DC2626]'"
                  >
                    {{ d }}
                  </button>
                </div>
              </div>

              <!-- Grupos Musculares -->
              <div>
                <h4 class="text-xs font-bold text-white uppercase tracking-wider mb-4">Grupos Musculares</h4>
                <div class="flex flex-wrap gap-2">
                  <button 
                    v-for="m in muscles" 
                    :key="m"
                    @click="toggleMuscleFilter(m)"
                    class="px-4 py-2 rounded-lg text-xs font-bold transition-all border"
                    :class="selectedMuscles.includes(m) ? 'bg-white border-white text-black' : 'bg-black border-[#1F2937] text-[#9CA3AF] hover:border-[#DC2626]'"
                  >
                    {{ m }}
                  </button>
                </div>
              </div>

              <!-- Acciones -->
              <div class="pt-2">
                <button @click="clearFilters" class="text-[#374151] hover:text-[#9CA3AF] text-[11px] font-black uppercase tracking-widest transition-colors flex items-center gap-2">
                  Limpiar filtros
                </button>
              </div>
            </div>
          </div>
        </transition>

        <!-- TABS STYLE -->
        <div class="flex gap-2 mb-6 overflow-x-auto whitespace-nowrap hide-scrollbar">
          <button 
            v-for="tab in tabs" 
            :key="tab.id"
            @click="activeTab = tab.id"
            class="px-4 py-2 text-xs font-black uppercase tracking-widest transition-all rounded-lg border"
            :class="[
              activeTab === tab.id 
                ? 'bg-[#DC2626]/20 text-[#DC2626] border-[#DC2626]/30'
                : 'text-[#9CA3AF] border-transparent hover:text-white hover:bg-white/5'
            ]"
          >
            {{ tab.name }}
          </button>
        </div>

        <!-- LISTA DE ENTRENAMIENTOS -->
        <div class="flex flex-col gap-4 animate-fade-in pb-12">
              <div v-if="activeTab === 'my'">
                <div v-if="filteredMyWorkouts.length === 0" class="flex flex-col items-center justify-center py-12 px-6 bg-[rgba(31,41,55,0.2)] border border-dashed border-[#374151] rounded-[12px] mt-6">
                  <p class="text-[#9CA3AF] text-center font-medium">No tienes ninguna rutina guardada.<br>Crea una nueva rutina o busca en la comunidad.</p>
                </div>
                <WorkoutCard 
                   v-for="workout in filteredMyWorkouts" 
                   :key="workout.id" 
                   :workout="workout" 
                   type="my"
                   @click="openDetail(workout)"
                   @delete="deleteWorkout"
                   @publish="publishWorkout"
                   @edit="openEditModal"
                />
             </div>



             <div v-if="activeTab === 'ai'">
                <!-- Banner Pro for AI Tab -->
                <!-- Premium AI Pro Access Section -->
                <div v-if="!isPro" class="relative py-20 px-8 text-center rounded-3xl mb-12 overflow-hidden border border-[#9333EA]/20 group">
                  <!-- Animated Background Blobs -->
                  <div class="absolute inset-0 bg-[#0A0D14]"></div>
                  <div class="absolute -top-24 -left-24 w-64 h-64 bg-[#9333EA]/10 blur-[100px] rounded-full animate-pulse"></div>
                  <div class="absolute -bottom-24 -right-24 w-80 h-80 bg-[#4F46E5]/10 blur-[120px] rounded-full animate-pulse" style="animation-delay: 1.5s"></div>
                  
                  <div class="relative z-10 flex flex-col items-center">
                    <!-- Icon Container with floating animation -->
                    <div class="w-20 h-20 bg-gradient-to-br from-[#1E1B4B] to-[#0A0A0A] border border-[#9333EA]/30 rounded-2xl flex items-center justify-center mb-8 shadow-2xl shadow-purple-900/40 relative group-hover:scale-110 transition-transform duration-500 animate-float">
                      <div class="absolute inset-0 bg-[#9333EA]/10 blur-xl opacity-0 group-hover:opacity-100 transition-opacity"></div>
                      <svg width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" class="relative z-10 drop-shadow-[0_0_8px_rgba(147,51,234,0.8)]">
                        <path d="m12 3 1.912 5.813a2 2 0 0 0 1.275 1.275L21 12l-5.813 1.912a2 2 0 0 0-1.275 1.275L12 21l-1.912-5.813a2 2 0 0 0-1.275-1.275L3 12l5.813-1.912a2 2 0 0 0 1.275-1.275L12 3Z"/>
                      </svg>
                    </div>

                    <!-- Title with Gradient -->
                    <h2 class="text-4xl md:text-5xl font-black mb-4 uppercase tracking-tighter bg-gradient-to-r from-white via-purple-200 to-white bg-clip-text text-transparent drop-shadow-sm">
                      Acceso IA Pro
                    </h2>
                    
                    <p class="text-[15px] text-gray-400 font-medium mb-10 max-w-sm mx-auto leading-relaxed">
                      Lleva tus entrenamientos al siguiente nivel con rutinas <span class="text-purple-300">ultra-personalizadas</span> basadas en tu progreso real y objetivos actuales.
                    </p>

                    <router-link to="/select-plan" class="relative bg-white text-black hover:text-white px-10 py-4 rounded-xl text-sm font-black transition-all hover:scale-105 active:scale-95 uppercase tracking-widest shadow-[0_0_40px_rgba(255,255,255,0.2)] hover:shadow-purple-500/40 group/btn border border-transparent overflow-hidden">
                      <div class="absolute inset-0 bg-gradient-to-r from-[#9333EA] to-[#7C3AED] translate-y-full group-hover/btn:translate-y-0 transition-transform duration-300 ease-out"></div>
                      <span class="relative z-10">Mejorar ahora</span>
                    </router-link>
                  </div>
                </div>

                <div v-if="filteredAiWorkouts.length === 0 && isPro" class="flex flex-col items-center justify-center py-12 px-6 bg-[rgba(31,41,55,0.2)] border border-dashed border-[#374151] rounded-[12px] mt-6">
                  <p class="text-[#9CA3AF] text-center font-medium">No tienes entrenamientos generados con IA.<br>Usa el botón "Generar con IA" para crear uno.</p>
                </div>
                <WorkoutCard 
                   v-for="workout in filteredAiWorkouts" 
                   :key="workout.id" 
                   :workout="workout" 
                   type="ai"
                   @click="openDetail(workout)"
                   @delete="deleteWorkout"
                   @edit="openEditModal"
                />
             </div>
        </div>
      </main>
    </div>

    <!-- MODALES -->
    <CreateWorkoutModal v-if="showCreateModal" @close="handleWorkoutCreated" />
    <AIGeneratorModal v-if="showIAModal" @close="showIAModal = false" @generate="handleGenerateAI" />
    <WorkoutDetailModal v-if="selectedWorkout" :workout="selectedWorkout" @close="selectedWorkout = null" />
    <EditWorkoutModal 
      v-if="showEditModal && workoutToEdit" 
      :workoutToEdit="workoutToEdit" 
      @close="showEditModal = false; workoutToEdit = null"
      @save="handleWorkoutEdited"
    />
    <CiclosModal
      v-if="showCiclosModal"
      :myWorkouts="myWorkouts"
      @close="showCiclosModal = false"
      @save="onCicloSave"
    />

    <!-- Los modales de eliminación y publicación se han migrado a uiStore.showConfirm -->
    <!-- El Toast local se ha migrado a uiStore.showToast -->
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import Sidebar from './Sidebar.vue';
import WorkoutCard from './workouts/WorkoutCard.vue';
import CreateWorkoutModal from './workouts/CreateWorkoutModal.vue';
import AIGeneratorModal from './workouts/AIGeneratorModal.vue';
import WorkoutDetailModal from './workouts/WorkoutDetailModal.vue';
import EditWorkoutModal from './workouts/EditWorkoutModal.vue';
import CiclosModal from './workouts/CiclosModal.vue';
import { rutinasApi } from '../api';
import { useAuthStore } from '../stores/auth';

import { useUIStore } from '../stores/ui';
import { useRouter } from 'vue-router';

const authStore = useAuthStore();
const router = useRouter();
const uiStore = useUIStore();
const activeTab = ref('my');
const showFilters = ref(false);

const showCreateModal = ref(false);
const showIAModal = ref(false);
const showEditModal = ref(false);
const showCiclosModal = ref(false);
const workoutToEdit = ref<any>(null);
const selectedWorkout = ref<any>(null);

const isPro = computed(() => authStore.isPro);

// Filtros
const searchQuery = ref('');
const filterDifficulty = ref('Todas');
const selectedMuscles = ref<string[]>([]);

const difficulties = ['Todas', 'Principiante', 'Intermedio', 'Avanzado'];
const muscles = ['Pecho', 'Espalda', 'Pierna', 'Hombro', 'Biceps', 'Triceps', 'Abdomen', 'Cardio', 'FullBody', 'Otro'];

const toggleMuscleFilter = (muscle: string) => {
  const index = selectedMuscles.value.indexOf(muscle);
  if (index > -1) selectedMuscles.value.splice(index, 1);
  else selectedMuscles.value.push(muscle);
};

const clearFilters = () => {
  searchQuery.value = '';
  filterDifficulty.value = 'Todas';
  selectedMuscles.value = [];
};

const onCicloSave = () => {
  uiStore.showToast('¡Ciclo organizado correctamente!', 'success');
};

const openIAGenerator = () => {
  if (isPro.value) {
    showIAModal.value = true;
  } else {
    router.push('/select-plan');
  }
};

const filterWorkouts = (list: any[]) => {
  return list.filter(w => {
    // Texto
    if (searchQuery.value && !w.title.toLowerCase().includes(searchQuery.value.toLowerCase())) {
      return false;
    }
    // Dificultad
    if (filterDifficulty.value !== 'Todas' && w.difficulty !== filterDifficulty.value) {
      return false;
    }
    // Músculos (si hay filtros activos, al menos 1 debe coincidir)
    if (selectedMuscles.value.length > 0) {
      if (!w.muscles || w.muscles.length === 0) return false;
      const hasMuscle = w.muscles.some((m: string) => selectedMuscles.value.includes(m));
      if (!hasMuscle) return false;
    }
    return true;
  });
};

const filteredMyWorkouts = computed(() => filterWorkouts(myWorkouts.value));
const filteredAiWorkouts = computed(() => filterWorkouts(aiWorkouts.value));

const tabs = [
  { id: 'my', name: 'Mis Rutinas' },
  { id: 'ai', name: 'Entrenamientos IA' }
];

// --- DATOS REALES ---
const myWorkouts = ref<any[]>([]);
const aiWorkouts = ref<any[]>([]);
const isLoading = ref(false);

const muscleGroupMap: Record<number, string> = {
  0: 'Pecho', 1: 'Espalda', 2: 'Pierna', 3: 'Hombro', 4: 'Bíceps',
  5: 'Tríceps', 6: 'Abdomen', 7: 'Cardio', 8: 'Full Body', 9: 'Otro'
};

const getMusclesFromRutina = (r: any): string[] => {
  // 1. Si viene explícito del backend
  if (r.musculos && r.musculos.length > 0) return r.musculos;
  if (r.musculosPrincipales && r.musculosPrincipales.length > 0) return r.musculosPrincipales;
  // 2. Extraer de la lista de ejercicios si la tenemos
  if (r.ejercicios && r.ejercicios.length > 0) {
    const muscles = new Set<string>();
    r.ejercicios.forEach((e: any) => {
      const gm = e.grupoMuscular;
      if (gm !== undefined && gm !== null) {
        const name = typeof gm === 'number' ? muscleGroupMap[gm] : String(gm);
        if (name && name !== 'Otro') muscles.add(name);
      }
    });
    if (muscles.size > 0) return Array.from(muscles);
  }
  // 3. parteCuerpo como fallback
  if (r.parteCuerpo) return [r.parteCuerpo];
  return [];
};

const mapToCard = (r: any) => ({
  id: r.id,
  title: r.nombre,
  difficulty: r.nivel,
  exercises: r.cantidadEjercicios || (r.ejercicios ? r.ejercicios.length : 0),
  duration: r.duracionMinutos,
  muscles: getMusclesFromRutina(r),
  image: r.urlImagen || '',
  author: r.creadorNombre,
  creatorId: r.creadorId,
  likes: r.likes,
  liked: r.liked,
  downloads: 0,
  public: r.estado === 'Publicada',
  isGeneradaPorIA: r.esGeneradaPorIA || r.nombre?.startsWith('IA -'),
  isCopia: r.esCopia || (r.creadorId && Number(r.creadorId) !== Number(authStore.userId))
});

const loadRutinas = async () => {
  isLoading.value = true;
  try {
    const [misRes] = await Promise.all([
      rutinasApi.getMisRutinas(),
    ]);
    const allMy = (misRes.data as any[]).map(mapToCard);
    
    // Las rutinas normales van a myWorkouts, y las copias también.
    myWorkouts.value = allMy.filter(r => !r.isGeneradaPorIA);
    aiWorkouts.value = allMy.filter(r => r.isGeneradaPorIA);
  } catch (e) {
    console.error('Error cargando rutinas', e);
  } finally {
    isLoading.value = false;
  }
};

const handleGenerateAI = async (formData: any) => {
    showIAModal.value = false;
    isLoading.value = true;
    try {
        // Asegurar tipos correctos para el DTO (Days debe ser int)
        const payload = {
            ...formData,
            days: parseInt(formData.days) || 3 // Convert days to number, default to 3 if invalid
        };
        
        const res = await rutinasApi.generarIA(payload);
        const newRoutine = res.data;
        
        // Mapear al estilo de tarjeta que usa el front
        const card = mapToCard(newRoutine);
        aiWorkouts.value.unshift(card);
        
        activeTab.value = 'ai';
        uiStore.showToast('¡Rutina generada por IA con éxito!', 'success');
    } catch (e) {
        console.error("Error generating AI routine", e);
        uiStore.showToast('Error al generar la rutina. Asegúrate de tener conexión.', 'error');
    } finally {
        isLoading.value = false;
    }
};

onMounted(async () => {
    await loadRutinas();
    // Manejar redirección desde comunidad
    const queryTab = router.currentRoute.value.query.tab;
    if (queryTab === 'community_saved') {
        activeTab.value = 'community_saved';
    }
});



const openDetail = async (workout: any) => {
  try {
    const res = await rutinasApi.getById(workout.id);
    const detail = res.data;

    // Extraer músculos del detalle para actualizar la tarjeta
    const musclesFromDetail = getMusclesFromRutina({ ejercicios: detail.ejercicios });

    // Actualizar la tarjeta en la lista con los músculos correctos
    const updateInList = (list: any[]) => {
      const idx = list.findIndex(w => w.id === workout.id);
      if (idx !== -1 && musclesFromDetail.length > 0) {
        list[idx] = { ...list[idx], muscles: musclesFromDetail };
      }
    };
    updateInList(myWorkouts.value);
    updateInList(aiWorkouts.value);

    selectedWorkout.value = {
        ...workout,
        muscles: musclesFromDetail.length > 0 ? musclesFromDetail : workout.muscles,
        exercises: detail.ejercicios ? detail.ejercicios.length : 0,
        exerciseList: (detail.ejercicios || []).map((e: any) => {
            const muscleNum = e.grupoMuscular;
            const muscleName = typeof muscleNum === 'number' ? (muscleGroupMap[muscleNum] || 'Otro') : String(muscleNum || '');
            return {
                ejercicioId: e.ejercicioId || e.id,
                id: e.ejercicioId || e.id,
                name: e.nombreEjercicio,
                sets: e.series,
                reps: e.repeticiones || '10-12',
                rest: e.descansoSegundos ? `${e.descansoSegundos}s` : '90s',
                weight: e.ultimoPeso || '0',
                muscle: muscleName
            };
        })
    };
  } catch (e) {
    console.error("Error cargando detalle", e);
    selectedWorkout.value = workout; // Fallback
  }
};

// Recarga después de crear una nueva rutina
const handleWorkoutCreated = async () => {
  showCreateModal.value = false;
  await loadRutinas();
};

// Abrir modal de edición cargando el detalle completo
const openEditModal = async (workout: any) => {
  try {
    const res = await rutinasApi.getById(workout.id);
    const detail = res.data;
    workoutToEdit.value = {
      ...workout,
      id: workout.id,
      nombre: workout.title,
      nivel: workout.difficulty,
      exerciseList: detail.ejercicios.map((e: any) => ({
        ejercicioId: e.ejercicioId,
        id: e.ejercicioId,
        name: e.nombreEjercicio,
        muscle: e.grupoMuscular,
        sets: e.series,
        reps: e.repeticiones || '10-12',
        rest: e.descansoSegundos || 90,
        weight: '0 kg'
      }))
    };
    showEditModal.value = true;
  } catch (e) {
    console.error('Error cargando detalle para editar', e);
    workoutToEdit.value = workout;
    showEditModal.value = true;
  }
};

// Recarga después de editar una rutina
const handleWorkoutEdited = async () => {
  showEditModal.value = false;
  workoutToEdit.value = null;
  await loadRutinas();
  uiStore.showToast('¡Rutina actualizada correctamente!', 'success');
};
// --- CUSTOM ALERTS & DELETE ---
const isDeleting = ref(false);
const isPublishing = ref(false);

const deleteWorkout = async (id: number) => {
  const confirm = await uiStore.showConfirm("Eliminar Rutina", "¿Estás seguro de que quieres eliminar esta rutina? Esta acción no se puede deshacer.", { type: 'danger' });
  if (!confirm) return;
  
  isDeleting.value = true;
  try {
      await rutinasApi.eliminar(id);
      await loadRutinas();
      uiStore.showToast('Rutina eliminada correctamente!', 'success');
  } catch (e) {
      console.error('Error deleting routine:', e);
      uiStore.showToast('No se pudo eliminar la rutina. Comprueba tu conexión.', 'error');
  } finally {
      isDeleting.value = false;
  }
};

const publishWorkout = async (id: number) => {
    const confirm = await uiStore.showConfirm("Publicar Rutina", "¿Deseas enviar esta rutina a revisión para la comunidad?");
    if (!confirm) return;

    isPublishing.value = true;
    try {
        await rutinasApi.publicar(id);
        await loadRutinas();
        uiStore.showToast('Rutina enviada a revisión. ¡Gracias por aportar!', 'success');
    } catch (e) {
        console.error('Error publishing routine:', e);
        uiStore.showToast('No se pudo publicar la rutina.', 'error');
    } finally {
        isPublishing.value = false;
    }
};

</script>

<style scoped>
.hide-scrollbar::-webkit-scrollbar {
  display: none;
}
.hide-scrollbar {
  -ms-overflow-style: none;
  scrollbar-width: none;
}
@keyframes fadeIn {
  from { opacity: 0; transform: translateY(10px); }
  to { opacity: 1; transform: translateY(0); }
}
.animate-fade-in {
  animation: fadeIn 0.4s ease-out forwards;
}

/* Transición de expansión */
.expand-enter-active, .expand-leave-active {
  transition: all 0.3s ease-in-out;
  max-height: 400px;
}
.expand-enter-from, .expand-leave-to {
  max-height: 0;
  opacity: 0;
  padding-top: 0;
  padding-bottom: 0;
  margin-bottom: 0;
}
@keyframes float {
  0%, 100% { transform: translateY(0); }
  50% { transform: translateY(-10px); }
}
.animate-float {
  animation: float 4s ease-in-out infinite;
}
</style>
