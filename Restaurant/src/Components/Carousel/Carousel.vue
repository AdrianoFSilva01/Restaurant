<template>
    <div class="cursor-grab" ref="containerElement" @touchstart="onInteractionStart" @mousedown="onInteractionStart" style="touch-action: none" >
        <slot />
    </div>
</template>

<script lang="ts" src="./Carousel.ts" />

<style scoped lang="scss" src="./Carousel.scss" />
