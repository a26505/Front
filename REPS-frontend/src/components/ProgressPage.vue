<template>
  <div class="flex min-h-screen bg-black text-white font-sans selection:bg-red-500/30 overflow-x-hidden">
    <!-- SIDEBAR -->
    <Sidebar active="progress" />

    <!-- MAIN CONTENT -->
    <div class="flex-1 ml-0 md:ml-[256px] transition-all duration-300">
      <!-- HEADER -->
      <header class="sticky top-0 z-40 bg-black/90 backdrop-blur-xl border-b border-[#1F2937] px-6 py-4 flex justify-between items-center">
        <h1 class="text-2xl md:text-3xl font-bold tracking-tight">Mi Progreso</h1>
        
        <button class="flex items-center gap-2 px-4 py-2 border border-[#1F2937] rounded-lg text-sm font-medium hover:border-[#DC2626] hover:text-[#DC2626] transition-all duration-300">
          <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <rect x="3" y="4" width="18" height="18" rx="2" ry="2"/>
            <line x1="16" y1="2" x2="16" y2="6"/>
            <line x1="8" y1="2" x2="8" y2="6"/>
            <line x1="3" y1="10" x2="21" y2="10"/>
          </svg>
          Este Mes
        </button>
      </header>

      <main class="p-6 max-w-[1600px] mx-auto">
        <div class="grid grid-cols-1 lg:grid-cols-12 gap-8">
          
          <!-- LEFT COLUMN (60%) -->
          <div class="lg:col-span-7 space-y-10">
            <!-- RANGOS POR GRUPO MUSCULAR -->
            <section>
              <h2 class="text-xl font-semibold mb-6 flex items-center gap-2">
                <span class="w-1 h-6 bg-[#DC2626] rounded-full"></span>
                Rangos por Grupo Muscular
              </h2>
              <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div v-for="muscle in muscleRanks" :key="muscle.name" 
                  class="bg-[#111827]/50 border border-[#1F2937] rounded-xl p-5 hover:border-[#DC2626]/50 transition-all duration-300 group shadow-lg shadow-black/20">
                  
                  <div class="flex justify-between items-center mb-4">
                    <span class="text-lg font-bold group-hover:text-red-500 transition-colors">{{ muscle.name }}</span>
                    <div :class="['w-10 h-10 rounded-xl flex items-center justify-center shadow-lg bg-gradient-to-br', getRankGradient(muscle.rank)]">
                      <component :is="getRankIcon(muscle.rank)" class="w-6 h-6 text-white drop-shadow-md" />
                    </div>
                  </div>

                  <div class="flex justify-between items-center text-sm mb-2">
                    <span :class="getRankTextColor(muscle.rank)" class="font-bold uppercase tracking-wider text-xs">{{ muscle.rank }}</span>
                    <span class="text-gray-400 font-medium">{{ formatNumber(muscle.points) }} pts</span>
                  </div>

                  <!-- Progress Bar -->
                  <div class="w-full h-2 bg-[#1F2937] rounded-full overflow-hidden mb-3 relative progress-bar">
                    <div 
                      class="h-full rounded-full transition-all duration-1000 ease-out bg-gradient-to-r"
                      :class="getRankGradient(muscle.rank)"
                      :style="{ width: `${muscle.progress}%` }"
                    ></div>
                  </div>

                  <div class="flex justify-between items-center text-[11px] text-gray-500 font-medium">
                    <span>{{ muscle.isMax ? '¡Rango máximo alcanzado! 🎉' : `${formatNumber(muscle.nextTarget)} pts para ${muscle.nextRank}` }}</span>
                    <span class="bg-white/5 px-2 py-0.5 rounded-full border border-white/5">{{ muscle.workouts }} entrenamientos</span>
                  </div>
                </div>
              </div>
            </section>

            <!-- RÉCORDS PERSONALES -->
            <section class="bg-[#0A0A0A] border border-[#1F2937]/50 rounded-2xl overflow-hidden shadow-2xl">
              <div class="px-6 py-5 border-b border-[#1F2937]/30 flex justify-between items-center bg-[#111827]/30">
                <h2 class="text-lg font-bold flex items-center gap-2">
                  <span class="w-1 h-5 bg-[#DC2626] rounded-full"></span>
                  Récords Personales
                </h2>
                <div class="p-2 rounded-lg bg-yellow-500/10 border border-yellow-500/20">
                  <svg class="text-yellow-500 w-5 h-5 drop-shadow-glow" fill="currentColor" viewBox="0 0 24 24">
                    <path d="M18 2H6v7a6 6 0 0012 0V2zM4 22h16M10 14.66V17c0 .55-.47.98-.97 1.21C7.85 18.75 7 20.24 7 22M14 14.66V17c0 .55.47.98.97 1.21C16.15 18.75 17 20.24 17 22M6 9H4.5a2.5 2.5 0 010-5H6M18 9h1.5a2.5 2.5 0 000-5H18"/>
                  </svg>
                </div>
              </div>

              <div class="divide-y divide-[#1F2937]/20">
                <div v-for="record in personalRecords" :key="record.exercise"
                  class="flex justify-between items-center px-6 py-5 hover:bg-white/5 transition-all duration-300 group">
                  <div class="space-y-1">
                    <h4 class="font-bold text-sm tracking-wide group-hover:text-red-500 transition-colors uppercase">{{ record.exercise }}</h4>
                    <p class="text-[10px] text-gray-500 font-medium">{{ record.date }}</p>
                  </div>
                  <div class="text-right">
                    <div class="text-xl font-black text-[#DC2626] tracking-tight">{{ record.value }}</div>
                    <div class="flex items-center justify-end gap-1 text-[#22C55E] text-[10px] font-bold">
                      <svg width="8" height="8" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="4" stroke-linecap="round" stroke-linejoin="round">
                        <line x1="12" y1="19" x2="12" y2="5"/><polyline points="5 12 12 5 19 12"/>
                      </svg>
                      +{{ record.improvement }}
                    </div>
                  </div>
                </div>
              </div>
            </section>
          </div>

          <!-- RIGHT COLUMN (40%) -->
          <div class="lg:col-span-5 space-y-10">
            <!-- PROGRESO GENERAL DE RANGO -->
            <section class="relative bg-gradient-to-br from-[#111827]/80 to-[#111827]/50 border border-[#1F2937] rounded-2xl p-6 overflow-hidden">
              <div class="absolute -top-20 -right-20 w-64 h-64 bg-red-600/5 blur-[100px] rounded-full"></div>
              
              <h2 class="text-lg font-semibold mb-6 relative z-10">Rango General</h2>
              
              <div class="flex justify-center mb-8 relative z-10">
                <div :class="['w-32 h-32 rounded-3xl flex flex-col items-center justify-center shadow-2xl bg-gradient-to-br transition-all duration-500 transform hover:scale-105', getRankGradient(allRanks[currentOverallRankIndex].name)]">
                  <component :is="getRankIcon(allRanks[currentOverallRankIndex].name)" class="w-14 h-14 text-white drop-shadow-xl mb-2" />
                  <span class="text-xs font-black uppercase tracking-widest text-white shadow-sm">{{ allRanks[currentOverallRankIndex].name }}</span>
                </div>
              </div>

              <div class="grid grid-cols-6 gap-2 relative z-10">
                <div v-for="(rank, index) in allRanks" :key="rank.name"
                  :class="[
                    'h-1.5 rounded-full transition-all duration-500',
                    currentOverallRankIndex >= index ? 'bg-red-600' : 'bg-gray-800'
                  ]"
                ></div>
              </div>
              
              <div class="mt-6 text-center relative z-10">
                <p class="text-sm text-gray-400 font-medium">Promedio: <span class="text-white">2,600 pts</span></p>
              </div>
            </section>

            <!-- GRÁFICAS -->
            <section class="bg-[#111827]/50 border border-[#1F2937] rounded-2xl p-6 transition-all duration-500">
              <div class="flex items-center justify-between mb-6">
                <h3 class="text-sm font-bold uppercase tracking-wider text-gray-400">Analítica</h3>
                <div class="flex bg-black/40 p-1 rounded-lg border border-[#1F2937]">
                  <button 
                    @click="activeChartTab = 'workouts'"
                    :class="['px-3 py-1 rounded-md text-[10px] font-bold transition-all', activeChartTab === 'workouts' ? 'bg-[#DC2626] text-white' : 'text-gray-500 hover:text-white']"
                  >
                    PESOS
                  </button>
                  <button 
                    @click="activeChartTab = 'volume'"
                    :class="['px-3 py-1 rounded-md text-[10px] font-bold transition-all', activeChartTab === 'volume' ? 'bg-[#DC2626] text-white' : 'text-gray-500 hover:text-white']"
                  >
                    VOLUMEN
                  </button>
                </div>
              </div>
              
              <div class="h-[250px] w-full bg-transparent overflow-hidden">
                  <apexchart 
                    v-if="activeChartTab === 'workouts'"
                    key="workoutsChart"
                    type="bar" 
                    height="250" 
                    :options="barChartOptions" 
                    :series="barChartSeries"
                  />
                  <apexchart 
                    v-else
                    key="volumeChart"
                    type="line" 
                    height="250" 
                    :options="lineChartOptions" 
                    :series="lineChartSeries"
                  />
              </div>
            </section>

            <!-- ESTADÍSTICAS RÁPIDAS (2x2 Grid fits better in 40% column) -->
            <section class="grid grid-cols-2 gap-3 sm:gap-4">
              <div v-for="stat in generalStats" :key="stat.label"
                class="rounded-2xl p-4 sm:p-5 transition-all duration-500 group shadow-xl relative overflow-hidden border hover:scale-[1.02]"
                :style="{ 
                  background: stat.gradient,
                  borderColor: stat.borderColor
                }">
                <!-- Glow Effect -->
                <div class="absolute -inset-1 bg-gradient-to-r opacity-0 group-hover:opacity-20 transition-opacity duration-500"
                     :style="{ backgroundImage: `linear-gradient(to right, ${stat.glowColor}, transparent)` }"></div>
                
                <div class="relative z-10 flex justify-between items-center mb-6">
                  <div class="w-10 h-10 rounded-xl bg-black/30 border border-white/5 group-hover:border-white/10 transition-all flex items-center justify-center shadow-inner shrink-0">
                    <div v-html="stat.icon" :class="['w-6 h-6 drop-shadow-glow transition-transform group-hover:rotate-6 duration-300', stat.iconColor]"></div>
                  </div>
                  <div class="text-2xl sm:text-3xl font-black tracking-tighter transition-all duration-300 drop-shadow-sm group-hover:scale-105 ml-2" :class="stat.accentColor">
                    {{ stat.value }}
                  </div>
                </div>
                <div class="relative z-10 text-[9px] text-gray-300/60 font-black uppercase tracking-[0.15em] group-hover:text-white transition-colors leading-none truncate">
                  {{ stat.label }}
                </div>
              </div>
            </section>
          </div>
        </div>
      </main>

      <footer class="p-8 border-t border-[#1F2937] text-center text-gray-600 text-[11px] font-medium tracking-widest uppercase">
        © 2026 REPS - Tu sistema de entrenamiento definitivo
      </footer>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import Sidebar from './Sidebar.vue';

// --- SVGS AS STRINGS ---
const FlameIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="100%" height="100%" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2"><path stroke-linecap="round" stroke-linejoin="round" d="M15.362 5.214A8.252 8.252 0 0 1 12 21 8.25 8.25 0 0 1 6.038 7.047 8.287 8.287 0 0 0 9 9.601a8.983 8.983 0 0 1 3.361-6.867 8.21 8.21 0 0 0 3 2.48Z" /><path stroke-linecap="round" stroke-linejoin="round" d="M12 18a3.75 3.75 0 0 0 .495-7.468 5.99 5.99 0 0 0-1.925 3.547 5.975 5.975 0 0 1-2.133-1.001A3.75 3.75 0 0 0 12 18Z" /></svg>`;

const DumbbellIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="100%" height="100%" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M14.4 14.4 9.6 9.6"/><path d="M18.657 21.485a2 2 0 1 1-2.829-2.828l-1.768 1.768a2 2 0 1 1-2.829-2.829l6.364-6.364a2 2 0 1 1 2.829 2.828l-1.768 1.768a2 2 0 1 1 2.828 2.829z"/><path d="m21.5 21.5-1.4-1.4"/><path d="M3.9 3.9 2.5 2.5"/><path d="M6.404 12.768a2 2 0 1 1-2.829-2.829l1.768-1.767a2 2 0 1 1-2.828-2.829l2.828-2.828a2 2 0 1 1 2.829 2.828l1.767-1.768a2 2 0 1 1 2.829 2.829z"/></svg>`;

const ConsistenciaIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="100%" height="100%" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10"/><circle cx="12" cy="12" r="6"/><circle cx="12" cy="12" r="2"/></svg>`;

const AchievementIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="100%" height="100%" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="8" r="6"/><path d="M15.477 12.89 17 22l-5-3-5 3 1.523-9.11"/></svg>`;

// --- SVGS AS COMPONENTS (for dynamic ranks) ---
const MedalIcon = {
  template: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M7.21 15 2.66 7.14a2 2 0 0 1 .13-2.2L4.4 2.8A2 2 0 0 1 6 2h12a2 2 0 0 1 1.6.8l1.62 2.14a2 2 0 0 1 .13 2.2L16.79 15"/><circle cx="12" cy="15" r="7"/><path d="M12 12c-1.1 0-2 .9-2 2s.9 2 2 2 2-.9 2-2-.9-2-2-2z"/></svg>`
};

const TrophyIcon = {
  template: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M6 9H4.5a2.5 2.5 0 0 1 0-5H6"/><path d="M18 9h1.5a2.5 2.5 0 0 0 0-5H18"/><path d="M4 22h16"/><path d="M10 14.66V17c0 .55-.47.98-.97 1.21C7.85 18.75 7 20.24 7 22"/><path d="M14 14.66V17c0 .55.47.98.97 1.21C16.15 18.75 17 20.24 17 22"/><path d="M18 2H6v7a6 6 0 0 0 12 0V2z"/></svg>`
};

const StarIcon = {
  template: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>`
};

const ShieldIcon = {
  template: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"/></svg>`
};

const DiamondIcon = {
  template: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M6 3h12l4 6-10 13L2 9l4-6z"/><path d="M11 3 8 9l3 13m2-22 3 6-3 13M2 9h20"/></svg>`
};

const CrownIcon = {
  template: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="m2 4 3 12h14l3-12-6 7-4-7-4 7-6-7z"/></svg>`
};

const TargetIcon = {
  template: `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10"/><circle cx="12" cy="12" r="6"/><circle cx="12" cy="12" r="2"/></svg>`
};

// --- DATA ---
const activeChartTab = ref<'workouts' | 'volume'>('workouts');

const muscleRanks = ref([
  { name: 'Pecho', rank: 'Leyenda', points: 3200, progress: 95, workouts: 145, nextRank: 'Máximo', nextTarget: 0, isMax: true },
  { name: 'Espalda', rank: 'Diamante', points: 2800, progress: 60, workouts: 88, nextRank: 'Leyenda', nextTarget: 5200 },
  { name: 'Piernas', rank: 'Platino', points: 4100, progress: 20, workouts: 52, nextRank: 'Diamante', nextTarget: 5900 },
  { name: 'Hombros', rank: 'Oro', points: 1800, progress: 53, workouts: 28, nextRank: 'Platino', nextTarget: 700 },
  { name: 'Brazos', rank: 'Plata', points: 2200, progress: 80, workouts: 32, nextRank: 'Oro', nextTarget: 300 },
  { name: 'Core', rank: 'Bronce', points: 1500, progress: 33, workouts: 25, nextRank: 'Plata', nextTarget: 1000 },
]);

const allRanks = [
  { name: 'Bronce' },
  { name: 'Plata' },
  { name: 'Oro' },
  { name: 'Platino' },
  { name: 'Diamante' },
  { name: 'Leyenda' }
];

const currentOverallRankIndex = ref(2); // Oro

const personalRecords = ref([
  { exercise: 'Sentadilla', value: '140 kg', date: 'Hace 3 días', improvement: '10 kg' },
  { exercise: 'Press Banca', value: '100 kg', date: 'Hace 1 semana', improvement: '5 kg' },
  { exercise: 'Peso Muerto', value: '160 kg', date: 'Hace 2 semanas', improvement: '15 kg' },
  { exercise: 'Dominadas', value: '+20 kg', date: 'Hace 5 días', improvement: '5 kg' },
]);

const generalStats = ref([
  { 
    label: 'Racha (días)', 
    value: '30', 
    icon: FlameIcon, 
    iconColor: 'text-[#F97316]',
    accentColor: 'text-[#FF8533]',
    gradient: 'linear-gradient(152.983deg, rgba(245, 73, 0, 0.25) 0%, rgba(126, 42, 12, 0.2) 100%)',
    borderColor: 'rgba(245, 73, 0, 0.5)',
    glowColor: '#F54900'
  },
  { 
    label: 'Entrenamientos', 
    value: '87', 
    icon: DumbbellIcon, 
    iconColor: 'text-[#EF4444]',
    accentColor: 'text-red-500',
    gradient: 'linear-gradient(152.983deg, rgba(220, 38, 38, 0.2) 0%, rgba(153, 27, 27, 0.15) 100%)',
    borderColor: 'rgba(220, 38, 38, 0.4)',
    glowColor: '#DC2626'
  },
  { 
    label: 'Consistencia', 
    value: '95%', 
    icon: ConsistenciaIcon, 
    iconColor: 'text-blue-400',
    accentColor: 'text-blue-500',
    gradient: 'linear-gradient(152.983deg, rgba(59, 130, 246, 0.2) 0%, rgba(29, 78, 216, 0.15) 100%)',
    borderColor: 'rgba(59, 130, 246, 0.4)',
    glowColor: '#3B82F6'
  },
  { 
    label: 'Logros', 
    value: '12', 
    icon: AchievementIcon, 
    iconColor: 'text-[#D8B4FE]',
    accentColor: 'text-[#D8B4FE]',
    gradient: 'linear-gradient(152.983deg, rgba(152, 16, 250, 0.25) 0%, rgba(89, 22, 139, 0.2) 100%)',
    borderColor: 'rgba(152, 16, 250, 0.5)',
    glowColor: '#9810FA'
  },
]);

// --- CHARTS CONFIG ---
const barChartSeries = [{
  name: 'Peso (kg)',
  data: [80, 82.5, 82.5, 85, 85, 87.5, 90]
}];

const barChartOptions = {
  chart: { toolbar: { show: false }, background: 'transparent' },
  colors: ['#DC2626'],
  plotOptions: { bar: { borderRadius: 6, columnWidth: '40%' } },
  xaxis: {
    categories: ['Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb', 'Dom'],
    labels: { style: { colors: '#9CA3AF', fontWeight: 600 } },
    axisBorder: { show: false },
    axisTicks: { show: false }
  },
  yaxis: { 
    labels: { style: { colors: '#9CA3AF' } },
    min: 0,
    max: 100,
    tickAmount: 5
  },
  grid: { 
    borderColor: 'rgba(255, 255, 255, 0.05)', 
    strokeDashArray: 0,
    xaxis: { lines: { show: false } },
    yaxis: { lines: { show: true } }
  },
  tooltip: { theme: 'dark', x: { show: true } },
  dataLabels: { enabled: false }
};

const lineChartSeries = [{
  name: 'Volumen',
  data: [45000, 52000, 48000, 61000, 55000, 67000]
}];

const lineChartOptions = {
  chart: { toolbar: { show: false }, background: 'transparent' },
  colors: ['#DC2626'],
  stroke: { curve: 'smooth', width: 4 },
  xaxis: {
    categories: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun'],
    labels: { style: { colors: '#9CA3AF', fontWeight: 600 } },
    axisBorder: { show: false }
  },
  yaxis: { labels: { style: { colors: '#9CA3AF' } } },
  grid: { 
    borderColor: 'rgba(255, 255, 255, 0.05)', 
    strokeDashArray: 0,
    xaxis: { lines: { show: false } }
  },
  markers: { 
    size: 4, 
    colors: ['#DC2626'], 
    strokeColors: '#000', 
    strokeWidth: 2,
    hover: { size: 6 }
  },
  tooltip: { theme: 'dark' },
  dataLabels: { enabled: false }
};

// --- HELPERS ---
const getRankGradient = (rank: string) => {
  switch (rank) {
    case 'Bronce': return 'from-[#B45309] to-[#78350F]';
    case 'Plata': return 'from-[#94A3B8] to-[#475569]';
    case 'Oro': return 'from-[#F59E0B] to-[#B45309]';
    case 'Platino': return 'from-[#2DD4BF] to-[#0D9488]';
    case 'Diamante': return 'from-[#3B82F6] to-[#1D4ED8]';
    case 'Leyenda': return 'from-[#EF4444] to-[#991B1B]';
    default: return 'from-[#1F2937] to-[#111827]';
  }
};

const getRankTextColor = (rank: string) => {
  switch (rank) {
    case 'Bronce': return 'text-orange-400';
    case 'Plata': return 'text-slate-300';
    case 'Oro': return 'text-yellow-400';
    case 'Platino': return 'text-teal-400';
    case 'Diamante': return 'text-blue-400';
    case 'Leyenda': return 'text-red-500';
    default: return 'text-white';
  }
};

const getRankIcon = (rank: string) => {
  switch (rank) {
    case 'Bronce': return ShieldIcon;
    case 'Plata': return MedalIcon;
    case 'Oro': return TrophyIcon;
    case 'Platino': return StarIcon;
    case 'Diamante': return DiamondIcon;
    case 'Leyenda': return CrownIcon;
    default: return MedalIcon;
  }
};

const formatNumber = (num: number) => {
  return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
};
</script>

<style scoped>
.progress-bar::before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: linear-gradient(
    to right,
    transparent,
    rgba(255, 255, 255, 0.2),
    transparent
  );
  animation: shimmer 3s infinite;
}

@keyframes shimmer {
  0% { transform: translateX(-100%); }
  100% { transform: translateX(100%); }
}

.animate-shimmer {
  animation: shimmer 1.5s infinite;
}

.progress-bar-glow {
  box-shadow: inset 0 2px 4px rgba(0,0,0,0.5), 0 0 10px rgba(220, 38, 38, 0.1);
}

@keyframes float {
  0%, 100% { transform: translateY(0); }
  50% { transform: translateY(-5px); }
}

.animate-float {
  animation: float 4s ease-in-out infinite;
}
</style>
