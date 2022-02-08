import { CategoryState, useStore } from "@/category.store";
import ContextMenuTs from "@/Components/ContextMenu/ContextMenu";
import ContextMenu from "@/Components/ContextMenu/ContextMenu.vue";
import Catalog from "@/Models/Catalog";
import Category from "@/Models/Category";
import Ingredient from "@/Models/Ingredient";
import { Axios } from "axios";
import { nextTick } from "vue";
import { Options, Vue } from "vue-class-component";
import { Inject, Prop, Ref, Watch } from "vue-property-decorator";
import { Store } from "vuex";

@Options({
    components: {
        ContextMenu
    }
})
export default class CatalogView extends Vue {
    @Inject() axios!: Axios;
    @Ref() contextMenu!: ContextMenuTs;
    @Prop() pageIndex!: number;

    store!: Store<CategoryState>;
    catalogs: Array<Catalog> = [];

    ingredients: Array<Ingredient> = [];
    categoryIndex: number = 0;
    categoriesLenght: number = 0;
    catalogInfoContainerHeight: number = 0;
    selectedIngredients: number = NaN;
    animateElements: boolean = true;

    created(): void {
        this.store = useStore();

        if(this.store.state.selectedCategories.size === 0) {
            this.$router.replace({
                path: "/order"
            });
        }

        this.categoriesLenght = this.store.state.selectedCategories.size;
    }

    mounted(): void {
        this.onCurrentIndexChange();
    }

    @Watch(nameof((catalog: CatalogView) => catalog.pageIndex))
    onCurrentIndexChange(): void {
        if(!this.store.state.selectedCategories.size) {
            return;
        }

        this.scrollToTop();
        this.animateElements = false;

        const selectedCategoriesSorted: Map<number, Map<number, Map<string, number>>> = new Map<number, Map<number, Map<string, number>>>([...this.store.state.selectedCategories.entries()].sort());
        this.categoryIndex = [...selectedCategoriesSorted][this.pageIndex][0];

        this.catalogs = this.store.state.categories.filter((c: Category) => c.id === this.categoryIndex)[0].catalogs;
        this.setupCatalogInfoContainer();
    }

    getCatalogsInfo(id: number): Map<string, number> {
        // eslint-disable-next-line
        return this.store?.state.selectedCategories.get(this.categoryIndex)?.get(id)!;
    };

    setupCatalogInfoContainer(): void {
        nextTick(() => {
            let priceDisplayHeight: number = 0;
            let quantityDisplayHeight: number = 0;

            if((this.$el as HTMLElement).getElementsByClassName("sizesDisplay").length) {
                priceDisplayHeight = ((this.$el as HTMLElement).getElementsByClassName("sizesDisplay")[0] as HTMLElement).getHeight();
            } else if((this.$el as HTMLElement).getElementsByClassName("price-display").length) {
                priceDisplayHeight = ((this.$el as HTMLElement).getElementsByClassName("price-display")[0] as HTMLElement).getHeight();
            }

            if((this.$el as HTMLElement).getElementsByClassName("quantity-display").length) {
                quantityDisplayHeight = ((this.$el as HTMLElement).getElementsByClassName("quantity-display")[0] as HTMLElement).getHeight();
            }

            this.catalogInfoContainerHeight = quantityDisplayHeight + priceDisplayHeight;
        });
    }

    addCatalog(id: number, size: string, quantity: number): void {
        if(this.store?.state.selectedCategories.get(this.categoryIndex)?.has(id)) {
            this.store?.state.selectedCategories.get(this.categoryIndex)?.delete(id);
            this.store.state.selectedCatalogs.delete(id);
        } else {
            this.store?.state.selectedCategories.get(this.categoryIndex)?.set(id, new Map<string, number>().set(size, quantity));
            this.store.state.selectedCatalogs.set(id, size);
        }
    }

    decreaseQuantity(catalogId: number, size: string): void {
        let quantity: number | undefined = this.store.state.selectedCategories.get(this.categoryIndex)?.get(catalogId)?.get(size);
        if(quantity) {
            this.store.state.selectedCategories.get(this.categoryIndex)?.get(catalogId)?.set(size, --quantity);
        }

        if(this.store.state.selectedCatalogs.get(catalogId) !== size) {
            this.selectSize(catalogId, size);
        }
    }

    increaseQuantity(catalogId: number, size: string): void {
        let quantity: number | undefined = this.store?.state.selectedCategories.get(this.categoryIndex)?.get(catalogId)?.get(size);
        if(quantity) {
            this.store?.state.selectedCategories.get(this.categoryIndex)?.get(catalogId)?.set(size, ++quantity);
        }

        if(this.store.state.selectedCatalogs.get(catalogId) !== size) {
            this.selectSize(catalogId, size);
        }
    }

    selectSize(catalogId: number, size: string): void {
        const categoriesMap: Map<number, Map<string, number>> | undefined = this.store.state.selectedCategories.get(this.categoryIndex);

        if(!categoriesMap?.has(catalogId)) {
            categoriesMap?.set(catalogId, new Map<string, number>().set(size, 1));
            this.store.state.selectedCatalogs.set(catalogId, size);
        } else {
            if(categoriesMap?.get(catalogId)?.has(size) && this.store.state.selectedCatalogs.get(catalogId) === size) {
                categoriesMap?.get(catalogId)?.delete(size)

                if(categoriesMap?.get(catalogId)?.size === 0) {
                    categoriesMap?.delete(catalogId);
                    this.store.state.selectedCatalogs.delete(catalogId);
                } else {
                    // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
                    this.store.state.selectedCatalogs.set(catalogId, categoriesMap?.get(catalogId)!.lastKey());
                }

            } else if(categoriesMap?.get(catalogId)?.has(size) && this.store.state.selectedCatalogs.get(catalogId) !== size) {
                this.store.state.selectedCatalogs.set(catalogId, size);
            } else {
                this.store.state.selectedCatalogs.set(catalogId, size);
                categoriesMap?.get(catalogId)?.set(size, 1);
            }
        }
    }

    selectMultipleSizes(catalogId: number, size: string): void {
        const categoriesMap: Map<number, Map<string, number>> | undefined = this.store.state.selectedCategories.get(this.categoryIndex);

        if(!categoriesMap?.has(catalogId)) {
            categoriesMap?.set(catalogId, new Map<string, number>().set(size, 1));
            this.store.state.selectedCatalogs.set(catalogId, size);
        } else {
            if(categoriesMap?.get(catalogId)?.has(size)) {
                categoriesMap?.get(catalogId)?.delete(size);
                this.store.state.selectedCatalogs.delete(catalogId);

                if(categoriesMap?.get(catalogId)?.size === 0) {
                    categoriesMap?.delete(catalogId);
                    this.store.state.selectedCatalogs.delete(catalogId);
                }else {
                    // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
                    this.store.state.selectedCatalogs.set(catalogId, categoriesMap?.get(catalogId)!.lastKey());
                }

            } else {
                categoriesMap?.get(catalogId)?.set(size, 1);
                this.store.state.selectedCatalogs.set(catalogId, size);
            }
        }
    }

    minWidthLarge(): boolean {
        return window.matchMedia('(min-width: 1024px)').matches;
    }

    checkIngredients(id: number, event: Event): void {
        this.selectedIngredients = id;
        this.ingredients = this.catalogs.filter((ct: Catalog) => ct.id === id)[0].ingredients;
        this.contextMenu.toggle(event);
    }

    onAnimationEnd(event: Event): void {
        this.animateElements = true;

        const element: HTMLElement = event.target as HTMLElement;
        element.style.opacity = "1";
        element.style.pointerEvents = "auto";
    }

    scrollToTop(): void {
        document.documentElement.scrollTop = 0;
    }
}