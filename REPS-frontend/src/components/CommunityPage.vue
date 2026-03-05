<template>
  <div class="flex min-h-screen bg-[#000000] text-white">
    <!-- 1️⃣ SIDEBAR (Usa el mismo componente que Dashboard/Profile) -->
    <Sidebar active="community" />

    <!-- CONTENIDO PRINCIPAL -->
    <div class="flex-1 md:ml-[256px] min-h-screen flex flex-col">
      <!-- 2️⃣ HEADER SUPERIOR STICKY -->
      <header class="sticky top-0 z-40 bg-black/95 backdrop-blur-md py-4 px-6 flex items-center justify-between border-b border-[#1F2937]/50">
        <h1 class="text-3xl font-bold text-white tracking-tight">Comunidad</h1>
        
        <div class="flex gap-3">
          <button @click="showAddFriendModal = true" class="bg-[#DC2626] hover:bg-[#B91C1C] rounded-lg px-4 py-2 flex items-center gap-2 text-sm font-bold transition-all hover:scale-105 active:scale-95">
            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5">
              <path d="M16 21v-2a4 4 0 00-4-4H5a4 4 0 00-4 4v2"/><circle cx="8.5" cy="7" r="4"/><line x1="20" y1="8" x2="20" y2="14"/><line x1="23" y1="11" x2="17" y2="11"/>
            </svg>
            Añadir Amigo
          </button>
        </div>
      </header>

      <!-- 3️⃣ TABS PRINCIPALES (Estilo Workouts) -->
      <div class="px-6 py-4">
        <div class="flex gap-2 overflow-x-auto whitespace-nowrap hide-scrollbar">
          <button 
            v-for="tab in mainTabs" 
            :key="tab.id"
            @click="activeTab = tab.id"
            class="px-5 py-2 text-xs font-black uppercase tracking-widest transition-all rounded-lg border"
            :class="activeTab === tab.id ? 'bg-[#DC2626]/20 text-[#DC2626] border-[#DC2626]/30' : 'text-[#9CA3AF] border-transparent hover:text-white hover:bg-white/5'"
          >
            {{ tab.label }}
          </button>
        </div>
      </div>

      <!-- 4️⃣ CONTENIDO DINÁMICO SEGÚN TAB -->
      <main class="w-full pb-12 overflow-x-hidden">
        
        <!-- TAB 1: AMIGOS -->
        <section v-if="activeTab === 'friends'" class="animate-in fade-in duration-300">
          <!-- Ya no hay título redundante aquí -->

          <!-- SOLICITUDES PENDIENTES (Ancho completo) -->
          <div v-if="pendingRequests.length > 0" class="mx-6 mb-8 bg-[#DC2626]/10 border border-[#DC2626]/30 rounded-[12px] p-4">
            <h3 class="text-[16px] font-semibold text-white mb-3 flex items-center">
              Solicitudes Pendientes
              <span class="ml-2 bg-[#DC2626] px-2 py-0.5 rounded-full text-[11px]">{{ pendingRequests.length }}</span>
            </h3>
            <div v-for="req in pendingRequests" :key="req.codigoAmigo ?? req.nombre" class="flex items-center justify-between bg-[#111827]/50 border border-[#374151] rounded-[10px] p-3 mb-2">
              <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-full bg-gradient-to-br from-[#DC2626]/30 to-[#991B1B]/30 flex items-center justify-center text-white">
                  <svg width="20" height="20" viewBox="0 0 24 24" fill="currentColor"><polygon points="13 2 3 14 12 14 11 22 21 10 12 10 13 2"/></svg>
                </div>
                <div>
                  <div class="text-[14px] font-semibold text-white">{{ req.nombre ?? req.name }}</div>
                  <div class="text-[12px] text-[#9CA3AF]">{{ req.rangoGeneral ?? '' }}</div>
                </div>
              </div>
              <div class="flex items-center gap-2">
                <button @click="responderSolicitud(req.codigoAmigo, true)" class="bg-[#22C55E] hover:bg-[#16A34A] p-2 rounded-md text-white"><svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3"><polyline points="20 6 9 17 4 12"/></svg></button>
                <button @click="responderSolicitud(req.codigoAmigo, false)" class="border border-[#374151] hover:border-[#DC2626] hover:text-[#DC2626] p-2 rounded-md text-[#9CA3AF]"><svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg></button>
              </div>
            </div>
          </div>

          <!-- LISTA AMIGOS (ESTILO RUTINAS/ENTRENAMIENTOS) -->
          <div v-if="friendsList.length > 0" class="px-6 flex flex-col gap-4">
            <div v-for="friend in friendsList" :key="friend.name" 
                 class="bg-[#111827]/50 border border-[#1F2937] rounded-[14px] p-5 flex flex-col md:flex-row md:items-center justify-between gap-6 transition-all duration-300 hover:border-[#DC2626] group">
              
              <!-- IZQUIERDA: AVATAR + INFO BÁSICA -->
              <div class="flex items-center gap-4 min-w-[250px]">
                <div class="relative">
                  <div class="w-16 h-16 rounded-full bg-[#1F2937] border-2 border-[#DC2626]/20 flex items-center justify-center overflow-hidden">
                    <img :src="getAvatarUrl(friend.avatarId)" class="w-full h-full object-cover" />
                  </div>
                  <span class="absolute bottom-0 right-0 w-4 h-4 rounded-full border-2 border-[#111827]" :class="friend.online ? 'bg-[#22C55E]' : 'bg-[#6B7280]'"></span>
                </div>
                <div>
                  <h4 class="text-[18px] font-bold text-white mb-0.5 group-hover:text-[#DC2626] transition-colors">{{ friend.name }}</h4>
                  <div class="text-[13px] text-[#9CA3AF] flex items-center gap-1.5">
                    {{ friend.statusText }}
                  </div>
                </div>
              </div>

              <!-- CENTRO: STATS -->
              <div class="flex-1 flex items-center justify-around md:justify-center md:gap-24 border-l border-[#1F2937]/50 px-4">
                <div class="flex flex-col items-center">
                  <span class="text-[20px] font-extrabold text-white">{{ friend.workouts }}</span>
                  <span class="text-[11px] text-[#9CA3AF] uppercase tracking-widest font-bold italic">Entrenos</span>
                </div>
                <div class="flex flex-col items-center">
                  <span class="text-[20px] font-extrabold text-white">{{ friend.streak }}d</span>
                  <span class="text-[11px] text-[#9CA3AF] uppercase tracking-widest font-bold italic">Racha</span>
                </div>
              </div>

              <!-- BIO (MÓVIL OCULTO O TRUNCADO) -->
              <div class="hidden lg:block flex-1 max-w-[300px]">
                <p class="text-[13px] text-[#6B7280] italic line-clamp-2">"{{ friend.bio }}"</p>
              </div>

              <!-- DERECHA: ACCIONES -->
              <div class="flex items-center gap-2">
                <button @click="viewFriendProfile(friend)" class="flex-1 md:flex-none bg-[#1F2937]/50 hover:bg-[#374151] border border-[#374151] text-white px-5 py-2.5 rounded-[10px] font-bold text-[13px] transition-all">Ver Perfil</button>
              </div>
            </div>
          </div>
          <div v-else class="px-6 flex flex-col items-center justify-center text-center py-10 opacity-70">
              <svg width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="mb-4 text-[#9CA3AF]">
                  <path d="M16 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/><circle cx="8.5" cy="7" r="4"/><line x1="20" y1="8" x2="20" y2="14"/><line x1="23" y1="11" x2="17" y2="11"/>
              </svg>
              <h3 class="text-xl font-bold text-white mb-2">Aún no tienes amigos</h3>
              <p class="text-[#9CA3AF] mb-4 text-sm max-w-sm">Como aún no tienes amigos, ¡agrégalos y empieza a compartir tu progreso para motivarse juntos!</p>
              <button @click="showAddFriendModal = true" class="bg-[#DC2626] hover:bg-[#B91C1C] rounded-lg px-6 py-2.5 font-bold transition-all hover:scale-105 active:scale-95 text-white">Añadir Amigo</button>
          </div>
        </section>

        <!-- TAB 2: RUTINAS / COMUNIDAD -->
        <section v-if="activeTab === 'routines'" class="animate-in fade-in duration-300">
          <div class="flex flex-col gap-4 mb-6 px-6">
            <h2 class="text-[20px] font-semibold text-white uppercase tracking-tighter">Comunidad</h2>
            <div class="flex gap-3">
              <div class="flex-1 relative">
                <svg class="absolute left-3 top-1/2 -translate-y-1/2 text-[#9CA3AF]" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><circle cx="11" cy="11" r="8"/><line x1="21" y1="21" x2="16.65" y2="16.65"/></svg>
                <input 
                  v-model="searchQuery"
                  type="text" 
                  placeholder="Buscar rutinas..." 
                  class="w-full bg-[#1F2937]/50 border border-[#374151] rounded-[8px] py-2.5 pl-10 pr-4 text-[14px] text-white focus:outline-none focus:border-[#DC2626]" 
                />
              </div>
              <select v-model="filterDifficulty" class="bg-[#1F2937]/50 border border-[#374151] rounded-[8px] px-4 py-2 text-[14px] text-white focus:outline-none focus:border-[#DC2626] min-w-[140px]">
                <option value="all">Todas</option>
                <option value="Principiante">Principiante</option>
                <option value="Intermedio">Intermedio</option>
                <option value="Avanzado">Avanzado</option>
              </select>
            </div>
          </div>

          <div class="px-6 grid grid-cols-1 gap-4">
            <div v-for="routine in filteredRoutines" :key="routine.id" 
                 class="bg-[#111827]/50 border border-[#1F2937] rounded-[14px] p-5 flex flex-col md:flex-row md:items-center justify-between gap-4 transition-all duration-300 hover:border-[#DC2626] group">
              
              <!-- INFO IZQUIERDA -->
              <div class="flex flex-col gap-2">
                <div class="flex flex-wrap items-center gap-3">
                  <h4 class="text-[18px] font-bold text-white">{{ routine.title }}</h4>
                  <div class="flex gap-1.5">
                    <span v-for="tag in routine.tags" :key="tag" class="bg-[#DC2626]/10 border border-[#DC2626]/20 px-2 py-0.5 rounded-[4px] text-[10px] text-[#FCA5A5] uppercase font-bold tracking-wider">
                      {{ tag }}
                    </span>
                  </div>
                </div>
                
                <div class="flex flex-wrap items-baseline gap-4 text-[13px] text-[#9CA3AF]">
                  <div class="flex items-center gap-1.5">
                    <svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5"><circle cx="12" cy="7" r="4"/><path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/></svg>
                    Por {{ routine.author }}
                  </div>
                  <span class="hidden md:inline text-[#374151]">|</span>
                  <div class="flex items-center gap-1.5">
                    <div class="flex items-center gap-1 text-[#DC2626]">
                      <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" class="drop-shadow-[0_0_5px_#DC2626]">
                        <circle cx="12" cy="12" r="9" />
                        <circle cx="12" cy="12" r="2.5" fill="currentColor" stroke="none" />
                      </svg>
                      <span class="text-white font-bold">{{ routine.difficulty }}</span>
                    </div>
                  </div>
                  <span class="hidden md:inline text-[#1F2937]">|</span>
                  <div class="flex items-center gap-1.5">
                    <button @click="likeRoutine(routine)" class="flex items-center gap-1 transition-all hover:scale-110" :class="routine.liked ? 'text-red-500' : 'text-[#9CA3AF] hover:text-red-500'">
                      <svg width="14" height="14" viewBox="0 0 24 24" :fill="routine.liked ? 'currentColor' : 'none'" stroke="currentColor" stroke-width="2.5" class="drop-shadow-[0_0_4px_currentColor]"><path d="M12 21.35l-1.45-1.32C5.4 15.36 2 12.28 2 8.5 2 5.42 4.42 3 7.5 3c1.74 0 3.41.81 4.5 2.09C13.09 3.81 14.76 3 16.5 3 19.58 3 22 5.42 22 8.5c0 3.78-3.4 6.86-8.55 11.54L12 21.35z"/></svg>
                      <span class="text-[15px] font-bold" :class="routine.liked ? 'text-white' : ''">{{ routine.likes || 0 }}</span>
                    </button>
                  </div>
                </div>
                
                <p class="text-[13px] text-[#6B7280] line-clamp-1 mt-1">{{ routine.description }}</p>
              </div>

              <!-- BOTONES DERECHA -->
              <div class="flex items-center gap-3">
                <button class="flex items-center justify-center gap-2 bg-[#1F2937]/50 hover:bg-[#374151] border border-[#374151] text-white px-5 py-2.5 rounded-[10px] font-bold text-[14px] transition-all">
                  <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"/><circle cx="12" cy="12" r="3"/></svg>
                  Ver
                </button>
                <button 
                  @click="copyRoutine(routine)"
                  class="flex items-center justify-center gap-2 px-5 py-2.5 rounded-[10px] font-bold text-[14px] transition-all min-w-[120px]"
                  :class="routine.copied ? 'bg-[#22C55E] text-white' : 'bg-[#DC2626] hover:bg-[#B91C1C] text-white'"
                >
                  <svg v-if="!routine.copied" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><rect x="9" y="9" width="13" height="13" rx="2" ry="2"/><path d="M5 15H4a2 2 0 01-2-2V4a2 2 0 012-2h9a2 2 0 012 2v1"/></svg>
                  <svg v-else width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3"><polyline points="20 6 9 17 4 12"/></svg>
                  {{ routine.copied ? 'Guardado' : 'Guardar' }}
                </button>
              </div>

            </div>
          </div>
        </section>

        <!-- TAB 3: LOGROS -->
        <section v-if="activeTab === 'achievements'" class="animate-in fade-in duration-300 pt-4">

           <!-- DESBLOQUEADOS -->
           <h3 class="px-6 text-[16px] font-semibold text-white mb-4">Desbloqueados</h3>
           
           <div v-if="achievementsUnlocked.length > 0" class="px-6 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4 mb-8">
             <div v-for="item in achievementsUnlocked" :key="item.title" 
                  class="bg-[#111827]/50 border border-opacity-50 rounded-[12px] p-5 relative overflow-hidden group"
                  :style="{ borderColor: item.color }">
               <div class="absolute -top-1/2 -right-1/2 w-full h-full pointer-events-none" 
                    :style="{ background: `radial-gradient(circle, ${item.color}26 0%, transparent 70%)` }"></div>
               <div class="w-16 h-16 rounded-full flex items-center justify-center text-white mb-3 relative z-10" :style="{ backgroundColor: item.color }" v-html="item.icon">
               </div>
               <h4 class="text-[16px] font-semibold text-white mb-1.5 relative z-10">{{ item.title }}</h4>
               <p class="text-[13px] text-[#9CA3AF] mb-3 relative z-10">{{ item.desc }}</p>
               <div class="flex items-center gap-1 text-[13px] font-semibold text-[#FBBF24] relative z-10">
                 <svg width="12" height="12" viewBox="0 0 24 24" fill="currentColor"><path d="M6 9H4.5A2.5 2.5 0 0 1 2 6.5V6A2.5 2.5 0 0 1 4.5 3.5H6M18 9h1.5A2.5 2.5 0 0 0 22 6.5V6A2.5 2.5 0 0 0 19.5 3.5H18M4 22h16M10 14.66V17c0 .55-.47.98-.97 1.21C7.85 18.75 7 20.24 7 22M14 14.66V17c0 .55.47.98.97 1.21C16.15 18.75 17 20.24 17 22M18 2H6v7a6 6 0 0 0 12 0V2Z"/></svg>
                 +{{ item.pts }} pts
               </div>
             </div>
           </div>

           <!-- Empty State for Achievements -->
           <div v-else class="mx-6 mb-8 py-10 flex flex-col items-center justify-center text-center bg-[#111827]/30 border border-dashed border-[#DC2626]/30 rounded-2xl">
              <div class="w-20 h-20 rounded-full bg-[#DC2626]/10 flex items-center justify-center text-[#DC2626] mb-6 shadow-[0_0_20px_rgba(220,38,38,0.2)]">
                  <svg width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round">
                      <circle cx="12" cy="12" r="10"/><path d="M8 12h8"/><path d="M12 8v8"/>
                  </svg>
              </div>
              <h4 class="text-xl font-bold text-white mb-2">¡Tu estantería de trofeos está vacía!</h4>
              <p class="text-[#9CA3AF] text-sm max-w-md px-6 mb-6">
                 "La gloria se construye con cada repetición." Empieza a entrenar ahora y desbloquea tus primeros logros para mostrarle al mundo de qué estás hecho.
              </p>
              <button @click="activeTab = 'routines'" class="bg-[#DC2626] hover:bg-[#B91C1C] text-white px-6 py-2.5 rounded-lg font-bold transition-all hover:scale-105 shadow-lg shadow-red-900/30">
                 Ver Rutinas para Empezar
              </button>
           </div>

           <!-- POR DESBLOQUEAR -->
           <h3 class="px-6 text-[16px] font-semibold text-white mb-4">Por Desbloquear</h3>
           <div class="px-6 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
             <div v-for="item in achievementsLocked" :key="item.title" 
                  class="bg-[#111827]/30 border border-[#374151] rounded-[12px] p-5 relative opacity-70">
               <svg class="absolute top-3 right-3 text-[#6B7280]" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><rect x="3" y="11" width="18" height="11" rx="2" ry="2"/><path d="M7 11V7a5 5 0 0 1 10 0v4"/></svg>
               <div class="w-16 h-16 rounded-full bg-[#4B5563] flex items-center justify-center text-white mb-3" v-html="item.icon">
               </div>
               <h4 class="text-[16px] font-semibold text-white mb-1.5">{{ item.title }}</h4>
               <p class="text-[13px] text-[#9CA3AF] mb-3">{{ item.desc }}</p>
               <div class="mt-3">
                 <div class="flex justify-between items-center mb-1.5 text-[12px] text-[#9CA3AF]">
                   <span>Progreso</span>
                   <span>{{ item.progress }}%</span>
                 </div>
                 <div class="w-full h-1.5 bg-[#1F2937] rounded-full overflow-hidden">
                   <div class="h-full bg-gradient-to-r from-[#DC2626] to-[#EF4444] rounded-full" :style="{ width: item.progress + '%' }"></div>
                 </div>
               </div>
               <div class="mt-3 text-[13px] font-semibold text-[#6B7280]">
                 +{{ item.pts }} pts
               </div>
             </div>
           </div>
        </section>

        <!-- TAB 4: RANKING -->
        <section v-if="activeTab === 'ranking'" class="animate-in fade-in duration-300 pt-4">

           <!-- TOP 3 PODIO -->
           <div class="flex justify-center items-end gap-3 sm:gap-4 mb-10 px-6">
             <!-- 2ndo Puesto -->
             <div class="flex flex-col items-center w-1/3 max-w-[150px]">
               <div class="relative">
                 <div class="w-16 h-16 rounded-full border-3 border-[#D1D5DB] bg-[#1F2937] flex items-center justify-center overflow-hidden">
                    <img v-if="rankingTop3[1]?.avatarId" :src="getAvatarUrl(rankingTop3[1].avatarId)" class="w-full h-full object-cover" />
                 </div>
                 <div class="absolute -bottom-2 translate-x-1/2 right-1/2 bg-[#D1D5DB] text-black text-[14px] font-bold w-6 h-6 rounded-full flex items-center justify-center border-2 border-black">2</div>
               </div>
               <div class="text-[13px] font-semibold text-white mt-4 text-center truncate w-full">{{ rankingTop3[1].name }}</div>
               <div class="text-[12px] font-bold text-[#D1D5DB]">{{ rankingTop3[1].pts }} pts</div>
             </div>

             <!-- 1er Puesto -->
             <div class="flex flex-col items-center w-1/3 max-w-[150px] -translate-y-4">
               <div class="relative">
                 <div class="w-20 h-20 rounded-full border-3 border-[#FBBF24] bg-[#1F2937] flex items-center justify-center overflow-hidden shadow-[0_0_20px_rgba(251,191,36,0.4)]">
                    <img v-if="rankingTop3[0]?.avatarId" :src="getAvatarUrl(rankingTop3[0].avatarId)" class="w-full h-full object-cover" />
                 </div>
                 <div class="absolute -bottom-2 translate-x-1/2 right-1/2 bg-[#FBBF24] text-black text-[18px] font-bold w-8 h-8 rounded-full flex items-center justify-center border-2 border-black">1</div>
               </div>
               <div class="text-[14px] font-bold text-white mt-4 text-center truncate w-full">{{ rankingTop3[0].name }}</div>
               <div class="text-[13px] font-black text-[#FBBF24]">{{ rankingTop3[0].pts }} pts</div>
             </div>

             <!-- 3er Puesto -->
             <div class="flex flex-col items-center w-1/3 max-w-[150px]">
               <div class="relative">
                 <div class="w-16 h-16 rounded-full border-3 border-[#F97316] bg-[#1F2937] flex items-center justify-center overflow-hidden">
                    <img v-if="rankingTop3[2]?.avatarId" :src="getAvatarUrl(rankingTop3[2].avatarId)" class="w-full h-full object-cover" />
                 </div>
                 <div class="absolute -bottom-2 translate-x-1/2 right-1/2 bg-[#F97316] text-black text-[14px] font-bold w-6 h-6 rounded-full flex items-center justify-center border-2 border-black">3</div>
               </div>
               <div class="text-[13px] font-semibold text-white mt-4 text-center truncate w-full">{{ rankingTop3[2].name }}</div>
               <div class="text-[12px] font-bold text-[#F97316]">{{ rankingTop3[2].pts }} pts</div>
             </div>
           </div>

           <!-- LISTA RANKING -->
           <div class="px-6 space-y-3">
             <div v-for="item in rankingList" :key="item.rank" 
                  class="bg-[#111827]/50 border border-[#1F2937] rounded-[12px] p-4 flex items-center justify-between hover:border-[#DC2626] transition-colors group">
               <div class="flex items-center gap-4">
                 <div class="w-8 text-center text-[18px] font-bold text-[#9CA3AF]">{{ item.rank }}</div>
                 <div class="w-12 h-12 rounded-full bg-[#1F2937] flex items-center justify-center overflow-hidden border border-[#374151]">
                    <img v-if="item.avatarId" :src="getAvatarUrl(item.avatarId)" class="w-full h-full object-cover" />
                 </div>
                 <div>
                   <div class="text-[15px] font-semibold text-white">{{ item.name }}</div>
                   <div class="text-[13px] text-[#9CA3AF]">{{ item.pts.toLocaleString() }} pts • {{ item.workouts }} entrenamientos</div>
                 </div>
               </div>
               <div class="flex items-center gap-2">
                 <div class="bg-opacity-15 px-2 py-1 rounded-[6px] text-[12px] font-bold flex items-center gap-1"
                      :class="item.trendClass">
                   <svg v-if="item.trend === 'up'" width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3"><polyline points="23 6 13.5 15.5 8.5 10.5 1 18"/><polyline points="17 6 23 6 23 12"/></svg>
                   <svg v-else-if="item.trend === 'down'" width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3"><polyline points="23 18 13.5 8.5 8.5 13.5 1 6"/><polyline points="17 18 23 18 23 12"/></svg>
                   <svg v-else width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3"><line x1="5" y1="12" x2="19" y2="12"/></svg>
                   {{ item.trendText }}
                 </div>
               </div>
             </div>
           </div>
        </section>
      </main>
    </div>

    <!-- 8️⃣ MODAL: AÑADIR AMIGO -->
    <div v-if="showAddFriendModal" class="fixed inset-0 z-50 flex items-center justify-center p-4">
        <!-- Overlay -->
        <div @click="showAddFriendModal = false" class="absolute inset-0 bg-black/80 backdrop-blur-sm"></div>
        
        <!-- Modal Content -->
        <div class="relative bg-[#111827] border border-[#1F2937] rounded-[16px] w-full max-w-[500px] overflow-hidden shadow-2xl animate-in zoom-in-95 duration-200">
            <header class="p-6 border-b border-[#1F2937] flex justify-between items-center">
                <h2 class="text-[20px] font-bold text-white">Añadir Amigo</h2>
                <button @click="showAddFriendModal = false" class="text-[#9CA3AF] hover:text-white transition-colors">
                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
                </button>
            </header>
            
            <div class="p-6">
                <label class="block text-[14px] font-semibold text-white mb-2">Código de Amigo</label>
                <div class="flex gap-2 mb-6">
                    <input 
                      v-model="friendCode" 
                      type="text" 
                      placeholder="Ej: REPS#CM2024" 
                      class="flex-1 bg-[#1F2937]/50 border border-[#374151] rounded-[8px] p-3 text-[14px] text-white font-mono focus:outline-none focus:border-[#DC2626]" 
                    />
                    <button 
                      @click="searchFriend" 
                      :disabled="isSearching"
                      class="bg-[#DC2626] hover:bg-[#B91C1C] disabled:opacity-50 text-white px-6 py-3 rounded-[8px] font-semibold text-[14px] transition-colors flex items-center gap-2"
                    >
                        <span v-if="isSearching" class="w-4 h-4 border-2 border-white/30 border-t-white rounded-full animate-spin"></span>
                        {{ isSearching ? 'Buscando' : 'Buscar' }}
                    </button>
                </div>

                <!-- Resultado búsqueda -->
                <div v-if="searchResult" class="bg-[#1F2937]/50 border border-[#374151] rounded-[12px] p-5 flex gap-4 animate-in fade-in slide-in-from-top-2">
                    <div class="w-16 h-16 rounded-full bg-[#1F2937] border border-[#374151] flex items-center justify-center overflow-hidden">
                        <img :src="getAvatarUrl(searchResult.avatarId)" class="w-full h-full object-cover" />
                    </div>
                    <div>
                        <div class="text-[18px] font-bold text-white">{{ searchResult.name }}</div>
                        <div class="text-[13px] text-[#9CA3AF] mt-1">Nv.{{ searchResult.level }} • {{ searchResult.levelName }} • {{ searchResult.memberSince }}</div>
                    </div>
                </div>

                <!-- Error -->
                <div v-if="searchError" class="bg-[#DC2626]/15 border border-[#DC2626]/30 rounded-[8px] p-3 text-[#FCA5A5] text-[13px] animate-in shake duration-300">
                    No se encontró ningún usuario con ese código
                </div>
            </div>

            <footer class="p-6 border-t border-[#1F2937] flex justify-end gap-3">
                <button @click="showAddFriendModal = false" class="px-5 py-2.5 border border-[#374151] hover:border-[#DC2626] text-white rounded-[8px] font-semibold text-[14px] transition-colors">Cancelar</button>
                <button 
                  @click="sendRequest" 
                  :disabled="!searchResult || isSending"
                  class="px-5 py-2.5 bg-[#DC2626] hover:bg-[#B91C1C] disabled:opacity-50 text-white rounded-[8px] font-semibold text-[14px] transition-all flex items-center gap-2"
                  :class="{ 'bg-[#22C55E] hover:bg-[#22C55E]': requestSent }"
                >
                    <span v-if="isSending" class="w-4 h-4 border-2 border-white/30 border-t-white rounded-full animate-spin"></span>
                    <svg v-if="requestSent" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3"><polyline points="20 6 9 17 4 12"/></svg>
                    {{ isSending ? 'Enviando...' : (requestSent ? '¡Enviado!' : 'Enviar Solicitud') }}
                </button>
            </footer>
        </div>
    </div>

    <!-- 9️⃣ MODAL: VER PERFIL DE AMIGO -->
    <div v-if="selectedFriend" class="fixed inset-0 z-50 flex items-center justify-center p-4">
        <!-- Overlay -->
        <div @click="selectedFriend = null" class="absolute inset-0 bg-black/80 backdrop-blur-sm"></div>
        
        <!-- Modal Content -->
        <div class="relative bg-gradient-to-b from-[#1F2937] to-[#111827] border border-[#374151] rounded-[16px] w-full max-w-[400px] overflow-hidden shadow-2xl animate-in zoom-in-95 duration-200">
            <header class="p-6 border-b border-[#374151]/50 flex justify-between items-start relative z-10">
                <div class="w-20 h-20 rounded-full border-4 border-[#111827] bg-[#1F2937] flex items-center justify-center overflow-hidden shadow-xl">
                    <img :src="getAvatarUrl(selectedFriend.avatarId)" class="w-full h-full object-cover" />
                </div>
                <button @click="selectedFriend = null" class="text-[#9CA3AF] hover:text-white transition-colors bg-black/20 rounded-full p-1">
                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
                </button>
            </header>
            
            <div class="p-6 pt-4 text-center">
                <h2 class="text-[24px] font-bold text-white flex justify-center items-center gap-2">
                    {{ selectedFriend.name }}
                    <span class="w-3 h-3 rounded-full" :class="selectedFriend.online ? 'bg-[#22C55E]' : 'bg-[#6B7280]'"></span>
                </h2>
                <div class="text-[14px] text-[#DC2626] font-bold uppercase tracking-widest mt-1 mb-4">{{ selectedFriend.statusText }}</div>
                
                <p v-if="selectedFriend.bio" class="text-[14px] text-[#9CA3AF] italic mb-6">"{{ selectedFriend.bio }}"</p>
                
                <div class="grid grid-cols-2 gap-4">
                    <div class="bg-[#111827]/50 border border-[#374151] rounded-[12px] p-4 flex flex-col items-center">
                        <span class="text-[24px] font-extrabold text-[#FBBF24]">{{ selectedFriend.streak }}</span>
                        <span class="text-[12px] text-[#9CA3AF] uppercase font-bold tracking-wider">Días Racha</span>
                    </div>
                    <div class="bg-[#111827]/50 border border-[#374151] rounded-[12px] p-4 flex flex-col items-center">
                        <span class="text-[24px] font-extrabold text-[#38BDF8]">{{ selectedFriend.workouts }}</span>
                        <span class="text-[12px] text-[#9CA3AF] uppercase font-bold tracking-wider">Entrenos</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useRouter } from 'vue-router';
import Sidebar from './Sidebar.vue';
import { usuariosApi, rankingApi, logrosApi, rutinasApi } from '../api';
import { useAuthStore } from '../stores/auth';
import { getErrorMessage } from '../utils/error-handler';

const authStore = useAuthStore();
const router = useRouter();

onMounted(async () => {
  document.title = 'Comunidad | REPS - Conecta y Compite';
  if (!authStore.profile) {
    await authStore.fetchProfile();
  }
  await loadAmigos();
  await loadSolicitudes();
  await loadRanking();
  await loadRutinasComunidad();
  if (authStore.userId) {
     await loadLogros(authStore.userId);
  }
});

const activeTab = ref('friends');
const searchQuery = ref('');
const filterDifficulty = ref('all');
const pendingRequests = ref<any[]>([]);
const mainTabs = [
  { id: 'friends', label: 'Amigos' },
  { id: 'routines', label: 'Comunidad' },
  { id: 'achievements', label: 'Logros' },
  { id: 'ranking', label: 'Ranking' }
];

const filteredRoutines = computed(() => {
  return sharedRoutines.value.filter(r => {
    const matchesSearch = r.title.toLowerCase().includes(searchQuery.value.toLowerCase()) || 
                         r.author.toLowerCase().includes(searchQuery.value.toLowerCase());
    const matchesDifficulty = filterDifficulty.value === 'all' || r.difficulty === filterDifficulty.value;
    return matchesSearch && matchesDifficulty;
  });
});

// SVG Icons as strings
const icons = {
  muscle: `<svg width="24" height="24" viewBox="0 0 24 24" fill="currentColor"><path d="M22 12c0-1.1-.9-2-2-2h-3V5c0-1.1-.9-2-2-2h-6c-1.1 0-2 .9-2 2v5H4c-1.1 0-2 .9-2 2s.9 2 2 2h3v5c0 1.1.9 2 2 2h6c1.1 0 2-.9 2-2v-5h3c1.1 0 2-.9 2-2zM9 5h6v14H9V5z"/></svg>`,
  flame: `<svg width="24" height="24" viewBox="0 0 24 24" fill="currentColor"><path d="M12 2C8.13 2 5 5.13 5 9c0 5.25 7 13 7 13s7-7.75 7-13c0-3.87-3.13-7-7-7zm0 10.5c-1.93 0-3.5-1.57-3.5-3.5S10.07 5.5 12 5.5 15.5 7.07 15.5 9s-1.57 3.5-3.5 3.5z" opacity=".4"/><path d="M12 2c-4 0-4 4-4 4s0 2.5 1.5 4.5c.5.7 1.5.5 1.5-1.5 0-1 1-1 1-1s1 0 1 1c0 2-1 2.5-1 4.5 0 2 2 4 4 4s4-4 4-8c0-4-7-7-7-7z"/></svg>`,
  rocket: `<svg width="24" height="24" viewBox="0 0 24 24" fill="currentColor"><path d="M13.13 22.19L11.5 18.35L9.87 22.19L11.5 20.5L13.13 22.19ZM16.32 15L15 13.68V8.25C15 6.45 13.65 5 12 5S9 6.45 9 8.25V13.68L7.68 15H16.32ZM12 2C10.34 2 9 3.34 9 5V6H13V5C13 3.34 11.66 2 10 2H12ZM19 14.5C19 12.01 16.99 10 14.5 10H14V14.5C14 17.54 16.46 20 19.5 20H20C20 16.96 17.54 14.5 14.5 14.5ZM5 14.5C5 16.99 7.01 19 9.5 19H10V14.5C10 11.46 7.54 9 4.5 9H4C4 12.04 6.46 14.5 9.5 14.5Z"/></svg>`,
  trophy: `<svg width="24" height="24" viewBox="0 0 24 24" fill="currentColor"><path d="M19 5h-2V3H7v2H5c-1.1 0-2 .9-2 2v1c0 2.55 1.92 4.63 4.39 4.94A5.01 5.01 0 0 0 11 15.9V19H7v2h10v-2h-4v-3.1c2.45-.4 4.41-2.4 4.89-4.82C20.27 10.79 22 8.6 22 6V5c0-1.1-.9-2-2-2zM5 8V7h2v3.82C5.84 10.4 5 9.3 5 8zm14 0c0 1.3-.84 2.4-2 2.82V7h2v1z"/></svg>`,
  crown: `<svg width="24" height="24" viewBox="0 0 24 24" fill="currentColor"><path d="M5 16L3 5L8.5 10L12 4L15.5 10L21 5L19 16H5ZM19 19c0 .55-.45 1-1 1H6c-.55 0-1-.45-1-1v-1h14v1z"/></svg>`,
  target: `<svg width="24" height="24" viewBox="0 0 24 24" fill="currentColor"><circle cx="12" cy="12" r="10" opacity=".3"/><circle cx="12" cy="12" r="7" opacity=".3"/><circle cx="12" cy="12" r="4"/><circle cx="12" cy="12" r="1.5"/></svg>`,
  bolt: `<svg width="24" height="24" viewBox="0 0 24 24" fill="currentColor"><path d="M11 21h-1l1-7H7.5c-.58 0-1.05-.47-1.05-1.05 0-.21.06-.42.18-.59L12.3 3h1.1l-1 7h3.5c.58 0 1.05.47 1.05 1.05 0 .21-.06.42-.18.59L11.7 21z"/></svg>`,
  star: `<svg width="24" height="24" viewBox="0 0 24 24" fill="currentColor"><path d="M12 17.27L18.18 21l-1.64-7.03L22 9.24l-7.19-.61L12 2 9.19 8.63 2 9.24l5.46 4.73L5.82 21z"/></svg>`,
};

// --- COMPONENTES GLOBALES Y HELPERS ---

const availableAvatars = [
  { id: 'avatar_default', url: 'https://api.dicebear.com/7.x/avataaars/svg?seed=Jack' },
  { id: 'avatar_robot', url: 'https://api.dicebear.com/7.x/bottts/svg?seed=robot' },
  { id: 'avatar_gymbro', url: 'https://api.dicebear.com/7.x/avataaars/svg?seed=Felix&accessoriesProbability=100' },
  { id: 'avatar_mujerfit', url: 'https://api.dicebear.com/7.x/avataaars/svg?seed=Aneka&top=longHair' },
  { id: 'avatar_hombrefit', url: 'https://api.dicebear.com/7.x/avataaars/svg?seed=George' }
];

const getAvatarUrl = (id?: string) => {
  const avatar = availableAvatars.find(a => a.id === id);
  return avatar ? avatar.url : availableAvatars[0].url;
};

// --- AMIGOS (REALES) ---
const friendsList = ref<any[]>([]);
const selectedFriend = ref<any>(null);

const viewFriendProfile = (friend: any) => {
    selectedFriend.value = friend;
};

const loadAmigos = async () => {
  try {
    const res = await usuariosApi.getMisAmigos();
    friendsList.value = (res.data as any[]).map((f: any) => ({
      name: f.nombre ?? 'Atleta',
      avatarId: f.avatarId,
      online: false,
      statusText: `${f.rangoGeneral ?? 'Bronce'}`,
      workouts: 0,
      streak: f.rachaDias ?? 0,
      bio: ''
    }));
  } catch (e) {
    console.warn('No se pudieron cargar amigos', e);
  }
};

// --- SOLICITUDES PENDIENTES (REALES) ---
const loadSolicitudes = async () => {
  try {
    const res = await usuariosApi.getSolicitudes();
    pendingRequests.value = res.data ?? [];
  } catch (e) {
    console.warn('No se pudieron cargar solicitudes', e);
  }
};

const responderSolicitud = async (codigoAmigo: string, aceptar: boolean) => {
  try {
    await usuariosApi.responderSolicitud(codigoAmigo, aceptar);
    await loadSolicitudes();
    await loadAmigos();
  } catch (e) {
    console.error('Error respondiendo solicitud', e);
  }
};

// --- RANKING (REAL) ---
const rankingTop3 = ref<any[]>([]);
const rankingList = ref<any[]>([]);

const loadRanking = async () => {
  try {
    const res = await rankingApi.getLeaderboard();
    const data = Array.isArray(res.data) ? res.data : [];
    const top3 = data.slice(0, 3);
    while (top3.length < 3) {
      top3.push({ nombre: '—', puntosTotales: 0 });
    }
    // Top 3 primeros
    rankingTop3.value = top3.map((item: any) => ({
      name: item.nombre,
      avatarId: item.avatarId,
      pts: item.puntosTotales ?? 0,
    }));
    // Resto del ranking
    rankingList.value = data.slice(3).map((item: any) => ({
      rank: item.posicion,
      name: item.nombre,
      avatarId: item.avatarId,
      pts: item.puntosTotales,
      workouts: item.entrenamientosTotal,
      trend: item.cambioPosicion > 0 ? 'up' : item.cambioPosicion < 0 ? 'down' : 'same',
      trendText: item.cambioPosicion > 0 ? `+${item.cambioPosicion}` : item.cambioPosicion < 0 ? `${item.cambioPosicion}` : '—',
      trendClass: item.cambioPosicion > 0 ? 'bg-[#22C55E]/15 text-[#22C55E]' : item.cambioPosicion < 0 ? 'bg-[#DC2626]/15 text-[#DC2626]' : 'bg-gray-500/15 text-gray-400'
    }));
  } catch (e) {
    console.warn('No se pudo cargar ranking', e);
    // Fallback datos de muestra
    rankingTop3.value = [
      { name: 'Ana García', avatarId: 'avatar_mujerfit', pts: 15420 },
      { name: 'Carlos Martínez', avatarId: 'avatar_gymbro', pts: 12890 },
      { name: 'María López', avatarId: 'avatar_default', pts: 11240 }
    ];
    rankingList.value = [];
  }
};

// Rutinas reales
const sharedRoutines = ref<any[]>([]);

const loadRutinasComunidad = async () => {
    try {
        const [comRes, misRes] = await Promise.all([
          rutinasApi.getComunidad(),
          rutinasApi.getMisRutinas()
        ]);
        
        const myRoutinesNames = new Set((misRes.data as any[]).map(r => r.nombre.toLowerCase()));
        const myUserId = authStore.userId;

        sharedRoutines.value = (comRes.data as any[])
          .filter((r: any) => {
            const isMine = Number(r.creadorId) === Number(myUserId);
            const isAlreadySaved = myRoutinesNames.has(r.nombre.toLowerCase());
            return !isMine && !isAlreadySaved;
          })
          .map((r: any) => ({
            id: r.id,
            title: r.nombre,
            author: r.creadorNombre,
            difficulty: r.nivel || 'Intermedio',
            description: r.descripcion || `Rutina de ${r.cantidadEjercicios} ejercicios.`,
            tags: r.musculosPrincipales?.length ? r.musculosPrincipales : ['Cuerpo Completo'],
            likes: r.likes || 0,
            liked: r.liked || false,
            copied: false
        }));
    } catch(e) {
        console.warn("No se pudo cargar la comunidad", e);
    }
};

const achievementsUnlocked = ref<any[]>([]);
const achievementsLocked = ref<any[]>([]);

const loadLogros = async (userId: string | number) => {
    try {
        const res = await logrosApi.getMisLogros(Number(userId));
        if (res.data && res.data.length > 0) {
            const unlocked: any[] = [];
            const locked: any[] = [];
            
            res.data.forEach((l: any) => {
                const isUnlocked = l.desbloqueado;
                const dto = {
                    title: l.titulo || l.logroNombre,
                    desc: l.descripcion,
                    icon: '<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M6 9H4.5A2.5 2.5 0 0 1 2 6.5V6A2.5 2.5 0 0 1 4.5 3.5H6M18 9h1.5A2.5 2.5 0 0 0 22 6.5V6A2.5 2.5 0 0 0 19.5 3.5H18M4 22h16M10 14.66V17c0 .55-.47.98-.97 1.21C7.85 18.75 7 20.24 7 22M14 14.66V17c0 .55.47.98.97 1.21C16.15 18.75 17 20.24 17 22M18 2H6v7a6 6 0 0 0 12 0V2Z"/></svg>',
                    pts: l.puntos,
                    color: isUnlocked ? '#EA580C' : '#4B5563',
                    progress: l.progreso || 0
                };
                if (isUnlocked) {
                    unlocked.push(dto);
                } else {
                    locked.push(dto);
                }
            });
            
            achievementsUnlocked.value = unlocked;
            achievementsLocked.value = locked;
        }
    } catch (e) {
        console.warn('No se pudieron cargar logros reales', e);
        // Fallback
        achievementsUnlocked.value = [
          { title: 'Primera Semana', icon: icons.flame, pts: 50, color: '#EA580C', desc: 'Completa 7 días consecutivos de entrenamiento' },
          { title: 'Campeón de Constancia', icon: icons.crown, pts: 150, color: '#EAB308', desc: 'Alcanza una racha de 30 días' }
        ];
        achievementsLocked.value = [
          { title: 'Rey del Gimnasio', icon: icons.crown, pts: 1000, progress: 18, desc: 'Alcanza el nivel 50 en todos los grupos' }
        ];
    }
}

// ACTIONS
const isCopying = ref(false);
const copyRoutine = async (routine: any) => {
  if (!routine.id || isCopying.value) return;
  isCopying.value = true;
  try {
      await rutinasApi.copiar(routine.id);
      routine.copied = true;
      alert("¡Rutina guardada! Te redirigimos a tus rutinas de comunidad.");
      router.push({ path: '/workouts', query: { tab: 'community_saved' } });
  } catch (e) {
      console.error(e);
      alert("No se pudo guardar la rutina.");
  } finally {
      isCopying.value = false;
  }
};

const likeRoutine = async (routine: any) => {
    try {
        await rutinasApi.like(routine.id);
        routine.liked = !routine.liked;
        routine.likes += routine.liked ? 1 : -1;
    } catch (e) {
        console.error('Error al dar like:', e);
    }
};

// MODAL ADD FRIEND (REAL)
const showAddFriendModal = ref(false);
const friendCode = ref('');
const isSearching = ref(false);
const searchResult = ref<any>(null);
const searchError = ref(false);
const isSending = ref(false);
const requestSent = ref(false);

const searchFriend = async () => {
  if (!friendCode.value) return;
  isSearching.value = true;
  searchResult.value = null;
  searchError.value = false;
  try {
    const res = await usuariosApi.buscarAmigo(friendCode.value);
    const data = res.data;
    searchResult.value = {
      name: data.nombre,
      avatarId: data.avatarId,
      level: 0,
      levelName: data.rangoGeneral ?? 'Atleta',
      memberSince: data.fechaRegistro ? new Date(data.fechaRegistro).toLocaleDateString('es-ES', { month: 'long', year: 'numeric' }) : ''
    };
    } catch (e: any) {
    searchError.value = true;
    // Podríamos usar getErrorMessage(e) aquí si tuviéramos un lugar donde mostrarlo más allá de true/false
  } finally {
    isSearching.value = false;
  }
};

const sendRequest = async () => {
  if (!searchResult.value) return;
  isSending.value = true;
  try {
    await usuariosApi.agregarAmigo(friendCode.value);
    requestSent.value = true;
    setTimeout(() => {
      showAddFriendModal.value = false;
      requestSent.value = false;
      searchResult.value = null;
      friendCode.value = '';
    }, 1500);
  } catch (e: any) {
    console.error('Error sending friend request:', e);
    // Podríamos añadir una notificación o alerta con getErrorMessage(e)
    alert(getErrorMessage(e));
    searchError.value = true;
    searchResult.value = null;
  } finally {
    isSending.value = false;
  }
};
</script>

<style scoped>
.animate-in {
  animation: fadeIn 0.3s ease-out;
}

@keyframes fadeIn {
  from { opacity: 0; transform: translateY(10px); }
  to { opacity: 1; transform: translateY(0); }
}

@keyframes shake {
  0%, 100% { transform: translateX(0); }
  25% { transform: translateX(-5px); }
  75% { transform: translateX(5px); }
}

.shake {
  animation: shake 0.3s ease-in-out;
}

/* Scrollbar customization */
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
