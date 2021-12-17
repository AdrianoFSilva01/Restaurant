<template>
    <div>
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
        <section class="weeklyFood">
            <template v-if="Categories">
                <InlineList ref="inlineList" class="mx-4" :list="CategoriesName" @changed-index="InlineListChangedIndex" />
                <div class="w-full overflow-hidden">
                    <Carousel ref="carousel" class="mx-4 transition-transform duration-500 flex space-x-2 w-full" v-model="carouselIndex">
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
                </div>
            </template>
        </section>
        <section class="restaurantInfo">
            <img src="https://tul.imgix.net/content/article/sasso_1.jpg?auto=format,compress&w=520&h=390&fit=crop" />
            <h1 class="text-3xl">
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
        </section>
        <section class="desserts">
            <template v-if="Categories">
                <Slider ref="dessertsSlider" class="slider" :images="catalogImage" v-model="dessertsCarouselIndex" @touchstart="sliderTouchStart" @touchmove="sliderTouchMoving" @touchend="sliderTouchEnded" />
                <div class="flex">
                    <div class="flex-shrink-0">
                        <AnimatedArrows
                            class="leftArrow"
                            @click="onDessertButtonClick(dessertsCarouselIndex - 1)"
                            :direction="ArrowDirection.left"
                        />

                    </div>
                    <div class="w-full overflow-hidden">
                        <Carousel ref="dessertsCarousel" class="flex transition-transform duration-500 flex-shrink w-full" v-model="dessertsCarouselIndex" @touchstart="dessertsCarouselTouchStart" @touchmove="dessertsCarouselTouchMoving" @touchend="dessertsCarouselTouchEnded">
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
    </div>
</template>

<script lang="ts" src="./MainView.ts" />

<style scoped lang="postcss" src="./MainView.pcss" />
