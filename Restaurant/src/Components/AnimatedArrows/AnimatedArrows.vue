<template>
    <button class="arrowButton" @click="onClick" @transitionend="onTransitionEnd">
        <hr class="sideLines" />
        <hr class="sideLines" />
        <hr class="middleLine" />
    </button>
</template>

<script lang="ts" src="./AnimatedArrows.ts" />

<style scoped lang="postcss" src="./AnimatedArrows.pcss" />
