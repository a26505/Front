<script setup lang="ts">
const emit = defineEmits(['open-signup']);
</script>

<template>
  <div class="cta-section mb-16">
    <div class="cta-container pa-16 text-center position-relative">
       <!-- Background Glow Effect via pseudo-element or container style -->
       
       <h2 class="text-h3 font-weight-bold text-white mb-6">
         ¿Listo para <span class="text-red">Unirte</span>?
       </h2>
       
       <p class="text-h6 text-grey-lighten-1 mx-auto mb-10" style="max-width: 768px;">
         Forma parte de una comunidad que te apoya, te motiva y celebra cada uno de tus logros
       </p>
       
       <button class="cta-button" @click="emit('open-signup')">
         ÚNETE A LA COMUNIDAD
       </button>
    </div>
  </div>
</template>

<style scoped>
.text-red { color: #DC2626 !important; }
.text-grey-lighten-1 { color: #9CA3AF !important; }

.cta-container {
    background: linear-gradient(180deg, #12121C 0%, #0A0A12 100%);
    border: 1px solid rgba(220, 38, 38, 0.3);
    border-radius: 24px;
    position: relative;
    overflow: hidden;
    z-index: 1;
}

/* Large red glow behind */
.cta-container::before {
    content: '';
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 60%;
    height: 60%;
    background: rgba(220, 38, 38, 0.2);
    filter: blur(80px); /* 3xl blur equivalent */
    z-index: -1;
    pointer-events: none;
}

.cta-button {
    background-color: #DC2626;
    color: white;
    font-size: 18px;
    font-weight: bold;
    padding: 20px 48px;
    border-radius: 9999px; /* pill */
    border: none;
    cursor: pointer;
    transition: all 0.3s ease;
    letter-spacing: 0.5px;
    position: relative;
    overflow: hidden;
}

.cta-button:hover {
    background-color: #B91C1C;
    transform: scale(1.05);
    box-shadow: 0 10px 25px -5px rgba(220, 38, 38, 0.4), 0 8px 10px -6px rgba(220, 38, 38, 0.4);
}

/* Gradient overlay on hover for button */
.cta-button::after {
    content: '';
    position: absolute;
    top: 0; left: 0; right: 0; bottom: 0;
    background: linear-gradient(to right, #B91C1C, #EA580C);
    opacity: 0;
    transition: opacity 0.3s ease;
    z-index: -1;
}

.cta-button:hover::after {
    opacity: 1; /* This might hide text if z-index is wrong, but text is inside button content */
}
</style>
