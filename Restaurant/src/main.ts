import "@/Assets/base.pcss";
import directives from "@/Extensions/Directives/installDirectives";
import "@/Extensions/Types/installTypes";
import axios from 'axios';
import { App, createApp } from 'vue';
import { createRouter, createWebHistory, Router } from "vue-router";
import { categoryStore, categoryStorekey } from "./category.store";
import MasterPage from './MasterPage/MasterPage.vue';
import CatalogView from "./Views/CatalogView/CatalogView.vue";
import CategoryView from "./Views/CategoryView/CategoryView.vue";
import MainView from "./Views/MainView/MainView.vue";
import OrderView from "./Views/OrderView/OrderView.vue";
import SummaryView from "./Views/SummaryView/SummaryView.vue";

const app: App<Element> = createApp(MasterPage);

const router: Router = createRouter({
    history: createWebHistory(),
    routes: [
        { path: "/", component: MainView },
        { path: "/order", component: OrderView, children: [
            { path: "", component: CategoryView },
            { path: "catalog", component: CatalogView, props: true },
            { path: "summary", component: SummaryView }
        ] }
    ]
});
app.use(router);

app.provide(nameof(axios), axios.create({
    baseURL: "https://restaurantsite.azurewebsites.net"
}));

app.use(directives);
app.use(categoryStore, categoryStorekey);

app.mount("#app");
