<script setup lang="ts">
import { ref, watch } from 'vue';
import { useAuthStore } from '../../stores/auth'; // Adjust path if necessary

const props = defineProps(['modelValue']);
const emit = defineEmits(['update:modelValue']);

const auth = useAuthStore();
const name = ref('');
const email = ref('');
const password = ref('');
const dialog = ref(props.modelValue);

watch(() => props.modelValue, (val) => {
  dialog.value = val;
});

watch(dialog, (val) => {
  emit('update:modelValue', val);
});

const handleRegister = async () => {
    await auth.register({ name: name.value, email: email.value, password: password.value });
    dialog.value = false;
};
</script>

<template>
    <v-dialog v-model="dialog" max-width="500" class="backdrop-blur">
      <v-card class="pa-8 rounded-xl border-red bg-gradient-to-br from-grey-900 to-black position-relative" style="background: linear-gradient(135deg, #111827 0%, #000000 50%, #111827 100%);">
        <v-btn icon="mdi-close" variant="text" color="grey" class="position-absolute top-0 right-0 ma-4" @click="dialog = false"></v-btn>
        
        <div class="mb-8">
            <v-img src="/logo_placeholder.svg" max-width="100" contain class="mb-6"></v-img>
            <h2 class="text-h4 font-weight-bold lh-1" style="font-family: 'Outfit', sans-serif;">
            COMIENZA TU<br />
            VIAJE DE<br />
            <span class="text-red">TRANSFORMACIÓN</span>
            </h2>
        </div>

        <v-form @submit.prevent="handleRegister">
             <v-text-field v-model="name" placeholder="Nombre Completo" variant="underlined" color="red" bg-color="transparent" class="mb-2 text-white font-weight-medium" style="font-family: 'Outfit', sans-serif;"></v-text-field>
             <v-text-field v-model="email" placeholder="Email" variant="underlined" color="red" bg-color="transparent" class="mb-2 text-white font-weight-medium" style="font-family: 'Outfit', sans-serif;"></v-text-field>
             <v-text-field v-model="password" placeholder="Contraseña" type="password" variant="underlined" color="red" bg-color="transparent" class="mb-6 text-white font-weight-medium" style="font-family: 'Outfit', sans-serif;"></v-text-field>

             <v-btn type="submit" block color="red" size="x-large" rounded="xl" class="mt-4 font-weight-bold box-shadow-glowing text-white" style="letter-spacing: 1px;">
                CREAR CUENTA
             </v-btn>
        </v-form>

        <p class="text-center text-body-2 text-grey mt-6">
            ¿Ya tienes cuenta? <span class="text-white text-decoration-underline cursor-pointer hover-red" @click="dialog = true">Inicia sesión</span>
        </p>
      </v-card>
    </v-dialog>
</template>

<style scoped>
.backdrop-blur {
  backdrop-filter: blur(5px);
}
.border-red {
    border: 1px solid #DC2626 !important;
}
.text-red {
    color: #DC2626 !important;
}
.cursor-pointer {
    cursor: pointer;
}
.hover-red:hover {
    color: #DC2626 !important; 
}
.lh-1 {
    line-height: 1;
}
</style>
