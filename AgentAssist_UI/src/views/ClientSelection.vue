<template>
  <ion-header :translucent="true">
    <ion-toolbar>
      <ion-title slot="start">Agent Assist</ion-title>
      <img slot="end" src="/assets/img/navient.png" title="Navient" class="logo" />
    </ion-toolbar>
  </ion-header>

  <ion-content class="ion-padding">
    <ion-grid>
      <ion-row>
        <ion-col
          sizeXs="12"
          sizeSm="12"
          sizeMd="6"
          size="6"
          sizeLg="4"
          sizeXl="3"
          v-for="(client, i) in clients"
          :key="i">
          <ion-card>
            <ion-card-header>
              <ion-toolbar>
                <ion-card-title>
                  <img class="clientLogo" :src="client.logo"/>
                </ion-card-title>
              </ion-toolbar>
            </ion-card-header>

            <ion-card-content>
              <h1>{{ client.name }}</h1>
              <div class="launch_button">
                <ion-button
                  color="primary"
                  fill="solid"
                  :href="`/${client.name}`"
                >
                  Launch
                </ion-button>
              </div>
            </ion-card-content>
          </ion-card>
        </ion-col>
      </ion-row>
    </ion-grid>

    <ion-loading
      :is-open="loading"
      message="Please wait..."
      :duration="timeout"
      @didDismiss="setLoading(false)"
    >
    </ion-loading>
  </ion-content>
</template>

<script lang="ts">
import {
  IonHeader,
  IonTitle,
  IonToolbar,
  IonButton,
  IonCol,
  IonGrid,
  IonLoading,
  IonRow,
  IonCard,
  IonCardContent,
  IonCardHeader,
  IonCardTitle,
  IonContent,
} from "@ionic/vue";
import { defineComponent, onBeforeMount } from "vue";

import useAgentAssist from '../hooks/agentassist';

export default defineComponent({
  name: "ClientSelection",
  props: {
    timeout: { type: Number, default: 30000 }
  },
  components: {
    IonHeader,
    IonTitle,
    IonToolbar,
    IonButton,
    IonCol,
    IonGrid,
    IonLoading,
    IonRow,
    IonCard,
    IonCardContent,
    IonCardHeader,
    IonCardTitle,
    IonContent,
  },
  setup() {
    const { loading, clients, getClientList } = useAgentAssist();
    const setLoading  = (state: boolean) => loading.value = state;

    onBeforeMount(() => {
      getClientList();
    });

    return {
      loading,
      setLoading,
      clients
    };
  }
});
</script>

<style scoped>
.logo {
  padding-inline-start: 20px;
  padding-inline-end: 20px;
}

.clientLogo {
  height: 42px;
}

.card {
  width: 300px;
}

.launch_button {
  margin: auto;
  padding-top: 20px;
}
</style>
