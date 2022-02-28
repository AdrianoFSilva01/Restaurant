<template>
    <div>
        <div class="space-y-2 lg:space-y-6 px-4 m-auto" style="max-width: 62rem" v-delayed-childs="[0.1, 'category-section', 'fade-animation', true]">
            <template v-for="([categoryId, catalogs], index) in categories" :key="categoryId">
                <div v-if="catalogs.size" class="category-section">
                    <router-link to="/order/catalog">
                        <div class="lg:flex" @click="selectedCategory(Number(index))">
                            <div class="category-name">
                                {{ getCategoryName(categoryId) }}
                            </div>
                            <div class="lg:flex-1 lg:border lg:border-gold">
                                <template v-for="[catalogId, catalogInfo] in catalogs" :key="catalogId">
                                    <div class="flex px-4 space-x-12 border-b border-gold last:border-0 py-1" :class="{ 'flex-col' : catalogInfo.size > 1 }">
                                        <div class="flex space-x-4 w-full">
                                            <div :class="{ 'invisible' : catalogInfo.size > 1 }">
                                                {{ catalogInfo.values().next().value }}
                                            </div>
                                            <div class="w-full">
                                                {{ getCatalogName(categoryId, catalogId) }}
                                            </div>
                                        </div>
                                        <template v-for="[catalogSize, quantity] in catalogInfo" :key="catalogSize">
                                            <template v-if="catalogInfo.size > 1">
                                                <div class="flex space-x-4">
                                                    <div>
                                                        {{ quantity }}
                                                    </div>
                                                    <div>
                                                        {{ getCatalogDescription(categoryId, catalogId, catalogSize) }}
                                                    </div>
                                                    <div class="flex-1 text-right">
                                                        {{ (getCatalogPrice(categoryId, catalogId, catalogSize) * quantity).toFixed(2) }} €
                                                    </div>
                                                </div>
                                            </template>
                                            <template v-else>
                                                <template v-if="getCatalogSizes(categoryId, catalogId)">
                                                    {{ getCatalogDescription(categoryId, catalogId, catalogSize) }}
                                                </template>
                                                <div class="flex space-x-4 flex-shrink-0">
                                                    <div class="text-right">
                                                        {{ (getCatalogPrice(categoryId, catalogId, catalogSize) * quantity).toFixed(2) }} €
                                                    </div>
                                                </div>
                                            </template>
                                        </template>
                                    </div>
                                </template>
                            </div>
                        </div>
                    </router-link>
                </div>
            </template>
            <div class="flex justify-between text-lg dark:text-white">
                <div>
                    Total:
                </div>
                <div>
                    {{ getTotalPrice() }}€
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts" src="./SummaryView.ts" />

<style scoped lang="scss" src="./Styles/SummaryView.scss" />