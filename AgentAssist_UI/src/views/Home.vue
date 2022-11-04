<template>
  <ion-page>
    <ion-header>
      <ion-toolbar v-if="sessionId !== null" class="headerBgColor">
        <ion-title class="headerFgColor">{{ config?.chatBotName }}</ion-title>
        <ion-buttons slot="end">
          <ion-button @click="resetCache" title="Reset Answer Cache" class="headerFgColor">
            <ion-icon :icon="refreshOutline"></ion-icon>
          </ion-button>
        </ion-buttons>
        <img class="logo" :src="config?.iaConfigData.displayConfig.logo" slot="end" />
      </ion-toolbar>
    </ion-header>

    <ion-content ref="content" class="dialogBgColor dialogFgColor">
      <div v-if="error !== null && sessionId === null">
        You do not have access to {{chatBotName}} at this time.
      </div>
      <div v-else>
        <div class="welcomeResponse" v-if="responses.length < 1 && config?.iaConfigData.welcomeIntent !== null">
          <response :response="welcomeResponse" :isTopLevel="false" />
        </div>
        
        <div v-for="(response, rI) in responses" :key="rI">
          <div class="query">
            {{ response.query }}
          </div>
          <div>
            <response :response="response" :isTopLevel="true" />
          </div>
        </div>
      </div>

      <ion-loading
        :is-open="loading"
        message="Please wait..."
        :duration="timeout"
        @didDismiss="setLoading(false)"
      >
      </ion-loading>
    </ion-content>

    <searchbar v-if="sessionId !== null" :scroll="scroll" />
  </ion-page>
</template>

<script lang="ts">
import {
  IonButton,
  IonButtons,
  IonContent,
  IonHeader,
  IonIcon,
  IonLoading,
  IonPage,
  IonTitle,
  IonToolbar,
} from "@ionic/vue";
import {
  chevronForwardOutline,
  listCircleOutline,
  refreshOutline
} from "ionicons/icons";
import { defineComponent, onMounted, onUpdated, onRenderTriggered, ref } from "vue";
import Response from "../components/Response.vue";
import Searchbar from "../components/Searchbar.vue";

import useAgentAssist from '../hooks/agentassist';

export default defineComponent({
  name: "HomeView",
  props: {
    timeout: { type: Number, default: 30000 },
    name: String
  },
  components: {
    IonButton,
    IonButtons,
    IonContent,
    IonHeader,
    IonIcon,
    IonLoading,
    IonPage,
    IonTitle,
    IonToolbar,
    Response,
    Searchbar,
  },
  setup(props: any) {
    const { error, loading, sessionId, config, newSession, query, responses, welcomeResponse, errorResponse, resetCache, getResponsesFromCache, getHistory } = useAgentAssist();
    const content = ref<typeof IonContent | null>(null);

    const setLoading  = (state: boolean) => loading.value = state;

    const scroll = () => {
      content.value?.$el.scrollToBottom(500)
        .then(() => {
          content.value?.$el.scrollByPoint(0, 1000, 500);
        });
    }

    let headerBgColor;

    onMounted(() => {
      newSession(props.name);

      getResponsesFromCache(props.name)
        .then(() => {
          setTimeout(scroll, 500);
        });

      getHistory(props.name);
    });

    onUpdated(() => {
      scroll();
    });

    onRenderTriggered(() => {
      scroll();
    });

    return {
      setLoading,
      error,
      loading,
      sessionId,
      config,
      content,
      query,
      responses,
      welcomeResponse,
      errorResponse,
      scroll,
      chevronForwardOutline,
      resetCache,
      refreshOutline,
      listCircleOutline,
      headerBgColor,
      chatBotName: props.name
    }
  }
});
</script>

<style scoped>
.welcomeResponse {
  margin-top: 20px;
}

.headerBgColor {
  --background: v-bind(config?.iaConfigData.displayConfig.headerBgColor);
}

.headerFgColor {
  --color: v-bind(config?.iaConfigData.displayConfig.headerFgColor);
}

.dialogBgColor {
  --background: v-bind(config?.iaConfigData.displayConfig.dialogBgColor);
}

.dialogFgColor {
  --color: v-bind(config?.iaConfigData.displayConfig.dialogFgColor);
}

.logo {
  height: 42px;
  padding-left: 20px;
  padding-right: 20px;
}

.query {
  --color: v-bind(config?.iaConfigData.displayConfig.headerFgColor);
  --background: v-bind(config?.iaConfigData.displayConfig.headerBgColor);
  color: var(--color);
  background: var(--background);
  border-radius: 8px;
  text-align: left;
  line-height: 1.5;
  padding: 10px;
  max-width: 85%;
  margin: 30px 20px 10px 0;
  float: right;
  display: block;
  position: relative;
}

.query_input {
  --padding-start: 20px;
}

.history .popover-content {
  --min-width: 600px !important;
}
</style>
