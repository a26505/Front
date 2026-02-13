<script setup lang="ts">
const contactCards = [
  {
    icon: 'email',
    title: 'Email',
    main: 'contacto@reps.com',
    secondary: 'soporte@reps.com'
  },
  {
    icon: 'phone',
    title: 'Teléfono',
    main: '+34 900 123 456',
    secondary: 'Lun - Vie: 9:00 - 18:00'
  },
  {
    icon: 'location',
    title: 'Ubicación',
    main: 'Madrid, España',
    secondary: 'Calle Innovación 123'
  },
  {
    icon: 'clock',
    title: 'Horario de Atención',
    rows: ['Lunes - Viernes: 9:00 - 18:00', 'Sábados: 10:00 - 14:00', 'Domingos: Cerrado'],
    isHighlight: true
  }
];
</script>

<template>
  <div class="contact-info-list h-full">
      <div v-for="(card, index) in contactCards" :key="index" class="info-card mb-4" :class="{ 'highlight-card': card.isHighlight }">
         <!-- Icon Left -->
         <div class="icon-wrapper mr-6">
             <div class="icon-box">
                <svg v-if="card.icon === 'email'" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <rect x="3" y="5" width="18" height="14" rx="2"/><path d="M3 7l9 6 9-6"/>
                </svg>
                <svg v-if="card.icon === 'phone'" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <path d="M22 16.92v3a2 2 0 0 1-2.18 2 19.79 19.79 0 0 1-8.63-3.07 19.5 19.5 0 0 1-6-6 19.79 19.79 0 0 1-3.07-8.67A2 2 0 0 1 4.11 2h3a2 2 0 0 1 2 1.72 12.84 12.84 0 0 0 .7 2.81 2 2 0 0 1-.45 2.11L8.09 9.91a16 16 0 0 0 6 6l1.27-1.27a2 2 0 0 1 2.11-.45 12.84 12.84 0 0 0 2.81.7A2 2 0 0 1 22 16.92z"/>
                </svg>
                <svg v-if="card.icon === 'location'" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z"/><circle cx="12" cy="10" r="3"/>
                </svg>
                <svg v-if="card.icon === 'clock'" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <circle cx="12" cy="12" r="10"/><path d="M12 6v6l4 2"/>
                </svg>
             </div>
         </div>
         
         <!-- Text Right -->
         <div class="content flex-1 text-left">
             <h3 class="card-title text-white font-weight-bold mb-1">{{ card.title }}</h3>
             
             <template v-if="card.isHighlight">
                 <div v-for="(row, r) in card.rows" :key="r" class="text-caption text-grey-lighten-1">{{ row }}</div>
             </template>
             <template v-else>
                 <div class="card-main text-red-darken-1 mb-1 font-weight-medium">{{ card.main }}</div>
                 <div class="card-secondary text-grey-lighten-1 text-caption">{{ card.secondary }}</div>
             </template>
         </div>
      </div>
  </div>
</template>

<style scoped>
.text-red-darken-1 { color: #991B1B !important; } /* Darker red text per screenshot */
.text-grey-lighten-1 { color: #9CA3AF !important; }

/* Cards */
.info-card {
    background-color: rgba(17, 24, 39, 0.5);
    backdrop-filter: blur(4px);
    border: 1px solid #1F2937;
    border-radius: 12px;
    padding: 24px;
    display: flex;
    align-items: center; /* Horizontal layout */
    transition: all 0.3s ease;
}

.info-card:hover {
    background-color: rgba(17, 24, 39, 0.7);
    border-color: #DC2626;
    transform: translateX(5px);
}

.icon-box {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 48px;
    height: 48px;
    border-radius: 8px;
    background-color: rgba(220, 38, 38, 0.1);
    color: #DC2626;
}

.highlight-card {
    background: linear-gradient(135deg, #1F0A0A 0%, #290B0B 100%);
    border-color: rgba(220, 38, 38, 0.2);
}

.highlight-card .contact-info-section {
    background-color: transparent;
    margin-bottom: 128px;
}

.highlight-card .icon-box {
    background-color: transparent;
    color: #EF4444;
}

.card-title {
    font-size: 1.125rem;
}
</style>
