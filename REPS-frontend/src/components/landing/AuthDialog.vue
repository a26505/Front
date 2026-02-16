<script setup lang="ts">
import { ref, watch, computed } from 'vue';
import { useAuthStore } from '../../stores/auth'; // Adjust path if necessary
import { useRouter } from 'vue-router'

const props = defineProps(['modelValue']);
const emit = defineEmits(['update:modelValue']);

const router = useRouter()
const authStore = useAuthStore()

// State
const isLogin = ref(true) // Default to Login
const name = ref('')
const email = ref('')
const password = ref('')
const isLoading = ref(false)
const errorMessage = ref('')

const dialog = ref(props.modelValue);

watch(() => props.modelValue, (val) => {
  dialog.value = val;
});

watch(dialog, (val) => {
  emit('update:modelValue', val);
});

// Toggle between Login and Register
const toggleMode = () => {
    isLogin.value = !isLogin.value
    errorMessage.value = ''
}

const handleSubmit = async () => {
    errorMessage.value = ''
    
    // Basic validation
    if (!email.value || !password.value || (!isLogin.value && !name.value)) {
        errorMessage.value = 'Por favor completa todos los campos requeridos.'
        return
    }

    isLoading.value = true
    try {
        if (isLogin.value) {
            await authStore.login(email.value, password.value)
        } else {
            await authStore.register({
                nombre: name.value,
                email: email.value,
                password: password.value
            })
        }
        // Success
        dialog.value = false; // Close dialog
        router.push('/') // Ensure we are on home/dashboard (or reload to update state)
        // Optionally show success message
    } catch (error: any) {
        errorMessage.value = error.message || 'Ocurrió un error. Verifica tus datos.'
    } finally {
        isLoading.value = false
    }
}
</script>

<template>
    <v-dialog v-model="dialog" max-width="480" class="backdrop-blur">
      <v-card class="pa-8 rounded-xl border-red bg-gradient-to-br from-grey-900 to-black position-relative" style="background: linear-gradient(135deg, #111827 0%, #000000 50%, #111827 100%);">
        <v-btn icon="mdi-close" variant="text" color="grey" class="position-absolute top-0 right-0 ma-4" @click="dialog = false"></v-btn>
        
        <div class="mb-8 text-center">
            <div class="d-inline-block px-3 py-1 mb-4 border border-red-900 rounded bg-black/50">
                 <span class="text-h6 font-weight-black text-white tracking-widest">REPS<span class="text-red">.</span></span>
            </div>

            <h2 class="text-h4 font-weight-bold lh-1 text-white text-uppercase" style="font-family: 'Outfit', sans-serif;">
                {{ isLogin ? 'BIENVENIDO' : 'COMIENZA TU' }}<br />
                {{ isLogin ? 'DE NUEVO' : 'VIAJE DE' }}<br />
                <span class="text-red">{{ isLogin ? 'A ENTRENAR' : 'TRANSFORMACIÓN' }}</span>
            </h2>
        </div>

        <v-form @submit.prevent="handleSubmit">
             <v-expand-transition>
                <div v-if="!isLogin">
                    <v-text-field 
                        v-model="name" 
                        placeholder="Nombre Completo" 
                        variant="underlined" 
                        color="red" 
                        bg-color="transparent" 
                        class="mb-2 text-white font-weight-medium" 
                        style="font-family: 'Outfit', sans-serif;"
                    ></v-text-field>
                </div>
            </v-expand-transition>

             <v-text-field v-model="email" placeholder="Email" variant="underlined" color="red" bg-color="transparent" class="mb-2 text-white font-weight-medium" style="font-family: 'Outfit', sans-serif;"></v-text-field>
             <v-text-field v-model="password" placeholder="Contraseña" type="password" variant="underlined" color="red" bg-color="transparent" class="mb-6 text-white font-weight-medium" style="font-family: 'Outfit', sans-serif;"></v-text-field>

            <!-- Error Alert -->
            <v-alert
                v-if="errorMessage"
                type="error"
                variant="tonal"
                color="red"
                density="compact"
                class="mb-4 text-caption"
            >
                {{ errorMessage }}
            </v-alert>

             <v-btn type="submit" block color="red" size="x-large" rounded="xl" class="mt-4 font-weight-bold box-shadow-glowing text-white" style="letter-spacing: 1px;" :loading="isLoading">
                {{ isLogin ? 'INICIAR SESIÓN' : 'CREAR CUENTA' }}
             </v-btn>
        </v-form>

        <p class="text-center text-body-2 text-grey mt-6 mb-0">
            {{ isLogin ? '¿Aún no tienes cuenta?' : '¿Ya tienes cuenta?' }} 
            <span class="text-white text-decoration-underline cursor-pointer hover-red font-weight-bold" @click="toggleMode">
                {{ isLogin ? 'Regístrate' : 'Inicia sesión' }}
            </span>
        </p>
      </v-card>
    </v-dialog>
</template>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Outfit:wght@300;400;600;700;800&display=swap');

.backdrop-blur {
  backdrop-filter: blur(8px);
}
.border-red {
    border: 1px solid #DC2626 !important;
    box-shadow: 0 0 20px rgba(220, 38, 38, 0.2);
}
.text-red {
    color: #DC2626 !important;
}
.cursor-pointer {
    cursor: pointer;
}
.hover-red:hover {
    color: #DC2626 !important; 
    transition: color 0.3s ease;
}
.lh-1 {
    line-height: 1.1;
}
.box-shadow-glowing {
    box-shadow: 0 4px 15px rgba(220, 38, 38, 0.4);
}
</style>
