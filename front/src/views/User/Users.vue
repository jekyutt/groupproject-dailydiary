<template>
  <div><UserList title="All users" /></div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import UserList from '@/components/User/UserList.vue';

export default defineComponent({
  name: 'UsersVue',
  components: {
    UserList,
  },
  setup() {
    return {};
  },
});
</script>

<style scoped></style>
