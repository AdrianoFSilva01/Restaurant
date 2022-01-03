<template>
    <main>
        <section class="restaurant">
            <Slider ref="slider" class="slider" :images="sliderImages" v-model="sliderIndex" />
            <div class="arrowsContainer">
                <AnimatedArrows
                    class="animatedArrows"
                    @click="onSliderButtonClick(sliderIndex - 1)"
                    :direction="ArrowDirection.left"
                />
                <AnimatedArrows
                    class="animatedArrows"
                    @click="onSliderButtonClick(sliderIndex + 1)"
                    :direction="ArrowDirection.right"
                />
            </div>
        </section>
        <section class="overflow-hidden">
            <div class="weeklyFood">
                <template v-if="Categories">
                    <InlineList ref="inlineList" class="mx-4 lg:mx-0" :list="CategoriesName" @changed-index="InlineListChangedIndex" />
                    <Carousel ref="carousel" class="mx-4 lg:mx-0 transition-transform duration-500 flex space-x-2" v-model="carouselIndex">
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
            </div>
        </section>
        <section class="restaurantInfo">
            <div class="space-y-4 flex-1">
                <img class="w-full" src="https://c1.wallpaperflare.com/preview/440/184/9/restaurant-glasses-drink-lichtspiel.jpg" />
                <img class="w-full hidden lg:block" src="https://static.independent.co.uk/s3fs-public/thumbnails/image/2020/06/05/13/1251-indybest.jpg?width=1200" />
            </div>
            <div class="flex-1">
                <div class="space-y-8 sticky top-12">
                    <h1 class="text-3xl lg:text-5xl">
                        What stays,
                        <br />
                        is what counts.
                    </h1>
                    <div class="restaurantInfoText">
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
        <section class="desserts">
            <template v-if="DessertCatalogs">
                <Slider ref="dessertsSlider" class="slider" :images="catalogImage" v-model="dessertsCarouselIndex" @interactionstart="sliderInteractionStart" @interactionmove="sliderInteractionMoving" @interactionend="sliderInteractionEnded" />
                <div class="flex items-center">
                    <div class="flex-shrink-0">
                        <AnimatedArrows
                            class="leftArrow"
                            @click="onDessertButtonClick(dessertsCarouselIndex - 1)"
                            :direction="ArrowDirection.left"
                        />
                    </div>
                    <div class="w-full overflow-hidden">
                        <Carousel ref="dessertsCarousel" class="flex transition-transform duration-500 flex-shrink w-full" :center-selected="true" v-model="dessertsCarouselIndex" @interactionstart="dessertsCarouselInteractionStart" @interactionmove="dessertsCarouselInteractionMoving" @interactionend="dessertsCarouselInteractionEnded">
                            <template v-for="(name, index) in catalogName" :key="index">
                                <div class="carouselItem">
                                    {{ name }}
                                </div>
                            </template>
                        </Carousel>
                    </div>
                    <div class="flex-shrink-0">
                        <AnimatedArrows
                            class="rightArrow"
                            @click="onDessertButtonClick(dessertsCarouselIndex + 1)"
                            :direction="ArrowDirection.right"
                        />
                    </div>
                </div>
            </template>
        </section>
    </main>
</template>

<script lang="ts" src="./MainView.ts" />

<style scoped lang="postcss" src="./Styles/MainView.pcss" />
