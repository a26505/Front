import { createRouter, createWebHistory } from 'vue-router'
import LandingPage from '../components/LandingPage.vue'
import CommunityPage from '../components/CommunityPage.vue'
import CommunityPublicPage from '../components/CommunityPublicPage.vue'
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
            meta: { requiresAuth: true },
        },
        {
            path: '/community-info',
            name: 'community-info',
            component: CommunityPublicPage,
        },
        {
            path: '/about',
            name: 'about',
            component: AboutPage,
        },
        {
            path: '/select-plan',
            name: 'select-plan',
            component: () => import('../components/SelectPlan.vue'),
            meta: { requiresAuth: true },
        },
        {
            path: '/dashboard',
            name: 'dashboard',
            component: () => import('../components/Dashboard.vue'),
            meta: { requiresAuth: true },
        },
        {
            path: '/workouts',
            name: 'workouts',
            component: () => import('../components/WorkoutsPage.vue'),
            meta: { requiresAuth: true },
        },
        {
            path: '/progress',
            name: 'progress',
            component: () => import('../components/ProgressPage.vue'),
            meta: { requiresAuth: true },
        },
        {
            path: '/profile',
            name: 'profile',
            component: () => import('../components/ProfilePage.vue'),
            meta: { requiresAuth: true },
        },
        {
            path: '/settings',
            name: 'settings',
            component: () => import('../components/SettingsPage.vue'),
            meta: { requiresAuth: true },
        },
        {
            path: '/admin',
            name: 'admin',
            component: () => import('../components/AdminPage.vue'),
            meta: { requiresAuth: true },
        },
    ],
})

// Guard de navegación: redirige a landing si no autenticado
router.beforeEach((to) => {
    // Reset scroll al cambiar de pantalla
    window.scrollTo(0, 0);

    const isAuthenticated = !!localStorage.getItem('token')

    // Si intenta ir a comunidad y NO está logueado, lo mandamos a la página pública
    if (to.name === 'community' && !isAuthenticated) {
        return { name: 'community-info' }
    }

    if (to.meta.requiresAuth && !isAuthenticated) {
        return { name: 'home' }
    }
})

export default router
