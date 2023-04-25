<template>
  <div
    v-if="commentary.owner != undefined"
    class="p-3 flex flex-col justify-left items-left md:items-start mb-2"
  >
    <div class="flex flex-row justify-left mr-2">
      <router-link 
      :to="{
        name: 'Profile',
        params: {id: commentary.owner.id},
      }"  >
        <img
          alt="avatar"
          width="45"
          height="45"
          class="rounded-full w-7 h-7 mr-4 shadow-lg mb-4"
          :src="commentary.owner.avatarLink"
        />
      </router-link>
      <h3 class="text-black font-semibold text-xs text-left md:text-left">
        {{ commentary.owner.firstname }} {{ commentary.owner.lastname }}
      </h3>
    </div>
    <p style="width: 90%" class="text-gray-600 text-xs text-left md:text-left">
      {{ commentary.text }}
    </p>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import { ICommentary } from '@/modules/commentaries';

export default defineComponent({
  name: 'Commentary',
  components: {},
  props: {
    commentary: {
      type: Object as PropType<ICommentary>,
      required: true,
    },
  },
});
</script>

<style scoped></style>
