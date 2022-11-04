<template>
  <div class="clickable" @click="toggleShowResponse">{{ answer.text }}</div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";

export default defineComponent({
  name: "clickable",
  props: [ "answer" ],
  setup() {
    const showResponse = ref<boolean>(false);

    const toggleShowResponse = () => {
      showResponse.value = !showResponse.value;
    }

    return {
      showResponse,
      toggleShowResponse
    }
  },
});
</script>

<style scoped>
.clickable {
  font-weight: bolder;
  cursor: pointer;
}
</style>