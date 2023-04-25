<template>
  <nav class="bg-gray-800">
    <div class="max-w-7xl mx-auto px-2 sm:px-6 lg:px-8">
      <div class="relative flex items-center justify-between h-16">
        <div
          class="
            flex-1 flex
            items-center
            justify-center
            sm:items-stretch
            sm:justify-start
          "
        >
          <div class="flex-shrink-0 flex items-center">
            <img
              class="block h-8 w-auto"
              src="https://i.imgur.com/fA8YczW.png"
              alt="Daily Diary"
            />
            <img
              class="hidden lg:block h-8 w-auto"
              src="https://i.imgur.com/fZm50X1.png"
              alt="Daily Diary"
            />
          </div>
          <div class="hidden sm:block sm:ml-6">
            <div class="flex space-x-4">
              <router-link
                to="/"
                class="
                  text-gray-300
                  hover:bg-gray-700
                  hover:text-white
                  px-3
                  py-2
                  rounded-md
                  text-sm
                  font-medium
                "
                active-class="bg-gray-900 text-white"
                >Home</router-link
              >
              <router-link
                v-if="user != null"
                to="/feed"
                class="
                  text-gray-300
                  hover:bg-gray-700
                  hover:text-white
                  px-3
                  py-2
                  rounded-md
                  text-sm
                  font-medium
                "
                active-class="bg-gray-900 text-white"
                >Feed</router-link
              >
            </div>
          </div>
        </div>

        <div
          class="
            relative
            inset-y-0
            right-0
            flex
            items-center
            pr-2
            sm:static
            sm:inset-auto
            sm:ml-6
            sm:pr-0
          "
        >
          <!-- <router-link
            to="/newuser"
            class="mr-4 text-gray-300 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium"
            active-class="bg-gray-900 text-white"
            >Create new user</router-link
          > -->
          <div v-if="true">
            <router-link
              to="/myposts"
              class="
                mr-4
                text-gray-300
                hover:bg-gray-700
                hover:text-white
                px-3
                py-2
                rounded-md
                text-sm
                font-medium
              "
              active-class="bg-gray-900 text-white"
              >My Posts</router-link
            >
          </div>

          <!-- Profile dropdown -->

          <div v-if="user != undefined">
            <div class="ml-3 relative">
              <div>
                <button
                  id="user-menu-button"
                  type="button"
                  class="
                    bg-gray-800
                    flex
                    text-sm
                    rounded-full
                    focus:outline-none
                    focus:ring-2
                    focus:ring-offset-2
                    focus:ring-offset-gray-800
                    focus:ring-white
                  "
                  @click="profileDropDownOpen = !profileDropDownOpen"
                >
                  <span class="sr-only">Open user menu</span>
                  <img
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
                    :src="
                      user.avatarLink != undefined
                        ? user.avatarLink
                        : 'https://images.unsplash.com/photo-1612259474641-8ff3fa45c89b?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1035&q=80'
                    "
                    :alt="user.userName"
                  />
                </button>
              </div>
              <transition
                enter-active-class="transition ease-out duration-100"
                enter-from-class="transform opacity-0 scale-95"
                enter-to-class="transform opacity-100 scale-100"
                leave-active-class="transition ease-in duration-75"
                leave-from-class="transform opacity-100 scale-100"
                leave-to-class="transform opacity-0 scale-95"
              >
                <div
                  v-show="profileDropDownOpen"
                  class="
                    z-40
                    origin-top-right
                    absolute
                    right-0
                    mt-2
                    w-48
                    rounded-md
                    shadow-lg
                    py-1
                    bg-white
                    ring-1 ring-black ring-opacity-5
                    focus:outline-none
                  "
                  role="menu"
                  aria-orientation="vertical"
                  aria-labelledby="user-menu-button"
                  tabindex="-1"
                >
                  <router-link
                    id="user-menu-item-0"
                    :to="{
                      name: 'Profile',
                      params: { id: user.id },
                    }"
                    class="block px-4 py-2 text-sm text-gray-700"
                    role="menuitem"
                    @click="profileDropDownOpen = false"
                    >Your Profile</router-link
                  >
                  <router-link
                    id="user-menu-item-0"
                    :to="{
                      name: 'Edit User',
                      params: { id: user.id },
                    }"
                    class="block px-4 py-2 text-sm text-gray-700"
                    role="menuitem"
                    @click="profileDropDownOpen = false"
                    >Settings</router-link
                  >
                  <button
                    class="block px-4 py-2 text-sm text-gray-700"
                    active-class="bg-gray-900 text-white"
                    @click="logout(), (profileDropDownOpen = false)"
                  >
                    Logout
                  </button>
                </div>
              </transition>
            </div>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import { IUser } from '@/modules/users';
import router from '@/router';
import { defaultAuthData } from '@/modules/auth';
import { state } from '@/modules/users';

export default defineComponent({
  name: 'Nav',
  props: {
    user: {
      type: Object as PropType<IUser>,
      required: true,
    },
  },
  data: () => ({
    isOpen: false,
    profileDropDownOpen: false,
  }),
  methods: {
    logout() {
      state.loggedUser = { id: '' } as IUser;
      localStorage.removeItem('token');
      defaultAuthData();
      router.push('/');
    },
  },
});
</script>
