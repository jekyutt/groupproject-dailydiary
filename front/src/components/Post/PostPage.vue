<template>
  <div class="max-w-screen-lg mx-auto mb-20">
    <main v-if="post != undefined" class="mt-10">
      <div class="mb-4 md:mb-0 w-full mx-auto relative">
        <div class="px-4 lg:px-0">
          <h2
            class="
              text-4xl
              font-semibold
              text-gray-800
              leading-tight
              mx-auto
              word-wrap:
              break-words
            "
          >
            {{ post.title }}
          </h2>
          <a
            href="#"
            class="
              py-2
              text-green-700
              inline-flex
              items-center
              justify-center
              mb-2
              mx-auto
            "
          >
            {{ post.releaseDate.toString().substring(0, 10) }}
          </a>
        </div>

        <img :src="post.imageLink" />
      </div>

      <div class="flex flex-col lg:flex-row lg:space-x-12">
        <div
          class="
            px-4
            lg:px-0
            mt-12
            text-gray-700 text-lg
            leading-relaxed
            w-full
            lg:w-3/4
          "
        >
          <p class="pb-6 word-wrap: break-words">{{ post.text }}</p>
        </div>

        <div class="w-full lg:w-1/4 m-auto mt-12 max-w-screen-sm">
          <div class="p-4 border-t border-b md:border md:rounded">
            <div class="flex py-2">
              <img
                :src="post.owner.avatarLink"
                class="h-10 w-10 rounded-full mr-2 object-cover"
              />
              <div>
                <p class="font-semibold text-gray-700 text-sm">
                  {{ post.owner.firstname }} | {{ post.owner.lastname }}
                </p>
                <p class="font-semibold text-gray-600 text-xs">
                  {{ post.owner.userName }}
                </p>
              </div>
            </div>
            <p class="text-gray-700 py-3">
              {{ post.owner.bio }}
            </p>
            <button
              class="
                px-2
                py-1
                text-gray-100
                bg-green-700
                flex
                w-full
                items-center
                justify-center
                rounded
              "
            >
              Follow
              <i class="bx bx-user-plus ml-2"></i>
            </button>
          </div>
        </div>
      </div>
    </main>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import allPosts, { getPostById, initialized } from '@/modules/posts';
import { useRoute } from 'vue-router';

export default defineComponent({
  name: 'PostPage',
  beforeRouteEnter(to, from, next) {
    allPosts()
      .loadPosts()
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
    let postId = parseInt(useRoute().params.id.toString());
    let post = getPostById(postId);
    return { post };
  },
});
</script>
