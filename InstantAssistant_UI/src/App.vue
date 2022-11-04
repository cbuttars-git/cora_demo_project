<template>
  <ion-app>
    <ion-split-pane content-id="main-content" :when="instantAssistant.showSideMenu">
      <ion-menu content-id="main-content">
        <ion-header :translucent="true">
          <ion-toolbar>
            <ion-buttons slot="start">
              <ion-button href="/">
                <ion-icon
                  :ios="arrowBackOutline"
                  :md="arrowBackOutline"
                ></ion-icon>
              </ion-button>
            </ion-buttons>

            <img src="/assets/images/navient.png" title="Navient" />

            <ion-buttons slot="end">
              <ion-button
                @click="instantAssistant.saveClientsInfo()"
                :disabled="!clientsInfoHasChanged">
                save
              </ion-button>

              <ion-button @click="toggleClientActive">
                <ion-icon
                  v-if="instantAssistant.clientsInfo.iaConfigData.active"
                  :ios="eyeOutline"
                  :md="eyeOutline"
                ></ion-icon>
                <ion-icon
                  v-if="!instantAssistant.clientsInfo.iaConfigData.active"
                  :ios="eyeOffOutline"
                  :md="eyeOffOutline"
                ></ion-icon>
              </ion-button>
            </ion-buttons>
          </ion-toolbar>
        </ion-header>

        <ion-content>
          <ion-list id="inbox-list">
            <ion-list-header>Instant Assistant Config</ion-list-header>
            <ion-note>{{ instantAssistant.clientsInfo.chatBotName }}</ion-note>

            <ion-accordion-group>
              <ion-accordion value="basic">
                <ion-item slot="header">
                  <ion-label>Basic</ion-label>
                </ion-item>

                <ion-list slot="content">
                  <ion-item>
                    <ion-label>Name:</ion-label>
                    <ion-input v-model="instantAssistant.clientsInfo.chatBotName"></ion-input>
                  </ion-item>

                  <ion-item>
                    <ion-label>Description:</ion-label>
                    <ion-input
                      v-model="instantAssistant.clientsInfo.iaConfigData.description"
                    ></ion-input>
                  </ion-item>

                  <ion-item>
                    <ion-label>User AD Group:</ion-label>
                    <ion-input
                      v-model="instantAssistant.clientsInfo.iaConfigData.userAdGroup"
                    ></ion-input>
                  </ion-item>

                  <ion-item>
                    <ion-label>Admin AD Group:</ion-label>
                    <ion-input
                      v-model="instantAssistant.clientsInfo.iaConfigData.adminAdGroup"
                    ></ion-input>
                  </ion-item>

                  <ion-item>
                    <ion-label>Query Placeholder:</ion-label>
                    <ion-input
                      v-model="instantAssistant.clientsInfo.iaConfigData.placeholder"
                    ></ion-input>
                  </ion-item>
                </ion-list>
              </ion-accordion>

              <ion-accordion value="theme">
                <ion-item slot="header">
                  <ion-label>Theme</ion-label>
                </ion-item>

                <ion-list slot="content">
                  <ion-item>
                    <ion-label>Header Background:</ion-label>
                    <ion-buttons slot="end">
                      <color-picker
                        v-model:pureColor="
                          instantAssistant.clientsInfo.iaConfigData.displayConfig.headerBgColor
                        "
                        shape="circle"
                        format="hex"
                        useType="pure"
                        :disableAlpha="true"
                      />
                    </ion-buttons>
                  </ion-item>

                  <ion-item>
                    <ion-label>Header Foreground:</ion-label>
                    <ion-buttons slot="end">
                      <color-picker
                        v-model:pureColor="
                          instantAssistant.clientsInfo.iaConfigData.displayConfig.headerFgColor
                        "
                        shape="circle"
                        format="hex"
                        useType="pure"
                        :disableAlpha="true"
                      />
                    </ion-buttons>
                  </ion-item>

                  <ion-item>
                    <ion-label>Footer Background:</ion-label>
                    <ion-buttons slot="end">
                      <color-picker
                        v-model:pureColor="
                          instantAssistant.clientsInfo.iaConfigData.displayConfig.footerBgColor
                        "
                        shape="circle"
                        format="hex"
                        useType="pure"
                        :disableAlpha="true"
                      />
                    </ion-buttons>
                  </ion-item>

                  <ion-item>
                    <ion-label>Footer Foreground:</ion-label>
                    <ion-buttons slot="end">
                      <color-picker
                        v-model:pureColor="
                          instantAssistant.clientsInfo.iaConfigData.displayConfig.footerFgColor
                        "
                        shape="circle"
                        format="hex"
                        useType="pure"
                        :disableAlpha="true"
                      />
                    </ion-buttons>
                  </ion-item>

                  <ion-item>
                    <ion-label>Dialog Background:</ion-label>
                    <ion-buttons slot="end">
                      <color-picker
                        v-model:pureColor="
                          instantAssistant.clientsInfo.iaConfigData.displayConfig.dialogBgColor
                        "
                        shape="circle"
                        format="hex"
                        useType="pure"
                        :disableAlpha="true"
                      />
                    </ion-buttons>
                  </ion-item>

                  <ion-item>
                    <ion-label>Dialog Foreground:</ion-label>
                    <ion-buttons slot="end">
                      <color-picker
                        v-model:pureColor="
                          instantAssistant.clientsInfo.iaConfigData.displayConfig.dialogFgColor
                        "
                        shape="circle"
                        format="hex"
                        useType="pure"
                        :disableAlpha="true"
                      />
                    </ion-buttons>
                  </ion-item>

                  <ion-item>
                    <ion-label>Links:</ion-label>
                    <ion-buttons slot="end">
                      <color-picker
                        v-model:pureColor="
                          instantAssistant.clientsInfo.iaConfigData.displayConfig.linkColor
                        "
                        shape="circle"
                        format="hex"
                        useType="pure"
                        :disableAlpha="true"
                      />
                    </ion-buttons>
                  </ion-item>

                  <ion-item>
                    <ion-label>Logo:</ion-label>
                    <logo-picker/>
                  </ion-item>
                </ion-list>
              </ion-accordion>

              <ion-accordion value="intents">
                <ion-item slot="header">
                  <ion-label>Intents</ion-label>
                </ion-item>

                <ion-list slot="content">
                  <ion-item lines="none">
                    <ion-icon
                      slot="start"
                      :icon="readerSharp"
                    ></ion-icon>
                    <ion-label
                      class="intent_list_item"
                      @click="setSelectedIntent(-2)"
                      >
                      Welcome Intent
                    </ion-label>
                  </ion-item>
                  <ion-item lines="none">
                    <ion-icon
                      slot="start"
                      :icon="readerSharp"
                    ></ion-icon>
                    <ion-label
                      class="intent_list_item"
                      @click="setSelectedIntent(-1)"
                      >
                      Error Intent
                    </ion-label>
                  </ion-item>

                  <ion-list-header>
                    <ion-button @click="presentNewIntentPrompt">
                      <ion-icon :ios="add" :md="add"></ion-icon>
                    </ion-button>
                  </ion-list-header>

                  <ion-item
                    lines="none"
                    v-for="(intent, i) in instantAssistant.clientsInfo.iaConfigData.intents"
                    :key="i"
                  >
                    <ion-icon
                      slot="start"
                      :ios="readerOutline"
                      :md="readerOutline"
                    ></ion-icon>

                    <ion-label
                      class="intent_list_item"
                      @click="setSelectedIntent(i)"
                      >{{ intent.name }}</ion-label
                    >

                    <ion-buttons slot="end">
                      <ion-button @click="toggleIntentActive(i)">
                        <ion-icon
                          v-if="intent.active"
                          :ios="eyeOutline"
                          :md="eyeOutline"
                        ></ion-icon>
                        <ion-icon
                          v-if="!intent.active"
                          :ios="eyeOffOutline"
                          :md="eyeOffOutline"
                        ></ion-icon>
                      </ion-button>
                      <ion-button @click="presentDeleteIntentPrompt(i)">
                        <ion-icon :ios="trash" :md="trash"></ion-icon>
                      </ion-button>
                    </ion-buttons>
                  </ion-item>
                </ion-list>
              </ion-accordion>
            </ion-accordion-group>
          </ion-list>

          <ion-loading
            :is-open="instantAssistant.isLoading"
            message="Please wait..."
            :duration="timeout"
            @didDismiss="setLoading(false)"
          >
          </ion-loading>

          <ion-toast
            color="success"
            :is-open="instantAssistant.showMessage"
            :message="instantAssistant.message"
            :duration="3000"
            @didDismiss="instantAssistant.showMessage = false"
          >
          </ion-toast>

          <ion-toast
            color="danger"
            :is-open="instantAssistant.showError"
            :message="instantAssistant.error"
            :duration="3000"
            @didDismiss="instantAssistant.showError = false"
          >
          </ion-toast>

          <ion-modal
            :is-open="instantAssistant.showIntentTestResponse"
            :swipe-to-close="true"
            :presenting-element="$parent?.$refs.ionRouterOutlet"
            @didDismiss="instantAssistant.showIntentTestResponse = false"
          >
            <ion-card>
              <ion-card-header>
                <ion-toolbar>
                  <ion-title>{{instantAssistant.intentTestResponse?.message}}:</ion-title>
                  <ion-buttons slot="end">
                    <ion-button @click="instantAssistant.showIntentTestResponse = false">
                      <ion-icon :icon="close"></ion-icon>
                    </ion-button>
                  </ion-buttons>
                </ion-toolbar>
              </ion-card-header>
            
              <ion-card-content>
                <ion-grid>
                  <ion-row v-for="(oldIntent, i) in instantAssistant.intentTestResponse?.results.old_Intent" :key="i">
                    <ion-col size="6">
                      Intent: "{{instantAssistant.intentTestResponse?.results.new_Intent[i]}}"
                      <p v-if="instantAssistant.intentTestResponse?.results.old_Userexample_ID[i] === 0"> (Ambiguity in intent name)</p>
                      <p v-else>(User Example: {{instantAssistant.intentTestResponse?.results.new_Userexample[i]}})</p>
                    </ion-col>
                    <ion-col size="6">
                      Is too similar to: "{{oldIntent}}"
                      <p v-if="instantAssistant.intentTestResponse?.results.old_Userexample_ID[i] === 0"> (Ambiguity in intent name)</p>
                      <p v-else>(Is too similar to: {{instantAssistant.intentTestResponse?.results.old_Userexample[i]}})</p>
                    </ion-col>
                  </ion-row>
                </ion-grid>
              </ion-card-content>
            </ion-card>
          </ion-modal>
        </ion-content>
      </ion-menu>
      <ion-router-outlet id="main-content"></ion-router-outlet>
    </ion-split-pane>
  </ion-app>
</template>

<script lang="ts">
import {
  IonApp,
  IonButton,
  IonButtons,
  IonCard,
  IonCardHeader,
  IonCardContent,
  IonHeader,
  IonNote,
  IonToolbar,
  IonAccordion,
  IonAccordionGroup,
  IonContent,
  IonIcon,
  IonInput,
  IonItem,
  IonLabel,
  IonList,
  IonLoading,
  IonListHeader,
  IonMenu,
  IonModal,
  IonRouterOutlet,
  IonToast,
  IonSplitPane,
  IonGrid,
  IonRow,
  IonCol,
  alertController,
} from "@ionic/vue";
import { defineComponent, ref } from "vue";
import {
  add,
  close,
  readerOutline,
  readerSharp,
  arrowBackOutline,
  eyeOutline,
  eyeOffOutline,
  trash,
  save,
} from "ionicons/icons";

import { ColorPicker } from "vue3-colorpicker";
import "vue3-colorpicker/style.css";

import LogoPicker from "./components/LogoPicker.vue";

import useInstantAssistant from "./store";

export default defineComponent({
  name: "App",
  props: {
    timeout: { type: Number, default: 30000 },
  },
  components: {
    IonApp,
    IonButton,
    IonButtons,
    IonCard,
    IonCardHeader,
    IonCardContent,
    IonHeader,
    IonNote,
    IonToolbar,
    IonAccordion,
    IonAccordionGroup,
    IonContent,
    IonIcon,
    IonInput,
    IonItem,
    IonLabel,
    IonList,
    IonListHeader,
    IonLoading,
    IonMenu,
    IonModal,
    IonRouterOutlet,
    IonToast,
    IonSplitPane,
    IonGrid,
    IonRow,
    IonCol,
    ColorPicker,
    LogoPicker
  },
  setup() {
    const clientsInfoHasChanged = ref(false);

    const instantAssistant = useInstantAssistant();
    instantAssistant.$subscribe((mutate, state) => {
      const prevClientsInfo = sessionStorage.getItem("clientsInfo");
      
      if(prevClientsInfo) {
        const prev = JSON.parse(prevClientsInfo);
        
        if(prev.chatBotID > -1) {
          clientsInfoHasChanged.value = prevClientsInfo !== JSON.stringify(state.clientsInfo);
        } else {
          clientsInfoHasChanged.value = false;
        }
      } else {
        clientsInfoHasChanged.value = false;
      }
    });

    const selectedIndex = ref(0);
    
    const setLoading = (state: boolean) => (instantAssistant.isLoading = state);
    const setSelectedIntent = (i: number) => {
      instantAssistant.showIntentEditor = false;
      instantAssistant.selectedIntentIndex = i;
      setTimeout(() => {instantAssistant.showIntentEditor = true;}, 50);
    }

    return {
      clientsInfoHasChanged,
      instantAssistant,
      setLoading,
      setSelectedIntent,
      selectedIndex,
      add,
      close,
      readerOutline,
      readerSharp,
      arrowBackOutline,
      eyeOutline,
      eyeOffOutline,
      trash
    };
  },
  methods: {
    toggleClientActive() {
      const clientsInfoStore = useInstantAssistant();
      clientsInfoStore.clientsInfo.iaConfigData.active =
        !clientsInfoStore.clientsInfo.iaConfigData.active;
    },
    async presentNewIntentPrompt() {
      const clientsInfoStore = useInstantAssistant();
      const alert = await alertController.create({
        header: "New Intent",
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
              this.instantAssistant.createIntent(data.name);
            },
          },
        ],
      });
      return alert.present();
    },
    markIntentUpdated(id: number) {
      this.instantAssistant.clientsInfo.iaConfigData.intents[id].updated = true;
    },
    toggleIntentActive(id: number) {
      this.instantAssistant.clientsInfo.iaConfigData.intents[id].active =
        !this.instantAssistant.clientsInfo.iaConfigData.intents[id].active;

      this.markIntentUpdated(id);
    },
    async presentDeleteIntentPrompt(id: number) {
      const alert = await alertController.create({
        header: `Delete ${this.instantAssistant.clientsInfo.iaConfigData.intents[id].name}?`,
        message: `Are you sure you would like to delete the ${this.instantAssistant.clientsInfo.iaConfigData.intents[id].name} intent?`,
        buttons: [
          {
            text: "Cancel",
            role: "cancel",
            cssClass: "secondary",
          },
          {
            text: "Delete",
            handler: () => {
              this.instantAssistant.deleteIntent(id);
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
.url_modal {
  --height: 90%;
  --width: 90%;
}

.intent_list_item {
  cursor: pointer;
}

ion-menu ion-content {
  --background: var(--ion-item-background, var(--ion-background-color, #fff));
}

ion-menu.md ion-content {
  --padding-start: 8px;
  --padding-end: 8px;
  --padding-top: 20px;
  --padding-bottom: 20px;
}

ion-menu.md ion-list {
  padding: 20px 0;
}

ion-menu.md ion-note {
  margin-bottom: 30px;
}

ion-menu.md ion-list-header,
ion-menu.md ion-note {
  padding-left: 10px;
}

ion-menu.md ion-list#inbox-list {
  border-bottom: 1px solid var(--ion-color-step-150, #d7d8da);
}

ion-menu.md ion-list#inbox-list ion-list-header {
  font-size: 22px;
  font-weight: 600;

  min-height: 20px;
}

ion-menu.md ion-list#labels-list ion-list-header {
  font-size: 16px;

  margin-bottom: 18px;

  color: #757575;

  min-height: 26px;
}

ion-menu.md ion-item {
  --padding-start: 10px;
  --padding-end: 10px;
  border-radius: 4px;
}

ion-menu.md ion-item.selected {
  --background: rgba(var(--ion-color-primary-rgb), 0.14);
}

ion-menu.md ion-item.selected ion-icon {
  color: var(--ion-color-primary);
}

ion-menu.md ion-item ion-icon {
  color: #616e7e;
}

ion-menu.md ion-item ion-label {
  font-weight: 500;
}

ion-menu.ios ion-content {
  --padding-bottom: 20px;
}

ion-menu.ios ion-list {
  padding: 20px 0 0 0;
}

ion-menu.ios ion-note {
  line-height: 24px;
  margin-bottom: 20px;
}

ion-menu.ios ion-item {
  --padding-start: 16px;
  --padding-end: 16px;
  --min-height: 50px;
}

ion-menu.ios ion-item.selected ion-icon {
  color: var(--ion-color-primary);
}

ion-menu.ios ion-item ion-icon {
  font-size: 24px;
  color: #73849a;
}

ion-menu.ios ion-list#labels-list ion-list-header {
  margin-bottom: 8px;
}

ion-menu.ios ion-list-header,
ion-menu.ios ion-note {
  padding-left: 16px;
  padding-right: 16px;
}

ion-menu.ios ion-note {
  margin-bottom: 8px;
}

ion-note {
  display: inline-block;
  font-size: 16px;

  color: var(--ion-color-medium-shade);
}

ion-item.selected {
  --color: var(--ion-color-primary);
}
</style>
