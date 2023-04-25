<template>
  <footer class="bg-gray-100">
    <div class="container mx-auto px-6 pt-10 pb-6">
      <div class="flex flex-wrap">
        <div class="w-full md:w-1/2 text-center md:text-left">
          <h5 class="uppercase mb-6 font-bold">Links</h5>
          <ul class="mb-4">
            <li class="mt-2">
              <router-link
                to="/contact"
                class="hover:underline text-gray-600 hover:text-orange-500"
                >Contact with us</router-link
              >
            </li>
            <li class="mt-2">
              <router-link
                to="/privacy"
                class="hover:underline text-gray-600 hover:text-orange-500"
                >Privacy</router-link
              >
            </li>
          </ul>
        </div>
      </div>
    </div>
  </footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'Footer',
});
</script>

<style scoped></style>
