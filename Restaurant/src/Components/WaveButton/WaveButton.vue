<template>
    <button v-first-hover="'wave-button-animation'" class="border-black">
        <template v-for="(char, index) in [...buttontext]" :key="index">
            <span :style="`animation-delay: ${500/4/buttontext.length * index}ms`">
                <template v-if="char === ' '">
                    &nbsp;
                </template>
                {{ char }}
            </span>
        </template>
    </button>
</template>

<script lang="ts" src="./WaveButton.ts" />

<style scoped lang="scss" src="./WaveButton.scss" />