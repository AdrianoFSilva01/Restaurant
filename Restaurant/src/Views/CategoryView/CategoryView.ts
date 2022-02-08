import { CategoryState, useStore } from "@/category.store";
import Category from "@/Models/Category";
import { Vue } from "vue-class-component";
import { Store } from "vuex";

export default class CategoryView extends Vue {
    store!: Store<CategoryState>;

    created(): void {
        this.store = useStore();
    }

    mounted(): void {
        this.$emit("selectedCategory", 0);
    }

    get Categories(): Array<Category> {
        return this.store.getters.categories2;
    }

    onCategoryClick(id: number): void {
        this.addCategory(id);
    }

    addCategory(id: number): void {
        const selectedCategories: Map<number, Map<number, Map<string, number>>> = this.store.state.selectedCategories;

        if(selectedCategories.has(id)) {
            // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
            for(const key of selectedCategories.get(id)!.getKeys()) {
                if(this.store.state.selectedCatalogs.has(key)) {
                    this.store.state.selectedCatalogs.delete(key);
                }
            }
            selectedCategories.delete(id);
        } else {
            selectedCategories.set(id, new Map<number, Map<string, number>>());
        }
    }

    onAnimationEnd(event: Event): void {
        const element: HTMLElement = event.target as HTMLElement;
        element.style.opacity = "1";
        element.style.pointerEvents = "auto";
    }
}