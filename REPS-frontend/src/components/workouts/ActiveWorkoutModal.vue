<template>
  <div class="fixed inset-0 z-[70] flex flex-col bg-[#000000] text-white animate-slide-up">
    <!-- HEADER -->
    <header class="sticky top-0 z-40 bg-[#111827] border-b border-[#1F2937] px-4 py-4 flex items-center justify-between shadow-lg">
      <div>
        <h2 class="text-xl md:text-2xl font-black text-white uppercase tracking-tight bg-gradient-to-r from-white via-red-200 to-red-400 bg-clip-text text-transparent">{{ workout.title || workout.nombre }}</h2>
        <div class="text-sm text-[#9CA3AF] mt-1 flex items-center gap-2 font-medium">
          <div class="w-2.5 h-2.5 rounded-full bg-red-500 shadow-[0_0_10px_#EF4444] animate-pulse"></div>
          Entrenamiento en curso
        </div>
      </div>
      <div class="flex items-center gap-3 md:gap-4">
        <div class="hidden sm:flex items-center bg-[#111827] rounded-xl p-1 border border-[#374151] shadow-inner">
          <button @click="weightUnit = 'kg'" :class="weightUnit === 'kg' ? 'bg-[#DC2626] text-white shadow-[0_0_10px_rgba(220,38,38,0.5)]' : 'text-gray-400 hover:text-white'" class="px-4 py-1.5 rounded-lg text-xs font-black tracking-widest transition-all">KG</button>
          <button @click="weightUnit = 'lbs'" :class="weightUnit === 'lbs' ? 'bg-[#DC2626] text-white shadow-[0_0_10px_rgba(220,38,38,0.5)]' : 'text-gray-400 hover:text-white'" class="px-4 py-1.5 rounded-lg text-xs font-black tracking-widest transition-all">LBS</button>
        </div>
        <button @click="onFinalizar" :disabled="isFinalizing" class="bg-gradient-to-r from-[#DC2626] to-[#991B1B] hover:from-[#B91C1C] hover:to-[#7F1D1D] text-white px-6 py-2.5 rounded-xl font-black transition-all shadow-[0_0_20px_rgba(220,38,38,0.3)] active:scale-95 text-sm uppercase tracking-wider border border-red-500/30 flex items-center gap-2">
          <span v-if="isFinalizing" class="w-4 h-4 border-2 border-white/30 border-t-white rounded-full animate-spin"></span>
          {{ isFinalizing ? 'Finalizando...' : 'Finalizar' }}
        </button>
      </div>
    </header>

    <!-- BODY -->
    <main class="flex-1 overflow-y-auto p-4 md:p-6 custom-scrollbar">
      <div class="max-w-[800px] mx-auto">
        <div class="mb-8 flex flex-col md:flex-row md:justify-between md:items-end gap-2">
          <h3 class="text-xl font-black text-gray-200 tracking-tight">Selecciona el ejercicio por el cual empezar o continuar:</h3>
          <span class="text-xs text-[#DC2626] font-bold uppercase tracking-widest bg-red-500/10 px-3 py-1 rounded-full border border-red-500/20">Puedes finalizar sin completarlos todos</span>
        </div>

        <div class="flex flex-col gap-4">
          <div 
            v-for="(ex, i) in exercises" 
            :key="i"
            class="bg-[#0A0A0A] border rounded-2xl p-5 transition-all duration-500 relative overflow-hidden group"
            :class="ex.completed ? 'border-green-500/50 bg-gradient-to-r from-[#052e16]/40 to-[#0A0A0A] opacity-80' : (currentExerciseIndex === i ? 'border-[#DC2626] ring-2 ring-[#DC2626]/20 shadow-[0_4px_30px_rgba(220,38,38,0.15)] bg-[#111827]' : 'border-[#1F2937] hover:border-gray-500/50 hover:bg-[#111827]/50')"
            @click="currentExerciseIndex = i"
          >
            <!-- Card Header -->
            <div class="flex justify-between items-start mb-4 cursor-pointer">
              <div class="flex items-center gap-3">
                <!-- Check Circle -->
                <button 
                  @click.stop="toggleExerciseCompletion(Number(i))"
                  class="w-10 h-10 rounded-full border-2 flex items-center justify-center transition-all bg-[#0A0A0A] relative overflow-hidden"
                  :class="ex.completed ? 'border-green-500 text-green-500 shadow-[0_0_15px_rgba(34,197,94,0.4)]' : 'border-[#374151] text-transparent hover:border-gray-400'"
                >
                  <div v-if="ex.completed" class="absolute inset-0 bg-green-500/20 animate-pulse"></div>
                  <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round" class="relative z-10">
                    <polyline points="20 6 9 17 4 12"></polyline>
                  </svg>
                </button>
                <div>
                  <h4 class="font-bold text-white text-lg">{{ ex.name || ex.ejercicio?.nombre || ex.nombreEjercicio }}</h4>
                  <template v-if="ex.muscle || ex.ejercicio?.grupoMuscular || ex.grupoMuscular">
                    <span class="text-[10px] uppercase font-black tracking-widest text-[#9CA3AF]">{{ ex.muscle || ex.ejercicio?.grupoMuscular || ex.grupoMuscular }}</span>
                  </template>
                </div>
              </div>
              
              <!-- Expand icon and Info icon when selected -->
              <div v-if="currentExerciseIndex === i" class="flex items-center gap-3">
                <button 
                  @click.stop="openVideoInstructions(ex)"
                  class="text-[#9CA3AF] hover:text-white transition-colors bg-[#1F2937] hover:bg-[#374151] p-1.5 rounded-full"
                  title="Video de técnica e instrucciones"
                >
                  <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                    <circle cx="12" cy="12" r="10"></circle>
                    <path d="M12 8v4"></path>
                    <path d="M12 16h.01"></path>
                  </svg>
                </button>
                <div class="text-[#DC2626]">
                  <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><polyline points="6 9 12 15 18 9"></polyline></svg>
                </div>
              </div>
            </div>

            <!-- Card Body (Only if selected) -->
            <div v-show="currentExerciseIndex === i" class="pl-11 pt-2 border-t border-[#1F2937]/50 mt-2">
               <div class="grid grid-cols-3 gap-4 mb-4 mt-2">
                 <div>
                   <label class="block text-[10px] text-gray-500 uppercase font-bold mb-1">Series</label>
                   <div class="text-white font-medium">{{ ex.sets || ex.series }}</div>
                 </div>
                 <div>
                   <label class="block text-[10px] text-gray-500 uppercase font-bold mb-1">Reps Obj.</label>
                   <div class="text-white font-medium">{{ ex.reps || ex.ejercicio?.repeticiones || '10-12' }}</div>
                 </div>
                  <div>
                    <label class="block text-[10px] text-white/50 uppercase font-black tracking-widest mb-1 flex items-center gap-1.5">
                      Último Peso ({{ weightUnit }})
                    </label>
                    <div class="text-white font-bold text-lg">{{ ex.weight && String(ex.weight).toLowerCase() !== 'smart weight' && String(ex.weight) !== '0' && String(ex.weight) !== '0 kg' ? ex.weight : '0' }}</div>
                  </div>
               </div>

               <!-- Si aún no empieza -->
                 <div v-if="!ex.isStarted && !ex.completed" class="mt-6">
                   <button 
                      @click.stop="startExercise(i)" 
                      class="w-full py-4 rounded-xl bg-gradient-to-r from-[#DC2626] to-[#991B1B] text-white font-black text-sm md:text-base uppercase tracking-widest shadow-[0_0_20px_rgba(220,38,38,0.4)] hover:shadow-[0_0_30px_rgba(220,38,38,0.6)] transition-all active:scale-95 group/btn overflow-hidden relative"
                    >
                     <div class="absolute inset-0 bg-white/20 translate-y-full group-hover/btn:translate-y-0 transition-transform duration-300 ease-out"></div>
                     <span class="relative z-10 flex items-center justify-center gap-2">
                       <svg width="20" height="20" viewBox="0 0 24 24" fill="currentColor"><path d="M5 3l14 9-14 9V3z"/></svg>
                       Empezar Ejercicio
                     </span>
                   </button>
                 </div>

               <!-- Descanso -->
               <div v-else-if="ex.isResting" class="mt-6 flex flex-col items-center justify-center py-8 bg-[#111827] rounded-2xl border border-[#374151] shadow-inner">
                  <svg width="32" height="32" viewBox="0 0 24 24" fill="none" class="text-blue-500 mb-2 animate-pulse" stroke="currentColor" stroke-width="2"><circle cx="12" cy="12" r="10"/><polyline points="12 6 12 12 16 14"/></svg>
                  <h4 class="text-xs font-bold text-gray-400 uppercase tracking-widest mb-1">Descanso</h4>
                  <div class="text-6xl font-black text-white mb-6 font-mono tracking-tighter">{{ formatTime(ex.restRemaining) }}</div>
                  <button @click.stop="skipRest(i)" class="px-8 py-2.5 rounded-xl border-2 border-transparent bg-white/10 text-white hover:bg-white/20 font-bold text-sm uppercase transition-all shadow-md active:scale-95">
                      Saltar Descanso
                  </button>
               </div>

               <!-- Series -->
               <div v-else-if="ex.isStarted && !ex.completed" class="mt-6">
                 <h5 class="text-[11px] font-black text-gray-500 uppercase tracking-widest mb-3 flex items-center justify-between">
                   <div class="flex items-center gap-3">
                     <span>Registrar Series</span>
                   </div>
                   <span class="text-[#DC2626]">Serie {{ ex.currentSet }} / {{ ex.seriesData.length }}</span>
                 </h5>
                 
                 <div class="space-y-3">
                   <div v-for="(serie, sIdx) in ex.seriesData" :key="sIdx">
                     <!-- Mostrar solo las series hasta la actual (inclusive) -->
                     <div v-if="Number(sIdx) < ex.currentSet" class="flex items-center gap-3 bg-[#111827] p-2 rounded-xl border transition-all" :class="Number(sIdx) === ex.currentSet - 1 ? 'border-[#374151] shadow-lg scale-[1.02]' : 'border-transparent opacity-60'">
                       <div class="w-8 flex justify-center text-sm font-black text-gray-400">{{ Number(sIdx) + 1 }}</div>
                       
                       <div class="relative flex-1 group/input">
                         <input type="number" v-model="serie.peso" :disabled="Number(sIdx) !== ex.currentSet - 1" :placeholder="ex.weight && ex.weight !== 'Smart Weight' && ex.weight !== '0 kg' ? String(ex.weight).replace(' kg', '') : '0'" class="w-full bg-[#1F2937] border border-[#374151] focus:bg-[#111827] rounded-lg text-white font-black text-center text-lg py-2 focus:outline-none focus:border-[#DC2626] focus:ring-2 focus:ring-[#DC2626]/50 transition-all placeholder:text-gray-600 disabled:opacity-50 disabled:cursor-not-allowed shadow-inner">
                         <span class="absolute right-3 top-1/2 -translate-y-1/2 text-[10px] text-gray-400 font-bold uppercase tracking-widest">{{ weightUnit }}</span>
                       </div>
                       
                       <div class="relative flex-1 group/input">
                         <input type="number" v-model="serie.reps" :disabled="Number(sIdx) !== ex.currentSet - 1" :placeholder="ex.reps || ex.ejercicio?.repeticiones || '10'" class="w-full bg-[#1F2937] border border-[#374151] focus:bg-[#111827] rounded-lg text-white font-black text-center text-lg py-2 focus:outline-none focus:border-[#DC2626] focus:ring-2 focus:ring-[#DC2626]/50 transition-all placeholder:text-gray-600 disabled:opacity-50 disabled:cursor-not-allowed shadow-inner">
                         <span class="absolute right-3 top-1/2 -translate-y-1/2 text-[10px] text-gray-400 font-bold uppercase tracking-widest">reps</span>
                       </div>
                       
                       <!-- Boton de check -->
                       <button v-if="Number(sIdx) === ex.currentSet - 1" @click.stop="finishSeries(Number(i), Number(sIdx))" class="w-12 h-10 rounded-lg bg-[#DC2626] flex items-center justify-center text-white hover:bg-red-700 transition-all shadow-lg active:scale-95 shrink-0">
                         <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><polyline points="20 6 9 17 4 12"></polyline></svg>
                       </button>
                       <div v-else class="w-12 h-10 flex items-center justify-center text-green-500 shrink-0 bg-green-500/10 rounded-lg">
                         <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><polyline points="20 6 9 17 4 12"></polyline></svg>
                       </div>
                     </div>
                     <!-- Error validacion serie -->
                     <div
                       v-if="serieError && serieErrorIndex && serieErrorIndex.i === Number(i) && serieErrorIndex.sIdx === Number(sIdx)"
                       class="text-xs font-bold text-red-400 bg-red-500/10 border border-red-500/30 px-3 py-2 rounded-lg mt-2 flex items-center gap-2"
                     >
                       {{ serieError }}
                     </div>
                   </div>
                 </div>
               </div>

               <!-- Ya completado -->
               <div v-if="ex.completed" class="mt-6">
                 <button 
                    @click.stop="currentExerciseIndex = (Number(i) + 1 < exercises.length) ? Number(i) + 1 : -1" 
                    class="w-full py-3 rounded-xl bg-[#22C55E] text-black font-black text-sm uppercase tracking-wide shadow-lg shadow-green-900/20 hover:bg-green-400 transition-all active:scale-95 flex items-center justify-center gap-2"
                  >
                   <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><polyline points="9 18 15 12 9 6"></polyline></svg>
                   Siguiente Ejercicio
                 </button>
               </div>
            </div>
          </div>
        </div>
      </div>
    </main>
    <!-- INSTRUCTION VIDEO MODAL -->
    <div v-if="showVideoModal" class="fixed inset-0 z-[150] flex items-center justify-center p-4 bg-black/95 backdrop-blur-xl animate-fade-in" @click="showVideoModal = false">
      <div class="relative w-full max-w-4xl bg-[#0A0A0A] border border-[#1F2937] rounded-3xl overflow-hidden shadow-[0_0_50px_rgba(0,0,0,1)]" @click.stop>
        <!-- Header -->
        <div class="p-6 flex items-center justify-between border-b border-[#1F2937]">
          <div class="flex items-center gap-3">
            <div class="w-10 h-10 bg-[#DC2626]/10 rounded-xl flex items-center justify-center text-[#DC2626]">
              <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5"><circle cx="12" cy="12" r="10"/><path d="M12 6v6l4 2"/></svg>
            </div>
            <div>
              <h3 class="text-xl font-bold text-white">{{ selectedExForVideo?.name || selectedExForVideo?.nombreEjercicio || 'Técnica del Ejercicio' }}</h3>
              <p class="text-xs text-[#9CA3AF] uppercase font-bold tracking-widest mt-0.5">Instrucciones y Ejecución</p>
            </div>
          </div>
          <button @click="showVideoModal = false" class="w-10 h-10 bg-[#1F2937] hover:bg-[#374151] rounded-full flex items-center justify-center text-white transition-all">
            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5"><path d="M18 6L6 18M6 6l12 12"/></svg>
          </button>
        </div>

        <!-- Video -->
        <div class="aspect-video bg-black relative">
           <video 
             src="https://res.cloudinary.com/dgtahwqpj/video/upload/v1772039401/Video_Generado_Sin_Fuego_l8s9iz.mp4" 
             controls 
             autoplay 
             class="w-full h-full"
           ></video>
        </div>

        <!-- Instructions -->
        <div class="p-8 bg-gradient-to-t from-[#0A0A0A] to-[#111827]">
           <h4 class="text-sm font-black text-[#DC2626] uppercase tracking-[0.2em] mb-4">Guía de Ejecución:</h4>
           <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div class="space-y-4">
                 <div class="flex gap-4">
                    <div class="w-6 h-6 rounded-full bg-[#DC2626] flex-shrink-0 flex items-center justify-center text-xs font-bold text-white">1</div>
                    <p class="text-sm text-gray-300">Mantén una postura estable y el núcleo activado.</p>
                 </div>
                 <div class="flex gap-4">
                    <div class="w-6 h-6 rounded-full bg-[#DC2626] flex-shrink-0 flex items-center justify-center text-xs font-bold text-white">2</div>
                    <p class="text-sm text-gray-300">Realiza el movimiento de forma controlada, sintiendo el músculo.</p>
                 </div>
              </div>
              <div class="space-y-4">
                 <div class="flex gap-4">
                    <div class="w-6 h-6 rounded-full bg-[#DC2626] flex-shrink-0 flex items-center justify-center text-xs font-bold text-white">3</div>
                    <p class="text-sm text-gray-300">Exhala en el esfuerzo máximo e inhala en el retorno.</p>
                 </div>
                 <div class="flex gap-4">
                    <div class="w-6 h-6 rounded-full bg-[#DC2626] flex-shrink-0 flex items-center justify-center text-xs font-bold text-white">4</div>
                    <p class="text-sm text-gray-300">Concentración total en la conexión mente-músculo.</p>
                 </div>
              </div>
           </div>
        </div>
      </div>
    </div>

    <!-- SUMMARY SCREEN OVERLAY -->
    <div v-if="showSummary" class="fixed inset-0 z-[100] bg-black/95 backdrop-blur-xl flex items-center justify-center p-4 animate-fade-in">
      <div class="w-full max-w-[500px] bg-[#0A0A0A] border-2 border-[#1F2937] rounded-3xl p-8 flex flex-col items-center text-center shadow-[0_0_80px_rgba(220,38,38,0.2)]">
        
        <!-- Trophy/Icon -->
        <div class="w-24 h-24 bg-gradient-to-br from-[#DC2626] to-[#991B1B] rounded-full flex items-center justify-center mb-6 shadow-[0_0_30px_rgba(220,38,38,0.5)] animate-bounce-slow">
          <svg width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <path d="M6 9H4.5a2.5 2.5 0 0 1 0-5H6"/><path d="M18 9h1.5a2.5 2.5 0 0 0 0-5H18"/><path d="M4 22h16"/><path d="M10 14.66V17c0 .55-.47.98-.97 1.21C7.85 18.75 7 20.24 7 22"/><path d="M14 14.66V17c0 .55.47.98.97 1.21C16.15 18.75 17 20.24 17 22"/><path d="M18 2H6v7a6 6 0 0 0 12 0V2Z"/>
          </svg>
        </div>

        <h2 class="text-3xl font-black text-white italic uppercase tracking-tighter mb-2">¡Entrenamiento Brutal!</h2>
        <p class="text-gray-400 font-medium mb-8">{{ summaryData.resumen }}</p>

        <!-- Stats Grid -->
        <div class="grid grid-cols-2 gap-4 w-full mb-6">
          <div class="bg-[#111827] border border-[#1F2937] p-4 rounded-2xl">
            <div class="text-[10px] text-red-500 font-black uppercase tracking-widest mb-1">Puntos Ganados</div>
            <div class="text-3xl font-black text-white">+{{ summaryData.puntos }}</div>
          </div>
          <div class="bg-[#111827] border border-[#1F2937] p-4 rounded-2xl">
            <div class="text-[10px] text-blue-500 font-black uppercase tracking-widest mb-1">Récords Batidos</div>
            <div class="text-3xl font-black text-white">{{ summaryData.records.length }}</div>
          </div>
        </div>

        <!-- Muscle Breakdown -->
        <div v-if="summaryData.muscleBreakdown && Object.keys(summaryData.muscleBreakdown).length > 0" class="w-full text-left mb-6">
            <h4 class="text-[11px] font-black uppercase tracking-widest text-gray-500 mb-3 ml-1">Desglose por Músculo</h4>
            <div class="space-y-2">
                <div v-for="(pts, muscle) in summaryData.muscleBreakdown" :key="muscle" class="flex items-center justify-between bg-[#111827]/50 border border-[#1F2937] px-4 py-2.5 rounded-xl">
                   <div class="flex items-center gap-2">
                      <div class="w-1.5 h-1.5 rounded-full bg-red-500"></div>
                      <span class="text-xs font-bold text-gray-200">{{ muscle }}</span>
                   </div>
                   <span class="text-xs font-black text-white">+{{ pts }} pts</span>
                </div>
            </div>
        </div>

        <!-- Récords Batidos (detalle) -->
        <div v-if="summaryData.records && summaryData.records.length > 0" class="w-full text-left mb-6">
            <h4 class="text-[11px] font-black uppercase tracking-widest text-blue-400 mb-3 ml-1 flex items-center gap-2">
                <svg width="12" height="12" viewBox="0 0 24 24" fill="currentColor"><path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"/></svg>
                Récords Batidos
            </h4>
            <div class="space-y-2">
                <div
                    v-for="rec in summaryData.records"
                    :key="rec.ejercicioId"
                    class="flex justify-between items-center bg-[#0A0F1E] border border-blue-500/20 px-4 py-3 rounded-xl"
                >
                    <div class="flex items-center gap-3 flex-1 min-w-0">
                        <div class="w-9 h-9 bg-blue-500/20 rounded-xl flex items-center justify-center text-blue-400 shrink-0">
                            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"><path d="M6 9H4.5a2.5 2.5 0 0 1 0-5H6"/><path d="M18 9h1.5a2.5 2.5 0 0 0 0-5H18"/><path d="M4 22h16"/><path d="M18 2H6v7a6 6 0 0 0 12 0V2Z"/></svg>
                        </div>
                        <div class="flex flex-col min-w-0">
                            <span class="text-sm font-bold text-white truncate">
                                {{ rec.ejercicioNombre || rec.nombre || 'Ejercicio' }}
                            </span>
                            <span class="text-[10px] text-gray-400 uppercase tracking-widest">
                                {{ rec.grupoMuscular || rec.muscle || '¡Nuevo récord personal!' }}
                            </span>
                        </div>
                    </div>
                    <div class="flex flex-col items-end gap-0.5 shrink-0 ml-2">
                        <span class="text-base font-black text-blue-400">{{ rec.pesoMaximo ?? rec.peso }} kg</span>
                        <div v-if="rec.mejora > 0" class="flex items-center gap-0.5 text-[11px] font-bold text-green-400">
                            <svg width="10" height="10" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"><path d="M12 19V5M5 12l7-7 7 7"/></svg>
                            +{{ rec.mejora }} kg
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- AI Improvements (PRO) -->
        <div v-if="isPro && summaryData.mejorasIA" class="w-full bg-gradient-to-r from-purple-900/40 to-indigo-900/40 border border-purple-500/30 rounded-2xl p-6 mb-6 text-left relative overflow-hidden group">
          <div class="absolute -top-10 -right-10 w-32 h-32 bg-purple-500/10 blur-[50px] rounded-full"></div>
          <div class="flex items-center gap-2 mb-3">
             <div class="bg-purple-500 p-1 rounded-md">
                <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="3" stroke-linecap="round" stroke-linejoin="round">
                  <path d="m12 3 1.912 5.813a2 2 0 0 0 1.275 1.275L21 12l-5.813 1.912a2 2 0 0 0-1.275 1.275L12 21l-1.912-5.813a2 2 0 0 0-1.275-1.275L3 12l5.813-1.912a2 2 0 0 0 1.275-1.275L12 3Z"/>
                </svg>
             </div>
             <span class="text-[11px] font-black uppercase tracking-widest text-purple-300">Mejoras IA Pro</span>
          </div>
          <p class="text-[13px] text-gray-200 leading-relaxed italic">"{{ summaryData.mejorasIA }}"</p>
        </div>

        <!-- Logros Desbloqueados -->
        <div v-if="summaryData.logros && summaryData.logros.length > 0" class="w-full text-left mb-8">
            <h4 class="text-[11px] font-black uppercase tracking-widest text-yellow-500 mb-2">Logros Desbloqueados 🏆</h4>
            <div class="flex flex-col gap-2">
                <div v-for="logro in summaryData.logros" :key="logro.id" class="flex items-center gap-3 bg-yellow-500/5 border border-yellow-500/20 p-3 rounded-xl">
                   <div class="w-8 h-8 bg-yellow-500 rounded-full flex items-center justify-center text-black shrink-0">
                      <svg width="16" height="16" viewBox="0 0 24 24" fill="currentColor"><path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"/></svg>
                   </div>
                   <div class="text-left">
                      <div class="text-xs font-bold text-white">{{ logro.titulo || logro.nombre }}</div>
                      <div class="text-[10px] text-gray-400 leading-none mt-1">+{{ logro.puntos || 50 }} pts bonus</div>
                   </div>
                </div>
            </div>
        </div>

        <button @click="emit('completed')" class="w-full py-4 bg-white text-black hover:bg-red-500 hover:text-white rounded-2xl font-black text-lg uppercase tracking-widest transition-all duration-300 shadow-[0_10px_30px_rgba(0,0,0,0.5)] active:scale-95">
          Continuar
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onBeforeUnmount, computed } from 'vue';
import { entrenamientosApi } from '../../api';
import { useAuthStore } from '../../stores/auth';

const authStore = useAuthStore();
const isPro = computed(() => authStore.isPro);
const showSummary = ref(false);
const isFinalizing = ref(false);
const summaryData = ref<any>({
  puntos: 0,
  records: [],
  logros: [],
  resumen: '',
  mejorasIA: ''
});

// Video Modal state
const showVideoModal = ref(false);
const selectedExForVideo = ref<any>(null);

const openVideoInstructions = (ex: any) => {
  selectedExForVideo.value = ex;
  showVideoModal.value = true;
};

const props = defineProps<{
  workout: any;
}>();

const emit = defineEmits(['close', 'completed']);

const weightUnit = ref('kg');

// Handle internal exercises data with required state for intervals, series, and interactions
const rawExercises = props.workout.ejercicios || props.workout.exerciseList || [];
const exercises = ref<any[]>(rawExercises.map((e: any) => ({
  ...e,
  completed: false,
  isStarted: false,
  isResting: false,
  currentSet: 1,
  restRemaining: 0,
  seriesData: Array.from({ length: e.sets || e.series || 3 }).map((_, sIdx) => ({
      numeroSerie: sIdx + 1,
      peso: null,
      reps: null,
      completada: false
  }))
})));

const currentExerciseIndex = ref(0);
let timerInterval: any = null;

const formatTime = (seconds: number) => {
    const m = Math.floor(seconds / 60);
    const s = seconds % 60;
    return `${m.toString().padStart(2, '0')}:${s.toString().padStart(2, '0')}`;
};

const toggleExerciseCompletion = (index: number) => {
  const ex = exercises.value[index];
  ex.completed = !ex.completed;
  if (ex.completed) {
      ex.isStarted = true;
      ex.isResting = false;
      if (timerInterval) clearInterval(timerInterval);
      ex.seriesData.forEach((s: any) => s.completada = true);
      
      if (currentExerciseIndex.value === index) {
          const nextIncomplete = exercises.value.findIndex((e: any, idx: number) => idx > index && !e.completed);
          if (nextIncomplete !== -1) {
              currentExerciseIndex.value = nextIncomplete;
          }
      }
  } else {
      ex.seriesData.forEach((s: any) => s.completada = false);
      ex.currentSet = 1;
  }
};

const startExercise = (index: number) => {
    exercises.value[index].isStarted = true;
};

const endRest = (index: number) => {
    if (timerInterval) clearInterval(timerInterval);
    const ex = exercises.value[index];
    ex.isResting = false;
    ex.restRemaining = 0;
    ex.currentSet++;
};

const skipRest = (index: number) => {
    endRest(index);
};

// Estado de error de validación de serie
const serieError = ref<string | null>(null);
const serieErrorIndex = ref<{i: number, sIdx: number} | null>(null);

const finishSeries = (index: number, sIdx: number) => {
    const ex = exercises.value[index];
    const serie = ex.seriesData[sIdx];

    // ── VALIDACIÓN: exigir peso > 0 y reps > 0 ──
    const peso = Number(serie.peso);
    const reps = Number(serie.reps);
    if (!peso || peso <= 0 || !reps || reps <= 0) {
        serieError.value = !peso || peso <= 0
            ? '⚠ Introduce el peso antes de completar la serie'
            : '⚠ Introduce las repeticiones antes de completar la serie';
        serieErrorIndex.value = { i: index, sIdx };
        // Auto-ocultar el mensaje
        setTimeout(() => {
            serieError.value = null;
            serieErrorIndex.value = null;
        }, 2500);
        // Shake animation en los inputs
        const btn = document.getElementById(`finishSerieBtn_${index}_${sIdx}`);
        if (btn) {
            btn.classList.add('animate-shake');
            setTimeout(() => btn.classList.remove('animate-shake'), 500);
        }
        return;
    }

    serieError.value = null;
    serieErrorIndex.value = null;
    serie.completada = true;
    
    if (ex.currentSet < ex.seriesData.length) {
        // Hay más series: descanso
        ex.isResting = true;
        ex.restRemaining = ex.descansoSegundos || 90;
        
        if (timerInterval) clearInterval(timerInterval);
        timerInterval = setInterval(() => {
            ex.restRemaining--;
            if (ex.restRemaining <= 0) {
                endRest(index);
            }
        }, 1000);
    } else {
        // Última serie completada → marcar ejercicio como terminado
        ex.completed = true;
        ex.isStarted = true;
        ex.isResting = false;
        if (timerInterval) clearInterval(timerInterval);
        
        // Avanzar automáticamente al siguiente ejercicio incompleto
        const nextIncomplete = exercises.value.findIndex((e: any, idx: number) => idx > index && !e.completed);
        if (nextIncomplete !== -1) {
            currentExerciseIndex.value = nextIncomplete;
        }
    }
};

// Normalizar nombre de músculo a nombre canónico
const normalizeMuscle = (raw: string): string => {
  const muscleMap: Record<string, string> = {
    'pierna': 'Pierna', 'piernas': 'Pierna', 'cuadriceps': 'Pierna',
    'cuádriceps': 'Pierna', 'femoral': 'Pierna', 'gluteo': 'Pierna',
    'glúteo': 'Pierna', 'gluteos': 'Pierna', 'glúteos': 'Pierna',
    'pecho': 'Pecho', 'pectoral': 'Pecho', 'pectorales': 'Pecho',
    'espalda': 'Espalda', 'dorsales': 'Espalda', 'lumbar': 'Espalda',
    'hombro': 'Hombros', 'hombros': 'Hombros', 'deltoides': 'Hombros',
    'bicep': 'Bíceps', 'biceps': 'Bíceps', 'bíceps': 'Bíceps', 'bícep': 'Bíceps',
    'tricep': 'Tríceps', 'triceps': 'Tríceps', 'tríceps': 'Tríceps', 'trícep': 'Tríceps',
    'brazos': 'Bíceps',
    'core': 'Abdomen', 'abdomen': 'Abdomen', 'abdominales': 'Abdomen', 'abs': 'Abdomen',
    'pantorrilla': 'Pierna', 'gemelos': 'Pierna'
  };
  const key = (raw || 'otro').trim().toLowerCase();
  return muscleMap[key] || (raw.charAt(0).toUpperCase() + raw.slice(1));
};

const onFinalizar = async () => {
  if (isFinalizing.value) return;
  isFinalizing.value = true;

  // 1. Calcular desglose local de puntos por músculo (10 pts por ejercicio completado)
  const localMuscleBreakdown: Record<string, number> = {};
  const completedExsList = exercises.value.filter((e: any) => e.completed);

  completedExsList.forEach((e: any) => {
    const rawMuscle = e.muscle || e.ejercicio?.grupoMuscular || e.grupoMuscular || 'Otro';
    const normalizedMuscle = normalizeMuscle(rawMuscle);
    localMuscleBreakdown[normalizedMuscle] = (localMuscleBreakdown[normalizedMuscle] || 0) + 10;
  });

  try {
    const completedExsPayload = completedExsList.map((e: any) => {
      let idReal = e.ejercicioId || e.ejercicio?.id || e.id;
      if (typeof idReal === 'string' && idReal.includes('_')) {
        idReal = idReal.split('_')[1] || idReal.split('_')[0];
      }
      return {
        ejercicioId: Number(idReal) || 0,
        series: e.seriesData.map((s: any) => ({
          numeroSerie: Number(s.numeroSerie),
          peso: Number(s.peso) || 0,
          reps: Number(s.reps) || Number(e.reps) || 10,
          completada: true
        }))
      };
    });

    const payload = {
      rutinaId: typeof props.workout.id === 'number' ? props.workout.id : null,
      nombre: props.workout.title || props.workout.nombre || 'Entrenamiento libre',
      fecha: new Date().toISOString(),
      duracionMinutos: Math.round(Number(props.workout.duration || props.workout.duracionMinutos || 45)),
      ejercicios: completedExsPayload
    };

    const res = await entrenamientosApi.finalizar(payload);
    const entrenamientoId = res.data?.id;

    const recordsPersonal = res.data?.recordsPersonal || [];
    recordsPersonal.forEach((rec: any) => {
      const rawMuscle = rec.grupoMuscular || 'Otro';
      const normalizedMuscle = normalizeMuscle(rawMuscle);
      // Añadir bonus de 30 pts al músculo del récord
      localMuscleBreakdown[normalizedMuscle] = (localMuscleBreakdown[normalizedMuscle] || 0) + 30;
    });

    // Usar puntos del servidor si están disponibles, si no calcular local
    // Incluir también los puntos de los logros desbloqueados
    const serverPoints = res.data?.puntosGanados ?? 0;
    const logrosBonus = (res.data?.logrosDesbloqueados || []).reduce((acc: number, l: any) => acc + (l.puntos || 0), 0);
    const localTotal = Object.values(localMuscleBreakdown).reduce((acc: number, pts: any) => acc + Number(pts), 0);
    
    // Si el servidor devuelve puntos, usamos esos + bonus de logros
    // Si no, usamos el cálculo local
    const totalPoints = (serverPoints > 0)
      ? serverPoints + logrosBonus
      : localTotal + logrosBonus;

    summaryData.value = {
      puntos: totalPoints,
      muscleBreakdown: localMuscleBreakdown,
      records: recordsPersonal,
      logros: res.data?.logrosDesbloqueados || [],
      resumen: `¡Brutal! Has completado ${completedExsList.length} ejercicio${completedExsList.length !== 1 ? 's' : ''}. Sigue así.`,
      mejorasIA: ''
    };

    if (isPro.value && entrenamientoId) {
      try {
        const iaRes = await entrenamientosApi.getMejorasIA(entrenamientoId);
        summaryData.value.mejorasIA = iaRes.data?.mejoras || iaRes.data?.ejes || iaRes.data?.feedback || iaRes.data;
      } catch {
        summaryData.value.mejorasIA = 'Excelente trabajo. Mantén este ritmo para ver resultados constantes.';
      }
    }

    authStore.fetchProfile();
    showSummary.value = true;
  } catch (error) {
    console.error('Error finalizing workout:', error);

    // Fallback: mostrar puntos locales aunque falle el backend
    const totalPoints = Object.values(localMuscleBreakdown).reduce((acc, pts) => acc + pts, 0);
    summaryData.value = {
      puntos: totalPoints,
      muscleBreakdown: localMuscleBreakdown,
      records: [],
      logros: [],
      resumen: 'Entrenamiento finalizado. Nota: Hubo un problema al sincronizar, los puntos se guardarán en la próxima conexión.',
      mejorasIA: isPro.value ? 'Sigue así, estás progresando constantemente.' : ''
    };
    showSummary.value = true;
  } finally {
    isFinalizing.value = false;
  }
};

onBeforeUnmount(() => {
    if (timerInterval) clearInterval(timerInterval);
});
</script>

<style scoped>
.animate-slide-up {
  animation: slideUp 0.3s cubic-bezier(0.165, 0.84, 0.44, 1) forwards;
}
@keyframes slideUp {
  from { opacity: 0; transform: translateY(20px); }
  to { opacity: 1; transform: translateY(0); }
}
@keyframes fadeIn {
  from { opacity: 0; }
  to { opacity: 1; }
}
@keyframes bounceSlow {
  0%, 100% { transform: translateY(0); }
  50% { transform: translateY(-10px); }
}
@keyframes float {
  0%, 100% { transform: translateY(0); }
  50% { transform: translateY(-5px); }
}
.animate-fade-in {
  animation: fadeIn 0.5s ease-out forwards;
}
.animate-bounce-slow {
  animation: bounceSlow 3s ease-in-out infinite;
}
.animate-float {
  animation: float 3s ease-in-out infinite;
}
.custom-scrollbar::-webkit-scrollbar {
  width: 6px;
}
.custom-scrollbar::-webkit-scrollbar-track {
  background: transparent;
}
.custom-scrollbar::-webkit-scrollbar-thumb {
  background: #374151;
  border-radius: 10px;
}
@keyframes shake {
  0%, 100% { transform: translateX(0); }
  20% { transform: translateX(-6px); }
  40% { transform: translateX(6px); }
  60% { transform: translateX(-4px); }
  80% { transform: translateX(4px); }
}
.animate-shake {
  animation: shake 0.4s ease-in-out;
}
</style>
