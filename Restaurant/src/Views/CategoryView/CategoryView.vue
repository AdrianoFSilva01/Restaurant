<template>
    <section>
        <template v-if="Categories.length">
            <div class="category-container" v-delayed-childs="[0.1, 'animation-container' ,'fade-animation', true]">
                <template v-for="category in Categories" :key="category.id">
                    <div class="animation-container" @animationend="onAnimationEnd">
                        <div class="category" :class="store?.state.selectedCategories.has(category.id) ? 'selected' : ''" @click="onCategoryClick(category.id)">
                            <img class="absolute top-0 left-0 w-full h-full object-cover" style="z-index: -1" :src="category.heroImageUrl" />
                            <img :src="category.imageUrl" />
                            <div class="lg:text-lg xl:text-xl">
                                {{ category.name }}
                            </div>
                        </div>
                    </div>
                </template>
            </div>
        </template>
        <template v-else>
            <div class="w-full flex justify-center">
                <svg class="w-5 animate-spin dark:text-white" viewBox="0 0 100 100">
                    <use href="@/Assets/Images/Loading.svg#loading" />
                </svg>
            </div>
        </template>
    </section>
</template>

<script lang="ts" src="./CategoryView.ts" />

<style scoped lang="scss" src="./Styles/CategoryView.scss" />
