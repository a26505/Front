<template>
  <div class="flex min-h-screen bg-[#000000] text-white">
    <!-- SIDEBAR UNIFICADO -->
    <Sidebar active="dashboard" />

    <!-- 2️⃣ CONTENIDO PRINCIPAL Y HEADER -->
    <div class="flex-1 md:ml-[256px] min-h-screen">
      <!-- HEADER -->
      <header class="sticky top-0 z-40 w-full h-[89px] bg-[rgba(0,0,0,0.95)] border-b border-[#1E2939] backdrop-blur-[10px] py-[16px] px-[24px] flex items-center justify-between">
        <div class="flex flex-col gap-0">
          <div class="flex items-center gap-2">
            <h1 class="text-[30px] leading-[36px] font-normal text-white">¡Hola, {{ userName }}!</h1>
          </div>
          <p class="text-[14px] leading-[20px] text-[#9CA3AF]">Listo para superar tus límites hoy</p>
        </div>

        <div class="flex items-center gap-[20px]">
          <div v-if="!isPro" class="hidden md:block">
            <router-link to="/select-plan" class="relative bg-gradient-to-r from-[#EAB308] to-[#A16207] rounded-[8px] h-[36px] px-[16px] flex items-center justify-center gap-[8px] cursor-pointer transition-all duration-300 hover:scale-105 active:scale-95 border border-[#FEF08A] overflow-hidden group">
              <div class="absolute inset-0 bg-white/20 translate-y-full group-hover:translate-y-0 transition-transform duration-300 ease-out"></div>
              <svg width="16" height="16" viewBox="0 0 16 16" fill="none" class="relative z-10">
                <path d="M2 11.3333L3.33333 3.33333L6.66667 6.66667L8 1.33333L9.33333 6.66667L12.6667 3.33333L14 11.3333H2Z" stroke="white" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
                <path d="M2 11.3333H14V13.3333C14 13.7 13.7 14 13.3333 14H2.66667C2.3 14 2 13.7 2 13.3333V11.3333Z" stroke="white" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
              </svg>
              <span class="text-white text-[14px] font-bold tracking-wide relative z-10 whitespace-nowrap">Mejorar a Pro</span>
            </router-link>
          </div>

          <!-- El usuario solicitó quitar el perfil del header -->
        </div>

      </header>

      <!-- 3️⃣ MAIN CONTENT -->
      <main class="p-[16px] md:p-[24px] bg-[#000000] min-h-[calc(100vh-89px)]">
        
        <v-alert
          v-if="hasError"
          type="error"
          variant="tonal"
          color="red"
          class="mb-6 rounded-xl border border-red-900/50"
          closable
        >
          {{ errorText || 'No pudimos cargar algunos datos. Por favor, recarga la página.' }}
        </v-alert>

        <!-- CARDS DE ESTADÍSTICAS -->
        <div class="grid grid-cols-1 md:grid-cols-3 gap-[24px] mb-[24px]">
          <!-- CARD 1: RACHA -->
          <div class="h-[206px] rounded-[14px] p-[25px] relative transition-all duration-300 hover:brightness-110 border border-[rgba(245,73,0,0.8)]" style="background: linear-gradient(152.983deg, rgba(245, 73, 0, 0.3) 0%, rgba(126, 42, 12, 0.3) 100%)">
            <div class="flex justify-between items-center mb-[40px]">
              <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="#F97316" class="w-8 h-8">
                <path stroke-linecap="round" stroke-linejoin="round" d="M15.362 5.214A8.252 8.252 0 0 1 12 21 8.25 8.25 0 0 1 6.038 7.047 8.287 8.287 0 0 0 9 9.601a8.983 8.983 0 0 1 3.361-6.867 8.21 8.21 0 0 0 3 2.48Z" />
                <path stroke-linecap="round" stroke-linejoin="round" d="M12 18a3.75 3.75 0 0 0 .495-7.468 5.99 5.99 0 0 0-1.925 3.547 5.975 5.975 0 0 1-2.133-1.001A3.75 3.75 0 0 0 12 18Z" />
              </svg>
              <div class="bg-[#F54900] rounded-[8px] py-[3px] px-[9px] text-[12px] font-medium text-white h-[22px] flex items-center">
                Activo
              </div>
            </div>
            <div class="text-[30px] leading-[36px] text-[#FF8533] font-bold mb-[28px]">{{ rachaDias }} días</div>
            <div class="text-[14px] leading-[20px] text-[#9CA3AF]">Racha actual</div>
          </div>

          <!-- CARD 2: RANGO -->
          <div class="h-[206px] rounded-[14px] p-[25px] relative transition-all duration-300 hover:brightness-110 border border-[rgba(234,179,8,0.8)]" style="background: linear-gradient(152.983deg, rgba(234, 179, 8, 0.3) 0%, rgba(161, 98, 7, 0.3) 100%)">
            <div class="flex justify-between items-center mb-[40px]">
              <RankIcon :rank="calculatedRangoGeneral" :size="48" />
            </div>


            <div class="text-[36px] leading-[40px] text-[#FACC15] font-black mb-[10px]">{{ calculatedRangoGeneral.toUpperCase() }}</div>
            <div class="text-[14px] leading-[20px] text-[#9CA3AF]">Puntos Rango: {{ averagePoints }} pts</div>
          </div>

          <!-- CARD 3: LOGROS (Usado para Ranking Puntos según pedido) -->
          <div class="h-[206px] rounded-[14px] p-[25px] relative transition-all duration-300 hover:brightness-110 border border-[rgba(152,16,250,0.8)]" style="background: linear-gradient(152.983deg, rgba(152, 16, 250, 0.3) 0%, rgba(89, 22, 139, 0.3) 100%)">
             <div class="flex justify-between items-center mb-[40px]">
              <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="#A855F7" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"/>
              </svg>
              <div class="bg-[#9810FA] rounded-[8px] py-[3px] px-[9px] text-[12px] font-medium text-white h-[22px] flex items-center">
                Ranking
              </div>
            </div>
            <div class="text-[30px] leading-[36px] text-[#D8B4FE] font-bold mb-[28px]">{{ calculatedRankingPts }}</div>
            <div class="text-[14px] leading-[20px] text-[#9CA3AF]">Ranking Pts</div>
          </div>
        </div>

        <!-- 4️⃣ CARD ENTRENAMIENTO DE HOY -->
        <div class="w-full flex flex-col md:flex-row justify-between items-start md:items-center rounded-[16px] p-[28px] mb-[24px] border border-[rgba(231,0,11,0.8)] transition-all duration-300 hover:brightness-110 gap-6 md:gap-0" style="background: linear-gradient(152.983deg, rgba(231, 0, 11, 0.3) 0%, rgba(115, 0, 5, 0.3) 100%)">
          <!-- CONTENIDO IZQUIERDO -->
          <div class="flex flex-col gap-[12px]">
            <div class="flex items-center gap-[8px]">
              <svg width="20" height="20" viewBox="0 0 20 20" fill="none">
                <path d="M6.66667 1.66667V5" stroke="#DC2626" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
                <path d="M13.3333 1.66667V5" stroke="#DC2626" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
                <rect x="2.5" y="3.33333" width="15" height="15" rx="1.66667" stroke="#DC2626" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
                <path d="M2.5 8.33333H17.5" stroke="#DC2626" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
              </svg>
               <span class="text-[14px] text-[#9CA3AF]">Entrenamiento de Hoy</span>
            </div>
            
            <h2 class="text-[30px] leading-[36px] text-white font-bold mt-[12px]">{{ rutinaSugerida?.nombre ?? 'Sin rutina sugerida' }}</h2>
            
            <div class="flex flex-wrap gap-[16px] items-center mt-[12px]">
              <div class="flex items-center gap-1">
                <span class="text-[14px] text-white">{{ (rutinaSugerida?.cantidadEjercicios || (rutinaSugerida?.ejercicios ? rutinaSugerida.ejercicios.length : 0)) || '—' }} ejercicios</span>
              </div>
              <div class="flex items-center gap-1">
                <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                  <circle cx="8" cy="8" r="6.66667" stroke="white" stroke-width="1.33" stroke-linecap="round" stroke-linejoin="round"/>
                  <path d="M8 4V8L10.6667 9.33333" stroke="white" stroke-width="1.33" stroke-linecap="round" stroke-linejoin="round"/>
                </svg>
                <span class="text-[14px] text-white">{{ rutinaSugerida?.duracionMinutos ?? '—' }} min</span>
              </div>
              <div class="bg-[#7F000B] rounded-[8px] py-[3px] px-[9px] text-[12px] font-medium text-[#FFB3B3]">
                {{ rutinaSugerida?.nivel ?? 'Sin datos' }}
              </div>
            </div>
          </div>

          <!-- BOTÓN COMENZAR -->
          <button @click="startSuggestedWorkout" class="w-full md:w-auto bg-[#7F000B] rounded-[10px] py-[12px] px-[24px] text-[16px] font-semibold text-[#FFB3B3] flex justify-center items-center gap-[8px] cursor-pointer hover:bg-[#99000B] transition-all duration-200 hover:scale-105 active:scale-95">
            Comenzar
            <svg width="20" height="20" viewBox="0 0 20 20" fill="none">
              <path d="M4.16667 10H15.8333" stroke="#FFB3B3" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
              <path d="M10 4.16667L15.8333 10L10 15.8333" stroke="#FFB3B3" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
          </button>
        </div>

        <!-- 5️⃣ SECCIÓN PROGRESO SEMANAL + LOGROS -->
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-[24px] mb-[24px]">
          <!-- CARD PROGRESO SEMANAL -->
          <div class="bg-[rgba(16,24,40,0.5)] border border-[#1E2939] rounded-[16px] p-[28px]">
            <h3 class="text-[20px] font-semibold text-white mb-[24px]">Progreso Semanal</h3>
            
            <!-- DÍAS DE LA SEMANA -->
            <div class="flex gap-[8px] sm:gap-[16px] justify-center mb-[24px]">
              <div 
                v-for="day in weekDaysWorked" 
                :key="day.label"
                class="w-[36px] h-[36px] sm:w-[44px] sm:h-[44px] rounded-full flex items-center justify-center text-[14px] font-semibold border-2"
                :class="[
                  day.done ? 'bg-[#E7000B] text-white border-transparent' : 'bg-[#1F2937] text-[#6B7280] border-transparent',
                  day.isToday && !day.done ? 'border-[#E7000B]/50 ring-2 ring-[#E7000B]/20' : ''
                ]"
              >{{ day.label }}</div>
            </div>

            <!-- BARRA DE PROGRESO -->
            <div class="mb-[16px]">
              <div class="flex justify-between mb-[8px]">
                <span class="text-[14px] text-[#9CA3AF]">Meta semanal</span>
                <span class="text-[14px] text-[#9CA3AF]">{{ entrenamientosSemana }}/{{ metaSemanal }} completados</span>
              </div>
              <div class="w-full h-[8px] bg-[#1F2937] rounded-[4px] overflow-hidden">
                <div class="h-full rounded-[4px] animate-[progress_1s_ease-out]" :style="{ width: progresoPorcentaje + '%', background: 'linear-gradient(90deg, #E7000B 0%, #F97316 100%)' }"></div>
              </div>
            </div>
          </div>

          <!-- CARD LOGROS RECIENTES -->
          <div class="bg-[rgba(16,24,40,0.5)] border border-[#1E2939] rounded-[16px] p-[28px]">
            <h3 class="text-[20px] font-semibold text-white mb-[24px]">Logros Recientes</h3>
            
            <!-- Estado vacío -->
            <div v-if="ultimosLogros.length === 0" class="py-8 text-center text-[#6B7280] text-[14px]">
              Aún no has desbloqueado logros. ¡Sigue entrenando!
            </div>

            <div v-else class="flex flex-col gap-[16px]">
              <div 
                v-for="logro in ultimosLogros" 
                :key="logro.id"
                class="flex items-center justify-between p-[12px] bg-[rgba(0,0,0,0.3)] rounded-[12px] transition-all duration-300 hover:bg-[rgba(231,0,11,0.1)]"
              >
                <div class="flex items-center gap-[12px]">
                  <div class="w-[40px] h-[40px] rounded-full bg-[rgba(249,115,22,0.2)] flex items-center justify-center">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="#F97316" class="w-6 h-6">
                      <circle cx="12" cy="8" r="6"/>
                      <path d="M15.477 12.89 17 22l-5-3-5 3 1.523-9.11"/>
                    </svg>
                  </div>
                  <div class="flex flex-col">
                    <span class="text-[16px] text-white font-medium">{{ logro.nombre }}</span>
                    <span class="text-[12px] text-[#9CA3AF]">{{ logro.descripcion }}</span>
                  </div>
                </div>
                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#FBBF24" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <circle cx="12" cy="8" r="6"/>
                  <path d="M15.477 12.89 17 22l-5-3-5 3 1.523-9.11"/>
                </svg>
              </div>
            </div>
          </div>
        </div>

        <!-- 6️⃣ RÉCORDS PERSONALES -->
        <div class="w-full flex flex-col pt-[24px]">
          <div class="flex justify-between items-center mb-[24px]">
            <h3 class="text-[20px] font-semibold text-white">Récords Personales</h3>
            <div class="w-[40px] h-[40px] rounded-[10px] bg-[rgba(234,179,8,0.15)] flex items-center justify-center border border-[rgba(234,179,8,0.2)]">
              <svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M6 9H4.5A2.5 2.5 0 0 1 2 6.5V6A2.5 2.5 0 0 1 4.5 3.5H6M18 9h1.5A2.5 2.5 0 0 0 22 6.5V6A2.5 2.5 0 0 0 19.5 3.5H18M4 22h16M10 14.66V17c0 .55-.47.98-.97 1.21C7.85 18.75 7 20.24 7 22M14 14.66V17c0 .55.47.98.97 1.21C16.15 18.75 17 20.24 17 22M18 2H6v7a6 6 0 0 0 12 0V2Z" 
                  fill="#EAB308" stroke="#EAB308" stroke-width="0.5" stroke-linejoin="round"/>
              </svg>
            </div>



          </div>
          
          <!-- Estado vacío -->
          <div v-if="records.length === 0 && !isLoading" class="py-8 text-center text-[#6B7280] text-[14px]">
            Aún no tienes récords personales. ¡Comienza a entrenar!
          </div>

          <div v-else class="flex flex-col gap-[16px]">
            <div 
              v-for="rec in records" 
              :key="rec.ejercicioId"
              class="flex justify-between items-center bg-[#07090E] rounded-[12px] p-[20px] border border-[rgba(255,255,255,0.08)]"
            >
              <div class="flex items-center gap-4 flex-1 min-w-0">
                <div class="w-[48px] h-[48px] bg-[#EF4444] rounded-[12px] flex items-center justify-center text-white shrink-0">
                  <div class="w-2.5 h-2.5 rounded-full bg-white shadow-[0_0_10px_rgba(255,255,255,0.5)]"></div>
                </div>
                <div class="flex flex-col gap-[4px] min-w-0">
                  <span class="text-[16px] text-white font-semibold truncate" :title="rec.ejercicioNombre">
                    {{ (rec.ejercicioNombre && String(rec.ejercicioNombre).trim() !== '') ? rec.ejercicioNombre : 'Ejercicio Desconocido' }}
                  </span>
                  <span class="text-[13px] text-[#9CA3AF] truncate">
                    {{ (rec.tiempoAtras && String(rec.tiempoAtras).trim() !== '') ? rec.tiempoAtras : (rec.fecha ? new Date(rec.fecha).toLocaleDateString('es-ES') : 'Fecha desconocida') }}
                  </span>
                </div>
              </div>
              <div class="flex flex-col items-end gap-[4px] shrink-0">
                <span class="text-[18px] font-bold text-[#EF4444]">{{ rec.pesoMaximo }} kg</span>
                <div v-if="rec.mejora > 0" class="flex items-center gap-[2px] text-[12px] font-bold text-[#22C55E]">
                  <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><path d="M12 19V5M5 12l7-7 7 7"/></svg>
                  <span>+{{ rec.mejora }} kg</span>
                </div>
              </div>
            </div>
          </div>
        </div>

      </main>
    </div>
    
    <ActiveWorkoutModal 
      v-if="showActiveWorkout && activeWorkoutData" 
      :workout="activeWorkoutData" 
      @completed="handleWorkoutCompleted"
      @close="showActiveWorkout = false"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import Sidebar from './Sidebar.vue';
import RankIcon from './common/RankIcon.vue';
import ActiveWorkoutModal from './workouts/ActiveWorkoutModal.vue';
import { useAuthStore } from '../stores/auth';
import { dashboardApi, recordsApi, rutinasApi, progresoApi, logrosApi } from '../api';
import { getErrorMessage } from '../utils/error-handler';

const authStore = useAuthStore();
const userName = computed(() => authStore.profile?.nombre ?? 'Atleta');
const isPro = computed(() => authStore.isPro);

// --- Ranking y Puntos (Lógica compartida con ProgressPage) ---
const muscleRanks = ref<any[]>([]);
const records = ref<any[]>([]);
const unlockedCount = ref(0);

/* rankOrder removed to fix lint */

// Suma total de puntos del catálogo muscular entrenado (Sincronizado con backend)
const averagePoints = computed(() => {
  const withPoints = muscleRanks.value.filter((m: any) => (m.puntosActuales || 0) > 0);
  if (withPoints.length === 0) return 0;
  
  return withPoints.reduce((acc: number, m: any) => acc + (m.puntosActuales || 0), 0);
});

// Calcular rango basado en el promedio de puntos (Sincronizado con Backend)
const calculatedRangoGeneral = computed(() => {
  const points = averagePoints.value;
  if (points >= 8000) return 'Leyenda';
  if (points >= 5000) return 'Diamante';
  if (points >= 3000) return 'Platino';
  if (points >= 1500) return 'Oro';
  if (points >= 500) return 'Plata';
  return 'Bronce';
});

// Puntos de logros desbloqueados (suma real de puntos de logros)
const logrosPoints = ref(0);

// Ranking = mediana de puntos del rango general + puntos totales de logros desbloqueados
const calculatedRankingPts = computed(() => {
    return Math.round(averagePoints.value + logrosPoints.value);
});

// --- Estado de datos ---
const rachaDias = ref(0);
const logrosDesbloqueados = ref(0);
const totalLogros = ref(0);
const rutinaSugerida = ref<any>(null);
const ultimosLogros = ref<any[]>([]);
const isLoading = ref(true);
const hasError = ref(false);
const errorText = ref('');

const showActiveWorkout = ref(false);
const activeWorkoutData = ref<any>(null);

const startSuggestedWorkout = async () => {
    if (rutinaSugerida.value) {
        try {
            // Cargar detalle completo para tener ejercicios
            const res = await rutinasApi.getById(rutinaSugerida.value.id);
            activeWorkoutData.value = {
                ...rutinaSugerida.value,
                ejercicios: res.data?.ejercicios || [],
                exerciseList: (res.data?.ejercicios || []).map((e: any) => ({
                    ejercicioId: e.ejercicioId || e.id,
                    id: e.ejercicioId || e.id,
                    name: e.nombreEjercicio,
                    sets: e.series,
                    reps: e.repeticiones || '10-12',
                    rest: `${e.descansoSegundos}s`,
                    weight: e.ultimoPeso || '0',
                    muscle: e.grupoMuscular
                }))
            };
            showActiveWorkout.value = true;
        } catch (e) {
            console.error("No se pudo cargar el detalle de la rutina", e);
            hasError.value = true;
            errorText.value = "Error al intentar comenzar rutina. Es posible que haya sido eliminada o tengas mala conexión.";
        }
    }
};

const handleWorkoutCompleted = () => {
    showActiveWorkout.value = false;
    authStore.fetchProfile(); // refrescar datos de usuario
    
    // Si la rutina era parte del ciclo, avanzamos el índice
    if (rutinaSugerida.value && rutinaSugerida.value.isFromCycle) {
        const savedCycle = localStorage.getItem('reps_cycle');
        if (savedCycle) {
            try {
                const cycleArray = JSON.parse(savedCycle);
                let currentIndex = parseInt(localStorage.getItem('reps_cycle_index') || '0', 10);
                currentIndex++;
                if (currentIndex >= cycleArray.length) currentIndex = 0;
                localStorage.setItem('reps_cycle_index', currentIndex.toString());
            } catch (e) {
                console.error('Error avanzando el ciclo', e);
            }
        }
    }
    
    loadData();
};

// --- Semana: días entrenados ---
const diasSemana = ['L','M','X','J','V','S','D'];
const weekDaysWorked = computed(() => {
  const r = authStore.profile?.rachaDias ?? 0;
  // JavaScript getDay(): 0 = Domingo, 1 = Lunes... 6 = Sábado
  // Mapeo JS a nuestro array (0=Lunes... 6=Domingo)
  let currentJsDay = new Date().getDay();
  let currentCustomDay = currentJsDay === 0 ? 6 : currentJsDay - 1; 
  
  return diasSemana.map((d, i) => {
      // Un día se cuenta como completado si la racha lo cubre mirando hacia atrás desde hoy
      // (asumiendo que la racha llega hasta hoy, por simplicidad)
      const diff = currentCustomDay - i;
      let done = false;
      if (diff >= 0 && diff < r) {
          done = true;
      } else if (diff < 0 && (7 + diff) < r) {
          done = true;
      }
      
      return { label: d, done, isToday: i === currentCustomDay };
  });
});

const metaSemanal = ref(5);
const entrenamientosSemana = computed(() => Math.min(authStore.profile?.rachaDias ?? 0, metaSemanal.value));
const progresoPorcentaje = computed(() => Math.round((entrenamientosSemana.value / metaSemanal.value) * 100));

// --- Carga de datos ---
const loadData = async () => {
  isLoading.value = true;
  hasError.value = false;
  try {
    const [dashRes, recRes, muscleRes] = await Promise.all([
      dashboardApi.getHome(),
      recordsApi.getMisRecords(),
      progresoApi.getMuscular()
    ]);
    const data = dashRes.data;
    rachaDias.value = data.rachaDias ?? 0;
    logrosDesbloqueados.value = data.logrosDesbloqueados ?? 0;
    totalLogros.value = data.totalLogros ?? 0;
    rutinaSugerida.value = data.rutinaSugerida ?? null;
    ultimosLogros.value = (data.ultimosLogros ?? []).slice(0, 3);
    records.value = recRes.data ?? [];
    muscleRanks.value = muscleRes.data ?? [];

    if (authStore.profile?.id) {
        try {
            const logsRes = await logrosApi.getMisLogros(authStore.profile.id);
            const unlockedLogros = (logsRes.data || []).filter((l: any) => l.desbloqueado);
            unlockedCount.value = unlockedLogros.length;
            // Sumar los puntos reales de los logros desbloqueados
            logrosPoints.value = unlockedLogros.reduce((acc: number, l: any) => acc + (l.puntos || 0), 0);
        } catch (e) {
            console.warn("No se pudieron cargar logros", e);
        }
    }
    
    // Lógica del Ciclo
    const savedCycle = localStorage.getItem('reps_cycle');
    if (savedCycle) {
      try {
        const cycleArray = JSON.parse(savedCycle);
        if (cycleArray && cycleArray.length > 0) {
          let currentIndex = parseInt(localStorage.getItem('reps_cycle_index') || '0', 10);
          if (currentIndex >= cycleArray.length) {
            currentIndex = 0;
            localStorage.setItem('reps_cycle_index', '0');
          }
          const cycleRutinaId = cycleArray[currentIndex].id;
          
          try {
             // Overwrite la sugerencia con la del ciclo!
             const resCycle = await rutinasApi.getById(cycleRutinaId);
             if (resCycle && resCycle.data) {
                rutinaSugerida.value = resCycle.data;
                rutinaSugerida.value.isFromCycle = true;
             }
          } catch(err) {
             console.warn('La rutina del ciclo no se pudo cargar', err);
          }
        }
      } catch(e) {
         console.error('Error aplicando ciclo al dashboard', e);
      }
    }
  } catch (e: any) {
    hasError.value = true;
    errorText.value = getErrorMessage(e);
    console.error('Error cargando dashboard', e);
  } finally {
    isLoading.value = false;
  }
};

onMounted(() => {
    loadData();
});
</script>

<style scoped>
@keyframes progress {
  from { width: 0; }
}

@keyframes wave {
  0% { transform: rotate(0deg); }
  10% { transform: rotate(14deg); }
  20% { transform: rotate(-8deg); }
  30% { transform: rotate(14deg); }
  40% { transform: rotate(-4deg); }
  50% { transform: rotate(10deg); }
  60% { transform: rotate(0deg); }
  100% { transform: rotate(0deg); }
}

/* Custom scrollbar to match the design */
::-webkit-scrollbar {
  width: 8px;
}
::-webkit-scrollbar-track {
  background: #000000;
}
::-webkit-scrollbar-thumb {
  background: #1F2937; 
  border-radius: 4px;
}
::-webkit-scrollbar-thumb:hover {
  background: #374151; 
}
</style>
