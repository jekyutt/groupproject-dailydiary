<template>
  <div class="h-screen bg-gray-200 flex items-center">
    <div
      class="
        container
        flex
        md:flex-row
        items-center
        justify-center
        px-5
        text-gray-700
      "
    >
      <div class="max-w-md">
        <div class="text-5xl font-dark font-bold">404</div>
        <p class="text-2xl md:text-3xl font-light leading-normal">
          Have you chosen a user? or
        </p>
        <p class="mb-8">You haven't made a single post yet!</p>

        <a
          class="
            px-4
            inline
            py-2
            text-sm
            font-medium
            leading-5
            shadow
            text-white
            transition-colors
            duration-150
            border border-transparent
            rounded-lg
            focus:outline-none
            focus:shadow-outline-blue
            bg-blue-600
            active:bg-blue-600
            hover:bg-blue-700
          "
          href="/"
        >
          back to homepage
        </a>
      </div>
      <div class="max-w-lg">
        <img src="https://i.imgur.com/svdVg3q.png" />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'NotFound',
});
</script>
