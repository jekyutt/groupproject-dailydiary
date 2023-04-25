<template>
  <div class="flex flex-col min-h-screen">
    <NavVue v-if="loggedUser.id != ''" :user="loggedUser" />
    <main class="flex-grow">
      <router-view />
    </main>
    <Footer />
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import NavVue from '@/views/Nav.vue';
import Footer from '@/views/Footer.vue';
import allUsers from '@/modules/users';

export default defineComponent({
  name: 'App',
  components: {
    NavVue,
    Footer,
  },
  setup() {
    const { loggedUser } = allUsers();
    console.log(loggedUser.value.id);
    return { loggedUser };
  },
});
</script>
