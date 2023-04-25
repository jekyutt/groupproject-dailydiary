<template>
  <div>
    <EditUserForm :id="id" />
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import EditUserForm from '@/components/User/EditUserForm.vue';

export default defineComponent({
  components: {
    EditUserForm,
  },
  props: {
    id: {
      type: String,
      required: true,
    },
  },
});
</script>

<style></style>
