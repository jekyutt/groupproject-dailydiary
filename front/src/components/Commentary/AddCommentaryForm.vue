<template>
  <div
    class="
      relative
      flex
      items-center
      self-center
      w-full
      max-w-xl
      p-4
      overflow-hidden
      text-gray-600
      focus-within:text-gray-400
    "
  >
    <img
      class="w-10 h-10 object-cover rounded-full shadow mr-2 cursor-pointer"
      alt="User avatar"
      :src="user.avatarLink"
    />
    <span class="absolute inset-y-0 right-0 flex items-center pr-6">
      <button
        type="submit"
        class="p-1 focus:outline-none focus:shadow-none hover:text-blue-500"
        @click="submitForm"
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          class="h-6 w-6"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
          transform="rotate(90)"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M12 19l9 2-9-18-9 18 9-2zm0 0v-8"
          />
        </svg>
      </button>
    </span>
    <input
      v-model="commentary.text"
      type="text"
      class="
        w-full
        py-2
        pl-4
        pr-10
        text-sm
        bg-gray-100
        border border-transparent
        appearance-none
        rounded-tg
        placeholder-gray-400
        focus:bg-white
        focus:outline-none
        focus:border-blue-500
        focus:text-gray-900
        focus:shadow-outline-blue
      "
      style="border-radius: 25px"
      placeholder="Post a comment..."
      autocomplete="off"
      @keyup.enter="submitForm"
    />
  </div>
</template>

<script lang="ts">
import allCommentaries, { IAddCommentaryRequest } from '@/modules/commentaries';
import { IPost } from '@/modules/posts';
import { defineComponent, Ref, ref, PropType } from 'vue';
import { IUser } from '@/modules/users';

export default defineComponent({
  name: 'AddCommentaryForm',
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
  setup(props) {
    const commentary: Ref<IAddCommentaryRequest> = ref({
      text: '',
      ownerId: props.user.id!,
      postId: props.post.id!,
    });

    const { addCommentary } = allCommentaries();

    const submitForm = () => {
      addCommentary({ ...commentary.value });
      commentary.value.text = '';
    };

    return { submitForm, commentary };
  },
});
</script>

<style></style>
