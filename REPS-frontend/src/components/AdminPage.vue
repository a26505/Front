<template>
  <div class="flex min-h-screen bg-[#000000] text-white">
    <Sidebar active="admin" />

    <div class="flex-1 md:ml-[256px] flex flex-col min-h-screen">
      <!-- HEADER STICKY -->
      <header class="sticky top-0 z-40 bg-black/95 backdrop-blur-md py-4 px-6 flex items-center justify-between border-b border-[#1F2937]/50">
        <div>
          <h1 class="text-2xl font-bold tracking-tight">Panel de Administración</h1>
          <p class="text-sm text-[#9CA3AF]">Gestiona usuarios y contenido de la comunidad</p>
        </div>
      </header>

      <main class="flex-1 p-6 max-w-7xl mx-auto w-full">
        <!-- TABS -->
        <div class="flex gap-4 mb-8 border-b border-[#1F2937]">
          <button 
            @click="activeTab = 'usuarios'" 
            :class="['pb-3 px-2 text-sm font-semibold transition-colors relative', activeTab === 'usuarios' ? 'text-[#DC2626]' : 'text-[#9CA3AF] hover:text-white']"
          >
            Usuarios
            <div v-if="activeTab === 'usuarios'" class="absolute bottom-0 left-0 w-full h-[2px] bg-[#DC2626]"></div>
          </button>
          <button 
            @click="activeTab = 'rutinas'" 
            :class="['pb-3 px-2 text-sm font-semibold transition-colors relative', activeTab === 'rutinas' ? 'text-[#DC2626]' : 'text-[#9CA3AF] hover:text-white']"
          >
            Rutinas / Comunidad
            <div v-if="activeTab === 'rutinas'" class="absolute bottom-0 left-0 w-full h-[2px] bg-[#DC2626]"></div>
          </button>
        </div>

        <!-- TAB USUARIOS -->
        <div v-if="activeTab === 'usuarios'">
          <div v-if="loadingUsuarios" class="flex justify-center py-10">
            <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-[#DC2626]"></div>
          </div>
          <div v-else class="bg-[#111827] rounded-xl border border-[#1F2937] overflow-hidden">
            <div class="overflow-x-auto">
              <table class="w-full text-left border-collapse">
                <thead>
                  <tr class="bg-[#1F2937] text-[#9CA3AF] text-xs uppercase tracking-wider">
                    <th class="p-4 font-medium">ID</th>
                    <th class="p-4 font-medium">Nombre</th>
                    <th class="p-4 font-medium">Email</th>
                    <th class="p-4 font-medium">Estado</th>
                    <th class="p-4 font-medium text-right">Acciones</th>
                  </tr>
                </thead>
                <tbody class="divide-y divide-[#1F2937]">
                  <tr v-for="user in usuarios" :key="user.id" class="hover:bg-[#1F2937]/50 transition-colors">
                    <td class="p-4 text-sm">{{ user.id }}</td>
                    <td class="p-4 text-sm font-medium">{{ user.nombre }} <span v-if="user.rol === 'Admin'" class="ml-2 text-xs bg-red-900 text-red-200 px-2 py-0.5 rounded-full">Admin</span></td>
                    <td class="p-4 text-sm text-[#9CA3AF]">{{ user.email }}</td>
                    <td class="p-4 text-sm">
                      <span v-if="user.estaBorrado" class="inline-flex items-center px-2 py-0.5 rounded text-xs font-medium bg-gray-800 text-gray-300">
                        Eliminado
                      </span>
                      <span v-else-if="user.estaActivo" class="inline-flex items-center px-2 py-0.5 rounded text-xs font-medium bg-green-900/30 text-green-400">
                        Activo
                      </span>
                      <span v-else class="inline-flex items-center px-2 py-0.5 rounded text-xs font-medium bg-red-900/30 text-[#DC2626]">
                        Baneado
                      </span>
                    </td>
                    <td class="p-4 text-sm text-right flex justify-end gap-2">
                      <button 
                        v-if="!user.estaBorrado"
                        @click="toggleUserStatus(user)" 
                        class="px-3 py-1 rounded text-xs font-semibold transition-colors"
                        :class="user.estaActivo ? 'bg-orange-900/50 text-orange-400 hover:bg-orange-900/80' : 'bg-green-900/50 text-green-400 hover:bg-green-900/80'"
                      >
                        {{ user.estaActivo ? 'Banear' : 'Desbanear' }}
                      </button>
                      <button 
                        v-if="!user.estaBorrado"
                        @click="deleteUser(user.id)" 
                        class="px-3 py-1 bg-red-900/50 text-[#DC2626] rounded text-xs font-semibold hover:bg-red-900/80 transition-colors"
                      >
                        Eliminar
                      </button>
                    </td>
                  </tr>
                </tbody>
              </table>
              <div v-if="usuarios.length === 0" class="p-8 text-center text-[#9CA3AF]">
                No hay usuarios encontrados.
              </div>
            </div>
          </div>
        </div>

        <!-- TAB RUTINAS -->
        <div v-if="activeTab === 'rutinas'">
          <!-- Filters -->
          <div class="flex gap-2 mb-4">
            <button @click="rutinasFilter = 'ALL'" :class="['px-3 py-1 text-xs rounded-full border border-[#374151]', rutinasFilter === 'ALL' ? 'bg-[#DC2626] text-white border-[#DC2626]' : 'bg-transparent text-[#9CA3AF] hover:text-white']">Todas</button>
            <button @click="rutinasFilter = 'EnRevision'" :class="['px-3 py-1 text-xs rounded-full border border-[#374151]', rutinasFilter === 'EnRevision' ? 'bg-[#DC2626] text-white border-[#DC2626]' : 'bg-transparent text-[#9CA3AF] hover:text-white']">Pendientes ({{ rutinasPendientesCount }})</button>
          </div>

          <div v-if="loadingRutinas" class="flex justify-center py-10">
            <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-[#DC2626]"></div>
          </div>
          <div v-else class="bg-[#111827] rounded-xl border border-[#1F2937] overflow-hidden">
            <div class="overflow-x-auto">
              <table class="w-full text-left border-collapse">
                <thead>
                  <tr class="bg-[#1F2937] text-[#9CA3AF] text-xs uppercase tracking-wider">
                    <th class="p-4 font-medium">ID</th>
                    <th class="p-4 font-medium">Nombre</th>
                    <th class="p-4 font-medium">Creador</th>
                    <th class="p-4 font-medium">Estado</th>
                    <th class="p-4 font-medium text-right">Acciones</th>
                  </tr>
                </thead>
                <tbody class="divide-y divide-[#1F2937]">
                  <tr v-for="rutina in filteredRutinas" :key="rutina.id" class="hover:bg-[#1F2937]/50 transition-colors">
                    <td class="p-4 text-sm">{{ rutina.id }}</td>
                    <td class="p-4 text-sm font-medium">{{ rutina.nombre }}</td>
                    <td class="p-4 text-sm text-[#9CA3AF]">{{ rutina.creadorNombre || 'Anónimo' }}</td>
                    <td class="p-4 text-sm">
                      <span v-if="rutina.estado === 'Publicada'" class="inline-flex items-center px-2 py-0.5 rounded text-xs font-medium bg-green-900/30 text-green-400">
                        Publicada
                      </span>
                      <span v-else-if="rutina.estado === 'EnRevision'" class="inline-flex items-center px-2 py-0.5 rounded text-xs font-medium bg-yellow-900/30 text-yellow-500">
                        Pendiente
                      </span>
                      <span v-else class="inline-flex items-center px-2 py-0.5 rounded text-xs font-medium bg-gray-800 text-gray-400">
                        {{ rutina.estado }}
                      </span>
                    </td>
                    <td class="p-4 text-sm text-right flex justify-end gap-2">
                      <button 
                        @click="verRutina(rutina)" 
                        class="px-3 py-1 bg-[#1F2937] text-white border border-[#374151] rounded text-xs font-semibold hover:bg-[#374151] transition-colors flex items-center gap-1"
                      >
                        <svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5"><path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"/><circle cx="12" cy="12" r="3"/></svg>
                        Ver
                      </button>
                      <button 
                        v-if="rutina.estado === 'EnRevision'"
                        @click="validarRutina(rutina.id)" 
                        class="px-3 py-1 bg-green-900/50 text-green-400 rounded text-xs font-semibold hover:bg-green-900/80 transition-colors"
                      >
                        Aprobar
                      </button>
                      <button 
                        v-if="rutina.estado === 'EnRevision'"
                        @click="rechazarRutina(rutina.id)" 
                        class="px-3 py-1 bg-orange-900/50 text-orange-400 rounded text-xs font-semibold hover:bg-orange-900/80 transition-colors"
                      >
                        Rechazar
                      </button>
                      <button 
                        @click="eliminarRutina(rutina.id)" 
                        class="px-3 py-1 bg-red-900/50 text-[#DC2626] rounded text-xs font-semibold hover:bg-red-900/80 transition-colors"
                      >
                        Eliminar
                      </button>
                    </td>
                  </tr>
                </tbody>
              </table>
              <div v-if="filteredRutinas.length === 0" class="p-8 text-center text-[#9CA3AF]">
                No hay rutinas que coincidan con el filtro.
              </div>
            </div>
          </div>
        </div>

      </main>
    </div>

    <!-- MODAL: VER DETALLE RUTINA (PARA ADMIN) -->
    <div v-if="selectedRoutineForView" class="fixed inset-0 z-50 flex items-center justify-center p-4">
        <div @click="selectedRoutineForView = null" class="absolute inset-0 bg-black/80 backdrop-blur-sm"></div>
        <div class="relative bg-[#111827] border border-[#1F2937] rounded-[16px] w-full max-w-[600px] max-h-[85vh] overflow-hidden shadow-2xl animate-in zoom-in-95 duration-200 flex flex-col">
            <header class="p-6 border-b border-[#1F2937]/50 flex justify-between items-center bg-black/20">
                <div>
                    <h2 class="text-[20px] font-bold text-white">{{ selectedRoutineForView.nombre }}</h2>
                    <p class="text-xs text-[#9CA3AF]">ID: {{ selectedRoutineForView.id }} • Creador: {{ selectedRoutineForView.creadorNombre || 'Anónimo' }}</p>
                </div>
                <button @click="selectedRoutineForView = null" class="text-[#9CA3AF] hover:text-white transition-colors bg-[#1F2937] p-2 rounded-full">
                    <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
                </button>
            </header>
            
            <div class="p-6 overflow-y-auto flex-1 custom-scrollbar">
                <div v-if="isLoadingDetail" class="flex flex-col items-center py-16">
                    <div class="w-10 h-10 border-4 border-[#DC2626]/20 border-t-[#DC2626] rounded-full animate-spin mb-4"></div>
                    <p class="text-[#9CA3AF] text-sm font-medium tracking-wide">Cargando ejercicios...</p>
                </div>
                <div v-else class="space-y-4">
                    <!-- Image and Description -->
                    <div v-if="selectedRoutineForView.imagenUrl" class="w-full h-40 rounded-xl overflow-hidden mb-4 border border-[#374151]">
                        <img :src="selectedRoutineForView.imagenUrl" class="w-full h-full object-cover" />
                    </div>
                    
                    <div v-if="selectedRoutineForView.descripcion" class="bg-[#1F2937]/30 border border-[#374151]/50 rounded-xl p-4 mb-4">
                        <p class="text-[#9CA3AF] italic text-sm">"{{ selectedRoutineForView.descripcion }}"</p>
                    </div>

                    <div v-for="(ej, idx) in routineDetailExercises" :key="idx" class="bg-[#1F2937]/50 border border-[#374151] rounded-xl p-4 flex items-center gap-4">
                        <div class="w-10 h-10 bg-[#DC2626]/20 border border-[#DC2626]/30 rounded-lg flex items-center justify-center text-[#DC2626] font-black text-lg">
                          {{ idx + 1 }}
                        </div>
                        <div class="flex-1">
                            <h4 class="text-white font-bold text-sm mb-0.5">{{ ej.nombreEjercicio }}</h4>
                            <div class="flex gap-3 text-[11px] font-medium uppercase tracking-wider">
                                <span class="text-[#9CA3AF]">{{ ej.series }} series</span>
                                <span class="text-[#9CA3AF]">{{ ej.repeticiones }} reps</span>
                                <span class="text-[#DC2626]">{{ ej.descansoSegundos }}s descanso</span>
                            </div>
                        </div>
                    </div>
                    <div v-if="routineDetailExercises.length === 0" class="py-10 text-center text-[#9CA3AF]">
                      Esta rutina no tiene ejercicios asignados.
                    </div>
                </div>
            </div>
            <footer class="p-6 border-t border-[#1F2937] flex justify-end gap-3 bg-black/20">
                <button @click="selectedRoutineForView = null" class="px-6 py-2 border border-[#374151] text-white rounded-lg font-bold hover:bg-white/5 transition-all text-sm">Cerrar</button>
                <div v-if="selectedRoutineForView.estado === 'EnRevision'" class="flex gap-3">
                  <button @click="rechazarRutina(selectedRoutineForView.id); selectedRoutineForView = null" class="bg-orange-900/40 text-orange-400 border border-orange-800/30 px-6 py-2 rounded-lg font-bold hover:bg-orange-900/60 transition-all text-sm">Rechazar</button>
                  <button @click="validarRutina(selectedRoutineForView.id); selectedRoutineForView = null" class="bg-green-900/40 text-green-400 border border-green-800/30 px-6 py-2 rounded-lg font-bold hover:bg-green-900/60 transition-all text-sm">Aprobar Rutina</button>
                </div>
            </footer>
        </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import Sidebar from './Sidebar.vue';
import { adminApi, rutinasApi } from '../api';
import { useAuthStore } from '../stores/auth';
import { useRouter } from 'vue-router';

const authStore = useAuthStore();
const router = useRouter();

const activeTab = ref<'usuarios' | 'rutinas'>('usuarios');

// Usuarios state
const usuarios = ref<any[]>([]);
const loadingUsuarios = ref(false);

// Rutinas state
const rutinas = ref<any[]>([]);
const loadingRutinas = ref(false);
const rutinasFilter = ref<'ALL' | 'EnRevision'>('EnRevision');

// Detail view for admin
const selectedRoutineForView = ref<any>(null);
const routineDetailExercises = ref<any[]>([]);
const isLoadingDetail = ref(false);

const verRutina = async (routine: any) => {
    selectedRoutineForView.value = routine;
    isLoadingDetail.value = true;
    try {
        const res = await rutinasApi.getById(routine.id);
        routineDetailExercises.value = res.data?.ejercicios || [];
    } catch (e) {
        console.error("No se pudo cargar el detalle", e);
        alert("No se pudo cargar la información de los ejercicios.");
        selectedRoutineForView.value = null;
    } finally {
        isLoadingDetail.value = false;
    }
};

onMounted(async () => {
    // Check if admin
    if (!authStore.profile) {
        await authStore.fetchProfile();
    }
    
    if (authStore.profile?.rol !== 'Admin') {
        router.push('/dashboard');
        return;
    }

    // Load data
    loadUsuarios();
    loadRutinas();
});

// -- USUARIOS LOGIC --
const loadUsuarios = async () => {
    loadingUsuarios.value = true;
    try {
        const res = await adminApi.getUsuarios();
        usuarios.value = res.data;
    } catch (e) {
        console.error("Error loading users", e);
    } finally {
        loadingUsuarios.value = false;
    }
};

const toggleUserStatus = async (user: any) => {
    try {
        const nuevoEstado = !user.estaActivo;
        await adminApi.cambiarEstadoUsuario(user.id, nuevoEstado);
        user.estaActivo = nuevoEstado;
    } catch (e) {
        console.error("Error al cambiar estado", e);
        alert("Hubo un error al cambiar el estado del usuario.");
    }
};

const deleteUser = async (id: number) => {
    if (!confirm("¿Estás seguro de que deseas eliminar (baja lógica) a este usuario?")) return;
    try {
        await adminApi.eliminarUsuario(id);
        const u = usuarios.value.find(x => x.id === id);
        if (u) u.estaBorrado = true;
    } catch (e) {
        console.error("Error al eliminar usuario", e);
        alert("Hubo un error al eliminar el usuario.");
    }
};

// -- RUTINAS LOGIC --
const loadRutinas = async () => {
    loadingRutinas.value = true;
    try {
        const res = await adminApi.getTodasRutinas();
        rutinas.value = res.data;
    } catch (e) {
        console.error("Error loading routines", e);
    } finally {
        loadingRutinas.value = false;
    }
};

const rutinasPendientesCount = computed(() => {
    return rutinas.value.filter(r => r.estado === 'EnRevision').length;
});

const filteredRutinas = computed(() => {
    let result = rutinas.value;
    
    if (rutinasFilter.value === 'EnRevision') {
        result = result.filter(r => r.estado === 'EnRevision');
    }
    
    // Podemos ordenar para que las pendientes siempre salgan arriba si mostramos TODAS
    if (rutinasFilter.value === 'ALL') {
        result = [...result].sort((a, b) => {
            if (a.estado === 'EnRevision' && b.estado !== 'EnRevision') return -1;
            if (a.estado !== 'EnRevision' && b.estado === 'EnRevision') return 1;
            return 0;
        });
    }

    return result;
});

const validarRutina = async (id: number) => {
    try {
        await adminApi.validarRutina(id);
        const r = rutinas.value.find(x => x.id === id);
        if (r) r.estado = 'Publicada';
    } catch (e) {
        console.error("Error validando", e);
    }
};

const rechazarRutina = async (id: number) => {
    try {
        await adminApi.rechazarRutina(id);
        const r = rutinas.value.find(x => x.id === id);
        if (r) r.estado = 'Rechazada';
    } catch (e) {
        console.error("Error rechazando", e);
    }
};

const eliminarRutina = async (id: number) => {
    if (!confirm("¿Estás seguro de que deseas eliminar esta rutina?")) return;
    try {
        await adminApi.eliminarRutina(id);
        rutinas.value = rutinas.value.filter(x => x.id !== id);
    } catch (e) {
        console.error("Error eliminando", e);
    }
};

</script>
