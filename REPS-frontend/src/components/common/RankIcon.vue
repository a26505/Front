<template>
  <div class="rank-icon" :style="{ width: size + 'px', height: size + 'px' }">
    <img :src="iconSrc" :alt="rank" class="rank-svg" />
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue';

const props = defineProps({
  rank: {
    type: String,
    required: true,
    validator: (value: string) => ['bronce', 'plata', 'oro', 'platino', 'diamante', 'leyenda'].includes(value.toLowerCase())
  },
  size: {
    type: [Number, String],
    default: 60
  }
});

const iconSrc = computed(() => {
  const rankLower = props.rank.toLowerCase();
  return `/ranks/${rankLower}.svg`;
});
</script>

<style scoped>
.rank-icon {
  display: flex;
  align-items: center;
  justify-content: center;
  transition: transform 0.3s ease;
}

.rank-icon:hover {
  transform: scale(1.1);
}

.rank-svg {
  width: 100%;
  height: 100%;
  object-fit: contain;
}
</style>
