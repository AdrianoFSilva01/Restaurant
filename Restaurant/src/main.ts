import "@/Assets/base.pcss";
import directives from "@/Extensions/Directives/installDirectives";
import axios from 'axios';
import { App, createApp } from 'vue';
import MasterPage from './MasterPage/MasterPage.vue';

const app: App<Element> =  createApp(MasterPage);

app.provide(nameof(axios), axios.create());

app.use(directives);

app.mount("#app");
