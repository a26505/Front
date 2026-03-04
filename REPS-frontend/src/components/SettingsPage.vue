<template>
  <div class="flex min-h-screen bg-[#000000] text-white">
    <!-- 1️⃣ SIDEBAR -->
    <Sidebar active="settings" />

    <!-- CONTENIDO PRINCIPAL -->
    <div class="flex-1 md:ml-[256px] min-h-screen flex flex-col">
      <!-- 2️⃣ HEADER SUPERIOR -->
      <header class="sticky top-0 z-40 bg-[rgba(0,0,0,0.95)] backdrop-blur-[12px] py-[16px] px-[24px] flex items-center border-b border-[#1F2937]">
        <h1 class="text-[24px] md:text-[32px] font-bold text-white tracking-tight">Configuración</h1>
      </header>

      <!-- 3️⃣ CONTENEDOR PRINCIPAL -->
      <main class="w-full max-w-[1024px] mx-auto p-[24px] flex flex-col gap-[32px] pb-[40px]">

        <!-- 4️⃣ CARD 1: PERFIL -->
        <div class="bg-[rgba(17,24,39,0.5)] border border-[#1F2937] rounded-[16px] p-[24px]">
          <div class="flex items-center gap-[12px] mb-[24px]">
            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#DC2626" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
              <path d="M20 21v-2a4 4 0 00-4-4H8a4 4 0 00-4 4v2"/>
              <circle cx="12" cy="7" r="4"/>
            </svg>
            <h2 class="text-[20px] font-semibold text-white">Perfil</h2>
          </div>
          <div class="flex flex-col gap-[16px]">
            <!-- Avatar Selection -->
            <div>
              <label class="text-[14px] font-semibold text-white mb-[12px] block">Selecciona tu Avatar</label>
              <div class="flex flex-wrap gap-[12px]">
                <button 
                  v-for="avatar in availableAvatars" 
                  :key="avatar.id"
                  @click="profile.avatar_id = avatar.id"
                  role="button"
                  tabindex="0"
                  class="w-[60px] h-[60px] rounded-full border-2 transition-all overflow-hidden bg-[#1F2937] p-0"
                  :class="profile.avatar_id === avatar.id ? 'border-[#DC2626] scale-110 shadow-[0_0_10px_rgba(220,38,38,0.5)]' : 'border-transparent opacity-60 hover:opacity-100'"
                >
                  <img :src="avatar.url" class="w-full h-full object-cover pointer-events-none" />
                </button>
              </div>
            </div>

            <div>
              <label class="text-[14px] font-semibold text-white mb-[8px] block">Nombre</label>
              <input type="text" v-model="profile.name" class="w-full bg-[#1F2937] border border-[#374151] rounded-[8px] py-[10px] px-[12px] text-[14px] text-white transition-colors duration-200 focus:border-[#DC2626] focus:outline-none" />
            </div>
            <div>
              <label class="text-[14px] font-semibold text-white mb-[8px] block">Correo Electrónico</label>
              <input type="email" v-model="profile.email" class="w-full bg-[#1F2937] border border-[#374151] rounded-[8px] py-[10px] px-[12px] text-[14px] text-white transition-colors duration-200 focus:border-[#DC2626] focus:outline-none" />
            </div>
            <div>
              <label class="text-[14px] font-semibold text-white mb-[8px] block">Biografía</label>
              <input type="text" v-model="profile.bio" class="w-full bg-[#1F2937] border border-[#374151] rounded-[8px] py-[10px] px-[12px] text-[14px] text-white transition-colors duration-200 focus:border-[#DC2626] focus:outline-none" />
            </div>
            <button @click="saveProfile" class="bg-[#DC2626] hover:bg-[#B91C1C] border-none rounded-[8px] py-[10px] px-[20px] text-[14px] font-semibold text-white cursor-pointer mt-[8px] self-start transition-colors duration-200 active:scale-95">Guardar Cambios</button>
          </div>
        </div>



        <!-- 6️⃣ CARD 3: PRIVACIDAD -->
        <div class="bg-[rgba(17,24,39,0.5)] border border-[#1F2937] rounded-[16px] p-[24px]">
          <div class="flex items-center gap-[12px] mb-[24px]">
            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#DC2626" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
              <path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"/>
            </svg>
            <h2 class="text-[20px] font-semibold text-white">Privacidad</h2>
          </div>
          <div class="flex flex-col">
            <template v-for="(item, index) in privacyList" :key="index">
              <div class="flex justify-between items-center py-[16px]">
                <div>
                  <h3 class="text-[15px] font-medium text-white mb-[4px]">{{ item.title }}</h3>
                  <p class="text-[13px] text-[#9CA3AF] leading-[1.4]">{{ item.desc }}</p>
                </div>
                <!-- COMPONENTE SWITCH -->
                <button 
                  role="switch"
                  :aria-checked="item.value"
                  class="relative inline-flex h-[24px] w-[44px] items-center rounded-full transition-colors duration-200 outline-none shrink-0"
                  :class="item.value ? 'bg-[#DC2626]' : 'bg-[#374151]'"
                  @click="togglePrivacy(item)"
                >
                  <span
                    class="inline-block h-[20px] w-[20px] transform rounded-full bg-white transition-transform duration-200 absolute"
                    :class="item.value ? 'translate-x-[22px] left-0' : 'translate-x-[2px] left-0'"
                  />
                </button>
              </div>
              <div v-if="index < privacyList.length - 1" class="h-[1px] bg-[#1F2937] w-full m-0"></div>
            </template>
          </div>
        </div>

        <!-- 7️⃣ CARD 4: SEGURIDAD -->
        <div class="bg-[rgba(17,24,39,0.5)] border border-[#1F2937] rounded-[16px] p-[24px]">
          <div class="flex items-center gap-[12px] mb-[24px]">
            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#DC2626" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
              <rect x="3" y="11" width="18" height="11" rx="2" ry="2"/>
              <path d="M7 11V7a5 5 0 0110 0v4"/>
            </svg>
            <h2 class="text-[20px] font-semibold text-white">Seguridad</h2>
          </div>
          <div class="flex flex-col gap-[12px]">
            <button class="w-full bg-transparent border border-[#374151] rounded-[8px] py-[10px] px-[20px] text-[14px] font-semibold text-white cursor-pointer transition-colors duration-200 hover:border-[#DC2626] hover:text-[#DC2626] active:scale-95 text-center">
              Cambiar Contraseña
            </button>
            <button class="w-full bg-transparent border border-[#374151] rounded-[8px] py-[10px] px-[20px] text-[14px] font-semibold text-white cursor-pointer transition-colors duration-200 hover:border-[#DC2626] hover:text-[#DC2626] active:scale-95 text-center">
              Autenticación de Dos Factores
            </button>
          </div>
        </div>



        <!-- 9️⃣ CARD 6: ZONA PELIGROSA -->
        <div class="bg-[rgba(127,29,29,0.2)] border border-[rgba(127,29,29,0.5)] rounded-[16px] p-[24px]">
          <h2 class="text-[20px] font-semibold text-[#DC2626] mb-[16px]">Zona Peligrosa</h2>
          <p class="text-[14px] text-[#9CA3AF] leading-[1.5] mb-[16px]">
            Una vez que elimines tu cuenta, no hay vuelta atrás. Por favor, ten cuidado.
          </p>
          <button class="w-full bg-[#DC2626] hover:bg-[#B91C1C] border-none rounded-[8px] py-[10px] px-[20px] text-[14px] font-semibold text-white cursor-pointer transition-colors duration-200 active:scale-95">
            Eliminar Cuenta
          </button>
        </div>

      </main>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import Sidebar from './Sidebar.vue';
import { useAuthStore } from '../stores/auth';
import { usuariosApi } from '../api';

const authStore = useAuthStore();

const profile = ref({
  name: '',
  email: '',
  bio: '',
  avatar_id: ''
});

const availableAvatars = [
  { id: 'avatar_default', url: 'https://api.dicebear.com/7.x/avataaars/svg?seed=Jack' },
  { id: 'avatar_robot', url: 'https://api.dicebear.com/7.x/bottts/svg?seed=robot' },
  { id: 'avatar_gymbro', url: 'https://api.dicebear.com/7.x/avataaars/svg?seed=Felix&accessoriesProbability=100' },
  { id: 'avatar_mujerfit', url: 'https://api.dicebear.com/7.x/avataaars/svg?seed=Aneka&top=longHair' },
  { id: 'avatar_hombrefit', url: 'https://api.dicebear.com/7.x/avataaars/svg?seed=George' }
];

const privacyList = ref([
  { id: 'EsPerfilPublico', title: 'Perfil Público', desc: 'Permite que otros usuarios vean tu perfil', value: true },
  { id: 'MostrarEstadisticas', title: 'Mostrar Estadísticas', desc: 'Compartir tus estadísticas con la comunidad', value: true },
  { id: 'RankingVisible', title: 'Ranking Visible', desc: 'Aparecer en las tablas de clasificación', value: true }
]);

onMounted(async () => {
  if (!authStore.profile) {
    await authStore.fetchProfile();
  }
  if (authStore.profile) {
    profile.value = {
      name: authStore.profile.nombre || '',
      email: authStore.profile.email || '',
      bio: authStore.profile.biografia || 'Apasionado del fitness y la vida saludable',
      avatar_id: authStore.profile.avatarId || 'avatar_default'
    };
    
    // Sincronizar privacidad
    privacyList.value[0].value = authStore.profile.esPerfilPublico;
    privacyList.value[1].value = authStore.profile.mostrarEstadisticas;
    privacyList.value[2].value = authStore.profile.rankingVisible;
  }
});

const saveProfile = async () => {
    try {
        await usuariosApi.updatePerfil({ 
            nombre: profile.value.name,
            biografia: profile.value.bio,
            avatarId: profile.value.avatar_id
        });
        if (authStore.profile) {
            authStore.profile.nombre = profile.value.name;
            authStore.profile.biografia = profile.value.bio;
            authStore.profile.avatarId = profile.value.avatar_id;
        }
        alert('Perfil guardado exitosamente');
    } catch (e) {
        console.error(e);
        alert('Error al guardar el perfil');
    }
};

const togglePrivacy = async (item: any) => {
    item.value = !item.value;
    try {
        const updateData: any = {};
        // Mapeamos el ID del item al campo del DTO
        if (item.id === 'EsPerfilPublico') updateData.esPerfilPublico = item.value;
        if (item.id === 'MostrarEstadisticas') updateData.mostrarEstadisticas = item.value;
        if (item.id === 'RankingVisible') updateData.rankingVisible = item.value;

        await usuariosApi.updatePerfil(updateData);
        
        // Actualizar store local
        if (authStore.profile) {
            if (item.id === 'EsPerfilPublico') authStore.profile.esPerfilPublico = item.value;
            if (item.id === 'MostrarEstadisticas') authStore.profile.mostrarEstadisticas = item.value;
            if (item.id === 'RankingVisible') authStore.profile.rankingVisible = item.value;
        }
    } catch (e) {
        console.error("Error updating privacy", e);
        // Revertir si falla
        item.value = !item.value;
    }
}

</script>
