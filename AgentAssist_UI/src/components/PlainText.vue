<template>
  <div>{{ plainText }}</div>
</template>

<script lang="ts">
export default {
  props: [ "text" ],
  setup(props: any) {
    let plainText = "";

    const doc = new DOMParser().parseFromString(props.text, 'text/html');
    plainText = doc.body.textContent || "";

    return {
      plainText,
    }
  },
}
</script>

<style scoped>

</style>