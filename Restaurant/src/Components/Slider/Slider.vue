<template>
    <div class="slider-container cursor-grab" v-bind="$attrs" @touchstart="onInteractionStart" @mousedown="onInteractionStart" @transitionstart="onTransitionStart">
        <img class="transition-opacity duration-500" />
        <img class="transition-opacity duration-500" />
        <img class="transition-opacity duration-500" />
    </div>
</template>

<script lang="ts" src="./Slider.ts" />

<style scoped lang="scss" src="./Slider.scss" />
