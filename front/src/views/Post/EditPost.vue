<template>
  <EditPostForm :id="id" />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import EditPostForm from '@/components/Post/EditPostForm.vue';

export default defineComponent({
  components: {
    EditPostForm,
  },
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  setup() {
    return {};
  },
});
</script>
