<template>
    <div id="app">
        <transition name="fade">
            <Overlay v-if="displayOverlay" :display="displayOverlay" @close-button-clicked="displayOverlay = false"  />
        </transition>
        <Header @hamburger-menu-clicked="displayOverlay = true" />
        <MainView class="mainView" />
        <Footer />
    </div>
</template>

<script lang="ts" src="./MasterPage.ts" />

<style scoped lang="postcss" src="./MasterPage.pcss" />
