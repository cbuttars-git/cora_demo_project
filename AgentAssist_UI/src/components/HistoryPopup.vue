<template>
  <ion-list>
    <ion-item v-for="(query, i) in history" :key="i" @click="setQuery(query)">
      {{ query }}
    </ion-item>
  </ion-list>
</template>

<script lang="ts">
import {
  IonItem,
  IonList,
} from "@ionic/vue";
import { defineComponent } from "vue";

import useAgentAssist from '../hooks/agentassist';

export default defineComponent({
  name: "HistoryPopup",
  components: {
    IonItem,
    IonList,
  },
  props: [ "setQuery" ],
  setup() {
    const { history } = useAgentAssist();

    return {
      history,
    }
  }
});
</script>

<style scoped>
ion-item {
  cursor: pointer;
  font-size: 12px;
  --inner-padding-bottom: 0;
  --inner-padding-end: 0;
  --inner-padding-start: 0;
  --inner-padding-top: 0;
  --padding-bottom: 0;
  --padding-end: 10px;
  --padding-start: 10px;
  --padding-top: 0;
  padding: 0;
}
</style>