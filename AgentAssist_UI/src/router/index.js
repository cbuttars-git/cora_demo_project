import { createRouter, createWebHistory } from '@ionic/vue-router';
import ClientSelection from '../views/ClientSelection.vue';
import Home from '../views/Home.vue';
const routes = [
    {
        path: '/',
        component: ClientSelection,
    },
    {
        path: '/:name',
        component: Home,
        props: true
    }
];
const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
});
export default router;
//# sourceMappingURL=index.js.map