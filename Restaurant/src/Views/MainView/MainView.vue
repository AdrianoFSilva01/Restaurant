<template>
    <main>
        <section>
            <div class="restaurant" v-in-viewport="'fade-animation'">
                <Slider ref="slider" class="slider" :images="sliderImages" v-model="sliderIndex" />
                <div class="arrows-container">
                    <AnimatedArrows
                        class="animated-arrows"
                        @click="onSliderButtonClick(sliderIndex - 1)"
                        :direction="ArrowDirection.left"
                    />
                    <AnimatedArrows
                        class="animated-arrows"
                        @click="onSliderButtonClick(sliderIndex + 1)"
                        :direction="ArrowDirection.right"
                    />
                </div>
            </div>
        </section>
        <template v-if="Categories">
            <section class="overflow-hidden" v-in-viewport="'fade-animation'">
                <div class="weekly-food">
                    <InlineList ref="inlineList" class="mx-4 lg:mx-0" :list="CategoriesName" @changed-index="InlineListChangedIndex" />
                    <Carousel ref="carousel" class="mx-4 lg:mx-0 transition-transform duration-500 flex space-x-2" v-model="carouselIndex">
                        <template v-for="(category, index) in Categories" :key="index">
                            <template v-for="catalog in category.catalogs" :key="catalog.id">
                                <div :id="`carouselCategory${index}`" class="carousel-item">
                                    <img class="carousel-background" :src="catalog.imageUrl" />
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
                </div>
            </section>
        </template>
        <template v-else>
            <section class="w-full flex justify-center">
                <svg class="w-5 animate-spin dark:text-white" viewBox="0 0 100 100">
                    <use href="@/Assets/Images/Loading.svg#loading" />
                </svg>
            </section>
        </template>
        <section class="restaurant-info" v-in-viewport="'fade-animation'">
            <div class="space-y-4 flex-1">
                <img class="w-full" src="https://c1.wallpaperflare.com/preview/440/184/9/restaurant-glasses-drink-lichtspiel.jpg" />
                <img class="w-full hidden lg:block" src="https://static.independent.co.uk/s3fs-public/thumbnails/image/2020/06/05/13/1251-indybest.jpg?width=1200" />
            </div>
            <div class="flex-1">
                <div class="space-y-8 sticky top-12 dark:text-white">
                    <h1 class="text-3xl lg:text-5xl">
                        What stays,
                        <br />
                        is what counts.
                    </h1>
                    <div class="restaurant-info-text">
                        <p>
                            Five rooms offer the restaurant different volumes and atmospheres so that everyone may find a sprig from home.
                        </p>
                        <p>
                            The architecture of the building created the interior spaces and their management has evolved over time.
                        </p>
                        <p>
                            The soul of the house would not be without an attentive staff, a strong team attached to the spirit,
                            which is still witness and relay of this hospitality that survives from generation to generation.
                        </p>
                    </div>
                </div>
            </div>
        </section>
        <template v-if="DessertCatalogs">
            <section class="desserts" v-in-viewport="'fade-animation'">
                <Slider ref="dessertsSlider" class="slider" :images="catalogImage" v-model="dessertsCarouselIndex" @interactionstart="sliderInteractionStart" @interactionmove="sliderInteractionMoving" @interactionend="sliderInteractionEnded" />
                <div class="flex items-center">
                    <div class="flex-shrink-0">
                        <AnimatedArrows
                            class="left-arrow"
                            @click="onDessertButtonClick(dessertsCarouselIndex - 1)"
                            :direction="ArrowDirection.left"
                        />
                    </div>
                    <div class="w-full overflow-hidden">
                        <Carousel ref="dessertsCarousel" class="flex transition-transform duration-500 flex-shrink w-full"
                                  :center-selected="true"
                                  v-model="dessertsCarouselIndex"
                                  @interactionstart="dessertsCarouselInteractionStart"
                                  @interactionmove="dessertsCarouselInteractionMoving"
                                  @interactionend="dessertsCarouselInteractionEnded"
                        >
                            <template v-for="(name, index) in catalogName" :key="index">
                                <div class="carousel-item">
                                    {{ name }}
                                </div>
                            </template>
                        </Carousel>
                    </div>
                    <div class="flex-shrink-0">
                        <AnimatedArrows
                            class="right-arrow"
                            @click="onDessertButtonClick(dessertsCarouselIndex + 1)"
                            :direction="ArrowDirection.right"
                        />
                    </div>
                </div>
            </section>
        </template>
        <template v-else>
            <section class="w-full flex justify-center">
                <svg class="w-5 animate-spin dark:text-white" viewBox="0 0 100 100">
                    <use href="@/Assets/Images/Loading.svg#loading" />
                </svg>
            </section>
        </template>
    </main>
</template>

<script lang="ts" src="./MainView.ts" />

<style scoped lang="scss" src="./Styles/MainView.scss" />
