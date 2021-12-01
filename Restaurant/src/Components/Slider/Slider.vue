<template>
    <div class="sliderContainer" v-bind="$attrs" @transitionend="onTransitionEnd">
        <img class="opacity-100" />
        <img class="opacity-0" />
        <img class="opacity-0" />
    </div>
</template>

<script lang="ts" src="./Slider.ts" />

<style scoped lang="postcss" src="./Slider.pcss" />
