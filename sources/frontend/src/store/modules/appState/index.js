import api from '@/api';
import { SET_STATE, setState } from '@/store/helpers';
import { showSuccessNotify, showErrorNotify } from '@/utils'

export default {
  namespaced: true,
  state: {
    loading: false,
    isInitAppState: false,
    isAuth: false,
    settings: {},
    user: {},
  },

  getters: {
    loading: state => state.loading,
    data: (state, getters) => getters,
    isInitAppState: state => state.isInitAppState,
    isAuth: state => state.isAuth,
    user: state => state.user,
    userIsAdmin: state => state.user.isAdmin,
    systemName: ({ settings }) => settings.name,
    version: ({ settings }) => settings.version,
  },

  mutations: {
    SET_STATE
  },

  actions: {
    async init({ state, commit, dispatch }) {
      if (state.isInitAppState) return;

      const isAuth = await api.isAuth();
      setState(commit, { state: 'isAuth', payload: isAuth });
      await dispatch('fetchSettings');

      if (isAuth) {
        await dispatch('fetchUserInfo');
      }

      setState(commit, { state: 'isInitAppState', payload: true });
    },

    async fetchSettings({ commit }) {
      const settings = await api.getSettings();
      setState(commit, { state: 'settings', payload: settings });
      return settings;
    },

    async fetchUserInfo({ commit }) {
      const user = await api.getUser();
      setState(commit, { state: 'user', payload: user });
      return user;
    },

    async login({ commit, dispatch }, {  username, password }) {
      try {
        setState(commit, { loading: true })
        let { token } = await api.loginUser({ username, password })
        if (token) {
          localStorage.setItem("access_token", token)
          setState(commit, { isAuth: true })
          await dispatch('fetchUserInfo');
        }
      }
      catch (error) {
        console.log(error)
        showErrorNotify(`Возникла ошибка при авторизации`)
      }
      finally {
        setState(commit, { loading: false })
      }
    },

    async logout({ commit, dispatch }) {
      try {
        setState(commit, { loading: true })
        localStorage.removeItem("access_token")
        setState(commit, { isAuth: false, user: {} })
      }
      finally {
        setState(commit, { loading: false })
      }
    }
  }
};
