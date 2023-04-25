import { reactive, toRef, toRefs } from 'vue';
import useApi from './api';
import { IPost, reLoad } from './posts';
import { IUser } from './users';

export interface ICommentary {
  id?: number;
  text: string;
  ownerId: string;
  postId: number;
  likes: IUser[];
  owner: IUser;
  post: IPost;
}

export interface IAddCommentaryRequest {
  text: string;
  ownerId: string;
  postId: number;
}

export interface IPutCommentaryRequest extends IAddCommentaryRequest {}

export interface IPatchCommentaryRequest {
  op: String;
  path: String;
  value: String;
}

export interface State {
  commentaries: ICommentary[];
}

let commentaries: ICommentary[] = [];

const state = reactive<State>({
  commentaries: [],
});

export let initialized = false;

export default function useCommentary() {
  const apiGetCommentaries = useApi<ICommentary[]>('Commentaries', {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
      Authorization: `bearer ${localStorage.getItem('token')}`,
    },
  });
  const loadCommentaries = async () => {
    if (!initialized) {
      await apiGetCommentaries.request();

      if (apiGetCommentaries.response.value) {
        commentaries = apiGetCommentaries.response.value!;
        state.commentaries = commentaries;
      }

      initialized = true;
    }
  };

  const addCommentary = async (commentary: IAddCommentaryRequest) => {
    const apiAddCommentary = useApi<IAddCommentaryRequest>('Commentaries', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        Authorization: `bearer ${localStorage.getItem('token')}`,
      },
      body: JSON.stringify(commentary),
    });

    await apiAddCommentary.request();

    if (apiAddCommentary.response.value) {
      commentaries.push(
        <ICommentary>(<unknown>apiAddCommentary.response.value!),
      );
      state.commentaries = commentaries;
      reLoad();
    }
  };

  const editCommentary = async (
    commentary: IPutCommentaryRequest,
    id: number,
  ) => {
    const apiEditCommentary = useApi<IPutCommentaryRequest>(
      'Commentaries/' + id,
      {
        method: 'PUT',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
          Authorization: `bearer ${localStorage.getItem('token')}`,
        },
        body: JSON.stringify(commentary),
      },
    );

    await apiEditCommentary.request();

    if (apiEditCommentary.response.value) {
      commentaries.push(
        <ICommentary>(<unknown>apiEditCommentary.response.value!),
      );
      state.commentaries = commentaries;
    }
  };

  const deleteCommentary = async (commentary: ICommentary) => {
    const apiDeleteCommentary = useApi<ICommentary>(
      'Commentaries/' + commentary.id,
      {
        method: 'DELETE',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
          Authorization: `bearer ${localStorage.getItem('token')}`,
        },
        body: JSON.stringify(commentary),
      },
    );

    await apiDeleteCommentary.request();
  };

  const patchCommentary = async (
    request: [IPatchCommentaryRequest],
    id: number | undefined,
  ) => {
    const apiPatchCommentary = useApi<[IPatchCommentaryRequest]>(
      'Commentaries/' + id,
      {
        method: 'PATCH',
        headers: {
          Accept: 'application/json-patch+json',
          'Content-Type': 'application/json-patch+json',
          Authorization: `bearer ${localStorage.getItem('token')}`,
        },
        body: JSON.stringify(request),
      },
    );

    await apiPatchCommentary.request();

    if (apiPatchCommentary.response.value) {
      commentaries.push(
        <ICommentary>(<unknown>apiPatchCommentary.response.value!),
      );
      state.commentaries = commentaries;
      initialized = false;
      loadCommentaries();
    }
  };

  return {
    ...toRefs(state),
    loadCommentaries,
    addCommentary,
    deleteCommentary,
    patchCommentary,
    editCommentary,
  };
}

export function GetCommentaryById(id: number): ICommentary | undefined {
  if (!initialized) {
    useCommentary().loadCommentaries();
  }
  return state.commentaries.find((x) => x.id == id);
}
