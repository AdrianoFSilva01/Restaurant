<template>
    <div class="sliderContainer" v-bind="$attrs" @touchstart="onTouchStart" @transitionend="onTransitionEnd" >
        <img class="opacity-100 transition-opacity duration-500" />
        <img class="opacity-0 transition-opacity duration-500" />
        <img class="opacity-0 transition-opacity duration-500" />
    </div>
</template>

<script lang="ts" src="./Slider.ts" />

<style scoped lang="postcss" src="./Slider.pcss" />
