<template>
  <body class="bg-gray-200 font-sans leading-normal tracking-normal">
    <!--Header-->
    <div
      class="m-0 p-0 bg-cover bg-bottom ;"
      style="height: 30vh; max-height: 460px; min-height: 200px"
    >
      <div
        class="container max-w-xl mx-auto pt-16 md:pt-8 text-left break-normal"
      >
        <!--Title-->
        <div v-if="showTypeChanged">
          <div
            class="
              mb-3
              bg-green-100
              border border-green-400
              text-green-700
              px-4
              py-3
              rounded
              relative
            "
            role="alert"
          >
            <strong class="font-bold">Successful!</strong>
            <span class="block sm:inline"> Post type is changed</span>
            <span
              class="absolute top-0 bottom-0 right-0 px-4 py-3"
              @click="showTypeChanged = false"
            >
              <svg
                class="fill-current h-6 w-6 text-green-500"
                role="button"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 20 20"
              >
                <title>Close</title>
                <path
                  d="M14.348 14.849a1.2 1.2 0 0 1-1.697 0L10 11.819l-2.651 3.029a1.2 1.2 0 1 1-1.697-1.697l2.758-3.15-2.759-3.152a1.2 1.2 0 1 1 1.697-1.697L10 8.183l2.651-3.031a1.2 1.2 0 1 1 1.697 1.697l-2.758 3.152 2.758 3.15a1.2 1.2 0 0 1 0 1.698z"
                />
              </svg>
            </span>
          </div>
        </div>

        <div class="flex items-center justify-between flex-wrap">
          <div class="order-3 mt-2 flex-shrink-0 sm:order-2 sm:mt-0 sm:w-auto">
            <router-link
              to="/newpost"
              class="
                flex
                items-center
                justify-center
                px-4
                py-2
                border border-transparent
                rounded-md
                shadow-sm
                text-sm
                font-medium
                text-indigo-600
                bg-white
                hover:bg-indigo-50
              "
            >
              New post
            </router-link>
          </div>

          <div>
            <input
              v-model="search"
              class="
                border-2 border-gray-300
                bg-white
                h-10
                px-5
                pr-16
                rounded-lg
                text-sm
                focus:outline-none
              "
              type="text"
              placeholder="search"
            />
          </div>
        </div>
      </div>
    </div>

    <!--Container-->
    <div class="container px-4 md:px-0 max-w-6xl mx-auto -mt-32 pb-9">
      <div v-if="filteredPosts.length != 0">
        <div
          v-for="post in filteredPosts"
          :key="post.id"
          class="mx-0 sm:mx-6 mb-9"
        >
          <Post :user="loggedUser" :post="post" @clicked="onPathClick()" />
        </div>
        <div class="grid grid-cols-5 gap-4 bg-gray-200 pb-20">
          <a
            class="
              col-start-3
              text-center
              flex-none
              items-center
              justify-center
              px-4
              py-4
              border border-transparent
              rounded-md
              shadow-sm
              text-sm
              font-medium
              text-indigo-600
              bg-white
              hover:bg-indigo-50
            "
            @click="scrollToTop()"
            >go to top!</a
          >
        </div>
      </div>
      <div v-else>
        <not-found></not-found>
      </div>
    </div>
  </body>
</template>

<script lang="ts">
import { defineComponent, onMounted } from 'vue';
import allPosts, { IPost } from '@/modules/posts';
import allUsers from '@/modules/users';
import Post from './Post.vue';
import NotFound from '@/components/NotFound.vue';

export default defineComponent({
  components: {
    Post,
    NotFound,
  },
  props: {
    title: {
      type: String,
      required: true,
    },
  },
  setup() {
    const { posts, loadPosts } = allPosts();
    posts.value.sort(
      (a, b) =>
        new Date(b.releaseDate).getTime() - new Date(a.releaseDate).getTime(),
    );
    const { loggedUser, loadUsers } = allUsers();

    onMounted(() => {
      loadPosts();
      loadUsers();
    });

    return { posts, loggedUser };
  },
  data: () => ({
    search: '',
    showTypeChanged: false,
  }),
  computed: {
    filteredPosts(): IPost[] {
      if (this.search != '' && this.search) {
        return this.postsToView.filter((post: IPost) => {
          return post.title.toLowerCase().includes(this.search.toLowerCase());
        });
      }
      return this.postsToView;
    },
    postsToView() {
      let a: IPost[] = [];
      this.posts.forEach((p) => {
        if (this.title == 'Feed') {
          if (p.type == '1') a.push(p);
        } else {
          if (p.ownerId == this.loggedUser.id) a.push(p);
        }
      });
      a.sort((a, b) => {
        return (
          new Date(b.releaseDate).getTime() - new Date(a.releaseDate).getTime()
        );
      });
      return a;
    },
  },
  methods: {
    scrollToTop() {
      window.scrollTo({
        top: 0,
        behavior: 'smooth',
      });
    },
    onPathClick() {
      this.showTypeChanged = true;
    },
  },
});
</script>
