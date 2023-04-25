import { reactive, toRefs } from 'vue';
import { jwtDecrypt, tokenAlive } from '../modules/jwtHelper';
import useApi from './api';
import useUser from './users';

export interface ILoginRequest {
  email: string;
  password: string;
}

export interface IRegisterRequest {
  userName: string;
  email: string;
  password: string;
  firstname: string;
  lastname: string;
  birthDate: Date;
  phoneNumber: string;
  bio: string;
  avatarLink: string;
}

export interface IAuthData {
  token: string;
  tokenExp: number;
  userId: string;
  email: string;
}

export interface State {
  authData: IAuthData;
  loginStatus: string;
}

const state = reactive<State>({
  authData: {} as IAuthData,
  loginStatus: '',
});

export default function useAuth() {
  const saveTokenData = async (data: {
    token: string;
    result: string;
    errors: string;
  }) => {
    localStorage.setItem('token', data.token);
    const jwtDecodedValue = jwtDecrypt(data.token);
    const newAuthData: IAuthData = {
      token: data.token,
      tokenExp: parseInt(jwtDecodedValue.exp),
      userId: jwtDecodedValue.Id,
      email: jwtDecodedValue.Email,
    };
    state.authData = newAuthData;
    useUser().getAndSetLoggedUserById(state.authData.userId);
  };

  const setLoginStatus = async (value: string) => {
    state.loginStatus = value;
  };

  const getLoginStatus = () => {
    return state.loginStatus;
  };

  const login = async (payload: ILoginRequest) => {
    const apiLogin = useApi<ILoginRequest>('AuthManagement/Login', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(payload),
    });

    await apiLogin.request();

    if (apiLogin.response.value) {
      saveTokenData(
        <{ token: string; result: string; errors: string }>(
          (<unknown>apiLogin.response.value!)
        ),
      );
      setLoginStatus('success');
    } else {
      setLoginStatus('failed');
    }
  };

  const register = async (payload: IRegisterRequest) => {
    const apiRegister = useApi<IRegisterRequest>('AuthManagement/Register', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(payload),
    });

    await apiRegister.request();

    if (apiRegister.response.value) {
      saveTokenData(
        <{ token: string; result: string; errors: string }>(
          (<unknown>apiRegister.response.value!)
        ),
      );
      setLoginStatus('success');
    } else {
      setLoginStatus('failed');
    }
  };

  const isTokenActive = () => {
    if (!state.authData.tokenExp) {
      return false;
    }

    return tokenAlive(state.authData.tokenExp);
  };

  return {
    ...toRefs(state),
    saveTokenData,
    setLoginStatus,
    getLoginStatus,
    login,
    register,
    isTokenActive,
  };
}

export function getAuthData() {
  return state.authData;
}

export function defaultAuthData() {
  state.authData = {
    token: '',
    tokenExp: 0,
    userId: '',
    email: '',
  };
  state.loginStatus = 'logged out';
}
