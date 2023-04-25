<template>
  <div v-if="user == undefined">
    <not-found></not-found>
  </div>
  <div v-else>
    <section class="relative block" style="height: 500px">
      <div
        class="absolute top-0 w-full h-full bg-center bg-cover"
        style="
          background-image: url('https://images.unsplash.com/photo-1499336315816-097655dcfbda?ixlib=rb-1.2.1&amp;ixid=eyJhcHBfaWQiOjEyMDd9&amp;auto=format&amp;fit=crop&amp;w=2710&amp;q=80');
        "
      >
        <span
          id="blackOverlay"
          class="w-full h-full absolute opacity-50 bg-black"
        ></span>
      </div>
      <div
        class="
          top-auto
          bottom-0
          left-0
          right-0
          w-full
          absolute
          pointer-events-none
          overflow-hidden
        "
        style="height: 70px"
      >
        <svg
          class="absolute bottom-0 overflow-hidden"
          xmlns="http://www.w3.org/2000/svg"
          preserveAspectRatio="none"
          version="1.1"
          viewBox="0 0 2560 100"
          x="0"
          y="0"
        >
          <polygon
            class="text-gray-300 fill-current"
            points="2560 0 2560 100 0 100"
          ></polygon>
        </svg>
      </div>
    </section>
    <section class="relative py-16 bg-gray-300">
      <User :user="user" />
    </section>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useRoute } from 'vue-router';
import allUsers, { initialized, getUserById, IUser } from '@/modules/users';
import User from '@/components/User/User.vue';
import NotFound from '@/components/NotFound.vue';

export default defineComponent({
  name: 'Profile',
  components: {
    User,
    NotFound,
  },
  beforeRouteEnter(to, from, next) {
    allUsers()
      .loadUsers()
      .then(() => {
        if (initialized) {
          next();
        } else {
          next('/');
        }
      });
  },
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  setup() {
    let user: IUser = getUserById(useRoute().params.id.toString())!;
    return { user };
  },
  mounted() {
    window.scrollTo(0, 0);
  },
});
</script>

<style scoped></style>
