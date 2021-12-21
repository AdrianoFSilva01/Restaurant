<template>
    <button class="arrowButton" @click="onClick" @transitionend="onTransitionEnd">
        <hr class="sideLines" :class="childClass" />
        <hr class="sideLines" :class="childClass" />
        <hr class="middleLine" :class="childClass" />
    </button>
</template>

<script lang="ts" src="./AnimatedArrows.ts" />

<style scoped lang="postcss" src="./AnimatedArrows.pcss" />
