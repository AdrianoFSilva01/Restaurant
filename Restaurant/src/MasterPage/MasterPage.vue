<template>
    <div id="app">
        <transition name="fade">
            <Overlay v-show="displayOverlay" :display="displayOverlay" @close-button-clicked="displayOverlay = false"  />
        </transition>
        <Header @hamburger-menu-clicked="displayOverlay = true" />
        <HelloWorld />
    </div>
</template>

<script lang="ts" src="./MasterPage.ts" />

<style lang="postcss" src="./MasterPage.pcss" />
