<template>
    <div>
        <section class="sliderSection">
            <Slider ref="slider" class="slider" :images="sliderImages" @transitionend="disableSliderButtons = false" />
            <div class="arrowsContainer">
                <AnimatedArrows class="animatedArrows" @click="onSliderPreviousButtonClick" :direction="ArrowDirection.left" :disabled="disableSliderButtons" />
                <AnimatedArrows class="animatedArrows" @click="onSliderNextButtonClick" :direction="ArrowDirection.right" :disabled="disableSliderButtons" />
            </div>
        </section>
        <HelloWorld />
    </div>
</template>

<script lang="ts" src="./MainView.ts" />

<style lang="postcss" src="./MainView.pcss" />
