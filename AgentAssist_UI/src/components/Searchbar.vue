<template>
  <ion-footer>
    <ion-toolbar class="footerBgColor">
      <ion-buttons slot="start">
        <ion-button @click="openPopover(0, $event)" title="Question History" class="footerFgColor">
          <ion-icon :icon="listCircleOutline"></ion-icon>
        </ion-button>
      </ion-buttons>
      <ion-input class="footerFgColor"
        ref="queryInput"
        :placeholder="config?.iaConfigData.placeholder"
        v-model="query"
        @keypress="searchKeypress"
      ></ion-input>
      <ion-buttons slot="end">
        <ion-button @click="fetchqnaresponse" title="Submt Question" class="footerFgColor">
          <ion-icon :icon="chevronForwardOutline"></ion-icon>
        </ion-button>
      </ion-buttons>
    </ion-toolbar>
  </ion-footer>
</template>

<script lang="ts">
import {
  IonButton,
  IonButtons,
  IonFooter,
  IonIcon,
  IonInput,
  IonToolbar,
  popoverController,
} from "@ionic/vue";
import {
  chevronForwardOutline,
  listCircleOutline,
} from "ionicons/icons";
import { defineComponent, onUpdated, onRenderTriggered, ref } from "vue";
import HistoryPopup from "../components/HistoryPopup.vue";

import useAgentAssist from '../hooks/agentassist';

export default defineComponent({
  name: "SearchbarComponent",
  props: [ "scroll" ],
  components: {
    IonButton,
    IonButtons,
    IonFooter,
    IonIcon,
    IonInput,
    IonToolbar
  },
  setup(props: any){
    const { config, query, history, getqnaresponse } = useAgentAssist();
    const queryInput = ref<typeof IonInput | null>(null);
    // eslint-disable-next-line
    let currentPopover: HTMLIonPopoverElement;
    
    const setQuery = (newQuery: string) => {
      query.value = newQuery;
      currentPopover.dismiss();
      
      setTimeout(() => {
        queryInput.value?.$el.getInputElement()
          .then((input: HTMLInputElement) => {
            input.focus();
          });
      }, 750);
    }

    const openPopover = async (repArrayRef: number, ev: Event) => {
      const popover = await popoverController
        .create({
          component: HistoryPopup,
          componentProps: { setQuery: setQuery },
          event: ev,
          showBackdrop: false,
          cssClass: "history"
        }); 

      currentPopover = popover;
      await popover.present();
    }

    onUpdated(() => {
      props.scroll();
    });

    onRenderTriggered(() => {
      props.scroll();
    });

    const sendQuery = () => {
        getqnaresponse()
          .then(() => {
            props.scroll();
            queryInput.value?.$el.getInputElement()
            .then((input: HTMLInputElement) => {
              input.focus();
            });
          });
    }

    return {
      config,
      queryInput,
      openPopover,
      query,
      history,
      setQuery,
      sendQuery,
      chevronForwardOutline,
      listCircleOutline,
    }
  },
  methods: {
    searchKeypress(e: KeyboardEvent) {
      if(e.key === "Enter") {
        this.sendQuery();
      }
    },
    async fetchqnaresponse() {
      this.sendQuery();
    }
  }
});
</script>

<style scoped>
.footerBgColor {
  --background: v-bind(config?.iaConfigData.displayConfig.footerBgColor);
}

.footerFgColor {
  --color: v-bind(config?.iaConfigData.displayConfig.footerFgColor);
}
</style>