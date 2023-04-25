import { reactive, Ref, toRefs } from 'vue';
import useApi from './api';
import { IUser } from './users';
import { ICommentary } from './commentaries';

export interface IPost {
  id?: number;
  type: string;
  title: string;
  text: string;
  releaseDate: Date;
  nsfw: boolean;
  imageLink: string;
  ownerId: string;
  likes: IUser[];
  owner: IUser;
  commentaries: ICommentary[];
}

export interface IAddPostRequest {
  type: string;
  title: string;
  text: string;
  releaseDate: Date;
  nsfw: boolean;
  imageLink: string;
  ownerId: string;
}

export interface IPutPostRequest extends IAddPostRequest {}

export interface IPatchPostRequest {
  op: String;
  path: String;
  value: String;
}

export interface State {
  posts: IPost[];
}

let posts: IPost[] = [];

const state = reactive<State>({
  posts: [],
});

export let initialized = false;

export default function usePost() {
  const apiGetPosts = useApi<IPost[]>('Posts', {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
      Authorization: `bearer ${localStorage.getItem('token')}`,
    },
  });
  const loadPosts = async () => {
    if (!initialized) {
      await apiGetPosts.request();

      if (apiGetPosts.response.value) {
        posts = apiGetPosts.response.value!;
        state.posts = posts;
      }

      initialized = true;
    }
  };

  const addPost = async (post: IAddPostRequest) => {
    const apiAddPost = useApi<IAddPostRequest>('Posts', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        Authorization: `bearer ${localStorage.getItem('token')}`,
      },
      body: JSON.stringify(post),
    });

    await apiAddPost.request();

    if (apiAddPost.response.value) {
      posts.push(<IPost>(<unknown>apiAddPost.response.value!));
      state.posts = posts;
    }
  };

  const editPost = async (post: IPutPostRequest, id: number) => {
    const apiEditPost = useApi<IPutPostRequest>('Posts/' + id, {
      method: 'PUT',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        Authorization: `bearer ${localStorage.getItem('token')}`,
      },
      body: JSON.stringify(post),
    });

    await apiEditPost.request();

    if (apiEditPost.response.value) {
      posts.push(<IPost>(<unknown>apiEditPost.response.value!));
      state.posts = posts;
    }
  };

  const deletePost = async (post: IPost) => {
    const apiDeletePost = useApi<IPost>('Posts/' + post.id, {
      method: 'DELETE',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        Authorization: `bearer ${localStorage.getItem('token')}`,
      },
      body: JSON.stringify(post),
    });

    await apiDeletePost.request();
  };

  const patchPost = async (
    request: [IPatchPostRequest],
    postId: number | undefined,
  ) => {
    const apiPatchPost = useApi<[IPatchPostRequest]>('Posts/' + postId, {
      method: 'PATCH',
      headers: {
        Accept: 'application/json-patch+json',
        'Content-Type': 'application/json-patch+json',
        Authorization: `bearer ${localStorage.getItem('token')}`,
      },
      body: JSON.stringify(request),
    });

    await apiPatchPost.request();

    if (apiPatchPost.response.value) {
      posts.push(<IPost>(<unknown>apiPatchPost.response.value!));
      state.posts = posts;
      initialized = false;
      loadPosts();
    }
  };

  return {
    ...toRefs(state),
    loadPosts,
    addPost,
    deletePost,
    patchPost,
    editPost,
  };
}

export function getPostById(id: number): IPost | undefined {
  if (!initialized) {
    usePost().loadPosts();
  }
  return state.posts.find((x) => x.id == id);
}

export function reLoad() {
  initialized = false;
  usePost().loadPosts();
}
