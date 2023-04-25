import { createApp } from 'vue';
import App from './App.vue';
import './index.css';
import { setApiUrl } from './modules/api';
import router from './router';

const getRuntimeConfig = async () => {
  const runtimeConfig = await fetch('/runtime-config.json');
  return await runtimeConfig.json();
};

getRuntimeConfig().then((json) => {
  setApiUrl(json.API_URL);
  createApp(App).use(router).mount('#app');
});
