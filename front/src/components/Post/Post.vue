<template>
  <!--Nav-->
  <nav class="flex max-w-xl my-0 mx-auto">
    <div class="container flex items-center">
      <div class="flex w-1/2 pl-4 text-sm">
        <ul
          class="
            list-reset
            flex
            justify-between
            flex-1
            md:flex-none
            items-center
          "
        >
          <li class="mr-2">
            <router-link
              class="
                inline-block
                text-gray-600
                no-underline
                hover:text-gray-400
                py-2
                px-2
              "
              :to="{
                name: 'PostPage',
                params: { id: post.id },
              }"
              >full</router-link
            >
          </li>
          <li class="mr-2">
            <router-link
              v-if="post.ownerId == user.id"
              class="
                inline-block
                text-gray-600
                no-underline
                hover:text-gray-400
                py-2
                px-2
              "
              :to="{ name: 'Edit Post', params: { id: post.id } }"
              >Edit
            </router-link>
          </li>
          <li>
            <router-link
              v-if="post.ownerId == user.id"
              to="/feed"
              class="
                inline-block
                text-gray-600
                no-underline
                hover:text-gray-400
                py-2
                px-2
              "
              @click="deletePostFromDb"
              >Delete</router-link
            >
          </li>
        </ul>
      </div>

      <div v-if="blurred" class="flex w-1/2 justify-center conternt-center">
        <button
          class="text-gray-600 no-underline hover:text-gray-400 py-2 px-2"
          @click="blurred = !blurred"
        >
          Show
        </button>
      </div>

      <div class="flex w-1/2 justify-end content-center">
        <a
          class="
            inline-block
            text-gray-500
            no-underline
            hover:text-white hover:text-underline
            text-center
            h-10
            p-2
            md:h-auto
            md:p-4
            avatar
          "
          data-tippy-content="@twitter_handle"
          href="https://twitter.com/intent/tweet?url=#"
        >
          <svg
            class="fill-current h-4"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 32 32"
          >
            <path
              d="M30.063 7.313c-.813 1.125-1.75 2.125-2.875 2.938v.75c0 1.563-.188 3.125-.688 4.625a15.088 15.088 0 0 1-2.063 4.438c-.875 1.438-2 2.688-3.25 3.813a15.015 15.015 0 0 1-4.625 2.563c-1.813.688-3.75 1-5.75 1-3.25 0-6.188-.875-8.875-2.625.438.063.875.125 1.375.125 2.688 0 5.063-.875 7.188-2.5-1.25 0-2.375-.375-3.375-1.125s-1.688-1.688-2.063-2.875c.438.063.813.125 1.125.125.5 0 1-.063 1.5-.25-1.313-.25-2.438-.938-3.313-1.938a5.673 5.673 0 0 1-1.313-3.688v-.063c.813.438 1.688.688 2.625.688a5.228 5.228 0 0 1-1.875-2c-.5-.875-.688-1.813-.688-2.75 0-1.063.25-2.063.75-2.938 1.438 1.75 3.188 3.188 5.25 4.25s4.313 1.688 6.688 1.813a5.579 5.579 0 0 1 1.5-5.438c1.125-1.125 2.5-1.688 4.125-1.688s3.063.625 4.188 1.813a11.48 11.48 0 0 0 3.688-1.375c-.438 1.375-1.313 2.438-2.563 3.188 1.125-.125 2.188-.438 3.313-.875z"
            ></path>
          </svg>
        </a>
        <a
          class="
            inline-block
            text-gray-500
            no-underline
            hover:text-white hover:text-underline
            text-center
            h-10
            p-2
            md:h-auto
            md:p-4
            avatar
          "
          data-tippy-content="#facebook_id"
          href="https://www.facebook.com/sharer/sharer.php?u=#"
        >
          <svg
            class="fill-current h-4"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 32 32"
          >
            <path
              d="M19 6h5V0h-5c-3.86 0-7 3.14-7 7v3H8v6h4v16h6V16h5l1-6h-6V7c0-.542.458-1 1-1z"
            ></path>
          </svg>
        </a>
      </div>
    </div>
  </nav>

  <div
    class="
      w-full
      text-xl
      md:text-2xl
      text-gray-800
      leading-normal
      rounded-t
      pb-9
    "
  >
    <!--Lead Card-->

    <div class="">
      <div
        class="
          flex
          max-w-xl
          my-5
          bg-white
          shadow-md
          rounded-lg
          overflow-hidden
          mx-auto
        "
      >
        <div class="flex items-center w-full">
          <div class="w-full">
            <div class="flex flex-row mt-2 px-2 py-3 mx-3">
              <div class="w-auto h-auto rounded-full border-2">
                <img
                  class="
                    w-12
                    h-12
                    object-cover
                    rounded-full
                    shadow
                    cursor-pointer
                  "
                  alt="User avatar"
                  :src="post.owner.avatarLink"
                />
              </div>
              <div class="flex flex-col mb-2 ml-4 mt-1">
                <router-link
                  class="text-gray-600 text-sm font-semibold"
                  :to="{
                    name: 'Profile',
                    params: { id: post.ownerId },
                  }"
                >
                  {{ post.owner.userName }}
                </router-link>
                <div class="flex w-full mt-1">
                  <div
                    class="text-blue-700 font-base text-xs mr-1 cursor-pointer"
                  >
                    {{ post.type == '0' ? 'Private' : 'Public' }}
                    {{ post.nsfw == true ? '| 18+' : '' }}
                  </div>
                  <div class="text-gray-400 font-thin text-xs">
                    {{ timeSince }}
                  </div>
                </div>
              </div>

              <div class="flex flex-col ml-auto">
                <div v-if="post.ownerId == user.id">
                  <label
                    for="postTypeToggle"
                    class="
                      relative
                      inline-flex
                      flex-shrink-0
                      h-6
                      w-11
                      border-2 border-transparent
                      rounded-full
                      cursor-pointer
                      transition-colors
                      ease-in-out
                      duration-200
                      focus-within:outline-none
                      focus-within:ring-2
                      focus-within:ring-offset-2
                      focus-within:ring-indigo-500
                    "
                    :class="[toggled == false ? 'bg-gray-200' : 'bg-green-700']"
                    @click="
                      patchPostOnDb(['/type', post.type == '0' ? '1' : '0']);
                      onPatchClick();
                    "
                  >
                    <span class="sr-only">Use setting</span>
                    <!-- On: "translate-x-5", Off: "translate-x-0" -->
                    <span
                      aria-hidden="true"
                      class="
                        inline-block
                        h-5
                        w-5
                        bg-white
                        rounded-full
                        shadow
                        transform
                        ring-0
                        transition
                        ease-in-out
                        duration-200
                        pointer-events-none
                      "
                      :class="[
                        toggled == false ? 'transalte-x-0' : 'translate-x-5',
                      ]"
                    ></span>
                    <input
                      id="settingToggle"
                      type="postTypeToggle"
                      class="absolute inset-0 sr-only"
                    />
                  </label>
                </div>
              </div>
            </div>
            <div
              class="border-b border-gray-100"
              :class="blurred ? 'filter blur-lg' : ''"
            >
              <div
                class="text-gray-400 font-medium text-sm mb-7 mt-6 mx-3 px-2"
              >
                <img class="rounded" :src="post.imageLink" alt="sorry" />
              </div>
              <div
                class="
                  text-gray-600
                  font-semibold
                  text-lg
                  mb-2
                  mx-3
                  px-2
                  word-wrap:
                  break-words
                "
              >
                {{ post.title }}
              </div>
              <div
                class="
                  text-gray-500
                  font-thin
                  text-sm
                  mb-6
                  mx-3
                  px-2
                  word-wrap:
                  break-words
                "
              >
                {{ post.text.slice(0, 200) }}
                <router-link
                  v-show="post.text.length >= 200"
                  class="
                    text-gray-600
                    no-underline
                    hover:text-gray-200
                    hover:underline
                    py-2
                    px-2
                  "
                  :to="{
                    name: 'PostPage',
                    params: { id: post.id },
                  }"
                >
                  Read more...</router-link
                >
              </div>
              <div class="flex justify-start mb-4 border-t border-gray-100">
                <div class="flex w-full mt-1 pt-2 pl-5">
                  <button
                    class="
                      bg-white
                      transition
                      ease-out
                      duration-300
                      hover:text-red-500
                      border
                      w-8
                      h-8
                      px-2
                      pt-2
                      text-center text-gray-400
                      rounded-full
                      cursor-pointer
                      mr-2
                      outline-none
                    "
                    @click="
                      patchPostOnDb(['/likes', user == undefined ? '1' : '0'])
                    "
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      className="h-6 w-6"
                      fill="none"
                      viewBox="2 5 20 20"
                      stroke="currentColor"
                    >
                      <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        strokeWidth="{2}"
                        d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z"
                      />
                    </svg>
                  </button>
                  <img
                    v-if="post.likes != null"
                    class="
                      inline-block
                      object-cover
                      w-8
                      h-8
                      text-white
                      border-2 border-white
                      rounded-full
                      shadow-sm
                      cursor-pointer
                    "
                    :src="post.owner.avatarLink"
                    alt="https://images.unsplash.com/photo-1491528323818-fdd1faba62cc?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80"
                  />
                </div>
              </div>
              <div
                class="
                  w-full
                  border-t border-gray-100
                  mt-3
                  mx-5
                  flex-col
                  divide-y divide-gray-200
                "
              >
                <div
                  v-for="commentary in post.commentaries"
                  :key="commentary.id"
                >
                  <Commentary :commentary="commentary" />
                </div>
              </div>
              <AddCommentaryForm :user="user" :post="post" />
            </div>
          </div>
        </div>
      </div>
    </div>
    <!--/Lead Card-->
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType, Ref, ref } from 'vue';
import allPosts, { IPatchPostRequest, IPost } from '@/modules/posts';
import { IUser } from '@/modules/users';
import router from '@/router';
import AddCommentaryForm from '@/components/Commentary/AddCommentaryForm.vue';
import Commentary from '@/components/Commentary/Commentary.vue';
//import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'Post',
  components: {
    Commentary,
    AddCommentaryForm,
  },
  props: {
    post: {
      type: Object as PropType<IPost>,
      required: true,
    },
    user: {
      type: Object as PropType<IUser>,
      required: true,
    },
  },
  emits: ['clicked'],
  setup(props) {
    const { deletePost, patchPost } = allPosts();

    const deletePostFromDb = () => {
      deletePost({ ...props.post });
      router.push({ name: 'Feed' });
    };
    const request: Ref<IPatchPostRequest> = ref({
      op: 'replace',
      path: '',
      value: '',
    });

    const patchPostOnDb = (params: [string, string]) => {
      request.value.path = params[0];
      request.value.value = params[1];
      patchPost([{ ...request.value }], props.post.id);
      request.value.op = 'replace';
      request.value.path = '';
      request.value.value = '';
    };

    return { deletePostFromDb, request, patchPostOnDb };
  },
  data: () => ({
    // longText: this.post.text,
    toggled: false,
    blurred: false,
  }),
  computed: {
    timeSince() {
      let currentDate = new Date().getTime();
      let input = new Date(this.post.releaseDate).getTime();
      var seconds = Math.floor((currentDate - input) / 1000);

      var interval = seconds / 31536000;
      if (interval > 1) {
        let number = Math.floor(interval);
        return number >= 2 ? number + ' years ago' : number + ' year ago';
      }
      interval = seconds / 2592000;
      if (interval > 1) {
        let number = Math.floor(interval);
        return number >= 2 ? number + ' months ago' : number + ' month ago';
      }
      interval = seconds / 86400;
      if (interval > 1) {
        let number = Math.floor(interval);
        return number >= 2 ? number + ' days ago' : number + ' day ago';
      }
      interval = seconds / 3600;
      if (interval > 1) {
        let number = Math.floor(interval);
        return number >= 2 ? number + ' hours ago' : number + ' hour ago';
      }
      interval = seconds / 60;
      if (interval > 1) {
        let number = Math.floor(interval);
        return number >= 2 ? number + ' minutes ago' : number + ' minute ago';
      }
      let number = Math.floor(seconds);
      return number >= 2 ? number + ' seconds ago' : number + ' second ago';
    },
  },
  created() {
    this.blurred = this.post.nsfw;
    this.toggled = this.post.type == '1' ? true : false;
  },
  methods: {
    onPatchClick() {
      this.$emit('clicked');
      this.toggled = !this.toggled;
    },
  },
});
</script>
