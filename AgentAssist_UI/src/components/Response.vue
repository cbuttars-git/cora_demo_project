<template>
  <div class="answer">
    <ion-card class="dialogBgColor dialogFgColor">
      <ion-card-content :class="{ collapsed: !showThisResponse, container: isTopLevel }">
        <ion-button v-if="isTopLevel" @click="toggleShowThisResponse" class="caret">
          <ion-icon v-if="showThisResponse" :icon="caretUpOutline"></ion-icon>
          <ion-icon v-if="!showThisResponse" :icon="caretDownOutline"></ion-icon>
        </ion-button>
        <div>
          <div v-if="isTopLevel" class="feedback">
            <ion-button
              @click="sendFeedback(true, response)"
              title="This was helpful"
              :class="{thumbs: response.feedbackId === -1 || !response.feedback, thumbs_up: response.feedbackId > -1 && response.feedback}">
              <ion-icon :icon="thumbsUpSharp"></ion-icon>
            </ion-button>
            <ion-button
              @click="sendFeedback(false, response)"
              title="This wasn't helpful"
              :class="{thumbs: response.feedbackId === -1 || response.feedback, thumbs_down: response.feedbackId > -1 && !response.feedback}">
              <ion-icon :icon="thumbsDownSharp"></ion-icon>
            </ion-button>
          </div>
          <div v-for="(answer, a) in response.answer" :key="a">
            <div v-if="answer.response">
              <div class="clickable" @click="toggleShowResponse(a)">{{ answer.text }}</div>
              <response v-if="showResponse[a]" :response="answer.response" />
            </div>
            <div v-else>
              <div v-if="response.answer.length === 1" v-html="answer.text"/>
              <div v-else v-html="answer.text"/>

              <div v-if="response.url" class="more-info">
                <a :href="response.url" target="aa_links">More Info</a>
              </div>
            </div>
          </div>
        </div>
      </ion-card-content>
    </ion-card>
  </div>
</template>

<script lang="ts">
import {
  IonButton,
  IonCard,
  IonCardContent,
  IonIcon,
} from "@ionic/vue";
import {
  caretDownOutline,
  caretUpOutline,
  thumbsUpSharp,
  thumbsDownSharp
} from "ionicons/icons";
import { defineComponent, ref } from "vue";
//import PlainText from "./PlainText.vue";

import useAgentAssist from '../hooks/agentassist';

export default defineComponent({
  name: "ResponseComponent",
  props: [ "response", "isTopLevel" ],
  components: {
    IonButton,
    IonCard,
    IonCardContent,
    IonIcon,
    //PlainText,
  },
  setup(props: any){
    const { config, sendFeedback } = useAgentAssist();

    const showThisResponse = ref<boolean>(true);
    const showResponse = ref<boolean[]>([]);

    props.response.answer.forEach(() => {
      showResponse.value.push(false);
    });

    const toggleShowThisResponse = () => {
      showThisResponse.value = !showThisResponse.value;
    }

    const toggleShowResponse = (index: number) => {
      showResponse.value[index] = !showResponse.value[index];
    }

    return {
      config,
      sendFeedback,
      showThisResponse,
      showResponse,
      toggleShowThisResponse,
      toggleShowResponse,
      caretDownOutline,
      caretUpOutline,
      thumbsUpSharp,
      thumbsDownSharp
    }
  }
});
</script>

<style scoped>
.dialogBgColor {
  --background: v-bind(config?.iaConfigData.displayConfig.dialogBgColor);
}

.dialogFgColor {
  --color: v-bind(config?.iaConfigData.displayConfig.dialogFgColor);
}

.answer {
  padding: 0 20px 5px;
  line-height: 1.5;
}

.clickable {
  color: #00757a;
  font-weight: bolder;
  cursor: pointer;
  margin-top: 10px;
}

ion-card {
  border-radius: 8px;
  box-shadow: rgb(0 0 0 / 75%) 0px 3px 1px -2px, rgb(0 0 0 / 75%) 0px 2px 2px 0px, rgb(0 0 0 / 75%) 0px 1px 5px 0px;
  display: flex;
  flex-direction: column;
  margin: 0 !important;
  width: 100%;
}

.more-info, .feedback {
  float: right;
}

.collapsed {
  height: 40px;
}

.caret {
  color: #00757a;
}

.thumbs {
  color: lightgrey;
}

.thumbs_up {
  color: lightseagreen;
}

.thumbs_down {
  color: lightcoral;
}

ion-button {
  --background: transparent;
  --box-shadow: transparent;
  --border-style: none;
  --padding-bottom: 0;
  --padding-end: 0;
  --padding-start: 0;
  --padding-top: 0;
  height: 18px;
  width: 18px;
  margin-right: 10px;
  margin-top: -1px;
}

.container {
  display: grid;
  grid-template-columns: 28px auto;
}

@media (prefers-color-scheme: dark) {
  .clickable {
    color: #00b97a;
  }

  .caret {
    color: #00b97a;
  }
}
</style>