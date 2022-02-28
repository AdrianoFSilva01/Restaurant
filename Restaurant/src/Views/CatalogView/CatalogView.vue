<template>
    <div>
        <ContextMenu
            ref="contextMenu"
            class="absolute top-20 left-0 z-50"
            @click.capture.stop
            :position-through-element="true"
            :open-to-left="true"
        >
            <div class="bg-black bg-opacity-50 border border-white dark:border-dark-gray" :class="{ 'selected' : store.state.selectedCategories.get(categoryIndex)?.has(selectedIngredients) }">
                <div class="bg-white dark:text-white text-2xl flex justify-center px-6 py-1"
                     :class="{
                         'bg-gold' : store.state.selectedCategories.get(categoryIndex)?.has(selectedIngredients),
                         'dark:bg-dark-gray' : !store.state.selectedCategories.get(categoryIndex)?.has(selectedIngredients)
                     }"
                >
                    Ingredients
                </div>
                <div class="px-2 py-1">
                    <template v-for="ingredient in ingredients" :key="ingredient.id">
                        <div class="text-white text-left font-bold">
                            {{ ingredient.name }}
                        </div>
                    </template>
                </div>
            </div>
        </ContextMenu>
        <div class="catalog-container" v-delayed-childs="[0.1, 'animation-container' ,'fade-animation']">
            <template v-if="catalogs">
                <template v-for="catalog in catalogs" :key="catalog.id">
                    <div class="animation-container" @animationend="onAnimationEnd">
                        <div class="catalog" style="tap-highlight-color: transparent" @click="addCatalog(catalog.id, catalog.catalogInfos[0].size, 1)" :class="{ 'selected' : store.state.selectedCategories.get(categoryIndex)?.has(catalog.id) }">
                            <template v-if="catalog.haveIngredients">
                                <div class="absolute top-0 right-0 p-0.5 bg-opacity-60 z-10"
                                     :class="[{ 'bg-black' : catalog.catalogInfos.length === 1 }, store.state.selectedCategories.get(categoryIndex)?.has(catalog.id) ? 'bg-none lg:bg-black lg:bg-opacity-60' : 'bg-black']"
                                     @click.stop="checkIngredients(catalog.id, $event)"
                                >
                                    <div class="text-black dark:text-white m-0.5 p-0.5 bg-white w-6 relative pointer-events-none"
                                         :class="{
                                             'bg-gold dark:bg-gold' : store.state.selectedCategories.get(categoryIndex)?.has(catalog.id),
                                             'dark:bg-dark-gray' : !store.state.selectedCategories.get(categoryIndex)?.has(catalog.id)
                                         }"
                                    >
                                        <svg viewBox="0 0 100 100">
                                            <use href="@/Assets/Images/Onion.svg#onion" />
                                        </svg>
                                    </div>
                                </div>
                            </template>
                            <img class="absolute top-0 left-0 object-cover w-full h-full" style="z-index: -2" :src="catalog.heroImageUrl" />
                            <div class="w-full h-full flex items-end" style="text-shadow: 2px black">
                                <div class="catalog-name">
                                    {{ catalog.name }}
                                </div>
                            </div>
                            <div class="w-full" :class="store.state.selectedCategories.get(categoryIndex)?.has(catalog.id) ? 'flex flex-col' : ''">
                                <template v-if="catalog.catalogInfos.length > 1">
                                    <div class="selected-sizes-container"
                                         :class="{ 'bg-black' : store.state.selectedCategories.get(categoryIndex)?.has(catalog.id) }"
                                    >
                                        <template v-for="[size, quantity] in getCatalogsInfo(catalog.id)" :key="size">
                                            <div class="flex space-x-px py-0.5 px-1.5 text-black dark:text-white"
                                                 :class="store.state.selectedCatalogs.get(catalog.id) === size ? 'bg-gold' : 'bg-darkGold'">
                                                <div>
                                                    {{ quantity }}
                                                </div>
                                                <div>
                                                    {{ size }}
                                                </div>
                                            </div>
                                        </template>
                                    </div>
                                </template>
                                <div class="h-full flex flex-col justify-end">
                                    <div class="quantity-display lg:hidden" :class="store.state.selectedCategories.get(categoryIndex)?.has(catalog.id) ? '' : 'hidden'">
                                        <div class="flex p-1">
                                            <button class="quantity-button"
                                                    @click.stop="decreaseQuantity(catalog.id, store.state.selectedCatalogs.get(catalog.id) ?? '')"
                                                    :disabled="store.state.selectedCategories.get(categoryIndex)?.get(catalog.id)?.get(store.state.selectedCatalogs.get(catalog.id) ?? '') === 1"
                                            >
                                                -
                                            </button>
                                            <div class="flex-1">
                                                {{ store.state.selectedCategories.get(categoryIndex)?.get(catalog.id)?.get(store.state.selectedCatalogs.get(catalog.id) ?? "" ) }}
                                            </div>
                                            <button class="quantity-button" @click.stop="increaseQuantity(catalog.id, store.state.selectedCatalogs.get(catalog.id) ?? '')">
                                                +
                                            </button>
                                        </div>
                                    </div>
                                    <div class="sizes-container">
                                        <template v-for="(info, index) in catalog.catalogInfos" :key="index">
                                            <div class="flex flex-grow space-x-1">
                                                <div class="size bg-white"
                                                     :class="{
                                                         'bg-darkGold dark:bg-darkGold lg:bg-gold lg:dark:bg-gold' : store.state.selectedCategories.get(categoryIndex)?.get(catalog.id)?.has(info.size) && store.state.selectedCatalogs.get(catalog.id) !== info.size,
                                                         'bg-gold dark:bg-gold' : store.state.selectedCatalogs.get(catalog.id) === info.size,
                                                         'dark:bg-dark-gray' : !store.state.selectedCategories.get(categoryIndex)?.get(catalog.id)?.has(info.size),
                                                         'lg:space-x-2' : catalog.catalogInfos.length > 1
                                                     }"
                                                     @click.stop=" minWidthLarge() ? selectMultipleSizes(catalog.id, info.size) : selectSize(catalog.id, info.size)"
                                                >
                                                    <div class="dark:text-white" :class="{ 'invisible lg:hidden' : catalog.catalogInfos.length === 1 }">
                                                        {{ info.size }}
                                                    </div>
                                                    <div class="dark:text-white" :class="{ 'h-full transform -translate-y-1/2 flex items-center justify-center lg:translate-y-0' : catalog.catalogInfos.length === 1 }">
                                                        {{ info.price }}
                                                    </div>
                                                </div>
                                                <div class="quantity-display hidden" :class="store.state.selectedCategories.get(categoryIndex)?.get(catalog.id)?.has(info.size) ? 'lg:block' : ''">
                                                    <div class="flex h-full">
                                                        <button class="quantity-button"
                                                                @click.stop="decreaseQuantity(catalog.id, info.size)"
                                                                :disabled="store.state.selectedCategories.get(categoryIndex)?.get(catalog.id)?.get(info.size) === 1"
                                                        >
                                                            -
                                                        </button>
                                                        <div class="flex-1 flex items-center justify-center">
                                                            {{ store.state.selectedCategories.get(categoryIndex)?.get(catalog.id)?.get(info.size) }}
                                                        </div>
                                                        <button class="quantity-button" @click.stop="increaseQuantity(catalog.id, info.size ?? '')">
                                                            +
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </template>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </template>
            </template>
        </div>
    </div>
</template>

<script lang="ts" src="./CatalogView.ts" />

<style scoped lang="scss" src="./Styles/CatalogView.scss" />