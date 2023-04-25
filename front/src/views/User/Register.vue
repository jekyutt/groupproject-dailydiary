<template>
  <div
    class="
      min-h-screen
      flex
      items-center
      justify-center
      bg-gray-50
      py-12
      px-4
      sm:px-6
      lg:px-8
    "
  >
    <div class="max-w-md w-full space-y-8">
      <div>
        <h2 class="mt-6 text-center text-3xl font-extrabold text-gray-900">
          Create an account
        </h2>
        <p class="mt-2 text-center text-sm text-gray-600">
          Or
          <router-link
            to="/"
            class="font-medium text-indigo-600 hover:text-indigo-500"
          >
            log in
          </router-link>
        </p>
      </div>
      <form action="#" method="POST">
        <div class="shadow overflow-hidden sm:rounded-md">
          <div class="px-4 py-5 bg-white sm:p-6">
            <div class="grid grid-cols-6 gap-6">
              <div class="col-span-6 sm:col-span-4">
                <label
                  for="username"
                  class="block text-sm font-medium text-gray-700"
                  >Username <span class="text-red-500"> *</span></label
                >
                <input
                  id="username"
                  v-model="newUser.userName"
                  type="text"
                  name="username"
                  autocomplete="username"
                  class="
                    mt-1
                    focus:ring-indigo-500
                    focus:border-indigo-500
                    block
                    w-full
                    shadow-sm
                    sm:text-sm
                    border-gray-300
                    rounded-md
                  "
                />
              </div>
              <div class="col-span-6 sm:col-span-4">
                <label
                  for="email"
                  class="block text-sm font-medium text-gray-700"
                  >Email address<span class="text-red-500"> *</span></label
                >
                <input
                  id="email"
                  v-model="newUser.email"
                  type="email"
                  name="email"
                  autocomplete="email"
                  class="
                    mt-1
                    focus:ring-indigo-500
                    focus:border-indigo-500
                    block
                    w-full
                    shadow-sm
                    sm:text-sm
                    border-gray-300
                    rounded-md
                  "
                />
              </div>
              <div class="col-span-6 sm:col-span-4">
                <label
                  for="password"
                  class="block text-sm font-medium text-gray-700"
                  >Password<span class="text-red-500"> *</span></label
                >
                <input
                  id="password"
                  v-model="newUser.password"
                  type="password"
                  name="password"
                  autocomplete="new-password"
                  class="
                    mt-1
                    focus:ring-indigo-500
                    focus:border-indigo-500
                    block
                    w-full
                    shadow-sm
                    sm:text-sm
                    border-gray-300
                    rounded-md
                  "
                />
              </div>
              <div class="col-span-6 sm:col-span-4">
                <label
                  for="firstname"
                  class="block text-sm font-medium text-gray-700"
                  >Firstname<span class="text-red-500"> *</span></label
                >
                <input
                  id="firstname"
                  v-model="newUser.firstname"
                  type="text"
                  name="firstname"
                  class="
                    mt-1
                    focus:ring-indigo-500
                    focus:border-indigo-500
                    block
                    w-full
                    shadow-sm
                    sm:text-sm
                    border-gray-300
                    rounded-md
                  "
                />
              </div>
              <div class="col-span-6 sm:col-span-4">
                <label
                  for="lastname"
                  class="block text-sm font-medium text-gray-700"
                  >Lastname<span class="text-red-500"> *</span></label
                >
                <input
                  id="lastname"
                  v-model="newUser.lastname"
                  type="text"
                  name="lastname"
                  class="
                    mt-1
                    focus:ring-indigo-500
                    focus:border-indigo-500
                    block
                    w-full
                    shadow-sm
                    sm:text-sm
                    border-gray-300
                    rounded-md
                  "
                />
              </div>
              <div class="col-span-6 sm:col-span-4">
                <label
                  for="birthDate"
                  class="block text-sm font-medium text-gray-700"
                  >Date of Bitrh<span class="text-red-500"> *</span></label
                >
                <input
                  id="birthDate"
                  v-model="newUser.birthDate"
                  type="date"
                  name="birthDate"
                  class="
                    mt-1
                    focus:ring-indigo-500
                    focus:border-indigo-500
                    block
                    w-full
                    shadow-sm
                    sm:text-sm
                    border-gray-300
                    rounded-md
                  "
                />
              </div>
              <div class="col-span-6 sm:col-span-4">
                <label
                  for="phoneNumber"
                  class="block text-sm font-medium text-gray-700"
                  >Phone Number</label
                >
                <input
                  id="phoneNumber"
                  v-model="newUser.phoneNumber"
                  type="tel"
                  name="phoneNumber"
                  class="
                    mt-1
                    focus:ring-indigo-500
                    focus:border-indigo-500
                    block
                    w-full
                    shadow-sm
                    sm:text-sm
                    border-gray-300
                    rounded-md
                  "
                />
              </div>
              <div class="col-span-6 sm:col-span-4">
                <label for="bio" class="block text-sm font-medium text-gray-700"
                  >Biography</label
                >
                <input
                  id="bio"
                  v-model="newUser.bio"
                  type="text"
                  name="bio"
                  class="
                    mt-1
                    focus:ring-indigo-500
                    focus:border-indigo-500
                    block
                    w-full
                    shadow-sm
                    sm:text-sm
                    border-gray-300
                    rounded-md
                  "
                />
              </div>
              <div class="col-span-6 sm:col-span-4">
                <label
                  for="avatarLink"
                  class="block text-sm font-medium text-gray-700"
                  >Avatar Link</label
                >
                <input
                  id="avatarLink"
                  v-model="newUser.avatarLink"
                  type="url"
                  name="avatarLink"
                  class="
                    mt-1
                    focus:ring-indigo-500
                    focus:border-indigo-500
                    block
                    w-full
                    shadow-sm
                    sm:text-sm
                    border-gray-300
                    rounded-md
                  "
                />
              </div>
            </div>
          </div>
          <div>
            <button
              type="button"
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
              @click="submit()"
            >
              Register
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, Ref, ref } from 'vue';
import useAuth, { IRegisterRequest } from '@/modules/auth';
import router from '@/router';

export default defineComponent({
  name: 'RegisterForm',
  setup() {
    const { register } = useAuth();
    const newUser: Ref<IRegisterRequest> = ref({
      userName: '',
      email: '',
      password: '',
      firstname: '',
      lastname: '',
      birthDate: new Date(),
      phoneNumber: '',
      bio: '',
      avatarLink: '',
    });

    const submit = async () => {
      await register({ ...newUser.value });
      newUser.value.userName = '';
      newUser.value.email = '';
      newUser.value.password = '';
      newUser.value.firstname = '';
      newUser.value.lastname = '';
      newUser.value.birthDate = new Date();
      newUser.value.phoneNumber = '';
      newUser.value.bio = '';
      newUser.value.avatarLink = '';

      router.push('/login');
    };

    return { newUser, submit };
  },
});
</script>
