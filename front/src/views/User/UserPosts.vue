<template>
  <div>
    <PostList title="UserPosts" />
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import PostList from '@/components/Post/PostList.vue';

export default defineComponent({
  name: 'UserPosts',
  components: {
    PostList,
  },
  setup() {
    return {};
  },
});
</script>
