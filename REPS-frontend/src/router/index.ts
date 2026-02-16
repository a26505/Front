import { createRouter, createWebHistory } from 'vue-router'
import LandingPage from '../components/LandingPage.vue'
import CommunityPage from '../components/CommunityPage.vue'
import AboutPage from '../components/AboutPage.vue'


const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            name: 'home',
            component: LandingPage,
        },
        {
            path: '/community',
            name: 'community',
            component: CommunityPage,
        },
        {
            path: '/about',
            name: 'about',
            component: AboutPage,
        },
        // Login route removed as it is now integrated into LandingPage
        // {
        //     path: '/login',
        //     name: 'login',
        //     component: LoginRegister,
        // },
        // Add more routes here later (login, dashboard, etc)
    ],
})

export default router
