import { reactive, toRefs } from 'vue';
import useApi from './api';
import { ICommentary } from './commentaries';
import { IPost } from './posts';

export type UserRequest = () => IUser;

export interface IUser {
  id?: string;
  type: number;
  userName: string;
  email: string;
  firstname: string;
  lastname: string;
  birthDate: Date;
  phoneNumber: string;
  bio: string;
  adult: boolean;
  avatarLink: string;
  usersPosts: IPost[];
  usersCommentaries: ICommentary[];
  followings: IUser[];
  followers: IUser[];
}

export interface IAddUserRequest {
  username: string;
  email: string;
  firstname: string;
  lastname: string;
  birthDate: Date;
  phoneNumber: string;
  bio: string;
  avatarLink: string;
}

export interface IPutUserRequest {
  username: string;
  email: string;
  firstname: string;
  lastname: string;
  phoneNumber: string;
  bio: string;
  avatarLink: string;
}

export interface State {
  users: IUser[];
  loggedUser: IUser;
}

let users: IUser[] = [];
let loggedUser: IUser;

export const state = reactive<State>({
  users: [],
  loggedUser: {} as IUser,
});

export let initialized = false;

export default function useUser() {
  const apiGetUsers = useApi<IUser[]>('Users', {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
      Authorization: `bearer ${localStorage.getItem('token')}`,
    },
  });
  const loadUsers = async () => {
    if (!initialized) {
      await apiGetUsers.request();

      if (apiGetUsers.response.value) {
        users = apiGetUsers.response.value!;
        state.users = users;
      }
      initialized = true;
    }
  };

  const getAndSetLoggedUserById = async (id: string) => {
    const apiGetUser = useApi<IUser>('Users/' + id, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        Authorization: `bearer ${localStorage.getItem('token')}`,
      },
    });
    await apiGetUser.request();

    if (apiGetUser.response.value) {
      state.loggedUser = <IUser>apiGetUser.response.value!;
    }
  };

  const addUser = async (user: IAddUserRequest) => {
    const apiAddUser = useApi<IAddUserRequest>('Users', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        Authorization: `bearer ${localStorage.getItem('token')}`,
      },
      body: JSON.stringify(user),
    });
    await apiAddUser.request();

    if (apiAddUser.response.value) {
      users.push(<IUser>(<unknown>apiAddUser.response.value!));
      state.users = users;
    }
  };

  const editUser = async (user: IPutUserRequest, id: string) => {
    const apiEditUser = useApi<IPutUserRequest>('Users/' + id, {
      method: 'PUT',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        Authorization: `bearer ${localStorage.getItem('token')}`,
      },
      body: JSON.stringify(user),
    });

    await apiEditUser.request();

    if (apiEditUser.response.value) {
      users.push(<IUser>(<unknown>apiEditUser.response.value!));
      state.users = users;
    }
  };

  return {
    ...toRefs(state),
    loadUsers,
    addUser,
    editUser,
    getAndSetLoggedUserById,
  };
}

export function setLoggedUser(input: IUser) {
  loggedUser = input;
  state.loggedUser = loggedUser;
}

export function getLoggedUser() {
  return state.loggedUser;
}

export function getUserById(id: string): IUser | undefined {
  if (!initialized) {
    useUser().loadUsers();
  }
  return state.users.find((x) => x.id == id);
}
