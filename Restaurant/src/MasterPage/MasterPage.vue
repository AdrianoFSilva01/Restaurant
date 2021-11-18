<template>
    <div id="app">
        <transition name="fade">
            <Overlay v-show="displayOverlay" :display="displayOverlay" @close-button-clicked="displayOverlay = false"  />
        </transition>
        <Header @hamburger-menu-clicked="displayOverlay = true" />
        <MainView class="space-y-8" />
    </div>
</template>

<script lang="ts" src="./MasterPage.ts" />

<style lang="postcss" src="./MasterPage.pcss" />
