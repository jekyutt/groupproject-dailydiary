<template>
  <div class="bg-gradient-to-r from-green-400 to-blue-500 relative overflow-hidden h-screen">
    <!-- <img
      src="https://i.imgur.com/rPL3WUb.png"
      class="absolute h-full w-full object-cover"
    /> -->
    <div class="inset-0 bg-black opacity-25 absolute"></div>
    <div
      class="container mx-auto relative z-20 flex items-center py-32 xl:py-60"
    >
      <div
        class="lg:w-3/5 xl:w-2/5 flex flex-col items-start relative z-10 mr-40 ml-40"
      >
        <span class="font-bold uppercase text-yellow-400">
          Ready to dive in?
        </span>
        <h1
          class="font-bold text-6xl sm:text-7xl text-white leading-tight mt-4"
        >
          Start keeping your diary now!
        </h1>
      </div>
      <router-link
        to="/login"
        class="block bg-white hover:bg-gray-100 py-3 px-4 rounded-lg text-lg text-gray-800 font-bold uppercase mt-10 mr-10"
      >
        Log in
      </router-link>
      <router-link
        to="/register"
        class="block bg-white hover:bg-gray-100 py-3 px-4 rounded-lg text-lg text-gray-800 font-bold uppercase mt-10 inset-y-0 right-0"
      >
        Register
      </router-link>
      <div class="ml-3 inline-flex rounded-md shadow"></div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    return {};
  },
});
</script>

<style scoped></style>
