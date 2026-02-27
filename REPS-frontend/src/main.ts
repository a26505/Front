import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify'
import Vue3ApexCharts from "vue3-apexcharts";

// Styles
import './style.css' // We will put global styles here later or remove

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(vuetify)
app.use(Vue3ApexCharts)

app.mount('#app')
