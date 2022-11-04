<template>
  <ion-header :translucent="true">
    <ion-toolbar>
      <ion-title slot="start">Instant Assistant</ion-title>
      <img slot="end" src="/assets/images/navient.png" title="Navient" class="logo" />
    </ion-toolbar>
  </ion-header>

  <ion-content class="ion-padding">
    <ion-button v-if="instantAssistant.isAdmin" fill="none" @click="presentNewClientPrompt">
      <ion-icon :ios="add" :md="add"></ion-icon>
    </ion-button>
    <ion-grid>
      <ion-row>
        <ion-col
          sizeXs="12"
          sizeSm="12"
          sizeMd="6"
          size="6"
          sizeLg="4"
          sizeXl="3"
          v-for="(client, i) in instantAssistant.clients"
          :key="i">
          <ion-card>
            <ion-card-header>
              <ion-toolbar>
                <ion-card-title>
                  <img class="clientLogo" :src="client.logo"/>
                </ion-card-title>
                <ion-buttons v-if="instantAssistant.isAdmin" slot="end">
                  <ion-button @click="presentDeleteClientPrompt(client)">
                    <ion-icon :ios="trash" :md="trash"></ion-icon>
                  </ion-button>
                </ion-buttons>
              </ion-toolbar>
            </ion-card-header>

            <ion-card-content>
              <h1>{{ client.name }}</h1>
              <div class="edit_button">
                <ion-button
                  color="primary"
                  fill="solid"
                  :href="`/EditConfig/${client.id}`"
                >
                  Edit Config
                </ion-button>
              </div>
            </ion-card-content>
          </ion-card>
        </ion-col>
      </ion-row>
    </ion-grid>
  </ion-content>
</template>

<script lang="ts">
import {
  IonHeader,
  IonTitle,
  IonToolbar,
  IonButton,
  IonButtons,
  IonCol,
  IonGrid,
  IonRow,
  IonCard,
  IonCardContent,
  IonCardHeader,
  IonCardTitle,
  IonContent,
  IonIcon,
  alertController,
} from "@ionic/vue";
import { defineComponent, onBeforeMount } from "vue";
import { add, trash } from "ionicons/icons";

import useInstantAssistant from "../store";

export default defineComponent({
  name: "ClientSelection",
  components: {
    IonHeader,
    IonTitle,
    IonToolbar,
    IonButton,
    IonButtons,
    IonCol,
    IonGrid,
    IonRow,
    IonCard,
    IonCardContent,
    IonCardHeader,
    IonCardTitle,
    IonContent,
    IonIcon,
  },
  setup() {
    const instantAssistant = useInstantAssistant();

    onBeforeMount(() => {
      sessionStorage.removeItem("clientsInfo");
      instantAssistant.fetchClientList();
      instantAssistant.fetchIsAdmin();
    });

    return {
      add,
      trash,
      instantAssistant
    };
  },
  methods: {
    async presentNewClientPrompt() {
      const alert = await alertController.create({
        header: "New Instant Assistant",
        inputs: [
          {
            name: "name",
            id: "name",
            value: "",
            placeholder: "Name",
          },
        ],
        buttons: [
          {
            text: "Cancel",
            role: "cancel",
            cssClass: "secondary",
          },
          {
            text: "Create",
            handler: (data) => {
              this.instantAssistant.createClient(data.name);
            },
          },
        ],
      });
      return alert.present();
    },
    async presentDeleteClientPrompt(client: { name: any; id: number; }) {
      const alert = await alertController.create({
        header: `Delete ${client.name}?`,
        message: `Are you sure you would like to delete the ${client.name} Instant Assistant?`,
        buttons: [
          {
            text: "Cancel",
            role: "cancel",
            cssClass: "secondary",
          },
          {
            text: "Delete",
            handler: () => {
              this.instantAssistant.deleteClient(client.id);
            },
          },
        ],
      });
      return alert.present();
    },
  },
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

.edit_button {
  margin: auto;
  padding-top: 20px;
}
</style>
