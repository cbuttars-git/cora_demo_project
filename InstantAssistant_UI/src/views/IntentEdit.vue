<template>
  <ion-page>
    <ion-header :translucent="true">
      <ion-toolbar>
        <ion-buttons slot="start">
          <ion-menu-button color="primary"></ion-menu-button>
        </ion-buttons>
        <ion-title>Edit Intent</ion-title>
        <ion-buttons slot="end">
          <ion-button :disabled="!intentsChanged" @click="testIntents" color="secondary" fill="solid">
            deploy
          </ion-button>
        </ion-buttons>
      </ion-toolbar>
    </ion-header>
    
    <ion-content v-if="instantAssistant.showIntentEditor" :fullscreen="true">
      <ion-header collapse="condense">
        <ion-toolbar>
          <ion-title size="large">Edit Intent</ion-title>
        </ion-toolbar>
      </ion-header>
    
      <div id="container">
        <ion-item>
          <ion-label>Intent Name:</ion-label>
          <ion-input v-model="instantAssistant.selectedIntent.name" @change="markIntentUpdated" :disabled="instantAssistant.selectedIntent.id === -1"></ion-input>
        </ion-item>

        <ion-accordion-group v-if="instantAssistant.selectedIntent.id !== -1">
          <ion-accordion value="examples">
            <ion-item slot="header">
              <ion-label>User Examples:</ion-label>
            </ion-item>

            <ion-list slot="content">
              <ion-item>
                <ion-buttons slot="start">
                  <ion-button @click="addUserExample">
                    <ion-icon :ios="add" :md="add"></ion-icon>
                  </ion-button>
                </ion-buttons>
              </ion-item>
              <ion-item v-for="(example, i) in instantAssistant.selectedIntent.userExamples" :key="i">
                <ion-label>{{example.id}}:</ion-label>
                <ion-input v-model="example.text" @change="markUserExampleUpdated(i)"></ion-input>

                <ion-buttons slot="end">
                  <ion-button @click="toggleExampleActive(i)">
                    <ion-icon v-if="example.active" :ios="eyeOutline" :md="eyeOutline"></ion-icon>
                    <ion-icon v-if="!example.active" :ios="eyeOffOutline" :md="eyeOffOutline"></ion-icon>
                  </ion-button>
                  <ion-button @click="presentDeleteExamplePrompt(i)">
                    <ion-icon :ios="trash" :md="trash"></ion-icon>
                  </ion-button>
                </ion-buttons>
              </ion-item>
            </ion-list>
          </ion-accordion>
        </ion-accordion-group>

        <ion-item>
          <ion-label>Response:</ion-label>
        </ion-item>

        <quill-editor
          content-type="html"
          theme="snow"
          toolbar="full"
          v-model:content="instantAssistant.selectedIntent.response"
          @text-change="textChange" />
        
      </div>
    </ion-content>

    <ion-footer v-if="instantAssistant.showIntentEditor" :translucent="true">
      <ion-toolbar>
        <ion-item>
          <ion-label>URL:</ion-label>
          <ion-input v-model="instantAssistant.selectedIntent.url" @change="markIntentUpdated"></ion-input>
        </ion-item>
        <ion-buttons slot="end">
          <ion-button color="primary" fill="solid" :href="instantAssistant.selectedIntent.url" target="ia_links">
            verify
          </ion-button>
        </ion-buttons>
      </ion-toolbar>
    </ion-footer>
  </ion-page>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import { IonIcon, IonAccordion, IonAccordionGroup, IonButton, IonButtons, IonContent, IonFooter, IonHeader, IonInput, IonItem, IonLabel, IonMenuButton, IonPage, IonTitle, IonToolbar, alertController } from '@ionic/vue';
import { add, eyeOutline, eyeOffOutline, trash } from 'ionicons/icons';

import useInstantAssistant from "../store";

import { QuillEditor, Delta } from '@vueup/vue-quill'
import '@vueup/vue-quill/dist/vue-quill.snow.css';

interface QuillTextChangeEvent {
  delta: Delta,
  oldContents: Delta,
  source: string
}

export default defineComponent({
  name: 'IntentEdit',
  props: {
    id: Number
  },
  components: {
    IonIcon,
    IonAccordion,
    IonAccordionGroup,
    IonButton,
    IonButtons,
    IonContent,
    IonFooter,
    IonHeader,
    IonInput,
    IonItem,
    IonLabel,
    IonMenuButton,
    IonPage,
    IonTitle,
    IonToolbar,
    QuillEditor
  },
  setup(props: any) {
    const instantAssistant = useInstantAssistant();

    const intentsChanged = ref(false);
    instantAssistant.$subscribe((mutate, state) => {
      intentsChanged.value = false;
      state.clientsInfo.iaConfigData.intents.forEach(intent => {
        if(intent.updated) {
          intentsChanged.value = true;
        }
      });
    });

    onMounted(() => {
      instantAssistant.fetchClientsInfo(props.id);
    });
    return {
      intentsChanged,
      add,
      eyeOutline,
      eyeOffOutline,
      trash,
      instantAssistant,
    }
  },
  methods: {
    textChange(e: QuillTextChangeEvent) {
      if (e.oldContents.ops[0].insert?.toString().trim() !== "") {
        this.markIntentUpdated();
      }
    },
    markIntentUpdated() {
      this.instantAssistant.selectedIntent.updated = true;
    },
    resetUserExampleIds() {
      const userExamples = this.instantAssistant.selectedIntent.userExamples.map((example, i) => {
        example.id = i + 1;
        return example;
      });

      this.instantAssistant.selectedIntent.userExamples = userExamples;
    },
    addUserExample() {
      this.instantAssistant.selectedIntent.userExamples.push({
        id: this.instantAssistant.selectedIntent.userExamples.length,
        text: "",
        active: true,
        updated: true,
        aIModelTestDate: "",
        aIModelDeployDate: ""
      });
      this.resetUserExampleIds();

      this.markIntentUpdated();
    },
    markUserExampleUpdated(id: number) {
      this.instantAssistant.selectedIntent.userExamples[id].updated = true;
      this.markIntentUpdated();
    },
    toggleExampleActive(id: number) {
      this.instantAssistant.selectedIntent.userExamples[id].active = !this.instantAssistant.selectedIntent.userExamples[id].active;
      this.markUserExampleUpdated(id);
    },
    async presentDeleteExamplePrompt(id: number) {
      const alert = await alertController
        .create({
          header: "Delete User Example?",
          message: "Are you sure you would like to delete the User Example?",
          buttons: [
            {
              text: 'Cancel',
              role: 'cancel',
              cssClass: 'secondary',
            },
            {
              text: 'Delete',
              handler: () => {
                this.instantAssistant.selectedIntent.userExamples.splice(id,1);
                this.resetUserExampleIds();
                
                this.markIntentUpdated();
              },
            },
          ],
        });
      return alert.present();
    },
    async testIntents() {
      this.instantAssistant.testIntents();
    },
    async deployIntents() {
      //const { deployIntents } = useInstantAssist();
      //deployIntents();
    }
  }
});
</script>

<style scoped>
#container {
  height: 60%;
}

#container strong {
  font-size: 20px;
  line-height: 26px;
}

#container p {
  font-size: 16px;
  line-height: 22px;
  color: #8c8c8c;
  margin: 0;
}

#container a {
  text-decoration: none;
}


</style>
