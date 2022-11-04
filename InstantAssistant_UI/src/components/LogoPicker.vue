<template>
  <input type="file"
    ref="fileInput"
    accept="image/*"
    multiple="false"
    @change="fileChanged"
    style="display: none"
    />

  <img class="logo" :src="instantAssistant.getLogo"/>

  <ion-buttons slot="end">
    <ion-button @click="pickLogo" fill="transparent">
      <ion-icon :icon="cloudUploadOutline"></ion-icon>
    </ion-button>
  </ion-buttons>
</template>

<script lang="ts">
import {
  IonButton,
  IonButtons,
  IonIcon
} from "@ionic/vue";
import { defineComponent, ref } from 'vue';
import { cloudUploadOutline } from 'ionicons/icons';

import useInstantAssistant from "../store";
  
export default defineComponent({
  name: "LogoPicker",
  components: {
    IonButton,
    IonButtons,
    IonIcon
  },
  setup() {
    const instantAssistant = useInstantAssistant();

    const fileInput = ref<HTMLInputElement>();

    const pickLogo = () => {
      fileInput.value?.click();
    }

    const fileChanged = (e: Event) => {
      const file = fileInput.value?.files?.item(0);
      if (file) {
        const reader = new FileReader();
        reader.onload = (e) => {
          if(e.target?.result)
            instantAssistant.setLogo(e.target?.result?.toString());
        }
        reader.readAsDataURL(file);
      }
    }

    return {
      cloudUploadOutline,
      fileInput,
      instantAssistant,
      pickLogo,
      fileChanged
    }
  }
});
</script>

<style scoped>
.logo {
  height: 42px;
}
</style>