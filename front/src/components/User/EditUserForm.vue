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
          <label for="username">Username</label>
          <input
            id="username"
            v-model="user.userName"
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
            placeholder="Username"
          />
        </div>
        <div>
          <label for="email">Email</label>
          <input
            id="email"
            v-model="user.email"
            type="email"
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
            placeholder="Email"
          />
        </div>
        <div>
          <label for="firstname">Firstname</label>
          <input
            id="firstname"
            v-model="user.firstname"
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
            placeholder="Firstname"
          />
        </div>
        <div>
          <label for="lastname">Lastname</label>
          <input
            id="lastname"
            v-model="user.lastname"
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
            placeholder="Lastname"
          />
        </div>
        <div>
          <label for="phoneNumber">Phone Number</label>
          <input
            id="phoneNumber"
            v-model="user.phoneNumber"
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
            placeholder="Phone Number"
          />
        </div>
        <div>
          <label for="bio">Bio</label>
          <textarea
            id="bio"
            v-model="user.bio"
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
        <div>
          <label for="avatar">Avatar Link</label>
          <input
            id="avatar"
            v-model="user.avatarLink"
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
            placeholder="link for your avatar"
          />
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
          @click="submitForm(user)"
        >
          Submit
        </button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, Ref, ref } from 'vue';
//import { useRouter } from 'vue-router';
import allUsers, { getUserById, IPutUserRequest, IUser } from '@/modules/users';

export default defineComponent({
  name: 'EditUserForm',
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  setup(props) {
    const user: Ref<IUser> = ref(getUserById(props.id)!);

    const { editUser } = allUsers();

    const submitForm = (user: IUser) => {
      const request: Ref<IPutUserRequest> = ref({
        username: user.userName,
        email: user.email,
        firstname: user.firstname,
        lastname: user.lastname,
        phoneNumber: user.phoneNumber,
        bio: user.bio,
        avatarLink: user.avatarLink,
      });
      console.log(request);

      editUser({ ...request.value }, user.id!);
    };

    return { user, submitForm };
  },
});
</script>

<style scoped></style>
