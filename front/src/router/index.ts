import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import useAuth, { getAuthData } from '@/modules/auth';

import AddPostVue from '@/views/Post/AddPost.vue';
import EditPostVue from '@/views/Post/EditPost.vue';
import AddUserVue from '@/views/User/AddUser.vue';
import LoginVue from '@/views/User/Login.vue';
import RegisterVue from '@/views/User/Register.vue';
//import UsersVue from '@/views/Users.vue';
import HomeVue from '@/views/Home.vue';
import PrivacyVue from '@/views/Privacy.vue';
import ContactVue from '@/views/Contact.vue';
import ProfileVue from '@/views/User/Profile.vue';
import FeedVue from '@/views/Post/Feed.vue';
import UserPostsVue from '@/views/User/UserPosts.vue';
import PostPageVue from '@/components/Post/PostPage.vue';
import NotFoundVue from '@/components/NotFound.vue';
import EditUserVue from '@/views/User/EditUser.vue';

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'Home',
    component: HomeVue,
    meta: {
      requiredAuth: false,
    },
  },
  {
    path: '/login',
    name: 'Login',
    component: LoginVue,
    meta: {
      requiredAuth: false,
    },
  },
  {
    path: '/register',
    name: 'Register',
    component: RegisterVue,
    meta: {
      requiredAuth: false,
    },
  },
  {
    path: '/feed',
    name: 'Feed',
    component: FeedVue,
    meta: {
      requiredAuth: true,
    },
  },
  {
    path: '/myposts',
    name: 'UserPosts',
    component: UserPostsVue,
    meta: {
      requiredAuth: true,
    },
  },
  // {
  //   path: '/users',
  //   name: 'Users',
  //   component: UsersVue,
  //   // meta: {
  //   //   requiredAuth:: true,
  //   // },
  // },
  {
    path: '/newpost',
    name: 'New Post',
    component: AddPostVue,
    meta: {
      requiredAuth: true,
    },
  },
  {
    path: '/editpost/:id',
    name: 'Edit Post',
    component: EditPostVue,
    props: true,
    meta: {
      requiredAuth: true,
    },
  },
  {
    path: '/newuser',
    name: 'New User',
    component: AddUserVue,
    meta: {
      requiredAuth: true,
    },
  },
  {
    path: '/privacy',
    name: 'Privacy',
    component: PrivacyVue,
    meta: {
      requiredAuth: false,
    },
  },
  {
    path: '/contact',
    name: 'Contact',
    component: ContactVue,
    meta: {
      requiredAuth: false,
    },
  },
  {
    path: '/post/:id',
    name: 'PostPage',
    component: PostPageVue,
    props: true,
    meta: {
      requiredAuth: true,
    },
  },
  {
    path: '/profile/:id',
    name: 'Profile',
    component: ProfileVue,
    props: true,
    meta: {
      requiredAuth: true,
    },
  },
  {
    path: '/edituser/:id',
    name: 'Edit User',
    component: EditUserVue,
    props: true,
    meta: {
      requiredAuth: true,
    },
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'not-found',
    component: NotFoundVue,
    meta: {
      requiredAuth: false,
    },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.resolve({
  name: 'not-found',
  params: { pathMatch: ['not', 'found'] },
}).href; // '/not/found'

router.beforeEach((to, from, next) => {
  if (to.meta.requiredAuth) {
    if (localStorage.getItem('token') == null) {
      return next({ path: '/login' });
    } else {
      if (getAuthData().userId == null || getAuthData().userId == '') {
        const { saveTokenData, setLoginStatus } = useAuth();
        saveTokenData({
          token: localStorage.getItem('token')!,
          result: '',
          errors: '',
        });
        setLoginStatus('success');
      }
      const { isTokenActive } = useAuth();
      if (!isTokenActive()) {
        return next({ path: '/login' });
      } else {
        return next();
      }
    }
  }
  return next();
});

export default router;
