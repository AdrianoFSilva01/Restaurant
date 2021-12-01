<template>
    <div ref="containerElement" @touchstart="onTouchStart" style="touch-action: none" >
        <slot :source="source" />
    </div>
</template>

<script lang="ts" src="./Carousel.ts" />

<style scoped lang="postcss" src="./Carousel.pcss" />
