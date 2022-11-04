import { createRouter, createWebHistory } from '@ionic/vue-router';
import ClientSelection from '../views/ClientSelection.vue';
import IntentEdit from '../views/IntentEdit.vue';
import { registerGuard } from './Guard';
const routes = [
    {
        path: '',
        component: ClientSelection,
        meta: {
            requiresAuth: true
        }
    },
    {
        path: '/EditConfig/:id',
        component: IntentEdit,
        props: true,
        meta: {
            requiresAuth: true
        }
    }
];
const router = createRouter({
    history: createWebHistory(),
    routes
});
registerGuard(router);
export default router;
//# sourceMappingURL=index.js.map