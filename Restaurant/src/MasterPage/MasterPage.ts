import { CategoryState, useStore } from "@/category.store";
import Category from "@/Models/Category";
import Footer from "@/Views/FooterView/FooterView.vue";
import Header from "@/Views/HeaderView/Header.vue";
import MainView from "@/Views/MainView/MainView.vue";
import Overlay from "@/Views/OverlayView/Overlay.vue";
import { Axios, AxiosResponse } from "axios";
import { Options, Vue } from "vue-class-component";
import { Inject, Watch } from "vue-property-decorator";
import { Store } from "vuex";

@Options({
    components: {
        MainView,
        Header,
        Overlay,
        Footer
    }
})

export default class MasterPage extends Vue {
    @Inject() axios!: Axios;

    displayOverlay: boolean = false;

    created(): void {
        const store: Store<CategoryState> = useStore();

        this.axios.get<Array<Category>>("Category/all")
            .then((response: AxiosResponse<Array<Category>>) => {
                store.commit("loadCategories", response.data);
            });
    }

    @Watch(nameof((masterPage: MasterPage) => masterPage.displayOverlay))
    onCurrentIndexChange(): void {
        if(this.displayOverlay) {
            document.getElementsByTagName("html")[0].style.overflow = "hidden";
        } else {
            document.getElementsByTagName("html")[0].style.overflow = "visible";
        }
    }
}