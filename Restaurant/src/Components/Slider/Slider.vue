<template>
    <div v-bind="$attrs" @transitionend="onTransitionEnd">
        <img ref="previousImageElement" />
        <img ref="currentImageElement" class="opacity-100 transition-none" />
        <img ref="nextImageElement" class="opacity-0 transition-none" />
    </div>
</template>

<script lang="ts" src="./Slider.ts" />

<style scoped lang="pcss" src="./Slider.pcss" />
