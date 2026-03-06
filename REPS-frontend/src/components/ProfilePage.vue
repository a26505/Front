<template>
  <div class="flex min-h-screen bg-[#000000] text-white">
    <!-- 1️⃣ SIDEBAR (Usa el mismo componente que Dashboard) -->
    <Sidebar active="profile" />

    <!-- CONTENIDO PRINCIPAL -->
    <div class="flex-1 md:ml-[256px] min-h-screen flex flex-col">
      <!-- 2️⃣ HEADER STICKY -->
      <header class="sticky top-0 z-40 w-full h-[80px] bg-black/95 backdrop-blur-md px-6 flex items-center border-b border-[#1F2937]/50">
        <h1 class="text-2xl md:text-3xl font-bold text-white tracking-tight">Mi Perfil</h1>
      </header>

      <main class="flex-1 max-w-[1280px] w-full mx-auto px-4 pb-12 pt-6">
        <!-- 3️⃣ CARD HEADER PERFIL (GRADIENTE ROJO) -->
        <div class="bg-gradient-to-r from-[#DC2626] to-[#991B1B] rounded-[16px] p-[32px] flex flex-col md:flex-row items-center md:items-start gap-[24px] mb-[24px]">
          <!-- AVATAR SECCIÓN -->
          <div class="relative">
            <div class="w-[128px] h-[128px] rounded-full border-4 border-white/20 overflow-hidden bg-[#1F2937]">
              <img :src="avatarUrl" :style="{ transform: selectedAvatarId === 'avatar_gymbro' ? 'scale(2.5)' : (selectedAvatarId === 'avatar_robot' ? 'scale(1.3)' : 'scale(1.4)') }" alt="Avatar" class="w-full h-full object-cover" />
            </div>
            <button @click="showAvatarDropdown = !showAvatarDropdown" class="absolute bottom-0 right-0 bg-black border-2 border-white/20 rounded-full w-[40px] h-[40px] flex items-center justify-center cursor-pointer transition-all duration-200 hover:bg-[#1F2937]">
              <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <path d="M23 19a2 2 0 01-2 2H3a2 2 0 01-2-2V8a2 2 0 012-2h4l2-3h6l2 3h4a2 2 0 012 2z"/>
                <circle cx="12" cy="13" r="4"/>
              </svg>
            </button>

            <!-- Avatar Dropdown -->
            <div v-if="showAvatarDropdown" class="absolute top-full mt-2 left-1/2 -translate-x-1/2 bg-[#111827] border border-[#374151] rounded-xl shadow-2xl p-2 z-50 flex gap-2 w-max">
               <button v-for="avatar in availableAvatars" :key="avatar.id" @click="selectAvatar(avatar.id)" 
                  class="w-12 h-12 rounded-full border-2 overflow-hidden hover:scale-110 transition-transform"
                  :class="selectedAvatarId === avatar.id ? 'border-[#DC2626]' : 'border-transparent'">
                  <img :src="avatar.url" :style="{ transform: avatar.id === 'avatar_gymbro' ? 'scale(2.5)' : (avatar.id === 'avatar_robot' ? 'scale(1.3)' : 'scale(1.4)') }" class="w-full h-full object-cover bg-[#1F2937]" />
               </button>
            </div>
          </div>

          <!-- INFO USUARIO -->
          <div class="flex-1 text-center md:text-left">
            <div class="flex items-center justify-center md:justify-start gap-3 mb-2">
              <h2 class="text-[32px] font-bold text-white">{{ nombre }}</h2>
              <div v-if="esPro" class="bg-[#FBBF24] text-black px-[10px] py-[4px] rounded-[12px] flex items-center gap-[4px] text-[11px] font-bold uppercase">
                <svg width="12" height="12" viewBox="0 0 24 24" fill="black">
                  <path d="M2 4l4 7 6-7 6 7 4-7v13H2V4z"/>
                </svg>
                PRO
              </div>
            </div>

            <div class="flex flex-col gap-1 text-white/80 text-[14px]">
              <div class="flex items-center justify-center md:justify-start gap-2">
                <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <path d="M4 4h16c1.1 0 2 .9 2 2v12c0 1.1-.9 2-2 2H4c-1.1 0-2-.9-2-2V6c0-1.1.9-2 2-2z"/>
                  <polyline points="22,6 12,13 2,6"/>
                </svg>
                <span>{{ email }}</span>
              </div>
              <div class="flex items-center justify-center md:justify-start gap-2">
                <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <rect x="3" y="4" width="18" height="18" rx="2" ry="2"/>
                  <line x1="16" y1="2" x2="16" y2="6"/>
                  <line x1="8" y1="2" x2="8" y2="6"/>
                  <line x1="3" y1="10" x2="21" y2="10"/>
                </svg>
                <span>{{ fechaRegistro }}</span>
              </div>
              <div class="flex items-center justify-center md:justify-start gap-2">
                <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <path d="M16 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/>
                  <circle cx="8.5" cy="7" r="4"/>
                  <line x1="20" y1="8" x2="20" y2="14"/>
                  <line x1="23" y1="11" x2="17" y2="11"/>
                </svg>
                <span>Código Amigo:</span>
                <button 
                  @click="handleCopyCode" 
                  class="flex items-center gap-1 bg-white/10 px-2 py-1 rounded-[6px] transition-all duration-200 hover:bg-white/20"
                >
                  <span class="font-mono font-semibold">{{ codigoAmigo }}</span>
                  <component :is="copiedCode ? CheckIcon : CopyIcon" :class="copiedCode ? 'text-[#22C55E]' : 'text-white'" />
                </button>
              </div>
            </div>

          </div>

          <!-- BOTÓN EDITAR -->
          <button @click="goToSettings" class="bg-white text-[#DC2626] rounded-[10px] px-6 py-3 flex items-center gap-2 text-[15px] font-semibold cursor-pointer transition-all duration-200 hover:bg-[#F3F4F6] w-full md:w-auto justify-center">
            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="#DC2626" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
              <path d="M11 4H4a2 2 0 00-2 2v14a2 2 0 002 2h14a2 2 0 002-2v-7"/>
              <path d="M18.5 2.5a2.121 2.121 0 013 3L12 15l-4 1 1-4 9.5-9.5z"/>
            </svg>
            Editar Perfil
          </button>
        </div>

        <!-- 4️⃣ GRID ESTADÍSTICAS -->
        <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-[16px] mb-[24px]">
          <div v-for="stat in stats" :key="stat.label" class="bg-[rgba(17,24,39,0.5)] border border-[#1F2937] rounded-[12px] p-[24px]">
            <div class="mb-3" :class="stat.color">
               <RankIcon v-if="stat.label === 'Rango General'" :rank="calculatedRangoGeneral" :size="40" />
               <div v-else v-html="stat.icon"></div>
            </div>
            <div class="text-[32px] font-bold text-white mb-1">{{ stat.value }}</div>
            <div class="text-[14px] text-[#9CA3AF]">{{ stat.label }}</div>
            <div v-if="stat.detail" class="text-[11px] text-[#9CA3AF]/70 mt-1">{{ stat.detail }}</div>
          </div>
        </div>

        <!-- 5️⃣ GRID 2 COLUMNAS: AMIGOS + LOGROS -->
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-[24px] mb-[24px]">
          <!-- MIS AMIGOS -->
          <div class="bg-[rgba(17,24,39,0.5)] border border-[#1F2937] rounded-[12px] p-[24px]">
            <div class="flex justify-between items-center mb-6">
              <h3 class="text-[20px] font-bold text-white">Mis Amigos</h3>
              <div class="border border-[#DC2626] text-[#DC2626] px-3 py-1 rounded-[12px] text-[13px]">{{ friends.length }} amigos</div>
            </div>
            
            <div class="flex flex-col gap-3 mb-4">
              <div v-for="friend in friends" :key="friend.name" class="flex items-center gap-3 bg-[rgba(31,41,55,0.5)] border border-[#374151] rounded-[12px] p-3 transition-all duration-300 hover:border-[#4B5563]">
                <div class="w-[48px] h-[48px] rounded-full border border-white/10 flex items-center justify-center text-[24px] overflow-hidden">
                  <img :src="getAvatarUrl(friend.avatarId)" class="w-full h-full object-cover bg-[#1F2937]" />
                </div>
                <div class="flex-1">
                  <div class="text-[14px] font-semibold text-white mb-1">{{ friend.name }}</div>
                  <div class="flex gap-3 text-[12px] text-[#9CA3AF]">
                    <div class="flex items-center gap-1">
                      <svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                        <circle cx="12" cy="8" r="7"/><polyline points="8.21 13.89 7 23 12 20 17 23 15.79 13.88"/>
                      </svg>
                      {{ friend.level }} pts
                    </div>
                      {{ friend.workouts }} entrenamientos
                    <div class="flex items-center gap-1">
                      <svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="#F97316" stroke-width="2">
                        <path d="M15.362 5.214A8.252 8.252 0 0 1 12 21 8.25 8.25 0 0 1 6.038 7.047 8.287 8.287 0 0 0 9 9.601a8.983 8.983 0 0 1 3.361-6.867 8.21 8.21 0 0 0 3 2.48Z"/><path d="M12 18a3.75 3.75 0 0 0 .495-7.468 5.99 5.99 0 0 0-1.925 3.547 5.975 5.975 0 0 1-2.133-1.001A3.75 3.75 0 0 0 12 18Z"/>
                      </svg>
                      <span class="text-[#F97316]">{{ friend.streak }}d</span>
                    </div>
                  </div>
                </div>
                <button @click="router.push('/community')" class="text-[12px] text-[#DC2626] hover:text-[#EF4444] cursor-pointer transition-colors">Ver perfil</button>
              </div>
            </div>

            <button @click="router.push('/community')" class="w-full mt-4 border border-[#374151] text-white rounded-[8px] py-[10px] flex items-center justify-center gap-2 hover:border-[#DC2626] hover:text-[#DC2626] transition-all duration-200">
              <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <path d="M16 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/><circle cx="8.5" cy="7" r="4"/><line x1="20" y1="8" x2="20" y2="14"/><line x1="23" y1="11" x2="17" y2="11"/>
              </svg>
              Ver todos los amigos
            </button>
          </div>

          <!-- LOGROS -->
          <div class="bg-[rgba(17,24,39,0.5)] border border-[#1F2937] rounded-[12px] p-[24px]">
            <div class="flex justify-between items-center mb-6">
              <h3 class="text-[20px] font-bold text-white">Logros</h3>
              <div class="text-[#9CA3AF] text-[13px] font-medium bg-[#1F2937] px-3 py-1 rounded-full border border-[#374151]">
                {{ unlockedCount }}/{{ achievements.length }}
              </div>
            </div>

            <div v-if="unlockedCount === 0" class="flex flex-col items-center justify-center py-10 px-6 bg-[rgba(31,41,55,0.2)] border border-dashed border-[#374151] rounded-[12px] mb-6">
               <div class="w-16 h-16 bg-[#1F2937] rounded-full flex items-center justify-center mb-4">
                  <svg width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="#DC2626" stroke-width="1.5">
                    <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"/>
                  </svg>
               </div>
               <p class="text-[#9CA3AF] text-center font-medium max-w-[300px]">
                 Aún no has desbloqueado ningún logro.<br>
                 <span class="text-white">¡Tu vitrina espera! Sigue entrenando para conseguir el primero.</span>
               </p>
            </div>

            <div class="grid grid-cols-2 gap-4">
              <div v-for="achievement in achievements" :key="achievement.title" 
                   class="p-4 rounded-[12px] border transition-all duration-300 flex flex-col h-full"
                   :class="achievement.locked ? 'bg-[rgba(17,24,39,0.3)] border-[#1F2937] opacity-60' : 'bg-[rgba(31,41,55,0.5)] border-[#374151] opacity-100'">
                <div class="flex items-center gap-3 mb-3">
                  <div class="w-[40px] h-[40px] shrink-0 rounded-full flex items-center justify-center text-white" :style="{ backgroundColor: achievement.color }">
                    <div v-html="achievement.icon"></div>
                  </div>
                  <div class="text-[14px] font-semibold text-white truncate">{{ achievement.title }}</div>
                </div>
                <div class="text-[12px] leading-relaxed" :class="achievement.locked ? 'text-[#6B7280]' : 'text-[#9CA3AF]'">
                  {{ achievement.locked ? 'Sigue así para lograr desbloquear tu logro' : achievement.desc }}
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- 6️⃣ ACTIVIDAD MENSUAL -->
        <div class="bg-[rgba(17,24,39,0.5)] border border-[#1F2937] rounded-[12px] p-[24px] mb-[24px]">
          <h3 class="text-[20px] font-bold text-white mb-6">Actividad Mensual</h3>
          <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-[16px]">
            <div v-for="month in activity" :key="month.name" class="flex flex-col gap-2">
              <div class="flex justify-between items-center text-[14px]">
                <span class="text-[#9CA3AF]">{{ month.name }}</span>
                <span class="text-white font-semibold">{{ month.total }} entrenamientos</span>
              </div>
              <div class="w-full h-[8px] bg-[#1F2937] rounded-full overflow-hidden">
                <div class="h-full bg-gradient-to-r from-[#DC2626] to-[#EF4444] rounded-full transition-all duration-500 ease-out" 
                     :style="{ width: month.percent + '%' }"></div>
              </div>
            </div>
          </div>
        </div>

        <!-- 7️⃣ RÉCORDS PERSONALES -->
        <div class="bg-[rgba(17,24,39,0.5)] border border-[#1F2937] rounded-[12px] p-[24px]">
          <h3 class="text-[20px] font-bold text-white mb-6">Récords Personales</h3>
          <div v-if="records.length > 0" class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-[24px]">
            <div v-for="record in records" :key="record.label" class="bg-[rgba(31,41,55,0.5)] border border-[#374151] rounded-[12px] p-4">
              <div class="flex items-center gap-3 mb-2">
                <div class="w-[40px] h-[40px] rounded-[10px] flex items-center justify-center" :style="{ backgroundColor: record.color }">
                   <div v-html="record.icon"></div>
                </div>
                <div class="flex flex-col">
                  <span class="text-[24px] font-bold text-white">{{ record.value }}</span>
                  <span class="text-[14px] text-[#9CA3AF]">{{ record.label }}</span>
                </div>
              </div>
            </div>
          </div>
          <div v-else class="flex flex-col items-center justify-center py-12 px-6 bg-[rgba(31,41,55,0.2)] border border-dashed border-[#374151] rounded-[12px]">
            <div class="w-16 h-16 bg-[#1F2937] rounded-full flex items-center justify-center mb-4">
              <svg width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="#6B7280" stroke-width="1.5">
                <circle cx="12" cy="8" r="7"/><polyline points="8.21 13.89 7 23 12 20 17 23 15.79 13.88"/>
              </svg>
            </div>
            <p class="text-[#9CA3AF] text-center font-medium">Aún no tienes récords personales.<br>¡Prepárate para batir tus propios límites!</p>
          </div>
        </div>
      </main>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, h, onMounted, computed, watch } from 'vue';
import Sidebar from './Sidebar.vue';
import RankIcon from './common/RankIcon.vue';
import { useAuthStore } from '../stores/auth';
import { usuariosApi, recordsApi, progresoApi, logrosApi, dashboardApi } from '../api';

import { useRouter } from 'vue-router';

const authStore = useAuthStore();
const router = useRouter();

const goToSettings = () => {
  router.push('/settings');
};

onMounted(async () => {
  document.title = 'Mi Perfil | REPS - Gestiona tu información y logros';
  if (!authStore.profile) {
    await authStore.fetchProfile();
  }
  
  // Cargar datos reales
  try {
      const [dashRes, progRes, muscleRes] = await Promise.all([
          dashboardApi.getHome(),
          progresoApi.getGeneral(),
          progresoApi.getMuscular()
      ]);
      rachaDias.value = dashRes.data.rachaDias ?? (authStore.profile?.rachaDias ?? 0);
      rangoGeneral.value = progRes.data.rangoGeneral ?? (authStore.profile?.rangoGeneral ?? 'Bronce');
      puntosTotales.value = progRes.data.puntosTotales ?? (authStore.profile?.puntosTotales ?? 0);
      
      muscleRanks.value = (muscleRes.data as any[]).map((m: any) => ({
        points: m.puntosActuales || 0
      }));
  } catch (e) {
      console.warn("Fallo al obtener estado fresco de progreso", e);
      rachaDias.value = authStore.profile?.rachaDias ?? 0;
      rangoGeneral.value = authStore.profile?.rangoGeneral ?? 'Bronce';
      puntosTotales.value = authStore.profile?.puntosTotales ?? 0;
  }

  await loadAmigos();
  await loadRecords();
  if (authStore.profile?.id) {
     await loadLogros(authStore.profile.id);
  }
});

// --- PERFIL DATOS ---
const nombre = computed(() => authStore.profile?.nombre ?? '—');
const email = computed(() => authStore.profile?.email ?? '—');
const codigoAmigo = computed(() => authStore.profile?.codigoAmigo ?? '—');
const fechaRegistro = computed(() => {
  const f = authStore.profile?.fechaRegistro;
  if (!f) return '—';
  const d = new Date(f);
  return `Miembro desde ${d.toLocaleDateString('es-ES', { month: 'long', year: 'numeric' })}`;
});
const esPro = computed(() => authStore.profile?.esPro ?? false);

// Estadísticas en tiempo real
const rachaDias = ref(0);
const rangoGeneral = ref('Bronce');
const puntosTotales = ref(0);
const muscleRanks = ref<any[]>([]);

// Lógica para rango calculado (igual que en ProgressPage/Dashboard)
const averagePoints = computed(() => {
  const withPoints = muscleRanks.value.filter((m: any) => (m.points || 0) > 0);
  if (withPoints.length === 0) return 0;
  const total = withPoints.reduce((acc: number, m: any) => acc + (m.points || 0), 0);
  return Math.round(total / withPoints.length);
});

const calculatedRangoGeneral = computed(() => {
  const pts = averagePoints.value;
  if (pts >= 1000) return 'Leyenda';
  if (pts >= 700) return 'Diamante';
  if (pts >= 400) return 'Platino';
  if (pts >= 250) return 'Oro';
  if (pts >= 100) return 'Plata';
  return 'Bronce'; // Default to backend's general rank if no points, or Bronce
});

// Icons as components for the copy logic
const CopyIcon = h('svg', { 
  width: 12, height: 12, viewBox: '0 0 24 24', fill: 'none', stroke: 'white', strokeWidth: 2, strokeLinecap: 'round', strokeLinejoin: 'round' 
}, [
  h('rect', { x: 9, y: 9, width: 13, height: 13, rx: 2, ry: 2 }),
  h('path', { d: 'M5 15H4a2 2 0 01-2-2V4a2 2 0 012-2h9a2 2 0 012 2v1' })
]);

const CheckIcon = h('svg', { 
  width: 12, height: 12, viewBox: '0 0 24 24', fill: 'none', stroke: '#22C55E', strokeWidth: 2, strokeLinecap: 'round', strokeLinejoin: 'round' 
}, [
  h('polyline', { points: '20 6 9 17 4 12' })
]);

const copiedCode = ref(false);
const handleCopyCode = () => {
  navigator.clipboard.writeText(codigoAmigo.value);
  copiedCode.value = true;
  setTimeout(() => copiedCode.value = false, 2000);
};

// --- AVATAR SELECTION ---
const availableAvatars = [
  { id: 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772035659/unnamed_t93s8g.jpg', url: 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772035659/unnamed_t93s8g.jpg' },
  { id: 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772035494/unnamed_l44n9h.jpg', url: 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772035494/unnamed_l44n9h.jpg' },
  { id: 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772034939/unnamed_w3uwac.jpg', url: 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772034939/unnamed_w3uwac.jpg' },
  { id: 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772024580/unnamed_kfdzjz.jpg', url: 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772024580/unnamed_kfdzjz.jpg' },
  { id: 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772024079/unnamed_ojydo4.png', url: 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772024079/unnamed_ojydo4.png' }
];

const selectedAvatarId = ref(availableAvatars[0].id);

// Sincronizar con el store cuando cargue el perfil
watch(() => authStore.profile?.avatarId, (newId) => {
  if (newId) {
    selectedAvatarId.value = newId;
  }
}, { immediate: true });

const avatarUrl = computed(() => {
  if (selectedAvatarId.value?.startsWith('http')) {
    return selectedAvatarId.value;
  }
  // Fallback for old IDs if they somehow persist in local state
  const avatar = availableAvatars.find(a => a.id === selectedAvatarId.value);
  return avatar ? avatar.url : availableAvatars[0].url;
});

const showAvatarDropdown = ref(false);

const selectAvatar = async (id: string) => {
    selectedAvatarId.value = id;
    showAvatarDropdown.value = false;
    try {
        // Actualizar en el servidor
        await usuariosApi.updatePerfil({ 
          avatarId: id,
          // Enviamos también el nombre para evitar que el back lo vea vacío si el DTO no es nullable
          nombre: authStore.profile?.nombre 
        });
        
        // Actualizar store localmente para feedback inmediato
        if (authStore.profile) {
            authStore.profile.avatarId = id;
        }
    } catch (e) {
        console.error("Error updating avatar", e);
    }
}

// --- ESTADÍSTICAS RÁPIDAS (del perfil) ---
const stats = computed(() => [
  { 
    label: 'Racha Actual', value: `${rachaDias.value} días`, color: 'text-[#F97316]',
    icon: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M15.362 5.214A8.252 8.252 0 0 1 12 21 8.25 8.25 0 0 1 6.038 7.047 8.287 8.287 0 0 0 9 9.601a8.983 8.983 0 0 1 3.361-6.867 8.21 8.21 0 0 0 3 2.48Z"/><path d="M12 18a3.75 3.75 0 0 0 .495-7.468 5.99 5.99 0 0 0-1.925 3.547 5.975 5.975 0 0 1-2.133-1.001A3.75 3.75 0 0 0 12 18Z"/></svg>`
  },
  { 
    label: 'Rango General', value: calculatedRangoGeneral.value, color: 'text-[#EAB308]',
    icon: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="8" r="7"/><polyline points="8.21 13.89 7 23 12 20 17 23 15.79 13.88"/></svg>`
  },
  { 
    label: 'Puntos totales', value: String(authStore.profile?.puntosTotales ?? 0), color: 'text-[#3B82F6]',
    icon: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><rect x="3" y="4" width="18" height="18" rx="2" ry="2"/><line x1="16" y1="2" x2="16" y2="6"/><line x1="8" y1="2" x2="8" y2="6"/><line x1="3" y1="10" x2="21" y2="10"/></svg>`
  },
  { 
    label: 'Récords', value: String(personalRecords.value.length), color: 'text-[#EAB308]',
    icon: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="8" r="7"/><polyline points="8.21 13.89 7 23 12 20 17 23 15.79 13.88"/></svg>`
  }
]);

// --- AMIGOS ---
const friends = ref<any[]>([]);

const getAvatarUrl = (id?: string) => {
  if (!id) return availableAvatars[0].url;
  if (id.startsWith('http')) return id;
  
  const oldAvatarMap: Record<string, string> = {
    'avatar_default': 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772035659/unnamed_t93s8g.jpg',
    'avatar_robot': 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772034939/unnamed_w3uwac.jpg',
    'avatar_gymbro': 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772035659/unnamed_t93s8g.jpg',
    'avatar_mujerfit': 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772035494/unnamed_l44n9h.jpg',
    'avatar_hombrefit': 'https://res.cloudinary.com/dgtahwqpj/image/upload/v1772024580/unnamed_kfdzjz.jpg'
  };

  return oldAvatarMap[id] || availableAvatars[0].url;
};

const loadAmigos = async () => {
  try {
    const res = await usuariosApi.getMisAmigos();
    friends.value = (res.data as any[]).map((f: any) => ({
      name: f.nombre ?? f.name ?? 'Atleta',
      avatarId: f.avatarId,
      level: f.puntosTotales ?? 0,
      workouts: 0,
      streak: f.rachaDias ?? 0,
      codigoAmigo: f.codigoAmigo,
      bio: f.biografia
    }));
  } catch (e) {
    console.warn('No se pudieron cargar amigos', e);
  }
};

// --- RÉCORDS ---
const personalRecords = ref<any[]>([]);
const loadRecords = async () => {
  try {
    const res = await recordsApi.getMisRecords();
    personalRecords.value = res.data ?? [];
  } catch (e) {
    console.warn('No se pudieron cargar récords', e);
  }
};

// Logros y actividad (mantienen datos de ejemplo mientras el backend los implementa)
const achievements = ref<any[]>([
  { title: 'Racha de Fuego', desc: '28 días consecutivos', icon: '<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2"><path d="M15.362 5.214A8.252 8.252 0 0 1 12 21 8.25 8.25 0 0 1 6.038 7.047 8.287 8.287 0 0 0 9 9.601a8.983 8.983 0 0 1 3.361-6.867 8.21 8.21 0 0 0 3 2.48Z"/><path d="M12 18a3.75 3.75 0 0 0 .495-7.468 5.99 5.99 0 0 0-1.925 3.547 5.975 5.975 0 0 1-2.133-1.001A3.75 3.75 0 0 0 12 18Z"/></svg>', color: '#EA580C', locked: false },
  { title: 'Centurión', desc: '100 entrenamientos', icon: '<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2"><path d="M6 9H4.5A2.5 2.5 0 0 1 2 6.5V6A2.5 2.5 0 0 1 4.5 3.5H6M18 9h1.5A2.5 2.5 0 0 0 22 6.5V6A2.5 2.5 0 0 0 19.5 3.5H18M4 22h16M10 14.66V17c0 .55-.47.98-.97 1.21C7.85 18.75 7 20.24 7 22M14 14.66V17c0 .55.47.98.97 1.21C16.15 18.75 17 20.24 17 22M18 2H6v7a6 6 0 0 0 12 0V2Z"/></svg>', color: '#CA8A04', locked: false },
  { title: 'Rey del Gimnasio', desc: 'Conseguir 2000 puntos', icon: '<svg width="24" height="24" viewBox="0 0 24 24" fill="white"><path d="M2 4l4 7 6-7 6 7 4-7v13H2V4z"/></svg>', color: '#9333EA', locked: false },
  { title: 'Estrella Ascendente', desc: '50 entrenamientos / 30 días', icon: '<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"/></svg>', color: '#3B82F6', locked: false },
  { title: 'Velocista', desc: '10 entrenamientos / semana', icon: '<svg width="24" height="24" viewBox="0 0 24 24" fill="white"><polygon points="13 2 3 14 12 14 11 22 21 10 12 10 13 2"/></svg>', color: '#0891B2', locked: false },
  { title: 'Inquebrantable', desc: '60 días de actividad', icon: '<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2"><path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"/></svg>', color: '#4B5563', locked: true }
]);

const loadLogros = async (userId: number) => {
    try {
        const res = await logrosApi.getMisLogros(userId);
        if (res.data && res.data.length > 0) {
            achievements.value = res.data.map((l: any) => ({
                title: l.titulo || l.logroNombre,
                desc: l.descripcion,
                icon: '<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2"><path d="M6 9H4.5A2.5 2.5 0 0 1 2 6.5V6A2.5 2.5 0 0 1 4.5 3.5H6M18 9h1.5A2.5 2.5 0 0 0 22 6.5V6A2.5 2.5 0 0 0 19.5 3.5H18M4 22h16M10 14.66V17c0 .55-.47.98-.97 1.21C7.85 18.75 7 20.24 7 22M14 14.66V17c0 .55.47.98.97 1.21C16.15 18.75 17 20.24 17 22M18 2H6v7a6 6 0 0 0 12 0V2Z"/></svg>',
                color: l.desbloqueado ? '#EA580C' : '#4B5563',
                locked: !l.desbloqueado
            }));
        }
    } catch (e) {
        console.warn('No se pudieron cargar logros reales', e);
    }
}

const generateLastSixMonths = () => {
  const months = ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'];
  const result = [];
  const now = new Date();
  
  for (let i = 5; i >= 0; i--) {
    const d = new Date(now.getFullYear(), now.getMonth() - i, 1);
    result.push({
      name: months[d.getMonth()],
      total: 0,
      percent: 0
    });
  }
  return result;
};

const activity = ref(generateLastSixMonths());

const loadActivity = async () => {
    try {
        const res = await progresoApi.getAnalitica();
        if (res.data && res.data.actividadMensual && res.data.actividadMensual.length > 0) {
            // Asegurarnos de tomar solo los últimos 6 si el back devuelve más
            activity.value = res.data.actividadMensual.slice(-6);
        } else {
            // Si no hay datos, al menos intentamos simular algo si quieres, 
            // pero por defecto mostrará 0 según generateLastSixMonths
        }
    } catch (e) {
        console.warn('No se pudo cargar actividad mensual', e);
    }
}

onMounted(async () => {
  loadActivity();
});

const records = computed(() => personalRecords.value.slice(0, 3).map((r: any) => ({
  label: r.ejercicioNombre || r.nombreEjercicio || 'Ejercicio Desconocido',
  value: `${r.pesoMaximo} kg`,
  color: '#DC2626',
  icon: '<svg width="12" height="12" viewBox="0 0 24 24" fill="white"><circle cx="12" cy="12" r="8"/></svg>'
})));

const unlockedCount = computed(() => achievements.value.filter(a => !a.locked).length);
</script>

<style scoped>
/* Custom transitions and scrollbar */
.transition-all {
  transition-property: all;
  transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
  transition-duration: 200ms;
}

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
