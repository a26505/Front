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
            <h1 class="text-[30px] leading-[36px] font-normal text-white">¡Hola, Carlos!</h1>
            <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24" fill="#F97316" class="animate-[wave_2s_ease-in-out_infinite] origin-[70%_70%] drop-shadow-md">
              <path d="M12.92,10.51C12.35,10.22 11.83,9.85 11.39,9.41C10.74,8.76 10.37,7.92 10.37,7C10.37,6.83 10.38,6.66 10.42,6.5L8.91,6.86C8.61,6.93 8.35,7.11 8.16,7.36L6.96,9H4.5A2.5,2.5 0 0,0 2,11.5A2.5,2.5 0 0,0 4.5,14H6V17C6,18.11 6.9,19 8,19V22H11C11.39,22 11.75,21.89 12.04,21.69L14.71,19.92H17.5A2.5,2.5 0 0,0 20,17.42A2.5,2.5 0 0,0 17.5,14.92H15V13A2.5,2.5 0 0,0 12.92,10.51Z"/>
            </svg>
          </div>
          <p class="text-[14px] leading-[20px] text-[#9CA3AF]">Listo para superar tus límites hoy</p>
        </div>

        <button class="relative bg-gradient-to-r from-[#EAB308] to-[#A16207] rounded-[8px] h-[36px] px-[16px] flex items-center justify-center gap-[8px] cursor-pointer transition-all duration-300 hover:scale-105 active:scale-95 shadow-[0_0_15px_rgba(234,179,8,0.4)] border border-[#FEF08A] overflow-hidden group">
          <div class="absolute inset-0 bg-white/20 translate-y-full group-hover:translate-y-0 transition-transform duration-300 ease-out"></div>
          <svg width="16" height="16" viewBox="0 0 16 16" fill="none" class="relative z-10">
            <path d="M2 11.3333L3.33333 3.33333L6.66667 6.66667L8 1.33333L9.33333 6.66667L12.6667 3.33333L14 11.3333H2Z" stroke="white" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M2 11.3333H14V13.3333C14 13.7 13.7 14 13.3333 14H2.66667C2.3 14 2 13.7 2 13.3333V11.3333Z" stroke="white" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
          </svg>
          <span class="text-white text-[14px] font-bold tracking-wide relative z-10 drop-shadow-md whitespace-nowrap">Mejorar a Pro</span>
        </button>
      </header>

      <!-- 3️⃣ MAIN CONTENT -->
      <main class="p-[16px] md:p-[24px] bg-[#000000] min-h-[calc(100vh-89px)]">
        
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
            <div class="text-[30px] leading-[36px] text-[#FF8533] font-bold mb-[28px]">12 días</div>
            <div class="text-[14px] leading-[20px] text-[#9CA3AF]">Racha actual</div>
          </div>

          <!-- CARD 2: RANGO -->
          <div class="h-[206px] rounded-[14px] p-[25px] relative transition-all duration-300 hover:brightness-110 border border-[rgba(234,179,8,0.8)]" style="background: linear-gradient(152.983deg, rgba(234, 179, 8, 0.3) 0%, rgba(161, 98, 7, 0.3) 100%)">
            <div class="flex justify-between items-center mb-[40px]">
              <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="#EAB308" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <path d="M6 9H4.5a2.5 2.5 0 0 1 0-5H6" />
                <path d="M18 9h1.5a2.5 2.5 0 0 0 0-5H18" />
                <path d="M4 22h16" />
                <path d="M10 14.66V17c0 .55-.47.98-.97 1.21C7.85 18.75 7 20.24 7 22" />
                <path d="M14 14.66V17c0 .55.47.98.97 1.21C16.15 18.75 17 20.24 17 22" />
                <path d="M18 2H6v7a6 6 0 0 0 12 0V2Z" />
              </svg>
              <div class="bg-[#A16207] rounded-[8px] py-[3px] px-[9px] text-[12px] font-medium text-[#FEF08A] h-[22px] flex items-center">
                Actual
              </div>
            </div>
            <div class="text-[30px] leading-[36px] text-[#FDE047] font-bold mb-[28px]">Oro</div>
            <div class="text-[14px] leading-[20px] text-[#9CA3AF]">Rango actual</div>
          </div>

          <!-- CARD 3: LOGROS -->
          <div class="h-[206px] rounded-[14px] p-[25px] relative transition-all duration-300 hover:brightness-110 border border-[rgba(152,16,250,0.8)]" style="background: linear-gradient(152.983deg, rgba(152, 16, 250, 0.3) 0%, rgba(89, 22, 139, 0.3) 100%)">
             <div class="flex justify-between items-center mb-[40px]">
              <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="#A855F7" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <circle cx="12" cy="8" r="6"/>
                <path d="M15.477 12.89 17 22l-5-3-5 3 1.523-9.11"/>
              </svg>
              <div class="bg-[#9810FA] rounded-[8px] py-[3px] px-[9px] text-[12px] font-medium text-white h-[22px] flex items-center">
                +3 nuevos
              </div>
            </div>
            <div class="text-[30px] leading-[36px] text-[#D8B4FE] font-bold mb-[28px]">27</div>
            <div class="text-[14px] leading-[20px] text-[#9CA3AF]">Logros desbloqueados</div>
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
            
            <h2 class="text-[30px] leading-[36px] text-white font-bold mt-[12px]">Empuje Superior A</h2>
            
            <div class="flex flex-wrap gap-[16px] items-center mt-[12px]">
              <div class="flex items-center gap-1">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <path d="M14.4 14.4 9.6 9.6"/>
                  <path d="M18.657 21.485a2 2 0 1 1-2.829-2.828l-1.768 1.768a2 2 0 1 1-2.829-2.829l6.364-6.364a2 2 0 1 1 2.829 2.828l-1.768 1.768a2 2 0 1 1 2.828 2.829z"/>
                  <path d="m21.5 21.5-1.4-1.4"/>
                  <path d="M3.9 3.9 2.5 2.5"/>
                  <path d="M6.404 12.768a2 2 0 1 1-2.829-2.829l1.768-1.767a2 2 0 1 1-2.828-2.829l2.828-2.828a2 2 0 1 1 2.829 2.828l1.767-1.768a2 2 0 1 1 2.829 2.829z"/>
                </svg>
                <span class="text-[14px] text-white">4 ejercicios</span>
              </div>
              <div class="flex items-center gap-1">
                <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                  <circle cx="8" cy="8" r="6.66667" stroke="white" stroke-width="1.33" stroke-linecap="round" stroke-linejoin="round"/>
                  <path d="M8 4V8L10.6667 9.33333" stroke="white" stroke-width="1.33" stroke-linecap="round" stroke-linejoin="round"/>
                </svg>
                <span class="text-[14px] text-white">32 min</span>
              </div>
              <div class="bg-[#7F000B] rounded-[8px] py-[3px] px-[9px] text-[12px] font-medium text-[#FFB3B3]">
                Intermedio
              </div>
            </div>
          </div>

          <!-- BOTÓN COMENZAR -->
          <button class="w-full md:w-auto bg-[#7F000B] rounded-[10px] py-[12px] px-[24px] text-[16px] font-semibold text-[#FFB3B3] flex justify-center items-center gap-[8px] cursor-pointer hover:bg-[#99000B] transition-all duration-200 hover:scale-105 active:scale-95">
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
              <div class="w-[36px] h-[36px] sm:w-[44px] sm:h-[44px] rounded-full bg-[#E7000B] text-white flex items-center justify-center text-[14px] font-semibold">L</div>
              <div class="w-[36px] h-[36px] sm:w-[44px] sm:h-[44px] rounded-full bg-[#E7000B] text-white flex items-center justify-center text-[14px] font-semibold">M</div>
              <div class="w-[36px] h-[36px] sm:w-[44px] sm:h-[44px] rounded-full bg-[#E7000B] text-white flex items-center justify-center text-[14px] font-semibold">X</div>
              <div class="w-[36px] h-[36px] sm:w-[44px] sm:h-[44px] rounded-full bg-[#1F2937] text-[#6B7280] flex items-center justify-center text-[14px] font-semibold">J</div>
              <div class="w-[36px] h-[36px] sm:w-[44px] sm:h-[44px] rounded-full bg-[#1F2937] text-[#6B7280] flex items-center justify-center text-[14px] font-semibold">V</div>
              <div class="w-[36px] h-[36px] sm:w-[44px] sm:h-[44px] rounded-full bg-[#1F2937] text-[#6B7280] flex items-center justify-center text-[14px] font-semibold">S</div>
              <div class="w-[36px] h-[36px] sm:w-[44px] sm:h-[44px] rounded-full bg-[#1F2937] text-[#6B7280] flex items-center justify-center text-[14px] font-semibold">D</div>
            </div>

            <!-- BARRA DE PROGRESO -->
            <div class="mb-[16px]">
              <div class="flex justify-between mb-[8px]">
                <span class="text-[14px] text-[#9CA3AF]">Meta semanal</span>
                <span class="text-[14px] text-[#9CA3AF]">3/5 completados</span>
              </div>
              <div class="w-full h-[8px] bg-[#1F2937] rounded-[4px] overflow-hidden">
                <div class="h-full rounded-[4px] w-[60%] animate-[progress_1s_ease-out]" style="background: linear-gradient(90deg, #E7000B 0%, #F97316 100%)"></div>
              </div>
            </div>
          </div>

          <!-- CARD LOGROS RECIENTES -->
          <div class="bg-[rgba(16,24,40,0.5)] border border-[#1E2939] rounded-[16px] p-[28px]">
            <h3 class="text-[20px] font-semibold text-white mb-[24px]">Logros Recientes</h3>
            
            <div class="flex flex-col gap-[16px]">
              <!-- Logro 1 -->
              <div class="flex items-center justify-between p-[12px] bg-[rgba(0,0,0,0.3)] rounded-[12px] transition-all duration-300 hover:bg-[rgba(231,0,11,0.1)]">
                <div class="flex items-center gap-[12px]">
                  <div class="w-[40px] h-[40px] rounded-full bg-[rgba(249,115,22,0.2)] flex items-center justify-center">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="#F97316" class="w-6 h-6">
                      <path stroke-linecap="round" stroke-linejoin="round" d="M15.362 5.214A8.252 8.252 0 0 1 12 21 8.25 8.25 0 0 1 6.038 7.047 8.287 8.287 0 0 0 9 9.601a8.983 8.983 0 0 1 3.361-6.867 8.21 8.21 0 0 0 3 2.48Z" />
                      <path stroke-linecap="round" stroke-linejoin="round" d="M12 18a3.75 3.75 0 0 0 .495-7.468 5.99 5.99 0 0 0-1.925 3.547 5.975 5.975 0 0 1-2.133-1.001A3.75 3.75 0 0 0 12 18Z" />
                    </svg>
                  </div>
                  <div class="flex flex-col">
                    <span class="text-[16px] text-white font-medium">Racha de 7 días</span>
                    <span class="text-[12px] text-[#9CA3AF]">Hace 2 días</span>
                  </div>
                </div>
                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#FBBF24" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <circle cx="12" cy="8" r="6"/>
                  <path d="M15.477 12.89 17 22l-5-3-5 3 1.523-9.11"/>
                </svg>
              </div>

              <!-- Logro 2 -->
              <div class="flex items-center justify-between p-[12px] bg-[rgba(0,0,0,0.3)] rounded-[12px] transition-all duration-300 hover:bg-[rgba(231,0,11,0.1)]">
                 <div class="flex items-center gap-[12px]">
                  <div class="w-[40px] h-[40px] rounded-full bg-[rgba(234,179,8,0.2)] flex items-center justify-center">
                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none">
                      <path d="M12 2L15.09 8.26L22 9.27L17 14.14L18.18 21.02L12 17.77L5.82 21.02L7 14.14L2 9.27L8.91 8.26L12 2Z" stroke="#EAB308" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                    </svg>
                  </div>
                  <div class="flex flex-col">
                    <span class="text-[16px] text-white font-medium">50 entrenamientos</span>
                    <span class="text-[12px] text-[#9CA3AF]">Hace 2 días</span>
                  </div>
                </div>
                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#FBBF24" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <circle cx="12" cy="8" r="6"/>
                  <path d="M15.477 12.89 17 22l-5-3-5 3 1.523-9.11"/>
                </svg>
              </div>

              <!-- Logro 3 -->
              <div class="flex items-center justify-between p-[12px] bg-[rgba(0,0,0,0.3)] rounded-[12px] transition-all duration-300 hover:bg-[rgba(231,0,11,0.1)]">
                 <div class="flex items-center gap-[12px]">
                  <div class="w-[40px] h-[40px] rounded-full bg-[rgba(168,85,247,0.2)] flex items-center justify-center">
                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none">
                      <path d="M3 17L4.5 5L9 10L12 2L15 10L19.5 5L21 17H3Z" stroke="#A855F7" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                      <path d="M3 17H21V20C21 20.5523 20.5523 21 20 21H4C3.44772 21 3 20.5523 3 20V17Z" stroke="#A855F7" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                    </svg>
                  </div>
                  <div class="flex flex-col">
                    <span class="text-[16px] text-white font-medium">Nivel alcanzado: Pro</span>
                    <span class="text-[12px] text-[#9CA3AF]">Hace 2 días</span>
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
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#FBBF24" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
              <path d="M6 9H4.5a2.5 2.5 0 0 1 0-5H6" />
              <path d="M18 9h1.5a2.5 2.5 0 0 0 0-5H18" />
              <path d="M4 22h16" />
              <path d="M10 14.66V17c0 .55-.47.98-.97 1.21C7.85 18.75 7 20.24 7 22" />
              <path d="M14 14.66V17c0 .55.47.98.97 1.21C16.15 18.75 17 20.24 17 22" />
              <path d="M18 2H6v7a6 6 0 0 0 12 0V2Z" />
            </svg>
          </div>
          
          <div class="flex flex-col gap-[16px]">
            <!-- Sentadilla -->
            <div class="flex justify-between items-center bg-[#07090E] rounded-[12px] p-[20px] border border-[rgba(255,255,255,0.08)]">
              <div class="flex flex-col gap-[8px]">
                <span class="text-[16px] text-white font-semibold">Sentadilla</span>
                <span class="text-[13px] text-[#9CA3AF]">Hace 3 días</span>
              </div>
              <div class="flex flex-col items-end gap-[8px]">
                <span class="text-[16px] font-semibold text-[#EF4444]">140 kg</span>
                <span class="text-[13px] text-[#10B981]">↑ +10 kg</span>
              </div>
            </div>

            <!-- Press Banca -->
            <div class="flex justify-between items-center bg-[#07090E] rounded-[12px] p-[20px] border border-[rgba(255,255,255,0.08)]">
              <div class="flex flex-col gap-[8px]">
                <span class="text-[16px] text-white font-semibold">Press Banca</span>
                <span class="text-[13px] text-[#9CA3AF]">Hace 1 semana</span>
              </div>
              <div class="flex flex-col items-end gap-[8px]">
                <span class="text-[16px] font-semibold text-[#EF4444]">100 kg</span>
                <span class="text-[13px] text-[#10B981]">↑ +5 kg</span>
              </div>
            </div>

            <!-- Peso Muerto -->
            <div class="flex justify-between items-center bg-[#07090E] rounded-[12px] p-[20px] border border-[rgba(255,255,255,0.08)]">
              <div class="flex flex-col gap-[8px]">
                <span class="text-[16px] text-white font-semibold">Peso Muerto</span>
                <span class="text-[13px] text-[#9CA3AF]">Hace 2 semanas</span>
              </div>
              <div class="flex flex-col items-end gap-[8px]">
                <span class="text-[16px] font-semibold text-[#EF4444]">160 kg</span>
                <span class="text-[13px] text-[#10B981]">↑ +15 kg</span>
              </div>
            </div>

            <!-- Dominadas -->
            <div class="flex justify-between items-center bg-[#07090E] rounded-[12px] p-[20px] border border-[rgba(255,255,255,0.08)]">
              <div class="flex flex-col gap-[8px]">
                <span class="text-[16px] text-white font-semibold">Dominadas</span>
                <span class="text-[13px] text-[#9CA3AF]">Hace 5 días</span>
              </div>
              <div class="flex flex-col items-end gap-[8px]">
                <span class="text-[16px] font-semibold text-[#EF4444]">+20 kg</span>
                <span class="text-[13px] text-[#10B981]">↑ +5 kg</span>
              </div>
            </div>
          </div>
        </div>

      </main>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import Sidebar from './Sidebar.vue';
import { Chart, registerables } from 'chart.js';
// Dashboard logic would go here
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
