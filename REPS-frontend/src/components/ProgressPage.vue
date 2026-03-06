<template>
  <div class="flex min-h-screen bg-[#000000] text-white">
    <!-- SIDEBAR -->
    <Sidebar active="progress" />

    <!-- MAIN CONTENT -->
    <div class="flex-1 md:ml-[256px] min-h-screen flex flex-col">
      <!-- HEADER -->
      <header class="sticky top-0 z-40 w-full h-[80px] bg-black/95 backdrop-blur-md px-6 flex items-center border-b border-[#1F2937]/50">
        <h1 class="text-2xl md:text-3xl font-bold text-white tracking-tight">Mi Progreso</h1>
      </header>

      <main class="flex-1 p-6 max-w-[1600px] mx-auto w-full relative z-10">
        
        <v-alert
          v-if="hasError"
          type="error"
          variant="tonal"
          color="red"
          class="mb-6 rounded-xl border border-red-900/50"
          closable
        >
          {{ errorText || 'Hubo un problema al cargar tus estadísticas.' }}
        </v-alert>

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
                  class="bg-[#111827]/50 border border-[#1F2937] rounded-xl p-5 hover:border-[#DC2626]/50 transition-all duration-300 group shadow-lg shadow-black/20 select-none">
                  
                  <div class="flex justify-between items-center mb-4">
                    <span class="text-lg font-bold transition-colors">{{ muscle.name }}</span>
                    <RankIcon :rank="muscle.rank" :size="40" />

                  </div>


                  <div class="flex justify-between items-center text-sm mb-2">
                    <span :class="getRankTextColor(muscle.rank)" class="font-bold uppercase tracking-wider text-xs">{{ muscle.rank }}</span>
                    <span class="text-gray-400 font-medium">{{ formatNumber(muscle.points) }} pts</span>
                  </div>

                  <div class="w-full h-2 bg-[#1F2937] rounded-full overflow-hidden mb-3 relative">
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
                <div class="w-[40px] h-[40px] rounded-[10px] bg-[rgba(234,179,8,0.15)] flex items-center justify-center border border-[rgba(234,179,8,0.2)]">
                  <svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M6 9H4.5A2.5 2.5 0 0 1 2 6.5V6A2.5 2.5 0 0 1 4.5 3.5H6M18 9h1.5A2.5 2.5 0 0 0 22 6.5V6A2.5 2.5 0 0 0 19.5 3.5H18M4 22h16M10 14.66V17c0 .55-.47.98-.97 1.21C7.85 18.75 7 20.24 7 22M14 14.66V17c0 .55.47.98.97 1.21C16.15 18.75 17 20.24 17 22M18 2H6v7a6 6 0 0 0 12 0V2Z" 
                      fill="#EAB308" stroke="#EAB308" stroke-width="0.5" stroke-linejoin="round"/>
                  </svg>
                </div>


              </div>

              <div class="divide-y divide-[#1F2937]/20">
                <div v-for="record in personalRecords" :key="record.exercise"
                  class="flex justify-between items-center px-6 py-5 hover:bg-white/5 transition-all duration-300 group select-none">
                  <div class="space-y-1">
                    <h4 class="font-bold text-sm tracking-wide transition-colors uppercase">{{ record.exercise }}</h4>
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
                <div class="flex flex-col items-center transform hover:scale-105 transition-all duration-500">
                  <RankIcon :rank="rangoGeneral" :size="120" />
                  <span :class="['mt-4 text-2xl font-black uppercase tracking-widest', getRankTextColor(rangoGeneral)]">
                    {{ rangoGeneral }}
                  </span>
                </div>
              </div>

              <div class="grid grid-cols-6 gap-2 relative z-10">
                <div v-for="(rank, index) in allRanks" :key="rank.name"
                  :class="[
                    'h-1.5 rounded-full transition-all duration-500',
                    currentCalculatedRankIndex >= index ? 'bg-red-600 shadow-[0_0_10px_rgba(220,38,38,0.5)]' : 'bg-gray-800'
                  ]"
                ></div>
              </div>
              
              <div class="mt-6 text-center relative z-10">
                <p class="text-sm text-gray-400 font-medium">Puntos Totales: <span class="text-white">{{ formatNumber(puntosTotales) }} pts</span></p>
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
                  <div class="w-10 h-10 rounded-xl bg-black/30 border border-white/5 group-hover:border-white/10 transition-all flex items-center justify-center shadow-inner shrink-0 relative overflow-hidden">
                    <RankIcon v-if="stat.label === 'Rango'" :rank="rangoGeneral" :size="32" class="p-1" />
                    <div v-else v-html="stat.icon" :class="['w-6 h-6 drop-shadow-glow transition-transform group-hover:rotate-6 duration-300', stat.iconColor]"></div>
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
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import Sidebar from './Sidebar.vue';
import RankIcon from './common/RankIcon.vue';
import { progresoApi, recordsApi, logrosApi } from '../api';
import { useAuthStore } from '../stores/auth';
import { getErrorMessage } from '../utils/error-handler';

const authStore = useAuthStore();

// --- SVGS AS STRINGS ---
const FlameIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="100%" height="100%" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2"><path stroke-linecap="round" stroke-linejoin="round" d="M15.362 5.214A8.252 8.252 0 0 1 12 21 8.25 8.25 0 0 1 6.038 7.047 8.287 8.287 0 0 0 9 9.601a8.983 8.983 0 0 1 3.361-6.867 8.21 8.21 0 0 0 3 2.48Z" /><path stroke-linecap="round" stroke-linejoin="round" d="M12 18a3.75 3.75 0 0 0 .495-7.468 5.99 5.99 0 0 0-1.925 3.547 5.975 5.975 0 0 1-2.133-1.001A3.75 3.75 0 0 0 12 18Z" /></svg>`;
const DumbbellIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="100%" height="100%" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10" /></svg>`;
const ConsistenciaIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="100%" height="100%" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10"/><circle cx="12" cy="12" r="6"/><circle cx="12" cy="12" r="2"/></svg>`;
const AchievementIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="100%" height="100%" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="8" r="6"/><path d="M15.477 12.89 17 22l-5-3-5 3 1.523-9.11"/></svg>`;

// --- DATA REACTIVA ---  
const activeChartTab = ref<'workouts' | 'volume'>('workouts');

// Rangos musculares desde backend
const muscleRanks = ref<any[]>([]);
const personalRecords = ref<any[]>([]);
const rangoGeneral = ref('Bronce');
const puntosTotales = ref(0);
const unlockedCount = ref(0);
const hasError = ref(false);
const errorText = ref('');


const currentCalculatedRankIndex = computed(() => {
  const idx = allRanks.findIndex(r => r.name === rangoGeneral.value);
  return idx >= 0 ? idx : 0;
});

// General stats: (racha, entrenamientos, etc. vienen del perfil)
const generalStats = computed(() => [
  {
    label: 'Racha (días)',
    value: String(authStore.profile?.rachaDias ?? 0),
    icon: FlameIcon,
    iconColor: 'text-[#F97316]',
    accentColor: 'text-[#F97316]',
    gradient: 'linear-gradient(152.983deg, rgba(245, 73, 0, 0.25) 0%, rgba(126, 42, 12, 0.2) 100%)',
    borderColor: 'rgba(245, 73, 0, 0.5)',
    glowColor: '#F54900'
  },
  {
    label: 'Puntos Musculares',
    value: String(puntosTotales.value),
    icon: DumbbellIcon,
    iconColor: 'text-white',
    accentColor: 'text-red-500',
    gradient: 'linear-gradient(152.983deg, rgba(220, 38, 38, 0.2) 0%, rgba(153, 27, 27, 0.15) 100%)',
    borderColor: 'rgba(220, 38, 38, 0.4)',
    glowColor: '#DC2626'
  },
  {
    label: 'Rango',
    value: rangoGeneral.value,
    icon: ConsistenciaIcon,
    iconColor: computed(() => getRankTextColor(rangoGeneral.value).replace('text-', '')),
    accentColor: computed(() => getRankTextColor(rangoGeneral.value)),
    gradient: 'linear-gradient(152.983deg, rgba(59, 130, 246, 0.2) 0%, rgba(29, 78, 216, 0.15) 100%)',
    borderColor: 'rgba(59, 130, 246, 0.4)',
    glowColor: '#3B82F6'
  },
  {
    label: 'Récords',
    value: String(personalRecords.value.length),
    icon: AchievementIcon,
    iconColor: 'text-[#D8B4FE]',
    accentColor: 'text-[#D8B4FE]',
    gradient: 'linear-gradient(152.983deg, rgba(152, 16, 250, 0.25) 0%, rgba(89, 22, 139, 0.2) 100%)',
    borderColor: 'rgba(152, 16, 250, 0.5)',
    glowColor: '#9810FA'
  },
]);

const allRanks = [
  { name: 'Bronce' },
  { name: 'Plata' },
  { name: 'Oro' },
  { name: 'Platino' },
  { name: 'Diamante' },
  { name: 'Leyenda' }
];

// --- CARGA DE DATOS ---
onMounted(async () => {
  try {
    const [muscularRes, generalRes, recordsRes, analiticaRes] = await Promise.all([
      progresoApi.getMuscular(),
      progresoApi.getGeneral(),
      recordsApi.getMisRecords(),
      progresoApi.getAnalitica(),
    ]);

    if (authStore.profile?.id) {
      try {
        const logsRes = await logrosApi.getMisLogros(authStore.profile.id);
        unlockedCount.value = (logsRes.data || []).filter((l: any) => l.desbloqueado).length;
      } catch (e) {
        console.warn("No se pudieron cargar logros en progreso", e);
      }
    }

    // Rangos musculares
    const muscleMap: Record<string, string> = {
      'Piernas': 'Pierna',
      'Pierna': 'Pierna',
      'Pecho': 'Pecho',
      'Pectorales': 'Pecho',
      'Espalda': 'Espalda',
      'Hombros': 'Hombro',
      'Hombro': 'Hombro',
      'Bíceps': 'Biceps',
      'Tríceps': 'Triceps',
      'Brazos': 'Biceps',
      'Core': 'Abdomen',
      'Abdominales': 'Abdomen'
    };

    muscleRanks.value = (muscularRes.data as any[]).map((m: any) => {
      // Normalizar a PascalCase (ej: PIERNA -> Pierna, piernas -> Pierna)
      let rawName = m.grupoMuscular || 'Otro';
      let formattedName = rawName.charAt(0).toUpperCase() + rawName.slice(1).toLowerCase();
      
      const normalizedName = muscleMap[formattedName] || formattedName;
      
      return {
        name: normalizedName,
        rank: m.rango || 'Bronce',
        points: m.puntosActuales || 0,
        progress: Math.round((m.porcentaje || 0) * 100),
        workouts: m.entrenamientosRealizados || 0,
        nextRank: 'Siguiente',
        nextTarget: m.siguienteNivelPuntos || 500,
        isMax: m.porcentaje >= 1,
      };
    });

    // General
    rangoGeneral.value = generalRes.data.rangoGeneral ?? 'Bronce';
    puntosTotales.value = generalRes.data.puntosTotales ?? 0;

    // Records personales
    personalRecords.value = (recordsRes.data as any[]).map((r: any) => ({
      exercise: r.ejercicioNombre || r.nombreEjercicio || 'Ejercicio Desconocido',
      value: `${r.pesoMaximo} kg`,
      date: r.tiempoAtras || (r.fecha ? new Date(r.fecha).toLocaleDateString('es-ES') : (r.fechaRecord ? new Date(r.fechaRecord).toLocaleDateString('es-ES') : '')),
      improvement: r.mejora > 0 ? `+${r.mejora} kg` : '',
    }));

    if (analiticaRes.data) {
        barChartSeries.value = [{ name: 'Peso (kg)', data: analiticaRes.data.pesos }];
        lineChartSeries.value = [{ name: 'Volumen', data: analiticaRes.data.volumen }];
    }
  } catch (e: any) {
    console.error('Error cargando progreso', e);
    hasError.value = true;
    errorText.value = getErrorMessage(e);
    
    // Fallback a datos de muestra si back no disponible o error
    muscleRanks.value = [
      { name: 'Pecho', rank: 'Bronce', points: 0, progress: 0, workouts: 0, nextRank: 'Plata', nextTarget: 1000, isMax: false },
      { name: 'Espalda', rank: 'Bronce', points: 0, progress: 0, workouts: 0, nextRank: 'Plata', nextTarget: 1000 },
      { name: 'Pierna', rank: 'Bronce', points: 0, progress: 0, workouts: 0, nextRank: 'Plata', nextTarget: 1000 },
      { name: 'Hombro', rank: 'Bronce', points: 0, progress: 0, workouts: 0, nextRank: 'Plata', nextTarget: 1000 },
      { name: 'Biceps', rank: 'Bronce', points: 0, progress: 0, workouts: 0, nextRank: 'Plata', nextTarget: 1000 },
      { name: 'Triceps', rank: 'Bronce', points: 0, progress: 0, workouts: 0, nextRank: 'Plata', nextTarget: 1000 },
      { name: 'Abdomen', rank: 'Bronce', points: 0, progress: 0, workouts: 0, nextRank: 'Plata', nextTarget: 1000 },
      { name: 'Cardio', rank: 'Bronce', points: 0, progress: 0, workouts: 0, nextRank: 'Plata', nextTarget: 1000 },
      { name: 'FullBody', rank: 'Bronce', points: 0, progress: 0, workouts: 0, nextRank: 'Plata', nextTarget: 1000 },
      { name: 'Otro', rank: 'Bronce', points: 0, progress: 0, workouts: 0, nextRank: 'Plata', nextTarget: 1000 },
    ];
    barChartSeries.value = [{ name: 'Peso (kg)', data: [0, 0, 0, 0, 0, 0, 0] }];
    lineChartSeries.value = [{ name: 'Volumen', data: [0, 0, 0, 0, 0, 0] }];
  }
});

// --- CHARTS CONFIG (estáticos de momento) ---
const barChartSeries = ref([{ name: 'Peso (kg)', data: [80, 82.5, 82.5, 85, 85, 87.5, 90] }]);
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
  yaxis: { labels: { style: { colors: '#9CA3AF' } }, min: 0, max: 100, tickAmount: 5 },
  grid: { borderColor: 'rgba(255, 255, 255, 0.05)', strokeDashArray: 0, xaxis: { lines: { show: false } }, yaxis: { lines: { show: true } } },
  tooltip: { theme: 'dark', x: { show: true } },
  dataLabels: { enabled: false }
};

const lineChartSeries = ref([{ name: 'Volumen', data: [45000, 52000, 48000, 61000, 55000, 67000] }]);
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
  grid: { borderColor: 'rgba(255, 255, 255, 0.05)', strokeDashArray: 0, xaxis: { lines: { show: false } } },
  markers: { size: 4, colors: ['#DC2626'], strokeColors: '#000', strokeWidth: 2, hover: { size: 6 } },
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
    case 'Bronce': return 'text-[#F97316]';
    case 'Plata': return 'text-[#CBD5E1]';
    case 'Oro': return 'text-[#FACC15]';
    case 'Platino': return 'text-[#22D3EE]';
    case 'Diamante': return 'text-[#3B82F6]';
    case 'Leyenda': return 'text-[#EF4444]';
    default: return 'text-gray-400';
  }
};

const formatNumber = (num: number) => {
  return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
};
</script>

<style scoped>
</style>
