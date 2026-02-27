<template>
  <div class="flex min-h-screen bg-[#000000] text-white">
    <!-- 1️⃣ SIDEBAR IZQUIERDO -->
    <aside
      class="fixed w-[256px] h-screen bg-[rgba(16,24,40,0.5)] border-r border-[#1E2939] p-6 hidden md:flex flex-col z-50 transition-transform duration-300"
    >
      <!-- LOGO -->
      <div class="absolute top-[24px] left-[24px]">
        <div class="flex items-center gap-2">
            <div class="w-10 h-10 bg-[#E7000B] rounded-lg flex items-center justify-center font-bold text-xl italic tracking-tighter">REPS</div>
        </div>
      </div>

      <!-- NAVEGACIÓN -->
      <nav class="absolute top-[112px] left-[24px] w-[207px] flex flex-col gap-2">
        <router-link to="/dashboard" class="h-[48px] p-[16px] rounded-[10px] flex items-center gap-[12px] bg-transparent hover:bg-[rgba(255,255,255,0.05)] transition-colors duration-200">
          <svg width="20" height="20" viewBox="0 0 20 20" fill="none">
            <path d="M2.5 7.5L10 2.5L17.5 7.5V16.6667C17.5 17.5 16.8333 18.3333 16 18.3333H4C3.16667 18.3333 2.5 17.5 2.5 16.6667V7.5Z" stroke="white" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M7.5 18.3333V10H12.5V18.3333" stroke="white" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
          </svg>
          <span class="text-white text-[16px]">Dashboard</span>
        </router-link>

        <router-link to="/workouts" class="h-[50px] p-[17px] rounded-[10px] flex items-center gap-[12px] bg-[rgba(231,0,11,0.2)] border border-[rgba(231,0,11,0.3)] transition-colors duration-200">
          <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#DC2626" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <path d="M14.4 14.4 9.6 9.6"/>
            <path d="M18.657 21.485a2 2 0 1 1-2.829-2.828l-1.768 1.768a2 2 0 1 1-2.829-2.829l6.364-6.364a2 2 0 1 1 2.829 2.828l-1.768 1.768a2 2 0 1 1 2.828 2.829z"/>
          </svg>
          <span class="text-white text-[16px]">Entrenamientos</span>
        </router-link>

        <a href="#" class="h-[48px] p-[16px] rounded-[10px] flex items-center gap-[12px] bg-transparent hover:bg-[rgba(255,255,255,0.05)] transition-colors duration-200">
          <svg width="20" height="20" viewBox="0 0 20 20" fill="none">
            <path d="M17.5 6.66667L10.8333 13.3333L7.5 10L2.5 15" stroke="white" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M13.3333 6.66667H17.5V10.8333" stroke="white" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
          </svg>
          <span class="text-white text-[16px]">Progreso</span>
        </a>

        <a href="#" class="h-[48px] p-[16px] rounded-[10px] flex items-center gap-[12px] bg-transparent hover:bg-[rgba(255,255,255,0.05)] transition-colors duration-200">
          <svg width="20" height="20" viewBox="0 0 20 20" fill="none">
            <path d="M14.1667 17.5V15.8333C14.1667 13.9833 12.6833 12.5 10.8333 12.5H4.16667C2.31667 12.5 0.833333 13.9833 0.833333 15.8333V17.5" stroke="white" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
            <circle cx="7.5" cy="5.83333" r="3.33333" stroke="white" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
          </svg>
          <span class="text-white text-[16px]">Comunidad</span>
        </a>

        <a href="#" class="h-[48px] p-[16px] rounded-[10px] flex items-center gap-[12px] bg-transparent hover:bg-[rgba(255,255,255,0.05)] transition-colors duration-200">
          <svg width="20" height="20" viewBox="0 0 20 20" fill="none">
            <path d="M16.6667 17.5V15.8333C16.6667 13.9833 15.1833 12.5 13.3333 12.5H6.66667C4.81667 12.5 3.33333 13.9833 3.33333 15.8333V17.5" stroke="white" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
            <circle cx="10" cy="5.83333" r="3.33333" stroke="white" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
          </svg>
          <span class="text-white text-[16px]">Perfil</span>
        </a>
      </nav>

      <div class="absolute bottom-[72px] left-[24px] w-[207px] h-[48px] rounded-[10px] p-[16px] flex items-center gap-[12px] cursor-pointer hover:bg-[rgba(255,255,255,0.05)] transition-colors duration-200">
        <svg width="20" height="20" viewBox="0 0 20 20" fill="none">
          <path d="M7.5 17.5H4.16667C3.24583 17.5 2.5 16.7542 2.5 15.8333V4.16667C2.5 3.24583 3.24583 2.5 4.16667 2.5H7.5" stroke="#9CA3AF" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
          <path d="M13.3333 14.1667L17.5 10L13.3333 5.83333" stroke="#9CA3AF" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
          <path d="M17.5 10H7.5" stroke="#9CA3AF" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
        <span class="text-white text-[16px]">Salir</span>
      </div>
    </aside>

    <!-- 2️⃣ CONTENIDO PRINCIPAL -->
    <div class="flex-1 md:ml-[256px] min-h-screen flex flex-col">
      <!-- HEADER STICKY -->
      <header class="sticky top-0 z-40 bg-black/95 backdrop-blur-md py-4 px-6 flex items-center justify-between border-b border-[#1F2937]/50">
        <h1 class="text-3xl font-bold text-white tracking-tight">Mis Entrenamientos</h1>
        
        <div class="flex gap-3">
          <button @click="showIAModal = true" class="bg-[#9333EA] hover:bg-[#7C3AED] rounded-lg px-4 py-2 flex items-center gap-2 text-sm font-bold transition-all hover:scale-105 active:scale-95">
            <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2.5"><path d="M12 3v18m9-9H3"/><path d="m19.5 4.5-15 15m0-15 15 15"/></svg>
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
        <div class="bg-gradient-to-r from-[#13072E] via-[#0F071C] to-[#0A0A0A] border border-[#9333EA]/30 hover:border-[#9333EA]/60 rounded-xl p-4 flex justify-between items-center cursor-pointer transition-all duration-500 mb-6 group shadow-[0_0_20px_rgba(147,51,234,0.15)] hover:shadow-[0_0_30px_rgba(147,51,234,0.25)]">
          <div class="flex items-center gap-4">
            <div class="w-10 h-10 bg-gradient-to-br from-[#9333EA] to-[#6B21A8] rounded-full flex items-center justify-center shadow-lg shadow-purple-900/40 group-hover:scale-110 transition-transform duration-300">
              <!-- Icono Sparkle/Estrella de la IA -->
              <svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                <path d="M12 2l2.4 7.6L22 12l-7.6 2.4L12 22l-2.4-7.6L2 12l7.6-2.4z"/>
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
                <div v-if="!isPro" class="py-16 text-center bg-[#0A0D14] border border-[#1F2937] rounded-xl mb-8">
                  <div class="w-16 h-16 bg-[#9333EA]/10 rounded-full flex items-center justify-center mx-auto mb-6">
                    <svg width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="#9333EA" stroke-width="2">
                       <path d="M12 3v18m9-9H3"/><path d="m19.5 4.5-15 15m0-15 15 15"/>
                    </svg>
                  </div>
                  <h2 class="text-3xl font-black text-white mb-3 uppercase tracking-tighter">Acceso IA Pro</h2>
                  <p class="text-sm text-[#9CA3AF] mb-8 max-w-sm mx-auto">Rutinas ultra-personalizadas basadas en tu progreso real y objetivos actuales.</p>
                  <button class="bg-[#9333EA] hover:bg-[#7C3AED] px-8 py-3 rounded-lg text-sm font-black text-white transition-all hover:scale-105 uppercase tracking-widest">
                    Mejorar ahora
                  </button>
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
import { ref } from 'vue';
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
</style>
