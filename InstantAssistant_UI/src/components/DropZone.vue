<template>
    <div :data-active="active"
      @dragenter.prevent="setActive(true)"
      @dragover.prevent="setActive(true)"
      @dragleave.prevent="setActive(false)"
      @drop.prevent="onDrop">
        <slot :dropZoneActive="active"></slot>
    </div>
</template>

<script lang="ts">
import { ref, onMounted, onUnmounted, defineComponent } from 'vue';
  
export default defineComponent({
name: "DropZone",
  setup() {
    const emit = defineEmits(["file-dropped"]);
    let active = ref(false);
    let activeTimeout: number;

    const setActive = (isActive: boolean) => {
      if(isActive) {
        active.value = true;
        clearTimeout(activeTimeout);
      } else {
        activeTimeout = setTimeout(() => {
          active.value = false;
        }, 50);
      }
    }

    const onDrop = (e: DragEvent) => {
      setActive(false);
      emit("file-dropped", e.dataTransfer?.files.item(0));
    }

    const preventDefaults = (e: Event) => {
      e.preventDefault();
    }

    const events = ["dragenter", "dragover", "dragleave", "drop"];

    onMounted(() => {
      events.forEach((eventName) => {
        document.body.addEventListener(eventName, preventDefaults);
      });
    });

    onUnmounted(() => {
      events.forEach((eventName) => {
        document.body.removeEventListener(eventName, preventDefaults);
      });
    });

    return {
      active,
      setActive,
      onDrop
    }
  }
});
</script>