<template>
  <div>
    <!-- GLOBAL TOAST -->
    <transition name="toast">
      <div v-if="uiStore.toast.show" 
           class="fixed bottom-8 left-1/2 -translate-x-1/2 z-[1000] flex items-center gap-3 px-6 py-4 rounded-2xl shadow-2xl border backdrop-blur-md animate-in slide-in-from-bottom"
           :class="[
             uiStore.toast.type === 'error' ? 'bg-red-900/90 border-red-500/50 text-white' : 
             uiStore.toast.type === 'warning' ? 'bg-yellow-900/90 border-yellow-500/50 text-white' :
             'bg-gray-900/95 border-emerald-500/50 text-white'
           ]">
        <div v-if="uiStore.toast.type === 'error'" class="w-2 h-2 rounded-full bg-red-500 animate-pulse"></div>
        <div v-else-if="uiStore.toast.type === 'warning'" class="w-2 h-2 rounded-full bg-yellow-500 animate-pulse"></div>
        <div v-else class="w-2 h-2 rounded-full bg-emerald-500 animate-pulse"></div>
        <span class="text-sm font-bold tracking-tight">{{ uiStore.toast.message }}</span>
      </div>
    </transition>

    <!-- GLOBAL DIALOG (MODAL) -->
    <transition name="modal">
      <div v-if="uiStore.dialog.show" class="fixed inset-0 z-[999] flex items-center justify-center p-4 bg-black/80 backdrop-blur-sm overflow-y-auto">
        <div class="relative w-full max-w-md bg-[#111827] border border-[#1F2937] rounded-3xl p-8 shadow-2xl animate-in zoom-in duration-300">
          <div class="flex flex-col items-center text-center">
            <!-- Icon based on type -->
            <div class="w-16 h-16 rounded-2xl bg-[#1F2937] flex items-center justify-center mb-6 border border-[#374151]">
              <svg v-if="uiStore.dialog.type === 'danger'" width="32" height="32" viewBox="0 0 24 24" fill="none" class="text-red-500">
                <path d="M12 9v4m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 18c-.77 1.333.192 3 1.732 3z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
              </svg>
              <svg v-else width="32" height="32" viewBox="0 0 24 24" fill="none" class="text-blue-500">
                <path d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
              </svg>
            </div>

            <h3 class="text-2xl font-black text-white mb-3 uppercase tracking-tighter">{{ uiStore.dialog.title }}</h3>
            <p class="text-gray-400 text-sm leading-relaxed mb-8">{{ uiStore.dialog.message }}</p>

            <div class="flex w-full gap-3">
              <button 
                v-if="uiStore.dialog.cancelText"
                @click="uiStore.closeDialog(false)" 
                class="flex-1 px-6 py-3.5 rounded-xl border border-[#374151] hover:bg-gray-800 text-gray-300 font-bold transition-all text-sm uppercase tracking-widest active:scale-95"
              >
                {{ uiStore.dialog.cancelText }}
              </button>
              <button 
                @click="uiStore.closeDialog(true)" 
                class="flex-1 px-6 py-3.5 rounded-xl font-bold transition-all text-sm uppercase tracking-widest active:scale-95 shadow-lg shadow-indigo-900/20"
                :class="uiStore.dialog.type === 'danger' ? 'bg-red-600 hover:bg-red-700 text-white shadow-red-900/30' : 'bg-indigo-600 hover:bg-indigo-700 text-white'"
              >
                {{ uiStore.dialog.confirmText }}
              </button>
            </div>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script setup lang="ts">
import { useUIStore } from '../../stores/ui';
const uiStore = useUIStore();
</script>

<style scoped>
.toast-enter-active, .toast-leave-active {
  transition: all 0.4s cubic-bezier(0.175, 0.885, 0.32, 1.275);
}
.toast-enter-from, .toast-leave-to {
  transform: translate(-50%, 100px);
  opacity: 0;
}

.modal-enter-active, .modal-leave-active {
  transition: opacity 0.3s ease;
}
.modal-enter-from, .modal-leave-to {
  opacity: 0;
}
</style>
