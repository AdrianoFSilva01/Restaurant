<template>
    <div>
        <section class="sliderSection">
            <Slider ref="slider" class="slider" :images="sliderImages" @transitionend="disableSliderButtons = false" />
            <div class="arrowsContainer">
                <AnimatedArrows
                    class="animatedArrows"
                    @click="onSliderPreviousButtonClick"
                    :direction="ArrowDirection.left"
                    :disabled="disableSliderButtons"
                />
                <AnimatedArrows
                    class="animatedArrows"
                    @click="onSliderNextButtonClick"
                    :direction="ArrowDirection.right"
                    :disabled="disableSliderButtons"
                />
            </div>
        </section>
        <section class="carouselSection">
            <Carousel v-if="Categories" class="mx-4 transition-transform duration-1000 inline-flex space-x-2">
                <template v-for="category in Categories" :key="category.id">
                    <template v-for="catalog in category.catalogs" :key="catalog.id">
                        <div class="carouselItem">
                            <img class="carouselBackground" :src="catalog.imageUrl" />
                            <div class="h-full text-xl">
                                {{ catalog.name }}
                            </div>
                            <div class="text-center flex justify-end">
                                <template v-for="(detail, index) in catalog.catalogInfos" :key="index">
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
        </section>
    </div>
</template>

<script lang="ts" src="./MainView.ts" />

<style lang="postcss" src="./MainView.pcss" />
