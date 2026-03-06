<template>
  <div class="flex min-h-screen bg-[#000000] text-white">
    <Sidebar active="admin" />

    <div class="flex-1 md:ml-[256px] flex flex-col min-h-screen">
      <!-- HEADER STICKY -->
      <header class="sticky top-0 z-40 w-full h-[80px] bg-black/95 backdrop-blur-md px-6 flex items-center justify-between border-b border-[#1F2937]/50">
        <div>
          <h1 class="text-2xl font-bold tracking-tight">Panel de Administración</h1>
          <p class="text-xs text-[#9CA3AF]">Gestiona usuarios y contenido de la comunidad</p>
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
                        @click="eliminarUsuario(user.id)" 
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
                        @click="verDetalle(rutina)" 
                        class="px-3 py-1 bg-blue-900/50 text-blue-400 rounded text-xs font-semibold hover:bg-blue-900/80 transition-colors"
                      >
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


        <!-- MODAL: DETALLE RUTINA PARA ADMIN -->
        <div v-if="selectedRoutine" class="fixed inset-0 z-50 flex items-center justify-center p-4">
          <div @click="selectedRoutine = null" class="absolute inset-0 bg-black/80 backdrop-blur-sm"></div>
          <div class="relative bg-[#0A0A0A] border border-[#1F2937] rounded-[24px] w-full max-w-[650px] max-h-[90vh] overflow-hidden flex flex-col shadow-[0_0_50px_rgba(0,0,0,0.5)] text-white animate-in zoom-in-95 duration-200">
            
            <!-- Banner Header -->
            <div class="relative h-[220px] w-full shrink-0 group">
              <img 
                :src="selectedRoutine.urlImagen || 'https://images.unsplash.com/photo-1534438327276-14e5300c3a48?q=80&w=1000'" 
                class="w-full h-full object-cover"
              />
              <div class="absolute inset-0 bg-gradient-to-t from-[#0A0A0A] via-[#0A0A0A]/40 to-transparent"></div>
              
              <!-- Badge Dificultad -->
              <div class="absolute top-6 left-6">
                <span class="bg-[#DC2626] text-white text-[10px] font-black uppercase tracking-widest px-3 py-1.5 rounded-md shadow-lg">
                  {{ selectedRoutine.nivel }}
                </span>
              </div>

              <!-- Titulo -->
              <div class="absolute bottom-6 left-8">
                <h2 class="text-4xl font-black italic uppercase tracking-tighter drop-shadow-2xl">{{ selectedRoutine.nombre }}</h2>
              </div>

              <!-- Close button overlay -->
              <button @click="selectedRoutine = null" class="absolute top-6 right-6 w-10 h-10 bg-black/40 hover:bg-black/60 rounded-full flex items-center justify-center text-white backdrop-blur-md transition-all">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
              </button>
            </div>
            
            <div class="px-8 py-6 flex-1 overflow-y-auto custom-scrollbar">
              <!-- Metadata Row -->
              <div class="flex flex-wrap items-center gap-6 mb-6 text-sm font-bold text-gray-300">
                <div class="flex items-center gap-2">
                  <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="#DC2626" stroke-width="2.5"><path d="M16 21v-2a4 4 0 00-4-4H5a4 4 0 00-4 4v2"/><circle cx="8.5" cy="7" r="4"/></svg>
                  <span>{{ routineDetailExercises.length }} ejercicios</span>
                </div>
                <div class="flex items-center gap-2">
                  <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="#DC2626" stroke-width="2.5"><circle cx="12" cy="12" r="10"/><polyline points="12 6 12 12 16 14"/></svg>
                  <span>{{ selectedRoutine.duracionMinutos }} min</span>
                </div>
                <div class="flex items-center gap-2">
                  <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="#DC2626" stroke-width="2.5"><path d="M20 21v-2a4 4 0 00-4-4H8a4 4 0 00-4 4v2"/><circle cx="12" cy="7" r="4"/></svg>
                  <span>Por {{ selectedRoutine.creadorNombre }}</span>
                </div>
              </div>

              <!-- Muscle Tags -->
              <div class="flex flex-wrap gap-2 mb-8" v-if="routineDetailExercises.length > 0">
                <div 
                  v-for="muscle in [...new Set(routineDetailExercises.map(e => e.grupoMuscular || 'Otro'))]" 
                  :key="muscle"
                  class="bg-red-900/20 border border-red-500/30 text-red-400 text-[10px] font-black uppercase tracking-widest px-4 py-2 rounded-lg"
                >
                  {{ muscle }}
                </div>
              </div>

              <!-- Descripcion -->
              <p v-if="selectedRoutine.descripcion" class="text-gray-400 text-sm italic mb-8 border-l-2 border-red-500/30 pl-4 py-1">
                "{{ selectedRoutine.descripcion }}"
              </p>

              <h3 class="text-xs font-black uppercase tracking-[0.2em] text-gray-500 mb-6 flex items-center gap-4">
                Ejercicios
                <div class="h-px flex-1 bg-gradient-to-r from-[#1F2937] to-transparent"></div>
              </h3>

              <div v-if="loadingDetail" class="flex flex-col items-center py-20">
                <div class="animate-spin rounded-full h-10 w-10 border-b-2 border-[#DC2626] mb-4"></div>
                <p class="text-[#9CA3AF] font-bold tracking-widest text-xs">Sincronizando rutinas...</p>
              </div>
              <div v-else class="space-y-4">
                <div v-for="(ej, idx) in routineDetailExercises" :key="idx" class="bg-[#111827] border border-[#1F2937] rounded-2xl p-5 flex items-center gap-5 transition-all hover:border-[#DC2626]/40 hover:bg-[#161B22] group">
                  <div class="w-12 h-12 bg-gradient-to-br from-[#DC2626] to-[#991B1B] rounded-full flex items-center justify-center font-black text-xl shadow-lg shadow-red-900/20 shrink-0 group-hover:scale-110 transition-transform">
                    {{ idx + 1 }}
                  </div>
                  <div class="flex-1 min-w-0">
                    <h4 class="font-black text-lg text-white mb-1 truncate">{{ ej.nombreEjercicio }}</h4>
                    <div class="flex flex-wrap gap-x-4 gap-y-2">
                       <div v-if="ej.grupoMuscular" class="bg-red-500/10 text-red-400 text-[9px] font-black px-2 py-0.5 rounded uppercase tracking-wider border border-red-500/20">{{ ej.grupoMuscular }}</div>
                       <div class="text-[11px] font-bold text-gray-400 flex items-center gap-4">
                          <span>Series: <span class="text-white">{{ ej.series }}</span></span>
                          <span>Reps: <span class="text-white">{{ ej.repeticiones }}</span></span>
                          <span>Descanso: <span class="text-white">{{ ej.descansoSegundos }}s</span></span>
                       </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <footer class="p-8 border-t border-[#1F2937]/50 flex flex-col gap-4 bg-[#0A0A0A]">
              <div class="flex justify-end gap-3">
                <button @click="selectedRoutine = null" class="px-6 py-3 border border-[#374151] rounded-xl text-sm font-bold hover:bg-white/5 transition-all shrink-0">Cerrar</button>
                <template v-if="selectedRoutine.estado === 'EnRevision'">
                  <button @click="rechazarRutina(selectedRoutine.id)" class="px-6 py-3 border border-orange-500/30 bg-orange-950/20 text-orange-400 hover:bg-orange-600 hover:text-white rounded-xl text-sm font-bold transition-all">Rechazar</button>
                  <button @click="validarRutina(selectedRoutine.id)" class="flex-1 px-6 py-3 bg-[#DC2626] hover:bg-red-700 text-white rounded-xl text-[16px] font-black uppercase tracking-widest transition-all shadow-lg shadow-red-900/20 flex items-center justify-center gap-2">
                    <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="4"><polyline points="20 6 9 17 4 12"/></svg>
                    Aprobar Rutina
                  </button>
                </template>
                <button v-else-if="selectedRoutine.estado === 'Publicada'" @click="eliminarRutina(selectedRoutine.id)" class="flex-1 px-6 py-3 bg-red-950/20 border border-red-500/30 text-red-500 hover:bg-red-600 hover:text-white rounded-xl text-[16px] font-black uppercase tracking-widest transition-all">
                  Eliminar de la Comunidad
                </button>
              </div>
            </footer>
          </div>
        </div>

      </main>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import Sidebar from './Sidebar.vue';
import { adminApi } from '../api';
import { useAuthStore } from '../stores/auth';
import { useRouter } from 'vue-router';
import { useUIStore } from '../stores/ui';

const authStore = useAuthStore();
const router = useRouter();
const uiStore = useUIStore();

const activeTab = ref<'usuarios' | 'rutinas'>('usuarios');

// Usuarios state
const usuarios = ref<any[]>([]);
const loadingUsuarios = ref(false);

// Rutinas state
const rutinas = ref<any[]>([]);
const loadingRutinas = ref(false);
const rutinasFilter = ref<'ALL' | 'EnRevision'>('EnRevision');

// Detalle Rutina Admin
const selectedRoutine = ref<any>(null);
const routineDetailExercises = ref<any[]>([]);
const loadingDetail = ref(false);

const verDetalle = async (rutina: any) => {
  selectedRoutine.value = rutina;
  loadingDetail.value = true;
  try {
    // Reutilizamos el endpoint público ya que el admin tiene permisos
    const { rutinasApi } = await import('../api');
    const res = await rutinasApi.getById(rutina.id);
    routineDetailExercises.value = res.data?.ejercicios || [];
  } catch (e) {
    console.error("Error cargando detalle", e);
  } finally {
    loadingDetail.value = false;
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

const eliminarUsuario = async (id: number) => {
    const confirm = await uiStore.showConfirm("Baja de Usuario", "¿Estás seguro de que deseas eliminar (baja lógica) a este usuario?", { type: 'danger' });
    if (!confirm) return;
    try {
        await adminApi.eliminarUsuario(id);
        const u = usuarios.value.find(x => x.id === id);
        if (u) u.estaBorrado = true;
        uiStore.showToast("Usuario eliminado correctamente");
    } catch (e) {
        console.error("Error al eliminar usuario", e);
        uiStore.showToast("Hubo un error al eliminar el usuario.", "error");
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
        if (selectedRoutine.value?.id === id) selectedRoutine.value = null;
    } catch (e) {
        console.error("Error validando", e);
    }
};

const rechazarRutina = async (id: number) => {
    try {
        await adminApi.rechazarRutina(id);
        const r = rutinas.value.find(x => x.id === id);
        if (r) r.estado = 'Rechazada';
        if (selectedRoutine.value?.id === id) selectedRoutine.value = null;
    } catch (e) {
        console.error("Error rechazando", e);
    }
};

const eliminarRutina = async (id: number) => {
    const confirm = await uiStore.showConfirm("Borrar Rutina", "¿Estás seguro de que deseas eliminar esta rutina?", { type: 'danger' });
    if (!confirm) return;
    try {
        await adminApi.eliminarRutina(id);
        rutinas.value = rutinas.value.filter(x => x.id !== id);
        uiStore.showToast("Rutina eliminada");
    } catch (e) {
        console.error("Error eliminando", e);
        uiStore.showToast("Error al eliminar rutina", "error");
    }
};

</script>
