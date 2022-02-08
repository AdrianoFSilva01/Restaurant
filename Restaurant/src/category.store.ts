import { InjectionKey } from "vue";
import { createStore, Store, useStore as baseUseStore } from "vuex";
import Category from "./Models/Category";

export interface CategoryState {
    categories: Array<Category>;
    selectedCategories: Map<number, Map<number, Map<string, number>>>;
    selectedCatalogs: Map<number, string>;
}

export const categoryStorekey: InjectionKey<Store<CategoryState>> = Symbol();

export const categoryStore: Store<CategoryState> = createStore<CategoryState>({
    state: {
        categories: [],
        selectedCategories: new Map<number, Map<number, Map<string, number>>>(),
        selectedCatalogs: new Map<number, string>()
    },
    mutations: {
        loadCategories(state: CategoryState, categories: Array<Category>) {
            state.categories = categories;
        }
    },
    getters: {
        categories2(state: CategoryState) {
            return state.categories;
        }
    }
});

export function useStore(): Store<CategoryState> {
    return baseUseStore(categoryStorekey);
}