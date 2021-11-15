import "@/Assets/base.pcss";
import axios from 'axios';
import { App, createApp } from 'vue';
import MasterPage from './MasterPage.vue';

const app: App<Element> =  createApp(MasterPage);

app.provide(nameof(axios), axios.create());

app.mount("#app");
