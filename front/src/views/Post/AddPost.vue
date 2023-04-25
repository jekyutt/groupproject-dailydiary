<template>
  <AddPostForm />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import AddPostForm from '@/components/Post/AddPostForm.vue';

export default defineComponent({
  components: {
    AddPostForm,
  },
  setup() {
    return {};
  },
});
</script>
