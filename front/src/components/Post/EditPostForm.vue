<template>
  <div
    class="min-h-screen flex justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8"
  >
    <div v-if="post != undefined" class="max-w-md w-full space-y-8">
      <div class="mt-8 space-y-6">
        <div>
          <label for="title">Title</label>
          <input
            id="title"
            v-model="post.title"
            type="text"
            class="appearance-none rounded relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            placeholder="Title"
          />
        </div>
        <div>
          <label for="text">Text</label>
          <textarea
            id="text"
            v-model="post.text"
            type="text"
            class="appearance-none rounded relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            placeholder="Text"
          />
        </div>

        <div>
          <label for="image">Image Link</label>
          <input
            id="image"
            v-model="post.imageLink"
            type="url"
            class="appearance-none rounded relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            placeholder="Link for your image"
          />
        </div>

        <fieldset>
          <div class="space-y-4">
            <div class="flex items-start">
              <div class="flex items-center h-5">
                <input
                  id="nsfw"
                  v-model="post.nsfw"
                  type="checkbox"
                  class="focus:ring-indigo-500 h-4 w-4 text-indigo-600 border-gray-300 rounded"
                />
              </div>
              <div class="ml-3 text-sm">
                <label for="nsfw" class="font-medium text-gray-700"
                  >This post have 18+ content</label
                >
              </div>
            </div>
          </div>
        </fieldset>

        <button
          class="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
          @click="submitForm(post)"
        >
          Submit
        </button>
      </div>
    </div>
  </div>
</template>
w
<script lang="ts">
import { defineComponent, ref, Ref } from 'vue';
import allPosts, { getPostById, IPutPostRequest, IPost } from '@/modules/posts';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'EditPostForm',
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  setup(props) {
    const router = useRouter();

    const post: Ref<IPost> = ref(getPostById(parseInt(props.id))!);
    const request: Ref<IPutPostRequest> = ref({
      type: post.value.type == '1' ? 'Public' : 'Private',
      title: post.value.title,
      text: post.value.text,
      releaseDate: post.value.releaseDate,
      nsfw: post.value.nsfw,
      imageLink: post.value.imageLink,
      ownerId: post.value.ownerId,
    });

    const { editPost } = allPosts();

    const submitForm = (post: IPost) => {
      request.value.title = post.title;
      request.value.text = post.text;
      request.value.imageLink = post.imageLink;
      request.value.nsfw = post.nsfw;

      editPost({ ...request.value }, post.id!);

      router.push({ name: 'UserPosts' });
    };

    return { submitForm, post };
  },
});
</script>
