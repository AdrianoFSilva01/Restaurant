<template>
    <div>
        <section class="sliderSection">
            <Slider ref="slider" class="slider" :images="sliderImages" />
            <div class="arrowsContainer">
                <AnimatedArrows
                    class="animatedArrows"
                    @click="onSliderButtonClick(false)"
                    :direction="ArrowDirection.left"
                />
                <AnimatedArrows
                    class="animatedArrows"
                    @click="onSliderButtonClick(true)"
                    :direction="ArrowDirection.right"
                />
            </div>
        </section>
        <section class="carouselSection">
            <template v-if="Categories">
                <InlineList ref="inlineList" class="mx-4" :list="CategoriesName" @changed-index="InlineListChangedIndex" />
                <Carousel ref="carousel" class="mx-4 transition-transform duration-500 inline-flex space-x-2" v-model="carouselIndex">
                    <template v-for="(category, index) in Categories" :key="index">
                        <template v-for="catalog in category.catalogs" :key="catalog.id">
                            <div :id="`carouselCategory${index}`" class="carouselItem">
                                <img class="carouselBackground" :src="catalog.imageUrl" />
                                <div class="h-full text-xl">
                                    {{ catalog.name }}
                                </div>
                                <div class="text-center flex justify-end">
                                    <template v-for="(detail, detailIndex) in catalog.catalogInfos" :key="detailIndex">
                                        <div>
                                            {{ detail.description }}
                                            {{ detail.price }}€
                                        </div>
                                    </template>
                                </div>
                            </div>
                        </template>
                    </template>
                </Carousel>
            </template>
        </section>
    </div>
</template>

<script lang="ts" src="./MainView.ts" />

<style scoped lang="postcss" src="./MainView.pcss" />
