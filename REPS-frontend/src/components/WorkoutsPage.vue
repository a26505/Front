<template>
  <div class="flex min-h-screen bg-[#000000] text-white">
    <!-- SIDEBAR UNIFICADO -->
    <Sidebar active="workouts" />

    <!-- 2️⃣ CONTENIDO PRINCIPAL -->
    <div class="flex-1 md:ml-[256px] min-h-screen flex flex-col">
      <!-- HEADER STICKY -->
      <header class="sticky top-0 z-40 bg-black/95 backdrop-blur-md py-4 px-6 flex items-center justify-between border-b border-[#1F2937]/50">
        <h1 class="text-3xl font-bold text-white tracking-tight">Mis Entrenamientos</h1>
        
        <div class="flex gap-3">
          <button @click="showIAModal = true" class="bg-[#9333EA] hover:bg-[#7C3AED] rounded-lg px-4 py-2 flex items-center gap-2 text-sm font-bold transition-all hover:scale-105 active:scale-95">
            <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
              <path d="m12 3 1.912 5.813a2 2 0 0 0 1.275 1.275L21 12l-5.813 1.912a2 2 0 0 0-1.275 1.275L12 21l-1.912-5.813a2 2 0 0 0-1.275-1.275L3 12l5.813-1.912a2 2 0 0 0 1.275-1.275L12 3Z"/>
            </svg>
            Generar con IA
          </button>
          
          <button @click="showCreateModal = true" class="bg-[#DC2626] hover:bg-[#B91C1C] rounded-lg px-4 py-2 flex items-center gap-2 text-sm font-bold transition-all hover:scale-105 active:scale-95">
            <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2.5"><path d="M12 5v14m-7-7h14"/></svg>
            Nueva Rutina
          </button>
        </div>
      </header>

      <!-- TABS NAVEGACIÓN -->
      <main class="flex-1 p-6 bg-black overflow-x-hidden">
        <!-- IA BANNER REFINADO (Tono morado profundo) -->
        <div 
          @click="activeTab = 'ai'"
          class="bg-gradient-to-r from-[#13072E] via-[#0F071C] to-[#0A0A0A] border border-[#9333EA]/30 hover:border-[#9333EA]/60 rounded-xl p-4 flex justify-between items-center cursor-pointer transition-all duration-500 mb-6 group shadow-[0_0_20px_rgba(147,51,234,0.15)] hover:shadow-[0_0_30px_rgba(147,51,234,0.25)]"
        >
          <div class="flex items-center gap-4">
            <div class="w-10 h-10 bg-gradient-to-br from-[#9333EA] to-[#6B21A8] rounded-full flex items-center justify-center shadow-lg shadow-purple-900/40 group-hover:scale-110 transition-transform duration-300">
              <!-- Icono Sparkle/Estrella de la IA -->
              <svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                <path d="m12 3 1.912 5.813a2 2 0 0 0 1.275 1.275L21 12l-5.813 1.912a2 2 0 0 0-1.275 1.275L12 21l-1.912-5.813a2 2 0 0 0-1.275-1.275L3 12l5.813-1.912a2 2 0 0 0 1.275-1.275L12 3Z"/>
              </svg>
            </div>
            <div>
              <h3 class="text-[15px] font-black text-white tracking-wide uppercase group-hover:text-purple-300 transition-colors">Entrenamientos Generados por IA</h3>
              <p class="text-[11px] text-[#9CA3AF] font-medium">Rutinas personalizadas basadas en tus objetivos</p>
            </div>
          </div>
          <div class="bg-gradient-to-r from-[#9333EA] via-[#A855F7] to-[#7C3AED] px-3 py-1.5 rounded-lg flex items-center gap-1.5 shadow-lg shadow-purple-900/40 hover:brightness-110 transition-all">
            <svg width="12" height="12" viewBox="0 0 24 24" fill="white">
              <path d="M13 2L3 14h8l-1 8 10-12h-8l1-8z"/>
            </svg>
            <span class="text-[10px] font-black text-white uppercase tracking-widest">Pro</span>
          </div>
        </div>

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
                ? (tab.id === 'friends' 
                    ? 'bg-[#3B82F6]/20 text-[#3B82F6] border-[#3B82F6]/30' 
                    : (tab.id === 'community' 
                        ? 'bg-[#10B981]/20 text-[#10B981] border-[#10B981]/30' 
                        : (tab.id === 'ai' ? 'bg-[#9333EA]/20 text-[#9333EA] border-[#9333EA]/30' : 'bg-[#DC2626]/20 text-[#DC2626] border-[#DC2626]/30')))
                : 'text-[#9CA3AF] border-transparent hover:text-white hover:bg-white/5'
            ]"
          >
            {{ tab.name }}
          </button>
        </div>

        <!-- LISTA DE ENTRENAMIENTOS -->
        <div class="flex flex-col gap-4 animate-fade-in pb-12">
             <div v-if="activeTab === 'my'">
                <WorkoutCard 
                   v-for="workout in myWorkouts" 
                   :key="workout.id" 
                   :workout="workout" 
                   type="my"
                   @click="openDetail(workout)"
                />
             </div>

             <div v-if="activeTab === 'friends'">
                <WorkoutCard 
                   v-for="workout in friendsWorkouts" 
                   :key="workout.id" 
                   :workout="workout" 
                   type="friends"
                   @click="openDetail(workout)"
                />
             </div>

             <div v-if="activeTab === 'community'">
                <WorkoutCard 
                   v-for="workout in communityWorkouts" 
                   :key="workout.id" 
                   :workout="workout" 
                   type="community"
                   @click="openDetail(workout)"
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

                    <button class="relative bg-white text-black hover:text-white px-10 py-4 rounded-xl text-sm font-black transition-all hover:scale-105 active:scale-95 uppercase tracking-widest shadow-[0_0_40px_rgba(255,255,255,0.2)] hover:shadow-purple-500/40 group/btn border border-transparent overflow-hidden">
                      <div class="absolute inset-0 bg-gradient-to-r from-[#9333EA] to-[#7C3AED] translate-y-full group-hover/btn:translate-y-0 transition-transform duration-300 ease-out"></div>
                      <span class="relative z-10">Mejorar ahora</span>
                    </button>
                  </div>
                </div>

                <WorkoutCard 
                   v-for="workout in aiWorkouts" 
                   :key="workout.id" 
                   :workout="workout" 
                   type="ai"
                   @click="openDetail(workout)"
                />
             </div>
        </div>
      </main>
    </div>

    <!-- MODALES -->
    <CreateWorkoutModal v-if="showCreateModal" @close="showCreateModal = false" />
    <AIGeneratorModal v-if="showIAModal" @close="showIAModal = false" />
    <WorkoutDetailModal v-if="selectedWorkout" :workout="selectedWorkout" @close="selectedWorkout = null" />
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue';
import Sidebar from './Sidebar.vue';
import WorkoutCard from './workouts/WorkoutCard.vue';
import CreateWorkoutModal from './workouts/CreateWorkoutModal.vue';
import AIGeneratorModal from './workouts/AIGeneratorModal.vue';
import WorkoutDetailModal from './workouts/WorkoutDetailModal.vue';

const activeTab = ref('my');
const showFilters = ref(false);
const showCreateModal = ref(false);
const showIAModal = ref(false);
const selectedWorkout = ref(null);
const isPro = ref(false); // Simulación

// Estados de Filtros
const searchQuery = ref('');
const filterDifficulty = ref('Todas');
const selectedMuscles = ref([]);

const difficulties = ['Todas', 'Principiante', 'Intermedio', 'Avanzado'];
const muscles = ['Pecho', 'Espalda', 'Piernas', 'Hombros', 'Brazos', 'Core'];

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

const tabs = [
  { id: 'my', name: 'Mis Rutinas' },
  { id: 'friends', name: 'Rutinas de Amigos' },
  { id: 'community', name: 'Comunidad' },
  { id: 'ai', name: 'Entrenamientos IA' }
];

const myWorkouts = ref([
  { id: 1, title: 'Empuje Superior A', difficulty: 'Intermedio', exercises: 6, duration: 45, muscles: ['Pecho', 'Hombros', 'Tríceps'], image: 'https://images.unsplash.com/photo-1571019614242-c5c5dee9f50b?auto=format&fit=crop&q=80&w=400', lastUsed: 'Hace 2 días' },
  { id: 2, title: 'Tirón Superior A', difficulty: 'Intermedio', exercises: 6, duration: 50, muscles: ['Espalda', 'Bíceps'], image: 'https://images.unsplash.com/photo-1597452485669-2c7bb5fef90d?auto=format&fit=crop&q=80&w=400', lastUsed: 'Hace 4 días' },
  { id: 3, title: 'Pierna A', difficulty: 'Avanzado', exercises: 7, duration: 60, muscles: ['Cuádriceps', 'Glúteos', 'Isquios'], image: 'https://images.unsplash.com/photo-1581009146145-b5ef03a726ec?auto=format&fit=crop&q=80&w=400', lastUsed: 'Hace 6 días' }
]);

const friendsWorkouts = ref([
  { id: 4, title: 'Upper Body Power', author: 'María García', avatar: 'https://i.pravatar.cc/150?u=maria', difficulty: 'Avanzado', exercises: 6, duration: 55, muscles: ['Pecho', 'Espalda'], image: 'https://images.unsplash.com/photo-1534438327276-14e5300c3a48?auto=format&fit=crop&q=80&w=400', lastUsed: 'Usado hace 1 día' },
  { id: 5, title: 'Legs & Glutes Focus', author: 'Carlos Ruiz', avatar: 'https://i.pravatar.cc/150?u=carlos', difficulty: 'Intermedio', exercises: 8, duration: 70, muscles: ['Piernas', 'Glúteos'], image: 'https://images.unsplash.com/photo-1434596922112-19c563067271?auto=format&fit=crop&q=80&w=400', lastUsed: 'Usado hace 3 días' }
]);

const communityWorkouts = ref([
  { id: 6, title: 'Hipertrofia Full Body 3x', author: 'Carlos Fitness', avatar: 'https://i.pravatar.cc/150?u=carlosfit', likes: 245, downloads: 156, tags: ['Full Body', 'Hipertrofia'], difficulty: 'Intermedio', exercises: 8, duration: 60, muscles: ['Todo el cuerpo'], image: 'https://images.unsplash.com/photo-1554344728-77ad90d6ed35?auto=format&fit=crop&q=80&w=400' },
  { id: 7, title: 'Push Pull Legs - Avanzado', author: 'María Pro', avatar: 'https://i.pravatar.cc/150?u=mariapro', likes: 389, downloads: 234, tags: ['PPL', 'Volumen'], difficulty: 'Avanzado', exercises: 10, duration: 75, muscles: ['Pecho', 'Espalda'], image: 'https://images.unsplash.com/photo-1574673130244-c7ec073998b5?auto=format&fit=crop&q=80&w=400' }
]);

const aiWorkouts = ref([
  { id: 8, title: 'Full Body - Fuerza', aiBadge: 'Nuevo', difficulty: 'Personalizado', aiGenerated: true, exercises: 8, duration: 55, muscles: ['Todo el cuerpo'], image: 'https://images.unsplash.com/photo-1517836357463-d25dfeac3438?auto=format&fit=crop&q=80&w=400' },
  { id: 9, title: 'Hipertrofia Pecho Intenso', aiBadge: 'Recomendado', difficulty: 'Avanzado', aiGenerated: true, exercises: 5, duration: 40, muscles: ['Pecho'], image: 'https://images.unsplash.com/photo-1534367507873-d2b7e2142712?auto=format&fit=crop&q=80&w=400' }
]);

const openDetail = (workout) => {
  selectedWorkout.value = workout;
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
