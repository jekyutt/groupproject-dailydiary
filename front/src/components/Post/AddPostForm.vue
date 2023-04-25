<template>
  <div
    class="
      min-h-screen
      flex
      justify-center
      bg-gray-50
      py-12
      px-4
      sm:px-6
      lg:px-8
    "
  >
    <div class="max-w-md w-full space-y-8">
      <div class="mt-8 space-y-6">
        <div>
          <label for="title">Title</label>
          <input
            id="title"
            v-model="post.title"
            type="text"
            class="
              appearance-none
              rounded
              relative
              block
              w-full
              px-3
              py-2
              border border-gray-300
              placeholder-gray-500
              text-gray-900
              focus:outline-none
              focus:ring-indigo-500
              focus:border-indigo-500
              focus:z-10
              sm:text-sm
            "
            placeholder="Title"
          />
        </div>
        <div>
          <label for="text">Text</label>
          <textarea
            id="text"
            v-model="post.text"
            type="text"
            class="
              appearance-none
              rounded
              relative
              block
              w-full
              px-3
              py-2
              border border-gray-300
              placeholder-gray-500
              text-gray-900
              focus:outline-none
              focus:ring-indigo-500
              focus:border-indigo-500
              focus:z-10
              sm:text-sm
            "
            placeholder="Text"
          />
        </div>
        <fieldset>
          <div>
            <legend class="text-base font-medium text-gray-900">
              Post Type
            </legend>
          </div>
          <div id="v-model-radiobutton" class="mt-4 space-y-4">
            <div class="flex items-center">
              <input
                id="type-private"
                v-model="post.type"
                type="radio"
                value="Private"
                class="
                  focus:ring-indigo-500
                  h-4
                  w-4
                  text-indigo-600
                  border-gray-300
                "
              />
              <label
                for="type-private"
                class="ml-3 block text-sm font-medium text-gray-700"
              >
                Private
              </label>
            </div>
            <div class="flex items-center">
              <input
                id="type-public"
                v-model="post.type"
                type="radio"
                value="Public"
                class="
                  focus:ring-indigo-500
                  h-4
                  w-4
                  text-indigo-600
                  border-gray-300
                "
              />
              <label
                for="type-public"
                class="ml-3 block text-sm font-medium text-gray-700"
              >
                Public
              </label>
            </div>
          </div>
        </fieldset>
        <fieldset>
          <legend class="text-base font-medium text-gray-900">18+</legend>
          <div class="mt-4 space-y-4">
            <div class="flex items-start">
              <div class="flex items-center h-5">
                <input
                  id="nsfw"
                  v-model="post.nsfw"
                  type="checkbox"
                  class="
                    focus:ring-indigo-500
                    h-4
                    w-4
                    text-indigo-600
                    border-gray-300
                    rounded
                  "
                />
              </div>
              <div class="ml-3 text-sm">
                <label for="nsfw" class="font-medium text-gray-700">Yes</label>
                <p class="text-gray-500">This post have 18+ content.</p>
              </div>
            </div>
          </div>
        </fieldset>
        <div>
          <label for="image">Image Link</label>
          <input
            id="image"
            v-model="post.imageLink"
            type="url"
            class="
              appearance-none
              rounded
              relative
              block
              w-full
              px-3
              py-2
              border border-gray-300
              placeholder-gray-500
              text-gray-900
              focus:outline-none
              focus:ring-indigo-500
              focus:border-indigo-500
              focus:z-10
              sm:text-sm
            "
            placeholder="link for your image"
          />
        </div>
        <div>
          <label for="image"
            >User:
            {{
              loggedUser.id != undefined ? loggedUser.id + ' |' : 'CHOOSE USER'
            }}
            {{ loggedUser != undefined ? loggedUser.userName : null }}</label
          >
        </div>
        <button
          class="
            group
            relative
            w-full
            flex
            justify-center
            py-2
            px-4
            border border-transparent
            text-sm
            font-medium
            rounded-md
            text-white
            bg-indigo-600
            hover:bg-indigo-700
            focus:outline-none
            focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500
          "
          @click="submitForm"
        >
          Submit
        </button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, Ref, ref } from 'vue';
import allPosts, { IAddPostRequest } from '@/modules/posts';
import allUsers from '@/modules/users';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'AddPostForm',
  setup() {
    const router = useRouter();

    const { loggedUser } = allUsers();

    const post: Ref<IAddPostRequest> = ref({
      type: '',
      title: '',
      text: '',
      releaseDate: new Date(Date.now()),
      nsfw: false,
      imageLink: '',
      ownerId: loggedUser.value.id!,
    });

    const { addPost } = allPosts();

    const submitForm = () => {
      if (loggedUser.value.id != null) {
        addPost({ ...post.value });
        post.value.type = '';
        post.value.title = '';
        post.value.text = '';
        post.value.releaseDate = new Date(Date.now());
        post.value.nsfw = false;
        post.value.imageLink = '';
        post.value.ownerId = '';

        router.push({ name: 'UserPosts' });
      }
    };

    return { post, submitForm, loggedUser };
  },
});
</script>
