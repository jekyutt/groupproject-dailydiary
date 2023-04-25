<template>
  <section class="relative py-16 bg-gray-300">
    <div class="container mx-auto px-4">
      <div
        class="
          relative
          flex flex-col
          min-w-0
          break-words
          bg-white
          w-full
          mb-6
          shadow-xl
          rounded-lg
          -mt-64
        "
      >
        <div class="px-6">
          <div class="flex flex-wrap justify-center">
            <div class="w-full lg:w-3/12 px-4 lg:order-2 flex justify-center">
              <div class="relative">
                <img
                  alt="..."
                  :src="user.avatarLink"
                  class="
                    shadow-xl
                    rounded-full
                    h-auto
                    align-middle
                    border-none
                    absolute
                    -m-16
                    -ml-20
                    lg:-ml-16
                  "
                  style="max-width: 150px"
                />
              </div>
            </div>
            <div
              class="
                w-full
                lg:w-4/12
                px-4
                lg:order-3
                lg:text-right
                lg:self-center
              "
            >
              <div class="py-6 px-3 mt-32 sm:mt-0"></div>
            </div>
            <div class="w-full lg:w-4/12 px-4 lg:order-1">
              <div class="flex justify-center py-4 lg:pt-4 pt-8">
                <div class="mr-4 p-3 text-center">
                  <span
                    class="
                      text-xl
                      font-bold
                      block
                      uppercase
                      tracking-wide
                      text-gray-700
                    "
                    >{{
                      user.followers != undefined ? user.followers.length : null
                    }}</span
                  ><span class="text-sm text-gray-500">Followers</span>
                </div>
                <div class="lg:mr-4 p-3 text-center">
                  <span
                    class="
                      text-xl
                      font-bold
                      block
                      uppercase
                      tracking-wide
                      text-gray-700
                    "
                    >{{
                      user.followings != undefined
                        ? user.followings.length
                        : null
                    }}</span
                  ><span class="text-sm text-gray-500">Following</span>
                </div>
              </div>
            </div>
          </div>
          <div class="text-center mt-12">
            <h3
              class="text-4xl font-semibold leading-normal mb-2 text-gray-800"
            >
              {{ user.firstname }} {{ user.lastname }}
            </h3>
            <div
              class="
                text-sm
                leading-normal
                mt-0
                mb-2
                text-gray-500
                font-bold
                uppercase
              "
            >
              <i class="fas fa-map-marker-alt mr-2 text-lg text-gray-500"></i>
              @{{ user.userName }}
            </div>
            <div class="mb-2 text-gray-700 mt-10">
              <i class="fas fa-briefcase mr-2 text-lg text-gray-500"></i
              >{{ dateTimeToDate }}
            </div>
            <div class="mb-2 text-gray-700">
              <i class="fas fa-university mr-2 text-lg text-gray-500"></i
              >{{ user.email }}
            </div>
            <div class="mb-2 text-gray-700">
              <i class="fas fa-university mr-2 text-lg text-gray-500"></i
              >{{ user.phoneNumber }}
            </div>
          </div>
          <div class="mt-10 py-10 border-t border-gray-300 text-center">
            <div class="flex flex-wrap justify-center">
              <p class="text-lg leading-relaxed text-gray-800">
                {{ user.bio }}
              </p>
            </div>
          </div>
          <div class="py-10 border-t border-gray-300 text-center">
            <div class="flex flex-wrap justify-center">
              <div class="w-full lg:w-9/12 px-4">
                <div
                  v-for="post in posts"
                  :key="post.id"
                  class="mb-4 text-lg leading-relaxed text-gray-800"
                >
                  <div v-if="post.ownerId == user.id && post.type == '1'">
                    <Post :user="user" :post="post" />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script lang="ts">
import { defineComponent, PropType, onMounted } from 'vue';
import { IUser } from '@/modules/users';
import allPosts from '@/modules/posts';
import Post from '@/components/Post/Post.vue';

export default defineComponent({
  name: 'User',
  components: {
    Post,
  },
  props: {
    user: {
      type: Object as PropType<IUser>,
      required: true,
    },
  },
  setup() {
    const { posts, loadPosts } = allPosts();
    onMounted(() => {
      loadPosts();
    });

    return { posts };
  },
  computed: {
    adultToString(): string {
      return this.user.adult == true ? 'Adult' : 'Child';
    },
    dateTimeToDate(): string {
      let date = new Date(this.user.birthDate);

      return (
        date.getDate().toString() +
        '.' +
        date.getMonth().toString() +
        '.' +
        date.getUTCFullYear().toString()
      );
    },
  },
});
</script>

<style scoped></style>
