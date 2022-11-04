import { createRouter, createWebHistory } from '@ionic/vue-router';
import { RouteRecordRaw } from 'vue-router';

import ClientSelection from '../views/ClientSelection.vue';
import IntentEdit from '../views/IntentEdit.vue';

const routes: Array<RouteRecordRaw> = [
  {
    path: '',
    component: ClientSelection
  },
  {
    path: '/EditConfig/:id',
    component: IntentEdit,
    props: true
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
