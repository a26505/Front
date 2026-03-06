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
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import Sidebar from './Sidebar.vue';
import { adminApi } from '../api';
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
