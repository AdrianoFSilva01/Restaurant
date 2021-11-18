<template>
    <div>
        <section class="space-y-4">
            <Slider ref="slider" class="slider" :images="sliderImages" @transitionend="this.disableSliderButton = false" />
            <button @click="onSliderNextButtonClick" :disabled="disableSliderButton">
                frente
            </button>
            <br />
            <button @click="onSliderPreviousButtonClick" :disabled="disableSliderButton">
                tras
            </button>
        </section>
        <HelloWorld />
    </div>
</template>

<script lang="ts" src="./MainView.ts" />

<style lang="postcss" src="./MainView.pcss" />
