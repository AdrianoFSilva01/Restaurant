<template>
    <div id="app" class="flex flex-col" style="min-height: 100vh">
        <transition name="fade">
            <Overlay v-if="displayOverlay" :display="displayOverlay" @close-button-clicked="displayOverlay = false"  />
        </transition>
        <Header @hamburger-menu-clicked="displayOverlay = true" />
        <router-view class="mainView" />
        <Footer />
    </div>
</template>

<script lang="ts" src="./MasterPage.ts" />

<style scoped lang="scss" src="./MasterPage.scss" />
