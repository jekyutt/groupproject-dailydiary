<template>
  <div>
    <PostList title="Feed" />
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import PostList from '@/components/Post/PostList.vue';

export default defineComponent({
  name: 'Feed',
  components: {
    PostList,
  },
  setup() {
    return {};
  },
});
</script>
<style></style>
