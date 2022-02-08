/* eslint-disable @typescript-eslint/no-non-null-assertion */
import { CategoryState, useStore } from "@/category.store";
import Catalog, { CatalogInfo } from "@/Models/Catalog";
import Category from "@/Models/Category";
import { Options, Vue } from "vue-class-component";
import { Store } from "vuex";

@Options({
    emits: [
        "selectedCategory"
    ]
})
export default class SummaryView extends Vue {
    store: Store<CategoryState> | undefined = undefined;
    categories!: Map<number, Map<number, Map<string, number>>>;

    created(): void {
        this.store = useStore();

        if(this.store.state.selectedCategories.size === 0) {
            this.$router.replace({
                path: "/order"
            });
        }

        const selectedCategoriesSorted: Map<number, Map<number, Map<string, number>>> = new Map<number, Map<number, Map<string, number>>>([...this.store.state.selectedCategories.entries()].sort());
        this.categories = selectedCategoriesSorted;
    }

    getCategoryName(categoryId: number): string {
        // eslint-disable-next-line
        return this.store?.state.categories.filter((x: Category) => x.id === categoryId)[0].name!;
    }

    getCatalogName(categoryId: number, catalogId: number) : string {
        // eslint-disable-next-line
        return this.store?.state.categories.filter((x: Category) => x.id === categoryId)[0].catalogs.filter((ct: Catalog) => ct.id === catalogId)[0].name!;
    }

    getCatalogDescription(categoryId: number, catalogId: number, catalogSize: string) : string {
        // eslint-disable-next-line
        return this.store?.state.categories
            .filter((x: Category) => x.id === categoryId)[0].catalogs
            .filter((ct: Catalog) => ct.id === catalogId)[0].catalogInfos
            .filter((cI: CatalogInfo) => cI.size === catalogSize)[0].description!;
    }

    getCatalogPrice(categoryId: number, catalogId: number, catalogSize: string) : number {
        // eslint-disable-next-line
        return this.store?.state.categories
            .filter((x: Category) => x.id === categoryId)[0].catalogs
            .filter((ct: Catalog) => ct.id === catalogId)[0].catalogInfos
            .filter((cI: CatalogInfo) => cI.size === catalogSize)[0].price!;
    }

    getTotalPrice() : number {
        // eslint-disable
        const total: number | undefined = this.store?.state.categories
            .filter((c: Category) => this.store?.state.selectedCategories.has(c.id))
            .map((c: Category) => {
                const potato123: Array<potato> = c.catalogs.map((ct: Catalog): potato => {
                    return {
                        categoryId: c.id,
                        catalogId: ct.id,
                        catalog: ct
                    }
                });
                return potato123
            })
            .reduce((a: Array<potato>, b: Array<potato>) => [...a, ...b], [])
            .filter((ct: potato) => this.store?.state.selectedCatalogs.has(ct.catalogId))
            .map((c: potato) => {
                const potato123: Array<number> = c.catalog.catalogInfos.map((ct: CatalogInfo): number => {
                    return ct.price * (this.store?.state.selectedCategories.get(c.categoryId)?.get(c.catalogId)?.get(ct.size) || 0)
                });
                return potato123
            })
            .reduce((a: Array<number>, b:  Array<number>) => [...a, ...b], [])
            .reduce((a: number | undefined, b: number | undefined) => a! + b!, 0);

        return Number(total!.toFixed(2));
    }

    getCatalogSizes(categoryId: number, catalogId: number): boolean {
        // eslint-disable-next-line
        return this.store?.state.categories
            .filter((x: Category) => x.id === categoryId)[0].catalogs
            .filter((ct: Catalog) => ct.id === catalogId)[0].catalogInfos.length! > 1;
    }

    selectedCategory(id: number): void {
        this.$emit("selectedCategory", id);
    }
}

class potato {
    categoryId!: number;
    catalogId!: number;
    catalog!: Catalog;
    size?: string;
    price?: number;
    total?: number;
}