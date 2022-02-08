<template>
    <main>
        <div>
            <router-view :page-index="pageIndex" @selectedCategory="changePageToSelectedCategory" />
            <div class="my-6 sticky bottom-2 z-10 h-full">
                <template v-if="$route.path === '/order'">
                    <div class="flex w-full justify-center">
                        <router-link to="/order/catalog" class="navigation flex-1" :class="{ 'pointer-events-none' : !store?.state.selectedCategories.size }">
                            <button class="navigation-button" :class="store?.state.selectedCategories.size ? 'visible' : 'invisible'">
                                Continue
                            </button>
                        </router-link>
                    </div>
                </template>
                <template v-if="$route.path === '/order/catalog'">
                    <div class="flex space-x-2 w-full justify-center">
                        <template v-if="store?.state.selectedCategories.size === 1 || pageIndex === 0">
                            <router-link to="/order" class="navigation flex-1">
                                <button class="navigation-button">
                                    Back
                                </button>
                            </router-link>
                        </template>
                        <template v-else>
                            <button class="flex-1 navigation-button" @click="pageIndex--">
                                Previous
                            </button>
                        </template>
                        <template v-if="store?.state.selectedCategories.size === 1 || pageIndex === store?.state.selectedCategories.size - 1">
                            <router-link to="/order/summary"
                                         class="navigation flex-1"
                                         :class="{ 'hidden' : store?.state.selectedCatalogs.size < 1 }"
                            >
                                <button class="navigation-button" :disabled="store?.state.selectedCatalogs.size < 1">
                                    Continue
                                </button>
                            </router-link>
                        </template>
                        <template v-else>
                            <button class="flex-1 navigation-button" @click="pageIndex++">
                                Next
                            </button>
                        </template>
                    </div>
                </template>
                <template v-if="$route.path === '/order/summary'">
                    <div class="flex space-x-2 w-full justify-center">
                        <router-link to="/order/catalog" class="navigation flex-1">
                            <button class="navigation-button">
                                Back
                            </button>
                        </router-link>
                        <router-link to="/" class="navigation flex-1">
                            <button class="navigation-button" @click="clearSelectedMap">
                                Finish Order
                            </button>
                        </router-link>
                    </div>
                </template>
            </div>
        </div>
    </main>
</template>

<script lang="ts" src="./OrderView.ts" />

<style scoped lang="scss" src="./Styles/OrderView.scss" />
