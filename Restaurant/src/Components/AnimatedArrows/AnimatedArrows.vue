<template>
    <button class="arrow-button" @click="onClick" @transitionend="onTransitionEnd">
        <hr class="side-lines" :class="childClass" />
        <hr class="side-lines" :class="childClass" />
        <hr class="middle-line" :class="childClass" />
    </button>
</template>

<script lang="ts" src="./AnimatedArrows.ts" />

<style scoped lang="scss" src="./AnimatedArrows.scss" />
