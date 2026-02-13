<script setup lang="ts">
import { ref } from 'vue';

const form = ref({
  name: '',
  email: '',
  subject: '',
  message: ''
});

const submitForm = () => {
    // Basic validation
    if (!form.value.name || !form.value.email || !form.value.subject || !form.value.message) {
        alert("Por favor completa todos los campos obligatorios.");
        return;
    }
    alert("¡Mensaje enviado con éxito!");
    form.value = { name: '', email: '', subject: '', message: '' };
};
</script>

<template>
  <div class="contact-form-height h-full">
      <div class="form-container h-full">
        <h3 class="text-h5 font-weight-bold text-white mb-6">Envíanos un Mensaje</h3>

        <form @submit.prevent="submitForm">
             <div class="form-group mb-4">
                <label for="name" class="form-label">Nombre Completo <span class="text-red">*</span></label>
                <input type="text" id="name" v-model="form.name" placeholder="Tu nombre" class="form-input" required>
             </div>

             <div class="form-group mb-4">
                <label for="email" class="form-label">Email <span class="text-red">*</span></label>
                <input type="email" id="email" v-model="form.email" placeholder="tu@email.com" class="form-input" required>
             </div>

             <div class="form-group mb-4">
                <label for="subject" class="form-label">Asunto <span class="text-red">*</span></label>
                <input type="text" id="subject" v-model="form.subject" placeholder="¿En qué podemos ayudarte?" class="form-input" required>
             </div>

             <div class="form-group mb-6">
                <label for="message" class="form-label">Mensaje <span class="text-red">*</span></label>
                <textarea id="message" v-model="form.message" placeholder="Escribe tu mensaje aquí..." class="form-textarea" required></textarea>
             </div>

             <button type="submit" class="submit-btn">ENVIAR MENSAJE</button>
             <p class="text-center text-grey-darken-1 mt-4 text-caption">
                 <span class="text-red">*</span> Todos los campos son obligatorios
             </p>
        </form>
      </div>
  </div>
</template>

<style scoped>
.text-red { color: #DC2626 !important; }
.text-grey-darken-1 { color: #9CA3AF !important; }

.form-container {
  background-color: rgba(17, 24, 39, 0.5);
  backdrop-filter: blur(4px);
  border: 1px solid #1F2937;
  border-radius: 16px;
  padding: 32px; /* reduced padding slightly to fit */
  transition: border-color 0.3s ease;
  height: 100%;
}

.form-container:hover {
    border-color: rgba(220, 38, 38, 0.3);
}

.form-label {
    display: block;
    font-size: 0.875rem;
    font-weight: 600;
    color: #D1D5DB;
    margin-bottom: 8px;
}

.form-input, .form-textarea {
    width: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    border: 1px solid #374151;
    border-radius: 8px;
    padding: 12px 16px;
    color: #FFFFFF;
    font-size: 1rem;
    transition: all 0.2s ease;
}

.form-textarea {
    height: 140px;
    resize: none;
}

.form-input:focus, .form-textarea:focus {
    border-color: #DC2626;
    outline: none;
}

.submit-btn {
    width: 100%;
    background-color: #DC2626;
    color: #FFFFFF;
    font-weight: 700;
    font-size: 1rem;
    padding: 14px 48px;
    border-radius: 8px;
    cursor: pointer;
    transition: background-color 0.3s ease;
    margin-top: 24px;
}

.contact-form-section {
    background-color: transparent;
    margin-bottom: 128px;
}

.submit-btn:hover {
    background-color: #B91C1C;
}
</style>
