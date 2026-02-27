<template>
  <!-- SIDEBAR IZQUIERDO -->
  <aside
    class="fixed w-[256px] h-screen bg-[#050505] border-r border-[#1E2939]/30 p-6 hidden md:flex flex-col z-50 transition-all duration-300"
  >
    <!-- LOGO -->
    <div class="mb-10 px-2 mt-2">
      <router-link to="/" class="flex items-center">
        <img src="/potential_logo_2.png" alt="REPS Logo" class="h-10 w-auto object-contain" />
      </router-link>
    </div>

    <!-- NAVEGACIÓN -->
    <nav class="flex flex-col gap-2 flex-1">
      <router-link 
        v-for="item in menuItems" 
        :key="item.id"
        :to="item.path" 
        class="h-[50px] px-[17px] rounded-[10px] flex items-center gap-[12px] transition-all duration-200 group"
        :class="active === item.id 
          ? 'bg-[rgba(231,0,11,0.15)] border border-[rgba(231,0,11,0.25)]' 
          : 'bg-transparent hover:bg-white/5 border border-transparent'"
      >
        <div 
          class="flex items-center justify-center transition-colors duration-200"
          :class="active === item.id ? 'text-[#DC2626]' : 'text-[#9CA3AF] group-hover:text-white'"
          v-html="item.icon"
        ></div>
        <span 
          class="text-[16px] transition-colors duration-200"
          :class="active === item.id ? 'text-white' : 'text-[#9CA3AF] group-hover:text-white'"
        >
          {{ item.label }}
        </span>
      </router-link>
    </nav>

    <!-- BOTÓN SALIR -->
    <div class="mt-auto px-4 py-8 border-t border-[#1E2939]/30">
      <div class="flex items-center gap-[12px] cursor-pointer group transition-all duration-200">
        <div class="text-[#9CA3AF] group-hover:text-white transition-colors">
          <svg width="20" height="20" viewBox="0 0 20 20" fill="none">
            <path d="M7.5 17.5H4.16667C3.24583 17.5 2.5 16.7542 2.5 15.8333V4.16667C2.5 3.24583 3.24583 2.5 4.16667 2.5H7.5" stroke="currentColor" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M13.3333 14.1667L17.5 10L13.3333 5.83333" stroke="currentColor" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M17.5 10H7.5" stroke="currentColor" stroke-width="1.67" stroke-linecap="round" stroke-linejoin="round"/>
          </svg>
        </div>
        <span class="text-[#9CA3AF] text-[16px] group-hover:text-white transition-colors">Salir</span>
      </div>
    </div>
  </aside>
</template>

<script setup lang="ts">
defineProps<{
  active: 'dashboard' | 'workouts' | 'progress' | 'community' | 'profile' | 'settings';
}>();

const menuItems = [
  {
    id: 'dashboard',
    label: 'Dashboard',
    path: '/dashboard',
    icon: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="m3 9 9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z"></path><polyline points="9 22 9 12 15 12 15 22"></polyline></svg>`
  },
  {
    id: 'workouts',
    label: 'Entrenamientos',
    path: '/workouts',
    icon: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
      <path d="M14.4 14.4 9.6 9.6"/>
      <path d="M18.657 21.485a2 2 0 1 1-2.829-2.828l-1.768 1.768a2 2 0 1 1-2.829-2.829l6.364-6.364a2 2 0 1 1 2.829 2.828l-1.768 1.768a2 2 0 1 1 2.828 2.829z"/>
      <path d="m21.5 21.5-1.4-1.4"/>
      <path d="M3.9 3.9 2.5 2.5"/>
      <path d="M6.404 12.768a2 2 0 1 1-2.829-2.829l1.768-1.767a2 2 0 1 1-2.828-2.829l2.828-2.828a2 2 0 1 1 2.829 2.828l1.767-1.768a2 2 0 1 1 2.829 2.829z"/>
    </svg>`
  },
  {
    id: 'progress',
    label: 'Progreso',
    path: '/progress',
    icon: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><polyline points="22 7 13.5 15.5 8.5 10.5 2 17"></polyline><polyline points="16 7 22 7 22 13"></polyline></svg>`
  },
  {
    id: 'community',
    label: 'Comunidad',
    path: '#',
    icon: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path><circle cx="9" cy="7" r="4"></circle><path d="M23 21v-2a4 4 0 0 0-3-3.87"></path><path d="M16 3.13a4 4 0 0 1 0 7.75"></path></svg>`
  },
  {
    id: 'profile',
    label: 'Perfil',
    path: '#',
    icon: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"></path><circle cx="12" cy="7" r="4"></circle></svg>`
  },
  {
    id: 'settings',
    label: 'Configuración',
    path: '#',
    icon: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="3"></circle><path d="M19.4 15a1.65 1.65 0 0 0 .33 1.82l.06.06a2 2 0 0 1 0 2.83 2 2 0 0 1-2.83 0l-.06-.06a1.65 1.65 0 0 0-1.82-.33 1.65 1.65 0 0 0-1 1.51V21a2 2 0 0 1-2 2 2 2 0 0 1-2-2v-.09A1.65 1.65 0 0 0 9 19.4a1.65 1.65 0 0 0-1.82.33l-.06.06a2 2 0 0 1-2.83 0 2 2 0 0 1 0-2.83l.06-.06a1.65 1.65 0 0 0 .33-1.82 1.65 1.65 0 0 0-1.51-1H3a2 2 0 0 1-2-2 2 2 0 0 1 2-2h.09A1.65 1.65 0 0 0 4.6 9a1.65 1.65 0 0 0-.33-1.82l-.06-.06a2 2 0 0 1 0-2.83 2 2 0 0 1 2.83 0l.06.06a1.65 1.65 0 0 0 1.82.33H9a1.65 1.65 0 0 0 1-1.51V3a2 2 0 0 1 2-2 2 2 0 0 1 2 2v.09a1.65 1.65 0 0 0 1 1.51 1.65 1.65 0 0 0 1.82-.33l.06-.06a2 2 0 0 1 2.83 0 2 2 0 0 1 0 2.83l-.06.06a1.65 1.65 0 0 0-.33 1.82V9a1.65 1.65 0 0 0 1.51 1H21a2 2 0 0 1 2 2 2 2 0 0 1-2 2h-.09a1.65 1.65 0 0 0-1.51 1z"></path></svg>`
  }
];
</script>
