<template>
    <div id="app" :class="{ 'w-screen h-screen overflow-y-hidden' : displayOverlay }">
        <transition name="fade">
            <Overlay v-if="displayOverlay" :display="displayOverlay" @close-button-clicked="displayOverlay = false"  />
        </transition>
        <Header @hamburger-menu-clicked="displayOverlay = true" />
        <MainView class="space-y-12" />
        <Footer class="mt-8" />
    </div>
</template>

<script lang="ts" src="./MasterPage.ts" />

<style scoped lang="postcss" src="./MasterPage.pcss" />
