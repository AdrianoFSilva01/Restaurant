import { CategoryState, useStore } from "@/category.store";
import { Vue } from "vue-class-component";
import { Store } from "vuex";

export default class OrderView extends Vue {
    pageIndex: number = 0;
    store!: Store<CategoryState>;

    created(): void {
        this.store = useStore();
    }

    changePageToSelectedCategory(id: number): void {
        this.pageIndex = id;
    }

    clearSelectedMap(): void {
        this.store.state.selectedCategories.clear();
        this.store.state.selectedCatalogs.clear();

        this.scrollToTop();
    }

    scrollToTop(): void {
        document.documentElement.scrollTop = 0;
    }
}